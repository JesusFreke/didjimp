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

namespace DidjImp
{
	public class ComplexMatrix
	{
		private Complex[,] elements = new Complex[2, 2];

		public Complex this[int a, int b]
		{
			get { return elements[a, b]; }
			set { elements[a, b] = value; }
		}

		public Complex Determinant
		{
			get { return (this[0, 0] * this[1, 1]) - (this[1, 0] * this[0, 1]); }
		}

		public ComplexMatrix Inverse
		{
			get
			{
				ComplexMatrix inverse = new ComplexMatrix();

				Complex det = this.Determinant;
				inverse[0, 0] = this[1, 1] / det;
				inverse[0, 1] = -1 * this[0, 1] / det;
				inverse[1, 0] = -1 * this[1, 0] / det;
				inverse[1, 1] = this[0, 0] / det;
				return inverse;
			}
		}

		public static ComplexMatrix operator *(ComplexMatrix m1, ComplexMatrix m2)
		{
			ComplexMatrix product = new ComplexMatrix();

			product[0, 0] = m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0];
			product[1, 0] = m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0];
			product[0, 1] = m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1];
			product[1, 1] = m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1];

			return product;
		}

		public static ComplexMatrix IdentityMatrix()
		{
			ComplexMatrix identity = new ComplexMatrix();
			identity[0, 0] = (Complex)1;
			identity[1, 0] = (Complex)0;
			identity[0, 1] = (Complex)0;
			identity[1, 1] = (Complex)1;
			return identity;
		}
	}
}
