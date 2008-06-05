/*
 * DidjImp, a Didgeridoo Impedance Calculator (www.sourceforge.net/didjimp)
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

namespace DidjImp
{
	/// <summary>
	/// This class represents a single conical section of a bore
	/// </summary>
	public class BoreSection
	{
		private double openingRadius;
		private double closingRadius;
		private double length;

		/// <summary>
		/// The radius of the end that is nearest the mouthpiece
		/// </summary>
		public double OpeningRadius
		{
			get { return openingRadius; }
		}

		/// <summary>
		/// The radius of the end that is nearest the bell
		/// </summary>
		public double ClosingRadius
		{
			get { return closingRadius; }
		}

		/// <summary>
		/// The length of the section
		/// </summary>
		public double Length
		{
			get { return length; }
		}

		/// <summary>
		/// Constructs a new BoreSection
		/// </summary>
		public BoreSection(double openingRadius, double closingRadius, double length)
		{
			this.openingRadius = openingRadius;
			this.closingRadius = closingRadius;
			this.length = length;
			DoCalcs();
		}

		public bool IsCylindrical
		{
			get { return closingRadius == openingRadius; }
		}



		/// <summary>
		/// Split the given section into smaller sections, each of which are no longer than
		/// maxLength long
		/// </summary>
		/// <param name="maxLength">The maximum length of each section</param>
		/// <returns>A list of smaller sections</returns>
		public static IList<BoreSection> Split(BoreSection section, decimal maxLength)
		{
			//using decimals in various places to prevent the small errors introduced
			//when using doubles
			List<BoreSection> sections = new List<BoreSection>();
			decimal remainingLength = (decimal)section.length;
			decimal slope = ((decimal)section.closingRadius - (decimal)section.openingRadius) / remainingLength;
			decimal currentOpeningRadius = (decimal)section.openingRadius;
			int currentSectionNumber = 0;

			while (remainingLength > maxLength)
			{
				decimal currentClosingRadius = (decimal)section.openingRadius + (slope * maxLength * (currentSectionNumber + 1));
				sections.Add(new BoreSection((double)currentOpeningRadius, (double)currentClosingRadius, (double)maxLength));
				remainingLength -= maxLength;
				currentOpeningRadius = currentClosingRadius;
				currentSectionNumber++;
			}
			if (remainingLength > 0)
				sections.Add(new BoreSection((double)currentOpeningRadius, (double)section.closingRadius, (double)remainingLength));
			return sections.AsReadOnly();
		}




		#region Impedance Calculations

		private double inputXi;
		public double InputXi
		{
			get { return inputXi; }
		}

		private double outputXi;
		public double OutputXi
		{
			get { return outputXi; }
		}

		/*private double inputXi_Div_OutputXi;
		public double InputXi_Div_OutputXi
		{
			get { return inputXi_Div_OutputXi; }
		}

		private double outputXi_Div_InputXi;
		public double OutputXi_Div_InputXi
		{
			get { return outputXi_Div_InputXi; }
		}*/


		private double averageRadius;
		public double AverageRadius
		{
			get { return averageRadius; }
		}

		private double sphericalAreaTimesAverageRadius;
		public double SphericalAreaTimesAverageRadius
		{
			get { return sphericalAreaTimesAverageRadius; }
		}


		//the distance between the spherical "end caps" on each side of the section
		//TODO: I'm pretty sure this is just the length.. need to verify
		private double sphericalDistance;
		public double SphericalDistance
		{
			get { return sphericalDistance; }
		}

		//the area of the spherical "end cap" at the opening end of the section
		private double sphericalArea;
		public double SphericalArea
		{
			get { return sphericalArea; }
		}


		//pre-calculate everything that doesn't depend on frequency
		private void DoCalcs()
		{
			if (!IsCylindrical)
			{
				double temp = Math.Sqrt(Math.Pow(length / (closingRadius - openingRadius), 2) + 1);

				inputXi = openingRadius * temp;
				outputXi = closingRadius * temp;
				//inputXi_Div_OutputXi = inputXi / outputXi;
				//outputXi_Div_InputXi = outputXi / inputXi;

				sphericalDistance = outputXi - inputXi;
				sphericalArea = Math.PI * Math.Pow(inputXi, 2) * 2 * (1 - (Math.Sqrt(1 - Math.Pow(openingRadius / inputXi, 2))));
			}
			else
			{
				sphericalDistance = this.length;
				sphericalArea = Math.PI * Math.Pow(openingRadius, 2);
			}

			averageRadius = (openingRadius + closingRadius) / 2.0;
			sphericalAreaTimesAverageRadius = sphericalArea * averageRadius;
		}


		#endregion
	}
}
