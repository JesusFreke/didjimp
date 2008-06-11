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

using System.Collections.Generic;

namespace DidjImp
{
	public class ImpedanceData
	{
		private SortedList<double, Complex> impedanceData = null;
		private List<double> impedancePeaks = null;

		public ImpedanceData(IDictionary<double, Complex> impedanceData)
		{
			this.impedanceData = new SortedList<double, Complex>(impedanceData);
			this.impedancePeaks = new List<double>();

			for (int i = 1; i < impedanceData.Count - 1; i++)
			{
				if (this.impedanceData.Values[i].Magnitude > this.impedanceData.Values[i - 1].Magnitude &&
					this.impedanceData.Values[i].Magnitude > this.impedanceData.Values[i + 1].Magnitude)
				{
					impedancePeaks.Add(this.impedanceData.Keys[i]);
				}
			}
		}
		
		public IList<double> ImpedancePeakFrequencies
		{
			get { return impedancePeaks.AsReadOnly(); }
		}

		public IList<ImpedancePoint> ImpedancePeaks
		{
			get
			{
				List<ImpedancePoint> impedancePeakPoints = new List<ImpedancePoint>();
				foreach (double frequency in impedancePeaks)
					impedancePeakPoints.Add(new ImpedancePoint(frequency, impedanceData[frequency]));
				return impedancePeakPoints.AsReadOnly();
			}
		}

		public IList<double> Frequencies
		{
			get
			{
				return new List<double>(impedanceData.Keys).AsReadOnly();
			}
		}

		public IList<double> ImpedanceMagnitudeValues
		{
			get
			{
				List<double> impedanceMagnitudeValues = new List<double>();
				foreach (Complex complex in impedanceData.Values)
					impedanceMagnitudeValues.Add(complex.Magnitude);
				return impedanceMagnitudeValues.AsReadOnly();
			}
		}

		public IList<double> RealImpedanceValues
		{
			get
			{
				List<double> realImpedanceValues = new List<double>();
				foreach (Complex complex in impedanceData.Values)
					realImpedanceValues.Add(complex.Real);
				return realImpedanceValues.AsReadOnly();
			}
		}

		public IList<double> ImaginaryImpedanceValues
		{
			get
			{
				List<double> imaginaryImpedanceValues = new List<double>();
				foreach (Complex complex in impedanceData.Values)
					imaginaryImpedanceValues.Add(complex.Imaginary);
				return imaginaryImpedanceValues.AsReadOnly();
			}
		}

		public IList<double> ImpedancePhaseValues
		{
			get
			{
				List<double> impedancePhaseValues = new List<double>();
				foreach (Complex complex in impedanceData.Values)
					impedancePhaseValues.Add(complex.Phase);
				return impedancePhaseValues.AsReadOnly();
			}
		}

		public struct ImpedancePoint
		{
			public double Frequency;
			public Complex Impedance;
			public ImpedancePoint(double frequency, Complex impedance)
			{
				this.Frequency = frequency;
				this.Impedance = impedance;
			}
		}
	}
}
