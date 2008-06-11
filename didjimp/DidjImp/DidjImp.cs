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
		private ImpedanceData impedanceData = null;
		private int calculatedFrequencyCount;
		private Progress progressDialog;
		private DidjImpSettings settings = new DidjImpSettings();
		private Bore bore = null;
		private int finishedThreads = 0;
		private SortedList<double, Complex> tempImpedanceData = null;

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
			impedancePlot.RightMenu = new ImpedancePlot.ImpedancePlotContextMenu();
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

			tempImpedanceData = null;
			finishedThreads = 0;

			
			progressDialog = new Progress();
			ParameterizedThreadStart threadStart = (threadNum) =>
			{
				int startFrequency = (int)Math.Round((2000.0 / settings.NumberOfThreads) * ((int)threadNum) + 1.0, 0);
				int endFrequency = (int)Math.Round((2000.0 / settings.NumberOfThreads) * ((int)threadNum + 1.0), 0);

				bore.CalculateInputImpedance(startFrequency, endFrequency, 1);
				SetImpedanceData();
				UpdateBorePlot();
			};


			for (int i=0; i<settings.NumberOfThreads; i++)
			{
				Thread thread = new Thread(threadStart);
				thread.Start(i);
			}

			progressDialog.ShowDialog(this);
		}

		private void bore_CalculatedFrequency(double frequency)
		{
			calculatedFrequencyCount++;
			progressDialog.SetProgress(Math.Min(100, (int)((calculatedFrequencyCount / 2000.0) * 100)));
		}

		private object SetImpedanceDataLock = new object();
		private void SetImpedanceData()
		{
			lock (SetImpedanceDataLock)
			{
				finishedThreads++;

				if (finishedThreads < settings.NumberOfThreads)
					return;
			}

			bore.FindResonances(2);

			progressDialog.Invoke(new VoidDelegate(delegate()
			{
				progressDialog.Close();
			}));
		
			this.impedanceData = new ImpedanceData(bore.InputImpedance);
			impedancePlot.ImpedanceData = this.impedanceData;

			IList<double> resonances = this.impedanceData.ImpedancePeakFrequencies;

			SelectFrequencyDropDown dropDown = new SelectFrequencyDropDown(comboHarmonics, resonances);
			comboHarmonics.DropDownControl = dropDown;
			dropDown.SelectFirstResonance();		
		}

		private void DidjImpApp_Load(object sender, EventArgs e)
		{		
			comboWaveformSelect.AllowResizeDropDown = false;
			comboImpedanceGraphType.SelectedIndex = 0;
			impedancePlot.ImpedanceData = this.impedanceData;
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
			double selectedFrequency;
			if (!Double.TryParse((string)comboHarmonics.SelectedItem, out selectedFrequency))
				return;
			impedancePlot.SelectedFrequency = selectedFrequency;
		}

		private void comboImpedanceGraphType_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboImpedanceGraphType.Text)
			{
				case "Impedance Magnitude":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Magnitude;
					break;
				case "Real Impedance":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Real;
					break;
				case "Imaginary Impedance":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Imaginary;
					break;
				case "Impedance Phase":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Phase;
					break;
			}
		}

		private void UpdateBorePlot()
		{
			if (borePlot.InvokeRequired)
			{
				borePlot.Invoke(new VoidDelegate(UpdateBorePlot));
				return;
			}

			List<double> xValues = new List<double>();
			List<double> yValues = new List<double>();
			double maxRadius = 0;
			double totalLength = bore.Length;

			for (int i = 0; i < bore.BoreDimensions.Count; i++)
			{
				if (bore.BoreDimensions[i].Radius > maxRadius)
					maxRadius = bore.BoreDimensions[i].Radius;

				xValues.Add(bore.BoreDimensions[i].Position);
				yValues.Add(bore.BoreDimensions[i].Radius);
			}

			for (int i = bore.BoreDimensions.Count - 1; i > -1; i--)
			{
				xValues.Add(bore.BoreDimensions[i].Position);
				yValues.Add(bore.BoreDimensions[i].Radius * -1);
			}

			xValues.Add(bore.BoreDimensions[0].Position);
			yValues.Add(bore.BoreDimensions[0].Radius);

			borePlot.Clear();
			borePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.HorizontalGridType = Grid.GridType.Fine;
			grid.VerticalGridType = Grid.GridType.None;
			borePlot.Add(grid);

			LinePlot boreLinePlot = new LinePlot(yValues, xValues);
			borePlot.Add(boreLinePlot);

			borePlot.XAxis2 = new LinearAxis(borePlot.XAxis1.WorldMin, borePlot.XAxis1.WorldMax);
			borePlot.YAxis1.Hidden = true;
			borePlot.YAxis1.Reversed = false;

			if (chkWaveform.Checked)
			{
				LinePlot waveformPlot = AddWaveformPlot();
				if (waveformPlot != null)
				{
					borePlot.AddAxesConstraint(new BoreAndWaveformStackedPlotAxesConstraint(boreLinePlot, waveformPlot));
					borePlot.Refresh();
					return;
				}
			}

			borePlot.AddAxesConstraint(new BoreOnlyAxesConstraint(boreLinePlot));
			borePlot.XAxis2.Hidden = true;
			borePlot.Refresh();
		}

		private LinePlot AddWaveformPlot()
		{
			double frequency = -1;

			if (!double.TryParse((string)comboWaveformSelect.SelectedItem, out frequency))
				return null;

			SortedList<double, Complex> pressures = bore.CalculateWaveform(frequency, .002m);

			List<double> xValues = new List<double>();
			List<double> yValues = new List<double>();
			List<double> negYValues = new List<double>();

			for (int i = 0; i < pressures.Count; i++)
			{
				xValues.Add(pressures.Keys[i]);
				yValues.Add(pressures.Values[i].Real);
				negYValues.Add(pressures.Values[i].Real * -1);
			}

			borePlot.YAxis2 = new LinearAxis();
			borePlot.YAxis2.Hidden = true;

			LinePlot waveformPlot = new LinePlot(yValues, xValues);
			borePlot.Add(waveformPlot, PlotSurface2D.XAxisPosition.Bottom, PlotSurface2D.YAxisPosition.Right, 0);
			LinePlot negWaveformPlot = new LinePlot(negYValues, xValues);
			borePlot.Add(negWaveformPlot, PlotSurface2D.XAxisPosition.Bottom, PlotSurface2D.YAxisPosition.Right, 0);

			borePlot.Add(new HorizontalLine(0), PlotSurface2D.XAxisPosition.Bottom, PlotSurface2D.YAxisPosition.Right, 0);

			return waveformPlot;
		}

		private void comboWaveformSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateBorePlot();
		}

		private void chkWaveform_CheckedChanged(object sender, EventArgs e)
		{
			if (chkWaveform.Checked)
			{
				comboWaveformSelect.DropDownControl = new SelectFrequencyDropDown(comboWaveformSelect, impedanceData.ImpedancePeakFrequencies);
				comboWaveformSelect.Enabled = true;
				((SelectFrequencyDropDown)comboWaveformSelect.DropDownControl).SelectFirstResonance();
			}
			else
			{
				comboWaveformSelect.Enabled = false;
				comboWaveformSelect.Items.Clear();
				UpdateBorePlot();
			}
		}

		public delegate void VoidDelegate();
	}
}
