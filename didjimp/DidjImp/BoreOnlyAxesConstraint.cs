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

using System.Collections;
using System.Drawing;
using NPlot;

namespace DidjImp
{
	/// <summary>
	/// This axes constraint ensures that the bore plot has an aspect ratio of
	/// 1:1, and that there is sufficient padding between the bore plot and
	/// the bottom x axis
	/// </summary>
	public class BoreOnlyAxesConstraint : AxesConstraint
	{
		private LinePlot borePlot;

		public BoreOnlyAxesConstraint(LinePlot borePlot)
		{
			this.borePlot = borePlot;
		}

		public override void ApplyConstraint(PhysicalAxis pXAxis1, PhysicalAxis pYAxis1, PhysicalAxis pXAxis2, PhysicalAxis pYAxis2)
		{
			double boreYMin, boreYMax;
			NPlot.Utils.ArrayMinMax(borePlot.OrdinateData as IList, out boreYMin, out boreYMax);

			double yPhysicalLength = ((boreYMax * 2)/pXAxis1.PixelWorldLength) + 15;
			
			double yWorldLength = yPhysicalLength * pXAxis1.PixelWorldLength;

			pYAxis1.Axis.WorldMin = -15 * pXAxis1.PixelWorldLength + boreYMin;
			pYAxis1.Axis.WorldMax = pYAxis1.Axis.WorldMin + yWorldLength;

			int change = (int)(pYAxis1.PhysicalLength - yPhysicalLength) / 2;

			pYAxis1.PhysicalMax = new Point(pYAxis1.PhysicalMax.X, pYAxis1.PhysicalMax.Y + change);
			pYAxis1.PhysicalMin = new Point(pYAxis1.PhysicalMin.X, pYAxis1.PhysicalMin.Y - change);

			pXAxis1.PhysicalMax = new Point(pXAxis1.PhysicalMax.X, pXAxis1.PhysicalMax.Y - change);
			pXAxis1.PhysicalMin = new Point(pXAxis1.PhysicalMin.X, pXAxis1.PhysicalMin.Y - change);
		}
	}
}
