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
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using NPlot;

namespace DidjImp
{
	public class ImpedancePlot : NPlot.Windows.PlotSurface2D
	{
		private ImpedanceData impedanceData;
		private ImpedancePlotType impedancePlotType;
		private double selectedFrequency;
		private List<VerticalLine> harmonicLines = new List<VerticalLine>();

		/// <summary>
		/// The impedance data to display
		/// </summary>
		public ImpedanceData ImpedanceData
		{
			get { return impedanceData; }
			set
			{
				impedanceData = value;
				selectedFrequency = 0;
				UpdatePlot();
			}
		}
		
		/// <summary>
		/// The type of impedance data to display
		/// </summary>
		public ImpedancePlotType ImpedancePlotType
		{
			get { return impedancePlotType; }
			set
			{
				impedancePlotType = value;
				UpdatePlot();
			}
		}

		/// <summary>
		/// The selected frequency and it's harmonics are shown with vertical
		/// lines on the plot
		/// </summary>
		public double SelectedFrequency
		{
			get { return selectedFrequency; }
			set
			{
				if (impedanceData == null)
					return;

				selectedFrequency = value;
				UpdateHarmonicLines();
			}
		}

		public ImpedancePlot()
			: base()
		{
		}

		private delegate void VoidDelegate();

		/// <summary>
		/// Clear the plot surface and completely re-create it using
		/// current values
		/// </summary>
		private void UpdatePlot()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new VoidDelegate(UpdatePlot));
				return;
			} 
			
			this.Clear();

			if (impedanceData == null)
			{
				this.Refresh();
				return;
			}

			this.ShowCoordinates = true;
			this.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.VerticalGridType = Grid.GridType.Coarse;
			grid.HorizontalGridType = Grid.GridType.Fine;
			this.Add(grid);

			bool isLogPlot = false;
			IList<double> values = null, frequencies;
			string verticalAxisLabel = "";
			switch (this.ImpedancePlotType)
			{
				case ImpedancePlotType.Magnitude:
					values = impedanceData.ImpedanceMagnitudeValues;
					isLogPlot = true;
					verticalAxisLabel = "Impedance Magnitude";
					break;
				case ImpedancePlotType.Real:
					values = impedanceData.RealImpedanceValues;
					isLogPlot = true;
					verticalAxisLabel = "Real Impedance";
					break;
				case ImpedancePlotType.Imaginary:
					values = impedanceData.ImaginaryImpedanceValues;
					verticalAxisLabel = "Imaginary Impedance";
					break;
				case ImpedancePlotType.Phase:
					values = impedanceData.ImpedancePhaseValues;
					verticalAxisLabel = "Impedance Phase";
					break;
			}
			frequencies = impedanceData.Frequencies;
			LinePlot linePlot = new LinePlot(values, frequencies);
			linePlot.Label = verticalAxisLabel;
			this.Add(linePlot);

			double worldMin, worldMax;
			GetMinMax(values, out worldMin, out worldMax);

			if (isLogPlot)
			{
				LogAxis verticalAxis = new LogAxis();				
				verticalAxis.WorldMin = worldMin / 5;
				verticalAxis.WorldMax = worldMax * 5;				
				this.YAxis1 = verticalAxis;
			}
			else
			{
				Axis verticalAxis = this.YAxis1;
				verticalAxis.WorldMin = worldMin * 1.3;
				verticalAxis.WorldMax = worldMax * 1.3;
			}
			this.YAxis1.Label = verticalAxisLabel;
			this.YAxis1.LabelFont = new Font(this.YAxis1.LabelFont, FontStyle.Bold);
			this.YAxis1.NumberFormat = "{0:0e0}";

			Axis horizontalAxis = this.XAxis1;
			horizontalAxis.Label = "Frequency";
			horizontalAxis.LabelFont = new Font(this.XAxis1.LabelFont, FontStyle.Bold);
			GetMinMax(frequencies, out worldMin, out worldMax);
			horizontalAxis.WorldMin = worldMin;
			horizontalAxis.WorldMax = worldMax;

			this.AddInteraction(new MouseDrag());
			this.AddInteraction(new MouseWheelZoom());

			this.UpdateHarmonicLines();
		}

		/// <summary>
		/// Update the vertical lines used to indicate
		/// the hamonics of a particular resonance
		/// </summary>
		private void UpdateHarmonicLines()
		{
			if (impedanceData == null)
				return;
			if (selectedFrequency == 0)
				return;

			if (this.InvokeRequired)
			{
				this.Invoke(new VoidDelegate(UpdateHarmonicLines));
			}
			double frequency = this.SelectedFrequency;

			foreach (VerticalLine line in harmonicLines)
				this.Remove(line, false);

			harmonicLines.Clear();

			int mult = 1;
			while (frequency * mult < 2000)
			{
				Pen pen = new Pen(Color.Black, 1);
				pen.DashPattern = new float[] { 8.0F, 4.0F };
				VerticalLine vLine = new VerticalLine(frequency * mult, pen);

				this.Add(vLine);
				harmonicLines.Add(vLine);
				mult++;
			}

			this.Refresh();
		}

		private void GetMinMax<T>(IList<T> list, out T min, out T max) where T : IComparable<T>
		{
			min = default(T);
			max = default(T);
			if (list.Count <= 1)
				return;
			
			if (list[0].CompareTo(list[1]) < 0)
			{
				min = list[0];
				max = list[1];
			}
			else
			{
				min = list[1];
				max = list[0];
			}
			for (int i = 2; i < list.Count; i++)
			{
				if (list[i].CompareTo(min) < 0)
					min = list[i];
				else if (list[i].CompareTo(max) > 0)
					max = list[i];
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			//We must have focus for the mouse wheel events to happen
			this.Focus();

			base.OnMouseEnter(e);
		}

		public class ImpedancePlotContextMenu : PlotContextMenu
		{
			public ImpedancePlotContextMenu()
			{
				List<PlotContextMenu.IPlotMenuItem> menuItems = new List<IPlotMenuItem>();

				menuItems.Add(new PlotContextMenu.PlotMenuItem("Reset View", 0, new EventHandler(mnuResetView_Click)));
				menuItems.Add(new PlotContextMenu.PlotMenuSeparator(1));
				menuItems.Add(new PlotContextMenu.PlotMenuItem("Save as Image", 2, new EventHandler(mnuSaveAsImage_Click)));
				menuItems.Add(new PlotContextMenu.PlotMenuItem("Save as Image (custom size)", 3, new EventHandler(mnuSaveAsImageCustomSize_Click)));
				menuItems.Add(new PlotContextMenu.PlotMenuSeparator(4));
				menuItems.Add(new PlotContextMenu.PlotMenuItem("View Data", 5, new EventHandler(mnuViewData_Click)));
				menuItems.Add(new PlotContextMenu.PlotMenuItem("View Peaks", 6, new EventHandler(mnuViewPeaks_Click)));
				
				this.SetMenuItems(menuItems);
			}

			private void mnuResetView_Click(object sender, EventArgs e)
			{
				((ImpedancePlot)plotSurface2D_).UpdatePlot();
			}

			private void mnuSaveAsImage_Click(object sender, EventArgs e)
			{
				ExportImage(plotSurface2D_.Width, plotSurface2D_.Height);
			}

			private void mnuSaveAsImageCustomSize_Click(object sender, EventArgs e)
			{
				ImageSizeDialog imageSizeDialog = new ImageSizeDialog(plotSurface2D_.Width, plotSurface2D_.Height);
				DialogResult dr = imageSizeDialog.ShowDialog(plotSurface2D_);
				if (dr != DialogResult.OK)
					return;

				ExportImage(imageSizeDialog.SelectedWidth, imageSizeDialog.SelectedHeight);
			}

			private void mnuViewData_Click(object sender, EventArgs e)
			{
				StringBuilder sb = new StringBuilder();

				for (int i = 0; i < plotSurface2D_.Drawables.Count; ++i)
				{
					IPlot plot = plotSurface2D_.Drawables[i] as IPlot;
					if (plot != null)
					{
						plot.WriteData(sb);
					}
				}

				ViewDataDialog viewDataDialog = new ViewDataDialog("Plot Data", sb.ToString());
				viewDataDialog.ShowDialog(plotSurface2D_);
			}

			private void mnuViewPeaks_Click(object sender, EventArgs e)
			{
				StringBuilder sb = new StringBuilder();
				IList<double> peakFrequencies = ((ImpedancePlot)plotSurface2D_).ImpedanceData.ImpedancePeakFrequencies;
				sb.AppendLine("[Impedance Peak Frequencies]");
				foreach (double frequency in peakFrequencies)
					sb.AppendLine(frequency.ToString());

				ViewDataDialog viewDataDialog = new ViewDataDialog("Impedance Peaks", sb.ToString());
				viewDataDialog.ShowDialog(plotSurface2D_);
			}


			private void ExportImage(int width, int height)
			{
				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.AddExtension = true;
				saveFileDialog.DefaultExt = ".bmp";
				saveFileDialog.Filter = "Bitmap (*.bmp)|*.bmp|JPEG (*.jpg)|*.jpg|GIF (*.gif)|*.gif|TIFF (*.tif)|*.tif";
				saveFileDialog.CreatePrompt = false;
				saveFileDialog.OverwritePrompt = true;
				saveFileDialog.CheckPathExists = true;
				saveFileDialog.CheckFileExists = false;
				saveFileDialog.ValidateNames = true;

				DialogResult dr = saveFileDialog.ShowDialog(plotSurface2D_);

				if (dr != DialogResult.OK)
					return;

				FileInfo fileInfo = new FileInfo(saveFileDialog.FileName);

				using (Bitmap b = new System.Drawing.Bitmap(width, height))
				{
					using (Graphics g = Graphics.FromImage(b))
					{
						g.Clear(Color.White);
						plotSurface2D_.Draw(g, new Rectangle(0, 0, b.Width - 1, b.Height - 1));

					}

					ImageFormat imageFormat;
					switch (fileInfo.Extension.ToLower())
					{
						case ".bmp":
							imageFormat = ImageFormat.Bmp;
							break;
						case ".jpg":
							imageFormat = ImageFormat.Jpeg;
							break;
						case ".gif":
							imageFormat = ImageFormat.Gif;
							break;
						case ".tif":
							imageFormat = ImageFormat.Tiff;
							break;
						default:
							imageFormat = ImageFormat.Bmp;
							break;
					}

					b.Save(fileInfo.FullName, imageFormat);
				}
			}
		}
	}

	public enum ImpedancePlotType
	{
		Magnitude,
		Real,
		Imaginary,
		Phase
	}
}
