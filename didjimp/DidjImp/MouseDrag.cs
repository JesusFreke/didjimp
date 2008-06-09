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
using System.Drawing;
using System.Windows.Forms;
using NPlot;

namespace DidjImp
{
	/// <summary>
	/// A custom user interaction that allows the user to drag the chart
	/// with the left mouse button, and zoom the chart with the right
	/// mouse button
	/// </summary>
	public class MouseDrag : NPlot.Windows.PlotSurface2D.Interactions.Interaction
	{
		private bool leftDragInitiated_ = false;
		private bool rightDragInitiated_ = false;
		private Point startPoint_ = new Point(-1, -1);
		private Point lastPoint_ = new Point(-1, -1);
		// this is the condition for an unset point
		private Point unset_ = new Point(-1, -1);

		public override bool DoMouseDown(MouseEventArgs e, Control ctr, KeyEventArgs lastKeyEventArgs)
		{
			NPlot.PlotSurface2D ps = ((NPlot.Windows.PlotSurface2D)ctr).Inner;

			if (e.X > ps.PlotAreaBoundingBoxCache.Left && e.X < (ps.PlotAreaBoundingBoxCache.Right) &&
				e.Y > ps.PlotAreaBoundingBoxCache.Top && e.Y < ps.PlotAreaBoundingBoxCache.Bottom &&
				!leftDragInitiated_ && !rightDragInitiated_)
			{

				if (e.Button == MouseButtons.Left)
					leftDragInitiated_ = true;
				else if (e.Button == MouseButtons.Right)
					rightDragInitiated_ = true;

				lastPoint_.X = e.X;
				lastPoint_.Y = e.Y;

				startPoint_.X = e.X;
				startPoint_.Y = e.Y;
			}

			return false;
		}

		public override bool DoMouseMove(MouseEventArgs e, Control ctr, KeyEventArgs lastKeyEventArgs)
		{
			NPlot.PlotSurface2D ps = ((NPlot.Windows.PlotSurface2D)ctr).Inner;

			if ((e.Button == MouseButtons.Left) && leftDragInitiated_)
			{
				int diffX = e.X - lastPoint_.X;
				int diffY = e.Y - lastPoint_.Y;

				((NPlot.Windows.PlotSurface2D)ctr).CacheAxes();

				if (ps.XAxis1 != null)
				{
					Axis axis = ps.XAxis1;
					PointF pMin = ps.PhysicalXAxis1Cache.PhysicalMin;
					PointF pMax = ps.PhysicalXAxis1Cache.PhysicalMax;

					PointF physicalWorldMin = pMin;
					PointF physicalWorldMax = pMax;
					physicalWorldMin.X -= diffX;
					physicalWorldMax.X -= diffX;
					double newWorldMin = axis.PhysicalToWorld(physicalWorldMin, pMin, pMax, false);
					double newWorldMax = axis.PhysicalToWorld(physicalWorldMax, pMin, pMax, false);
					axis.WorldMin = newWorldMin;
					axis.WorldMax = newWorldMax;
				}

				if (ps.XAxis2 != null)
				{
					Axis axis = ps.XAxis2;
					PointF pMin = ps.PhysicalXAxis2Cache.PhysicalMin;
					PointF pMax = ps.PhysicalXAxis2Cache.PhysicalMax;

					PointF physicalWorldMin = pMin;
					PointF physicalWorldMax = pMax;
					physicalWorldMin.X -= diffX;
					physicalWorldMax.X -= diffX;
					double newWorldMin = axis.PhysicalToWorld(physicalWorldMin, pMin, pMax, false);
					double newWorldMax = axis.PhysicalToWorld(physicalWorldMax, pMin, pMax, false);
					axis.WorldMin = newWorldMin;
					axis.WorldMax = newWorldMax;
				}

				if (ps.YAxis1 != null)
				{
					Axis axis = ps.YAxis1;
					PointF pMin = ps.PhysicalYAxis1Cache.PhysicalMin;
					PointF pMax = ps.PhysicalYAxis1Cache.PhysicalMax;

					PointF physicalWorldMin = pMin;
					PointF physicalWorldMax = pMax;
					physicalWorldMin.Y -= diffY;
					physicalWorldMax.Y -= diffY;
					double newWorldMin = axis.PhysicalToWorld(physicalWorldMin, pMin, pMax, false);
					double newWorldMax = axis.PhysicalToWorld(physicalWorldMax, pMin, pMax, false);
					axis.WorldMin = newWorldMin;
					axis.WorldMax = newWorldMax;
				}

				if (ps.YAxis2 != null)
				{
					Axis axis = ps.YAxis2;
					PointF pMin = ps.PhysicalYAxis2Cache.PhysicalMin;
					PointF pMax = ps.PhysicalYAxis2Cache.PhysicalMax;

					PointF physicalWorldMin = pMin;
					PointF physicalWorldMax = pMax;
					physicalWorldMin.Y -= diffY;
					physicalWorldMax.Y -= diffY;
					double newWorldMin = axis.PhysicalToWorld(physicalWorldMin, pMin, pMax, false);
					double newWorldMax = axis.PhysicalToWorld(physicalWorldMax, pMin, pMax, false);
					axis.WorldMin = newWorldMin;
					axis.WorldMax = newWorldMax;
				}

				lastPoint_ = new Point(e.X, e.Y);

				return true;
			}
			else if (e.Button == MouseButtons.Right && rightDragInitiated_)
			{
				float xDist = (e.X - lastPoint_.X);
				float yDist = (e.Y - lastPoint_.Y);

				lastPoint_ = new Point(e.X, e.Y);

				if (xDist != 0 && ps.XAxis1 != null)
				{
					Axis axis = ps.XAxis1;
					PhysicalAxis physicalAxis = ps.PhysicalXAxis1Cache;

					PointF pMin = physicalAxis.PhysicalMin;
					PointF pMax = physicalAxis.PhysicalMax;
					double physicalWorldLength = physicalAxis.PhysicalLength;

					float prop = (float)(2 * physicalWorldLength * xDist / zoomSensitivity_);

					float relativePosX = (startPoint_.X - pMin.X) / (pMax.X - pMin.X);

					if (float.IsInfinity(relativePosX) || float.IsNaN(relativePosX))
					{
						relativePosX = 0.0f;
					}


					PointF physicalWorldMin = pMin;
					PointF physicalWorldMax = pMax;

					physicalWorldMin.X += relativePosX * prop;
					physicalWorldMax.X -= (1 - relativePosX) * prop;
					axis.WorldMin = axis.PhysicalToWorld(physicalWorldMin, pMin, pMax, false);
					axis.WorldMax = axis.PhysicalToWorld(physicalWorldMax, pMin, pMax, false);
				}

				if (xDist != 0 && ps.XAxis2 != null)
				{
					Axis axis = ps.XAxis2;
					PhysicalAxis physicalAxis = ps.PhysicalXAxis2Cache;

					PointF pMin = physicalAxis.PhysicalMin;
					PointF pMax = physicalAxis.PhysicalMax;
					double physicalWorldLength = physicalAxis.PhysicalLength;

					float prop = (float)(2 * physicalWorldLength * xDist / zoomSensitivity_);

					float relativePosX = (startPoint_.X - pMin.X) / (pMax.X - pMin.X);

					if (float.IsInfinity(relativePosX) || float.IsNaN(relativePosX))
					{
						relativePosX = 0.0f;
					}

					PointF physicalWorldMin = pMin;
					PointF physicalWorldMax = pMax;

					physicalWorldMin.X += relativePosX * prop;
					physicalWorldMax.X -= (1 - relativePosX) * prop;
					axis.WorldMin = axis.PhysicalToWorld(physicalWorldMin, pMin, pMax, false);
					axis.WorldMax = axis.PhysicalToWorld(physicalWorldMax, pMin, pMax, false);
				}

				if (yDist != 0 && ps.YAxis1 != null)
				{
					Axis axis = ps.YAxis1;
					PhysicalAxis physicalAxis = ps.PhysicalYAxis1Cache;

					PointF pMin = physicalAxis.PhysicalMin;
					PointF pMax = physicalAxis.PhysicalMax;
					double physicalWorldLength = physicalAxis.PhysicalLength;

					float prop = (float)(2 * physicalWorldLength * yDist / zoomSensitivity_);

					float relativePosY = (startPoint_.Y - pMin.Y) / (pMax.Y - pMin.Y);

					if (float.IsInfinity(relativePosY) || float.IsNaN(relativePosY))
					{
						relativePosY = 0.0f;
					}


					PointF physicalWorldMin = pMin;
					PointF physicalWorldMax = pMax;

					physicalWorldMin.Y += relativePosY * prop;
					physicalWorldMax.Y -= (1 - relativePosY) * prop;
					axis.WorldMin = axis.PhysicalToWorld(physicalWorldMin, pMin, pMax, false);
					axis.WorldMax = axis.PhysicalToWorld(physicalWorldMax, pMin, pMax, false);
				}


				return true;
			}

			return false;
		}


		private double zoomSensitivity_ = 200;
		public double ZoomSensitivity
		{
			get { return zoomSensitivity_; }
			set { zoomSensitivity_ = value; }
		}
		

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		/// <param name="ctr"></param>
		public override bool DoMouseUp(MouseEventArgs e, Control ctr, KeyEventArgs lastKeyEventArgs)
		{
			if ((e.Button == MouseButtons.Left) && leftDragInitiated_)
			{
				lastPoint_ = unset_;
				leftDragInitiated_ = false;
			}
			else if (e.Button == MouseButtons.Right && rightDragInitiated_)
			{
				lastPoint_ = unset_;
				rightDragInitiated_ = false;
			}

			return false;
		}
	}
}
