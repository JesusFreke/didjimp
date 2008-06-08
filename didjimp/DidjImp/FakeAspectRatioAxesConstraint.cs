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
using NPlot;

namespace DidjImp
{
	/// <summary>
	/// This is a "fake" constraint used for calculation purposes only. It is used
	/// to determine how much to resize the control in a single direction, to obtain
	/// the target aspect ratio
	/// </summary>
	public class FakeAspectRatioAxesConstraint : AxesConstraint
	{
		private double a_;
		private bool holdYConstant;

		private int changeAmount;
		public int ChangeAmount
		{
			get { return changeAmount; }
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="a">Aspect Ratio</param>
		public FakeAspectRatioAxesConstraint(double a, bool holdYConstant)
		{
			this.a_ = a;
			this.holdYConstant = holdYConstant;
		}

		/// <summary>
		/// Applies the constraint to the axes.
		/// </summary>
		/// <param name="pXAxis1">The bottom x-axis.</param>
		/// <param name="pYAxis1">The left y-axis.</param>
		/// <param name="pXAxis2">The top x-axis.</param>
		/// <param name="pYAxis2">The right y-axis.</param>
		public override void ApplyConstraint(
			PhysicalAxis pXAxis1, PhysicalAxis pYAxis1,
			PhysicalAxis pXAxis2, PhysicalAxis pYAxis2)
		{
			double xWorldRange = Math.Abs(pXAxis1.Axis.WorldMax - pXAxis1.Axis.WorldMin);
			double xPhysicalRange = Math.Abs(pXAxis1.PhysicalMax.X - pXAxis1.PhysicalMin.X);
			double xDirPixelSize = xWorldRange / xPhysicalRange;

			double yWorldRange = Math.Abs(pYAxis1.Axis.WorldMax - pYAxis1.Axis.WorldMin);
			double yPhysicalRange = Math.Abs(pYAxis1.PhysicalMax.Y - pYAxis1.PhysicalMin.Y);
			double yDirPixelSize = yWorldRange / yPhysicalRange;

			double currentAspectRatio = yDirPixelSize / xDirPixelSize;

			// we want to change the current aspect ratio to be the desired.
			// to do this, we may only add the world pixel lengths.

			if (!holdYConstant)
			{
				// calculate the required height change to acheive the target aspect ratio
				double toAdd = (this.a_ - currentAspectRatio) * xDirPixelSize;
				int newHeight =
					(int)(Math.Abs(pYAxis1.Axis.WorldMax - pYAxis1.Axis.WorldMin) / (yDirPixelSize + toAdd));
				changeAmount = (int)yPhysicalRange - newHeight;
			}
			else
			{
				//calculate the required width change to acheive the target aspect ratio
				double toAdd = yDirPixelSize / this.a_ - xDirPixelSize;
				int newWidth =
					(int)(Math.Abs(pXAxis1.Axis.WorldMax - pXAxis1.Axis.WorldMin) / (xDirPixelSize + toAdd));
				changeAmount = (int)xPhysicalRange - newWidth;
			}
		}
	}
}
