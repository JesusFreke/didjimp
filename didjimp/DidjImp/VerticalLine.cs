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
using NPlot;

namespace DidjImp
{
	/// <summary>
	/// A vertical line to use to show the peak and related harmonics.
	/// We can't use NPlot's built-in vertical line, because it is an IPlot,
	/// and it changes the axis when added to the chart
	/// </summary>
	public class VerticalLine : IDrawable
	{
		private System.Drawing.Drawing2D.SmoothingMode smoothingMode_ = System.Drawing.Drawing2D.SmoothingMode.Default;
		public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
		{
			get
			{
				return smoothingMode_;
			}
			set
			{
				smoothingMode_ = value;
			}
		}

		private double x;
		private Pen pen;

		public VerticalLine(double x, Pen pen)
		{
			this.x = x;
			this.pen = pen;
		}

		public void Draw(System.Drawing.Graphics g, PhysicalAxis xAxis, PhysicalAxis yAxis)
		{
			int physicalX = (int)xAxis.WorldToPhysical(x, false).X;

			g.DrawLine(pen, new Point(physicalX, yAxis.PhysicalMin.Y), new Point(physicalX, yAxis.PhysicalMax.Y));
		}
	}
}
