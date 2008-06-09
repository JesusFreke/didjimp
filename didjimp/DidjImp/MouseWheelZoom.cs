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

using System.Drawing;
using System.Windows.Forms;
using NPlot;
using System;

namespace DidjImp
{
	/// <summary>
	/// A custom user interaction that zooms into or out of the plot, using the mouse
	/// position as the center of the zoom
	/// </summary>
	public class MouseWheelZoom : NPlot.Windows.PlotSurface2D.Interactions.Interaction
	{
		private Point point_ = new Point(-1, -1);

		public override bool DoMouseWheel(MouseEventArgs e, Control ctr, KeyEventArgs lastKeyEventArgs)
		{
			NPlot.PlotSurface2D ps = ((NPlot.Windows.PlotSurface2D)ctr).Inner;

			((NPlot.Windows.PlotSurface2D)ctr).CacheAxes();

			double delta = e.Delta / (double)SystemInformation.MouseWheelScrollDelta;
			
			double zoomFactor = Math.Pow(sensitivity_, delta);			

			PointD pMin = new PointD((double)ps.PhysicalXAxis1Cache.PhysicalMin.X, (double)ps.PhysicalYAxis1Cache.PhysicalMin.Y);
			PointD pMax = new PointD((double)ps.PhysicalXAxis1Cache.PhysicalMax.X, (double)ps.PhysicalYAxis1Cache.PhysicalMax.Y);

			PointD pMinRelative = new PointD(pMin.X - e.X, pMin.Y - e.Y);
			PointD pMaxRelative = new PointD(pMax.X - e.X, pMax.Y - e.Y);

			Point pMinZoomed = new Point((int)Math.Round((pMinRelative.X * zoomFactor) + e.X, 0), (int)Math.Round((pMinRelative.Y * zoomFactor) + e.Y, 0));
			Point pMaxZoomed = new Point((int)Math.Round((pMaxRelative.X * zoomFactor) + e.X, 0), (int)Math.Round((pMaxRelative.Y * zoomFactor) + e.Y, 0));

			ps.XAxis1.WorldMin = ps.PhysicalXAxis1Cache.PhysicalToWorld(pMinZoomed, false);
			ps.XAxis1.WorldMax = ps.PhysicalXAxis1Cache.PhysicalToWorld(pMaxZoomed, false);
			ps.YAxis1.WorldMin = ps.PhysicalYAxis1Cache.PhysicalToWorld(pMinZoomed, false);
			ps.YAxis1.WorldMax = ps.PhysicalYAxis1Cache.PhysicalToWorld(pMaxZoomed, false);

			return true;
		}


		private double sensitivity_ = .9;
		/// <summary>
		/// The amount to zoom out for 1 wheel step up. For example, .9 means that it will zoom out 10%, 1.15 means that it will zoom in 15%
		/// </summary>
		public double Sensitivity
		{
			get
			{
				return sensitivity_;
			}
			set
			{
				sensitivity_ = value;
			}
		}
	}
}
