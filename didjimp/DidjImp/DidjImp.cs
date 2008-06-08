/*
 * DidjImp, a Didgeridoo Impedance Calculator (didjimp.sourceforge.net)
 * Copyright (C) 2008 Ben Gruver (JesusFreke@JesusFreke.com)
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using NPlot;

namespace DidjImp
{
	public partial class DidjImpApp : Form
	{
		private int calculatedFrequencyCount;
		private Progress progressDialog;
		private DidjImpSettings settings = new DidjImpSettings();
		private Bore bore = null;

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DidjImpApp());
		}

		public DidjImpApp()
		{
			InitializeComponent();
		}

		private void ShowError(string message, params object[] values)
		{
			MessageBox.Show(this, String.Format(message, values));
		}

		private Regex dimensionRegex = new Regex(@"^\s*([0-9.]+)(?:(?:\s+)|(?:\s*,\s*))([0-9.]+)\s*(?:;.*)?$");
		private void btnCalculate_Click(object sender, EventArgs e)
		{
			tabControl1.Enabled = true;
			verticalLines.Clear();

			double unitConversionFactor = 1;

			switch (settings.Units)
			{
				case DidjImpSettings.UnitType.millimeter:
					unitConversionFactor = .001;
					break;
				case DidjImpSettings.UnitType.centimeter:
					unitConversionFactor = .01;
					break;
				case DidjImpSettings.UnitType.meter:
					unitConversionFactor = 1;
					break;
				case DidjImpSettings.UnitType.inch:
					unitConversionFactor = .0254;
					break;
				case DidjImpSettings.UnitType.foot:
					unitConversionFactor = .3048;
					break;
				case DidjImpSettings.UnitType.yard:
					unitConversionFactor = .9144;
					break;
			}

			calculatedFrequencyCount = 0;
			List<BoreDimension> dimensions = new List<BoreDimension>();
			int lineNumber = 0;
			double previousPosition = 0;
			foreach (string dimensionLine in txtDimensions.Text.Replace('\r', '\n').Replace("\n\n", "\n").Split('\n'))
			{
				string line = dimensionLine.Trim();
				//ignore blank lines and lines with only a comment
				if (line.Length == 0 || line[0] == ';')
					continue;

				Match match = dimensionRegex.Match(line);
				if (!match.Success)
				{
					ShowError("Line {0}: \"{1}\" - The line is not in a valid format. Expecting 2 positive numbers.", lineNumber, dimensionLine);
					return;
				}

				double position;
				if (!Double.TryParse(match.Groups[1].Captures[0].Value, out position))
				{
					ShowError("Line {0}: \"{1}\" - The position \"{2}\" is not a valid number.", lineNumber, dimensionLine, match.Groups[1].Captures[0].Value);
					return;
				}

				if (position < previousPosition)
				{
					ShowError("Line {0}: \"{1}\" - The position \"{2}\" must be greater than or equal to the previous position \"{3}\"", lineNumber, dimensionLine, position, previousPosition);
					return;
				}
				previousPosition = position;

				double radius;
				if (!Double.TryParse(match.Groups[2].Captures[0].Value, out radius))
				{
					ShowError("Line {0}: \"{1}\" - The radius \"{2}\" is not a valid number.", lineNumber, dimensionLine, match.Groups[2].Captures[0].Value);
					return;
				}

				if (radius == 0)
				{
					ShowError("Line {0}: \"{1}\" - The radius cannot be 0", lineNumber, dimensionLine);
					return;
				}
				dimensions.Add(new BoreDimension(position * unitConversionFactor, radius * unitConversionFactor));

				lineNumber++;
			}

			if (dimensions.Count == 1)
			{
				ShowError("There must be at least 2 dimensions entered");
				return;
			}

			bore = new Bore(dimensions, 1.0m / 500);
			if (bore.Length == 0)
			{
				ShowError("The bore has a length of 0.");
				return;				
			}
			bore.CalculatedFrequency += new Bore.CalculatedFrequencyDelegate(bore_CalculatedFrequency);

			progressDialog = new Progress();
			Thread thread = new Thread(() =>
			{
				SortedList<double, Complex> impedanceData = bore.CalculateInputImpedance(1, 2000, 1, .001);
				SetImpedanceData(impedanceData);
				SetBoreData(dimensions);
			});

			thread.Start();

			progressDialog.ShowDialog(this);
		}

		private void bore_CalculatedFrequency(double frequency)
		{
			calculatedFrequencyCount++;
			progressDialog.SetProgress(Math.Min(100, (int)((calculatedFrequencyCount / 2000.0) * 100)));
		}

		private SortedList<double, Complex> impedanceData;
		private void SetImpedanceData(SortedList<double, Complex> impedanceData)
		{
			impedancePlot.Clear();

			this.impedanceData = impedanceData;

			List<double> peaks = new List<double>();
			for (int i = 1; i < impedanceData.Count - 1; i++)
			{
				if (impedanceData.Values[i].Magnitude > impedanceData.Values[i - 1].Magnitude &&
					impedanceData.Values[i].Magnitude > impedanceData.Values[i + 1].Magnitude)
				{
					peaks.Add(impedanceData.Keys[i]);
				}
			}

			this.Invoke(new VoidDelegate(delegate()
			{
				progressDialog.Close();
				UpdateImpedanceGraph();

				using (Bitmap b = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(b))
					{
						double maxWidth = 0;
						comboHarmonics.Items.Clear();
						for (int i = 0; i < peaks.Count; i++)
						{
							comboHarmonicsItem item = new comboHarmonicsItem(i + 1, peaks[i]);
							SizeF itemSize = g.MeasureString(item.Name, comboHarmonics.Font);
							if (itemSize.Width > maxWidth)
								maxWidth = itemSize.Width;
							comboHarmonics.Items.Add(item);
						}
						comboHarmonics.Width = (int)maxWidth + 17;
						comboHarmonics.SelectedIndex = 0;
					}
				}

				SelectFrequencyDropDown selectFrequencyDropDown = new SelectFrequencyDropDown(comboWaveformSelect, peaks);
				selectFrequencyDropDown.SelectFirstResonance();
				comboWaveformSelect.DropDownControl = selectFrequencyDropDown;
			}));

			

			
		}

		private class comboHarmonicsItem
		{
			private string name;
			public string Name
			{
				get { return name; }
			}

			private double frequency;
			public double Frequency
			{
				get { return frequency; }
			}

			public comboHarmonicsItem(int peakNumber, double frequency)
			{
				this.name = String.Format("{0}: {1:0.00}Hz", peakNumber, frequency);
				this.frequency = frequency;
			}

			public override string ToString()
			{
				return name;
			}
		}

		private delegate void VoidDelegate();

		private T Min<T> (IList<T> list)where T:IComparable<T>
		{
			if (list.Count == 0)
				return default(T);
			if (list.Count == 1)
				return list[0];
			T min;
			if (list[0].CompareTo(list[1]) < 0)
				min=list[0];
			else
				min=list[1];
			for (int i=2; i<list.Count; i++)
				if (list[i].CompareTo(min) < 0)
					min = list[i];
			return min;
		}

		
		private T Max<T>(IList<T> list) where T :IComparable<T>
		{
			if (list.Count == 0)
				return default(T);
			if (list.Count == 1)
				return list[0];
			T max;
			if (list[0].CompareTo(list[1]) > 0)
				max = list[0];
			else
				max = list[1];
			for (int i = 2; i < list.Count; i++)
				if (list[i].CompareTo(max) > 0)
					max = list[i];
			return max;
		}

		private void PrepareMagnitudeGraph()
		{
			impedancePlot.ShowCoordinates = true;
			impedancePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			impedancePlot.Add(grid);

			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Magnitude);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			impedancePlot.Add(linePlot);

			LogAxis verticalAxis = new LogAxis();
			verticalAxis.Label = "Impedance Magnitude";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) / 5;
			verticalAxis.WorldMax = Max(values) * 5;
			verticalAxis.NumberFormat = "{0:0e0}";
			impedancePlot.YAxis1 = verticalAxis;

			Axis horizontalAxis = impedancePlot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(impedancePlot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void PrepareRealGraph()
		{
			impedancePlot.ShowCoordinates = true;
			impedancePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			impedancePlot.Add(grid);

			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Real);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			impedancePlot.Add(linePlot);

			LogAxis verticalAxis = new LogAxis();
			verticalAxis.Label = "Real Impedance";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) / 5;
			verticalAxis.WorldMax = Max(values) * 5;
			verticalAxis.NumberFormat = "{0:0e0}";
			impedancePlot.YAxis1 = verticalAxis;

			Axis horizontalAxis = impedancePlot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(impedancePlot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void PrepareImaginaryGraph()
		{
			impedancePlot.ShowCoordinates = true;
			impedancePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			impedancePlot.Add(grid);
			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Imaginary);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			impedancePlot.Add(linePlot);

			Axis verticalAxis = impedancePlot.YAxis1;
			verticalAxis.Label = "Imaginary Impedance";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) * 1.3;
			verticalAxis.WorldMax = Max(values) * 1.3;
			verticalAxis.NumberFormat = "{0:0e0}";

			Axis horizontalAxis = impedancePlot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(impedancePlot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void PreparePhaseGraph()
		{
			impedancePlot.ShowCoordinates = true;
			impedancePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			impedancePlot.Add(grid);

			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Phase);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			impedancePlot.Add(linePlot);

			Axis verticalAxis = impedancePlot.YAxis1;
			verticalAxis.Label = "Impedance Phase";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) * 1.3;
			verticalAxis.WorldMax = Max(values) * 1.3;
			verticalAxis.NumberFormat = "{0:0e0}";

			Axis horizontalAxis = impedancePlot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(impedancePlot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			impedancePlot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void UpdateHarmonicLines()
		{
			comboHarmonicsItem item = (comboHarmonicsItem)comboHarmonics.SelectedItem;
			if (item == null)
				return;

			double frequency = item.Frequency;

			foreach (VerticalLine line in verticalLines)
				impedancePlot.Remove(line, false);

			verticalLines.Clear();

			int mult = 1;
			while (frequency * mult < 2000)
			{
				Pen pen = new Pen(Color.Black, 1);
				pen.DashPattern = new float[] { 8.0F, 4.0F };
				VerticalLine vLine = new VerticalLine(frequency * mult, pen);

				impedancePlot.Add(vLine);
				verticalLines.Add(vLine);
				mult++;
			}

			impedancePlot.Refresh();
		}

		private void plot_MouseEnter(object sender, EventArgs e)
		{
			//The chart control must have focus for the mouse wheel events to happen
			impedancePlot.Focus();
		}



		private void DidjImp_Load(object sender, EventArgs e)
		{
			impedancePlot.RightMenu = NPlot.Windows.PlotSurface2D.DefaultContextMenu;
			comboWaveformSelect.AllowResizeDropDown = false;
			comboImpedanceGraphType.SelectedIndex = 0;
		}


		private void saveDimensionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Text Files (*.txt;*.csv) | *.txt;*.csv";
			DialogResult res = dlg.ShowDialog(this);
			if (res == DialogResult.OK)
			{
				File.WriteAllText(dlg.FileName, txtDimensions.Text);
			}
		}

		private void loadDimensionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Text File (*.txt)|*.txt";
			dlg.CheckFileExists = true;
			DialogResult res = dlg.ShowDialog(this);
			if (res == DialogResult.OK)
			{
				txtDimensions.Text = File.ReadAllText(dlg.FileName);
			}
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			About about = new About();
			about.ShowDialog();
		}

		private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Options options = new Options(settings);
			DialogResult dr = options.ShowDialog();
		}

		private List<VerticalLine> verticalLines = new List<VerticalLine>();
		private void comboHarmonics_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateHarmonicLines();
		}

		private void comboImpedanceGraphType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (impedanceData == null || impedanceData.Count == 0)
				return;

			UpdateImpedanceGraph();
		}

		private void UpdateImpedanceGraph()
		{
			impedancePlot.Clear();
			switch (comboImpedanceGraphType.Text)
			{
				case "Impedance Magnitude":
					PrepareMagnitudeGraph();
					break;
				case "Real Impedance":
					PrepareRealGraph();
					break;
				case "Imaginary Impedance":
					PrepareImaginaryGraph();
					break;
				case "Impedance Phase":
					PreparePhaseGraph();
					break;
			}

			UpdateHarmonicLines();
			impedancePlot.Refresh();
		}

		AxesConstraint.AspectRatio borePlotAspectRatioConstraint = null;
		private void SetBoreData(List<BoreDimension> boreDimensions)
		{
			LinePlot linePlot = new LinePlot();
			List<double> xValues = new List<double>();
			List<double> yValues = new List<double>();
			double maxRadius = 0;
			double totalLength = boreDimensions[boreDimensions.Count - 1].Position;


			for (int i=0; i<boreDimensions.Count; i++)
			{
				if (boreDimensions[i].Radius > maxRadius)
					maxRadius = boreDimensions[i].Radius;

				xValues.Add(boreDimensions[i].Position);
				yValues.Add(boreDimensions[i].Radius);
			}

			for (int i = boreDimensions.Count - 1; i > -1; i--)
			{
				xValues.Add(boreDimensions[i].Position);
				yValues.Add(boreDimensions[i].Radius * -1);
			}

			xValues.Add(boreDimensions[0].Position);
			yValues.Add(boreDimensions[0].Radius);

			borePlot.Clear();

			borePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.HorizontalGridType = Grid.GridType.Fine;
			grid.VerticalGridType = Grid.GridType.None;
			borePlot.Add(grid);

			borePlotAspectRatioConstraint = new AxesConstraint.AspectRatio(1);
			borePlot.AddAxesConstraint(borePlotAspectRatioConstraint);	

			borePlot.Add(new LinePlot(yValues, xValues));

			borePlot.XAxis1.WorldMax += borePlot.XAxis1.WorldLength * .01;
			borePlot.YAxis1.WorldMax += borePlot.YAxis1.WorldLength * .2;

			borePlot.XAxis2 = new LinearAxis(borePlot.XAxis1.WorldMin, borePlot.XAxis1.WorldMax);
			borePlot.XAxis1.Hidden = true;
			borePlot.YAxis1.Hidden = true;

			this.Invoke(new VoidDelegate(delegate()
				{
					ResizeBorePlot();
				}));
		}

		private bool inResizeBorePlot = false;
		private void ResizeBorePlot()
		{
			if (inResizeBorePlot)
				return;
			if (borePlotAspectRatioConstraint == null)
				return;

			inResizeBorePlot = true;

			FakeAspectRatioAxesConstraint fakeAR = new FakeAspectRatioAxesConstraint(1, false);
			borePlot.AddAxesConstraint(fakeAR);
			borePlot.RemoveAxesConstraint(borePlotAspectRatioConstraint);

			Bitmap tempBitmap = new Bitmap(borePlot.Width, borePlot.Height);
			Graphics g = Graphics.FromImage(tempBitmap);

			borePlot.Draw(g, new Rectangle(0, borePlot.Height, borePlot.Width, borePlot.Height));

			borePlot.RemoveAxesConstraint(fakeAR);
			borePlot.AddAxesConstraint(borePlotAspectRatioConstraint);

			if (fakeAR.ChangeAmount != 0)
			{
				borePlot.Height -= fakeAR.ChangeAmount;
			}

			inResizeBorePlot = false;
		}

		private void borePlot_Layout(object sender, LayoutEventArgs e)
		{
			ResizeBorePlot();
		}

		private void comboWaveformSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			double frequency = -1;

			if (!double.TryParse((string)comboWaveformSelect.SelectedItem, out frequency))
				return;

			SortedList<double, Complex> pressures = bore.CalculateWaveform(frequency, .002m);

			List<double> xValues = new List<double>();
			List<double> yValues = new List<double>();
			List<double> negYValues = new List<double>();

			for (int i=0; i<pressures.Count; i++)
			{
				xValues.Add(pressures.Keys[i]);
				yValues.Add(pressures.Values[i].Real);
				negYValues.Add(pressures.Values[i].Real * -1);
			}

			waveformPlot.Clear();
			waveformPlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			waveformPlot.Title = String.Format("Waveform for {0}Hz", frequency);
			waveformPlot.TitleFont = new Font(waveformPlot.TitleFont, FontStyle.Bold);
			
			Grid grid = new Grid();
			grid.HorizontalGridType = Grid.GridType.Fine;
			grid.VerticalGridType = Grid.GridType.None;
			waveformPlot.Add(grid);

			waveformPlot.Add(new LinePlot(yValues, xValues));
			waveformPlot.Add(new LinePlot(negYValues, xValues));
			waveformPlot.XAxis1.Hidden = true;
			waveformPlot.YAxis1.Hidden = true;
			waveformPlot.XAxis1.WorldMax += waveformPlot.XAxis1.WorldLength * .01;

			waveformPlot.Add(new HorizontalLine(0));

			
			waveformPlot.Refresh();
		}
	}
}
