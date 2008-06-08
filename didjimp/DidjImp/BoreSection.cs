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

namespace DidjImp
{
	/// <summary>
	/// This class represents a single conical section of a bore. A cylindrical section
	/// is a special case when the opening and closing radius are equal.
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
		/// The length of the section, in rectangular coordinates
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

		public BoreSection(BoreDimension openingDimension, BoreDimension closingDimension)
			: this(openingDimension.Radius, closingDimension.Radius, closingDimension.Position - openingDimension.Position)
		{
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
		public static IList<BoreSection> Split(BoreSection boreSection, decimal maxSectionLength)
		{
			//using decimals in various places to prevent the small errors introduced
			//when using doubles
			List<BoreSection> sections = new List<BoreSection>();
			decimal remainingLength = (decimal)boreSection.length;
			decimal slope = ((decimal)boreSection.closingRadius - (decimal)boreSection.openingRadius) / remainingLength;
			decimal currentOpeningRadius = (decimal)boreSection.openingRadius;
			int currentSectionNumber = 0;

			while (remainingLength > maxSectionLength)
			{
				decimal currentClosingRadius = (decimal)boreSection.openingRadius + (slope * maxSectionLength * (currentSectionNumber + 1));
				sections.Add(new BoreSection((double)currentOpeningRadius, (double)currentClosingRadius, (double)maxSectionLength));
				remainingLength -= maxSectionLength;
				currentOpeningRadius = currentClosingRadius;
				currentSectionNumber++;
			}
			if (remainingLength > 0)
				sections.Add(new BoreSection((double)currentOpeningRadius, (double)boreSection.closingRadius, (double)remainingLength));
			return sections.AsReadOnly();
		}




		#region Impedance Calculations

		private double inputXi;
		/// <summary>
		/// The distance from the apex of the cone to the input side of the truncated cone,
		/// in spherical coordinates
		/// </summary>
		public double InputXi
		{
			get { return inputXi; }
		}

		private double outputXi;
		/// <summary>
		/// The distance from the apex of the cone to the output side of the truncated cone,
		/// in spherical coordinates
		/// </summary>
		public double OutputXi
		{
			get { return outputXi; }
		}

		private double averageRadius;
		/// <summary>
		/// The average radius of the conical/cylindrical section
		/// </summary>
		public double AverageRadius
		{
			get { return averageRadius; }
		}

		private double sphericalAreaTimesAverageRadius;
		/// <summary>
		/// An intermediate value used in impedance calculations
		/// </summary>
		public double SphericalAreaTimesAverageRadius
		{
			get { return sphericalAreaTimesAverageRadius; }
		}


		//the distance between the spherical "end caps" on each side of the section
		//sqrt((LinearLength^2 + (closingRadius - openingRadius)^2)
		private double sphericalLength;
		/// <summary>
		/// The spherical length of the section -- the distance between the
		/// spherical "end caps" on each end of the truncated conical section.
		/// In the case of a cylindrical section, this is the length of the section
		/// </summary>
		public double SphericalLength
		{
			get { return sphericalLength; }
		}

		
		private double sphericalArea;
		/// <summary>
		/// The area of the spherical "end cap" at the opening end of the section.
		/// In the case of a cylindrical section, the planar area of the opening
		/// </summary>
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

				sphericalLength = outputXi - inputXi;
				sphericalArea = Math.PI * Math.Pow(inputXi, 2) * 2 * (1 - (Math.Sqrt(1 - Math.Pow(openingRadius / inputXi, 2))));
			}
			else
			{
				sphericalLength = this.length;
				sphericalArea = Math.PI * Math.Pow(openingRadius, 2);
			}

			averageRadius = (openingRadius + closingRadius) / 2.0;
			sphericalAreaTimesAverageRadius = sphericalArea * averageRadius;
		}
		#endregion
	}
}
