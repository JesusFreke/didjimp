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
	public class BorePlot : NPlot.Windows.PlotSurface2D
	{
		private SortedList<double, Complex> pressureData = null;

		private Bore bore;
		public Bore Bore
		{
			get { return bore; }
			set
			{
				bore = value;
				UpdateBorePlot();
			}
		}

		private bool showWaveformPlot = false;
		public bool ShowWaveformPlot
		{
			get { return showWaveformPlot; }
			set
			{
				showWaveformPlot = value;
				UpdateBorePlot();
			}
		}

		private double selectedFrequency;
		public double SelectedFrequency
		{
			get { return selectedFrequency; }
			set
			{
				selectedFrequency = value;
				UpdateBorePlot();
			}
		}


		public BorePlot()
			: base()
		{
		}

		private void UpdateBorePlot()
		{
			if (bore == null)
			{
				this.Clear();
				this.Refresh();
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

			this.Clear();
			this.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			Grid grid = new Grid();
			grid.HorizontalGridType = Grid.GridType.Fine;
			grid.VerticalGridType = Grid.GridType.None;
			this.Add(grid);

			LinePlot boreLinePlot = new LinePlot(yValues, xValues);
			boreLinePlot.Label = "Bore";
			boreLinePlot.Pen = new Pen(Color.Black, 2);
			boreLinePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			this.Add(boreLinePlot);

			this.XAxis2 = new LinearAxis(this.XAxis1.WorldMin, this.XAxis1.WorldMax);
			this.YAxis1.Hidden = true;

			if (ShowWaveformPlot)
			{
				LinePlot waveformPlot = AddWaveformPlot();
				if (waveformPlot != null)
				{
					this.AddAxesConstraint(new BoreAndWaveformStackedPlotAxesConstraint(boreLinePlot, waveformPlot));
					this.Refresh();
					return;
				}
			}

			this.AddAxesConstraint(new BoreOnlyAxesConstraint(boreLinePlot));
			this.XAxis2.Hidden = true;
			this.Refresh();
		}


		private LinePlot AddWaveformPlot()
		{
			pressureData = bore.CalculateWaveform(SelectedFrequency, .002m);

			List<double> xValues = new List<double>();
			List<double> yValues = new List<double>();
			List<double> negYValues = new List<double>();

			for (int i = 0; i < pressureData.Count; i++)
			{
				xValues.Add(pressureData.Keys[i]);
				yValues.Add(pressureData.Values[i].Real);
				negYValues.Add(pressureData.Values[i].Real * -1);
			}

			this.YAxis2 = new LinearAxis();
			this.YAxis2.Hidden = true;

			LinePlot waveformPlot = new LinePlot(yValues, xValues);
			waveformPlot.Label = "Pressure Waveform";
			this.Add(waveformPlot, PlotSurface2D.XAxisPosition.Bottom, PlotSurface2D.YAxisPosition.Right, 0);
			LinePlot negWaveformPlot = new LinePlot(negYValues, xValues);
			negWaveformPlot.Label = "Pressure Waveform (negative)";
			this.Add(negWaveformPlot, PlotSurface2D.XAxisPosition.Bottom, PlotSurface2D.YAxisPosition.Right, 0);

			this.Add(new HorizontalLine(0), PlotSurface2D.XAxisPosition.Bottom, PlotSurface2D.YAxisPosition.Right, 0);

			return waveformPlot;
		}

		public class BorePlotContextMenu : PlotContextMenu
		{
			private class ViewNodesPlotMenuItem : PlotMenuItem
			{
				public ViewNodesPlotMenuItem(string caption, int index, EventHandler callback)
					:base(caption, index, callback)
				{
				}

				public override void OnPopup(PlotContextMenu plotContextMenu)
				{
					if (!((BorePlot)((BorePlotContextMenu)plotContextMenu).plotSurface2D_).ShowWaveformPlot)
						MenuItem.Enabled = false;
					else
						MenuItem.Enabled = true;
				}
			}

			public BorePlotContextMenu()
			{
				List<PlotContextMenu.IPlotMenuItem> menuItems = new List<IPlotMenuItem>();

				menuItems.Add(new PlotContextMenu.PlotMenuItem("Save as Image", 2, new EventHandler(mnuSaveAsImage_Click)));
				menuItems.Add(new PlotContextMenu.PlotMenuItem("Save as Image (custom size)", 3, new EventHandler(mnuSaveAsImageCustomSize_Click)));
				
				menuItems.Add(new PlotContextMenu.PlotMenuSeparator(4));				
				menuItems.Add(new PlotContextMenu.PlotMenuItem("View Data", 5, new EventHandler(mnuViewData_Click)));		
				menuItems.Add(new ViewNodesPlotMenuItem("View Nodes and Anti-nodes", 6, new EventHandler(mnuViewNodes_Click)));
				this.SetMenuItems(menuItems);
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

			private void mnuViewNodes_Click(object sender, EventArgs e)
			{
				List<double> nodes = new List<double>();
				List<double> antiNodes = new List<double>();

				antiNodes.Add(0);
				nodes.Add(((BorePlot)plotSurface2D_).Bore.Length);

				SortedList<double, Complex> pressureData = ((BorePlot)plotSurface2D_).pressureData;

				for (int i = 1; i < pressureData.Count - 1; i++)
				{
					if (Math.Abs(pressureData.Values[i].Real) > Math.Abs(pressureData.Values[i - 1].Real) && Math.Abs(pressureData.Values[i].Real) > Math.Abs(pressureData.Values[i + 1].Real))
					{
						antiNodes.Add(pressureData.Keys[i]);
					}
					else if (pressureData.Values[i - 1].Real * pressureData.Values[i].Real < 0)
					{
						if (Math.Abs(pressureData.Values[i - 1].Real) < Math.Abs(pressureData.Values[i].Real))
							nodes.Add(pressureData.Keys[i - 1]);
						else
							nodes.Add(pressureData.Keys[i]);
					}
				}

				nodes.Sort();
				antiNodes.Sort();

				StringBuilder sb = new StringBuilder();
				
				sb.AppendLine("[Pressure Node Locations]");
				foreach (double node in nodes)
					sb.AppendLine(node.ToString());
				
				sb.AppendLine("");
				sb.AppendLine("[Pressure Anti-Node Locations]");
				foreach (double antiNode in antiNodes)
					sb.AppendLine(antiNode.ToString());

				ViewDataDialog viewDataDialog = new ViewDataDialog("Pressure Nodes and Anti-Nodes", sb.ToString());
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

		protected override void OnMouseEnter(EventArgs e)
		{
			//We must have focus for the mouse wheel events to happen
			this.Focus();

			base.OnMouseEnter(e);
		}
	}
}
