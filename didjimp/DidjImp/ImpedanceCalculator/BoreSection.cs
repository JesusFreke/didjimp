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
using System.Runtime.Serialization;

namespace DidjImp
{
	/// <summary>
	/// This class represents a single conical section of a bore. A cylindrical section
	/// is a special case when the opening and closing radius are equal.
	/// </summary>
	[Serializable()]
	public class BoreSection : IEquatable<BoreSection>
	{
		protected decimal openingRadius;
		protected decimal closingRadius;
		protected decimal length;

		/// <summary>
		/// The radius of the end that is nearest the mouthpiece
		/// </summary>
		public decimal OpeningRadius
		{
			get { return openingRadius; }
		}

		/// <summary>
		/// The radius of the end that is nearest the bell
		/// </summary>
		public decimal ClosingRadius
		{
			get { return closingRadius; }
		}

		/// <summary>
		/// The length of the section, in rectangular coordinates
		/// </summary>
		public decimal Length
		{
			get { return length; }
		}

		/// <summary>
		/// Constructs a new BoreSection
		/// </summary>
		public BoreSection(decimal openingRadius, decimal closingRadius, decimal length)
		{
			if (openingRadius <= 0)
				throw new Exception("The opening radius must be greater than 0");
			this.openingRadius = openingRadius;
			
			if (closingRadius <= 0)
				throw new Exception("The closing radius must be greater than 0");
			this.closingRadius = closingRadius;
			
			if (length < 0)
				throw new Exception("The length cannot be less than 0");
			this.length = length;
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
				sections.Add(new BoreSection(currentOpeningRadius, currentClosingRadius, maxSectionLength));
				remainingLength -= maxSectionLength;
				currentOpeningRadius = currentClosingRadius;
				currentSectionNumber++;
			}
			if (remainingLength > 0)
				sections.Add(new BoreSection(currentOpeningRadius, boreSection.closingRadius, remainingLength));
			return sections.AsReadOnly();
		}

		public bool Equals(BoreSection other)
		{
			if (this.OpeningRadius == other.OpeningRadius &&
				this.ClosingRadius == other.ClosingRadius &&
				this.Length == other.Length)
				return true;
			else return false;
		}
	}
}
