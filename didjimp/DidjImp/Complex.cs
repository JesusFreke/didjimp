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
	/// This class represents a complex number
	/// </summary>
	public class Complex
	{
		private double real;
		private double imaginary;

		/// <summary>
		/// The real part of this complex number
		/// </summary>
		public double Real
		{
			get { return real; }
		}

		/// <summary>
		/// The imaginary part of this complex number
		/// </summary>
		public double Imaginary
		{
			get { return imaginary; }
		}

		/// <summary>
		/// The magnitude of this complex number
		/// magnitude(a+bi) = sqrt(a*a + b*b)
		/// </summary>
		public double Magnitude
		{
			get { return Math.Sqrt(real * real + imaginary * imaginary); }
		}

		/// <summary>
		/// The phase of this complex number
		/// phase(a+bi) = atan(b/a)
		/// </summary>
		public double Phase
		{
			get { return Math.Atan(imaginary / real); }
		}

		/// <summary>
		/// Constructs a new complex
		/// </summary>
		public Complex(double real, double imaginary)
		{
			this.real = real;
			this.imaginary = imaginary;
		}

		/// <summary>
		/// Constructs a new complex from a double
		/// d = d+0i
		/// </summary>
		public Complex(double real)
		{
			this.real = real;
			this.imaginary = 0;
		}

		/// <summary>
		/// Implicitly converts from a double to a complex
		/// d = d+0i
		/// </summary>
		public static implicit operator Complex(double d)
		{
			return new Complex(d);
		}

		/// <summary>
		/// Calculates c1 + c2, where c1 and c2 are complex
		/// (a+bi)+(c+di) = (a+c)+(b+d)i
		/// </summary>
		public static Complex operator +(Complex c1, Complex c2)
		{
			return new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary);
		}

		/// <summary>
		/// Calculates c1 - c2, where c1 and c2 are complex
		/// (a+bi)-(c+di) = (a-c)+(b-d)i
		/// </summary>
		public static Complex operator -(Complex c1, Complex c2)
		{
			return new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary);
		}

		/// <summary>
		/// Calculates c1 * c2, where c1 and c2 are complex
		/// (a+bi)*(c+di} = (a*c - b*d) + (a*d + c*b)i
		/// </summary>
		public static Complex operator *(Complex c1, Complex c2)
		{
			return new Complex(c1.real * c2.real - c1.imaginary * c2.imaginary, c1.real * c2.imaginary + c2.real * c1.imaginary);
		}

		/// <summary>
		/// Calculates c1 * c2 where c1 is a complex and c2 is a double
		/// (a+bi) * d = (a*d)+(b*d)i
		/// </summary>
		public static Complex operator *(Complex c1, double c2)
		{
			return new Complex(c1.real * c2, c1.imaginary * c2);
		}

		/// <summary>
		/// Calculates c1 * c2, where c1 is a double and c2 is a complex
		/// d * (a+bi) = (d*a)+(d*b)i
		/// </summary>
		public static Complex operator *(double c1, Complex c2)
		{
			return c2 * c1;
		}

		/// <summary>
		/// Calculates c1/c2, where c1 and c2 are complex
		/// (a+bi)/(c+di) = (a+bi) * 1/(c+di)
		/// </summary>
		public static Complex operator /(Complex c1, Complex c2)
		{
			return c1 * c2.Reciprocal();
		}

		/// <summary>
		/// Calculates c1/c2, where c1 is a complex and c2 is a double
		/// (a+bi)/d = (a/d)+(b/d)i
		/// </summary>
		public static Complex operator /(Complex c1, double c2)
		{
			return new Complex(c1.real / c2, c1.imaginary / c2);
		}

		/// <summary>
		/// Calculates the reciprocal of this complex number
		/// reciprocal(a+bi) = 1.0/(a+bi) = (a/(a*a + b*b)) - (b/(a*a + b*b))i
		/// </summary>
		public Complex Reciprocal()
		{
			double denom = (real * real + imaginary * imaginary);
			return new Complex(real / denom, -1 * imaginary / denom);
		}

		/// <summary>
		/// Calculates the conjugate of this complex number
		/// conjugate(a+bi) = a-bi
		/// </summary>
		public Complex Conjugate()
		{
			return new Complex(real, imaginary * -1);
		}

		/// <summary>
		/// Returns a string representation of this complex number in the form a+bi
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			if (imaginary < 0)
				return String.Format("{0}{1}i", real, imaginary);
			else
				return String.Format("{0}+{1}i", real, imaginary);
		}
	}
}
