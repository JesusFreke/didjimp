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

namespace DidjImp
{
	public class ComplexMathUtils
	{
		//b == Exp(a)
		//c == 1/Exp(a)
		public static void Exp(Complex a, out Complex b, out Complex c)
		{
			double d = Math.Exp(a.Real);
			Complex e = new Complex(Math.Cos(a.Imaginary), Math.Sin(a.Imaginary));
			b = e * d;
			c = b.Reciprocal();
		}

		//b == Cosh(a)
		//c == Sinh(a)
		public static void CoshSinh(Complex a, out Complex b, out Complex c)
		{
			Complex d, e;
			Exp(a, out d, out e);
			b = (d + e) / 2;
			c = (d - e) / 2;
		}
	}
}
