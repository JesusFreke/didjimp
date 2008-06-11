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

namespace DidjImp
{
	/// <summary>
	/// This class represents the radius of a bore at a single position
	/// </summary>
	public class BoreDimension : IComparable<BoreDimension>
	{
		private double position;
		private double radius;

		/// <summary>
		/// The position in the bore that this dimension is for. A position of
		/// 0 represents the bore at the mouthpiece.
		/// </summary>
		public double Position
		{
			get { return position; }
		}

		/// <summary>
		/// The radius at this position in the bore
		/// </summary>
		public double Radius
		{
			get { return radius; }
		}

		/// <summary>
		/// Constructs a new BoreDimension
		/// </summary>
		public BoreDimension(double position, double radius)
		{
			this.position = position;
			this.radius = radius;
		}

		/// <summary>
		/// Compares a BoreDimension to another, based on position
		/// </summary>
		public int CompareTo(BoreDimension other)
		{
			return this.position.CompareTo(other.position);
		}
	}
}
