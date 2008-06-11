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
using System.Collections;
using NPlot;

namespace DidjImp
{
	class BoreAndWaveformStackedPlotAxesConstraint : AxesConstraint
	{
		private LinePlot borePlot;
		private LinePlot waveformPlot;

		/// <summary>
		/// This axes constraint ensures that the bore plot and waveform plot are
		/// drawn at the top and bottom of the plot respectively, with no overlap.
		/// It is assumed that borePlot is associated with the left Y Axis, and
		/// waveformPlot is associated with the right Y Axis.
		/// 
		/// It also ensures that the aspect ratio of the bore plot is 1:1
		/// </summary>
		public BoreAndWaveformStackedPlotAxesConstraint(LinePlot borePlot, LinePlot waveformPlot)
		{
			this.borePlot = borePlot;
			this.waveformPlot = waveformPlot;
		}

		public override void ApplyConstraint(PhysicalAxis pXAxis1, PhysicalAxis pYAxis1, PhysicalAxis pXAxis2, PhysicalAxis pYAxis2)
		{
			double boreYMin, boreYMax, waveYMin, waveYMax;
			NPlot.Utils.ArrayMinMax(borePlot.OrdinateData as IList, out boreYMin, out boreYMax);
			NPlot.Utils.ArrayMinMax(waveformPlot.OrdinateData as IList, out waveYMin, out waveYMax);
			
			//we calculate the position of the bore plot first, indepedently of the waveform plot
			int borePlotHeight = AdjustBorePlot(pXAxis1, pYAxis1, boreYMax);

			AdjustWaveformPlot(pYAxis2, borePlotHeight, Math.Max(waveYMax, Math.Abs(waveYMin)));
		}


		/// <summary>
		/// Adjusts the left y axis to ensure that the bore plot is placed at the top of the plot, and has a 1:1
		/// aspect ratio
		/// </summary>
		/// <param name="pXAxis1">The physical bottom x axis</param>
		/// <param name="pYAxis1">The physical left y axis</param>
		/// <param name="yMax">the actual max Y value of the bore</param>
		/// <returns>The physical height of the bore plot</returns>
		private int AdjustBorePlot(PhysicalAxis pXAxis1, PhysicalAxis pYAxis1, double yMax)
		{
			double xDirPixelSize = pXAxis1.PixelWorldLength;		
			//now calculate the required yWorldRange for a 1:1 aspect ratio	
			double yWorldRange = xDirPixelSize * pYAxis1.PhysicalLength;

			//set the WorldMax of the axis to the top of the bore plot, plus 10 pixels for padding
			pYAxis1.Axis.WorldMax = yMax + 15 * xDirPixelSize;
			pYAxis1.Axis.WorldMin = pYAxis1.Axis.WorldMax - yWorldRange;

			//return the physical height of the bore plot.
			//this includes the height of the plot itself, plus 15 pixels padding on the top 
			//and bottom
			return (int)(yMax * 2/xDirPixelSize) + 30;
		}


		/// <summary>
		/// Adjusts the right y axis to ensure that the waveform plot is placed below the bore plot with
		/// no overlap
		/// </summary>
		/// <param name="pYAxis2">The physical right y axis</param>
		/// <param name="borePlotSizeY">The physical height of the bore plot</param>
		/// <param name="yMax">The max y world value of the waveform plot</param>
		private void AdjustWaveformPlot(PhysicalAxis pYAxis2, int borePlotHeight, double yMax)
		{
			//the physical height that we have remaining, under the bore plot
			double yRemaining = pYAxis2.PhysicalLength - borePlotHeight;

			//calculate the required yDirPixelSize, in order to fit the wave plot in the remaining space,
			//leaving 15 pixels padding at the top and bottom
			double yDirPixelSize = (yMax * 2)/(yRemaining-30);

			pYAxis2.Axis.WorldMax = (borePlotHeight + 15) * yDirPixelSize + yMax;
			pYAxis2.Axis.WorldMin = -yMax - (15 * yDirPixelSize);
		}
	}
}
