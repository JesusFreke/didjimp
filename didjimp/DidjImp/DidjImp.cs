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
	public partial class DidjImp : Form
	{
		private int calculatedFrequencyCount;
		private Progress progressDialog;
		private DidjImpSettings settings = new DidjImpSettings();

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DidjImp());
		}

		public DidjImp()
		{
			InitializeComponent();
		}

		private void ShowError(string message, params object[] values)
		{
			MessageBox.Show(String.Format(message, values));
		}

		private Regex dimensionRegex = new Regex(@"^\s*([0-9.]+)(?:(?:\s+)|(?:\s*,\s*))([0-9.]+)\s*(?:;.*)?$");
		private void btnCalculate_Click(object sender, EventArgs e)
		{
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

			Bore bore = new Bore(dimensions, 1.0m / 500);
			if (bore.Length == 0)
			{
				ShowError("The bore has a length of 0.");
				return;				
			}
			bore.CalculatedFrequency += new Bore.CalculatedFrequencyDelegate(bore_CalculatedFrequency);

			progressDialog = new Progress();

			Thread thread = new Thread(() =>
			{
				SortedList<double, Complex> impedanceData = bore.CalculateImpedance(1, 2000, 1, .001);
				SetImpedanceData(impedanceData);
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
			plot.Clear();

			this.impedanceData = impedanceData;

			if (radioMagnitude.Checked)
				PrepareMagnitudeGraph();
			else if (radioReal.Checked)
				PrepareRealGraph();
			else if (radioImaginary.Checked)
				PrepareImaginaryGraph();
			else if (radioPhase.Checked)
				PreparePhaseGraph();

			List<double> peaks = new List<double>();
			for (int i = 1; i < impedanceData.Count - 1; i++)
			{
				if (impedanceData.Values[i].Magnitude > impedanceData.Values[i - 1].Magnitude &&
					impedanceData.Values[i].Magnitude > impedanceData.Values[i + 1].Magnitude)
				{
					peaks.Add(impedanceData.Keys[i]);
				}
			}

			plot.Invoke(new VoidDelegate(delegate()
			{
				progressDialog.Close();
				plot.Refresh();
			}));

			comboPeaks.Invoke(new VoidDelegate(delegate()
			{
				using (Bitmap b = new Bitmap(1, 1))
				{
					using (Graphics g = Graphics.FromImage(b))
					{
						double maxWidth = 0;
						comboPeaks.Items.Clear();
						for (int i=0; i<peaks.Count; i++)
						{
							PeakComboBoxItem item = new PeakComboBoxItem(i+1, peaks[i]);
							SizeF itemSize = g.MeasureString(item.Name, comboPeaks.Font);
							if (itemSize.Width > maxWidth)
								maxWidth = itemSize.Width;
							comboPeaks.Items.Add(item);			
						}
						comboPeaks.Width = (int)maxWidth + 17;
						comboPeaks.SelectedIndex = 0;
					}
				}
			}));
		}

		private class PeakComboBoxItem
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

			public PeakComboBoxItem(int peakNumber, double frequency)
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
			plot.ShowCoordinates = true;
			plot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			plot.Add(grid);

			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Magnitude);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			plot.Add(linePlot);

			LogAxis verticalAxis = new LogAxis();
			verticalAxis.Label = "Impedance Magnitude";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) / 5;
			verticalAxis.WorldMax = Max(values) * 5;
			verticalAxis.NumberFormat = "{0:0e0}";
			plot.YAxis1 = verticalAxis;

			Axis horizontalAxis = plot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(plot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void PrepareRealGraph()
		{
			plot.ShowCoordinates = true;
			plot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			plot.Add(grid);

			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Real);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			plot.Add(linePlot);

			LogAxis verticalAxis = new LogAxis();
			verticalAxis.Label = "Real Impedance";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) / 5;
			verticalAxis.WorldMax = Max(values) * 5;
			verticalAxis.NumberFormat = "{0:0e0}";
			plot.YAxis1 = verticalAxis;

			Axis horizontalAxis = plot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(plot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void PrepareImaginaryGraph()
		{
			plot.ShowCoordinates = true;
			plot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			plot.Add(grid);
			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Imaginary);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			plot.Add(linePlot);

			Axis verticalAxis = plot.YAxis1;
			verticalAxis.Label = "Imaginary Impedance";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) * 1.3;
			verticalAxis.WorldMax = Max(values) * 1.3;
			verticalAxis.NumberFormat = "{0:0e0}";

			Axis horizontalAxis = plot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(plot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void PreparePhaseGraph()
		{
			plot.ShowCoordinates = true;
			plot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			plot.Add(grid);

			List<double> values = new List<double>();
			foreach (Complex c in impedanceData.Values)
				values.Add(c.Phase);
			List<double> frequencies = new List<double>(impedanceData.Keys);

			LinePlot linePlot = new LinePlot(values, frequencies);
			plot.Add(linePlot);

			Axis verticalAxis = plot.YAxis1;
			verticalAxis.Label = "Impedance Phase";
			verticalAxis.LabelFont = new Font(verticalAxis.LabelFont, FontStyle.Bold);
			verticalAxis.WorldMin = Min(values) * 1.3;
			verticalAxis.WorldMax = Max(values) * 1.3;
			verticalAxis.NumberFormat = "{0:0e0}";

			Axis horizontalAxis = plot.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(plot.XAxis1.LabelFont, FontStyle.Bold);
			horizontalAxis.WorldMin = Min(frequencies);
			horizontalAxis.WorldMax = Max(frequencies);

			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.HorizontalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.VerticalDrag());
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.AxisDrag(true));
			plot.AddInteraction(new NPlot.Windows.PlotSurface2D.Interactions.MouseWheelZoom());
		}

		private void radioMagnitude_CheckedChanged(object sender, EventArgs e)
		{
			if (radioMagnitude.Checked && impedanceData != null && impedanceData.Count > 0)
			{
				plot.Clear();
				PrepareMagnitudeGraph();
				AddHarmonicLines();
				plot.Refresh();
			}
		}

		private void radioReal_CheckedChanged(object sender, EventArgs e)
		{
			if (radioReal.Checked && impedanceData != null && impedanceData.Count > 0)
			{
				plot.Clear();
				PrepareRealGraph();
				AddHarmonicLines();
				plot.Refresh();
			}
		}

		private void radioImaginary_CheckedChanged(object sender, EventArgs e)
		{
			if (radioImaginary.Checked && impedanceData != null && impedanceData.Count > 0)
			{
				plot.Clear();
				PrepareImaginaryGraph();
				AddHarmonicLines();
				plot.Refresh();
			}
		}

		private void radioPhase_CheckedChanged(object sender, EventArgs e)
		{
			if (radioPhase.Checked && impedanceData != null && impedanceData.Count > 0)
			{
				plot.Clear();
				PreparePhaseGraph();
				AddHarmonicLines();
				plot.Refresh();
			}
		}

		private void AddHarmonicLines()
		{
			PeakComboBoxItem item = (PeakComboBoxItem)comboPeaks.SelectedItem;

			double frequency = item.Frequency;

			foreach (VerticalLine line in verticalLines)
				plot.Remove(line, false);

			verticalLines.Clear();

			int mult = 1;
			while (frequency * mult < 2000)
			{
				Pen pen = new Pen(Color.Black, 1);
				pen.DashPattern = new float[] { 8.0F, 4.0F };
				VerticalLine vLine = new VerticalLine(frequency * mult, pen);

				plot.Add(vLine);
				verticalLines.Add(vLine);
				mult++;
			}

			plot.Refresh();
		}

		private void plot_MouseEnter(object sender, EventArgs e)
		{
			//need to do this to make the mouse wheel events happen
			plot.Focus();
		}



		private void DidjImp_Load(object sender, EventArgs e)
		{
			plot.RightMenu = NPlot.Windows.PlotSurface2D.DefaultContextMenu;
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

		private int currentPeaksComboIndex = -1;
		private List<VerticalLine> verticalLines = new List<VerticalLine>();
		private void comboPeaks_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboPeaks.SelectedIndex == currentPeaksComboIndex)
				return;

			currentPeaksComboIndex = comboPeaks.SelectedIndex;
			AddHarmonicLines();
		}

		private void comboPeaks_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (comboPeaks.Text.Length == 0)
				return;

			//don't allow an entry that's not in the list
			if (!comboPeaks.Items.Contains(comboPeaks.Text))
				comboPeaks.SelectedIndex = currentPeaksComboIndex;
		}		
	}
}
