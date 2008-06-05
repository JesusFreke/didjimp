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
 * along with this p)rogram.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;

namespace DidjImp
{
	/// <summary>
	/// This class represents an arbitrary bore
	/// </summary>
	public class Bore
	{
		private List<BoreSection> sections;

		/// <summary>
		/// Constructs a new BoreDimensions class, for the given dimensions. It is assumed the
		/// bore is linear between each given dimension.
		/// 
		/// The bore is internally split up into sections with a length not greater than maxSectionLength,
		/// in order to improve accuracy with conical sections. As maxSectionLength gets smaller, the
		/// calculation time increases with order O(n), and the solution stabilizes, approaching an asymptote.
		/// 
		/// A maxSectionLength of around .002m is a good general choice. The solution has usually mostly stabilized,
		/// and the calculation time isn't too onerous.
		/// 
		/// If maxSectionLength is 0, then the sections are used as given, they are not split up into smaller sections
		/// </summary>
		/// <param name="dimensions">The dimensions of the bore</param>
		/// <param name="maxSectionLength">The maximum length of each internal section</param>
		public Bore(List<BoreDimension> dimensions, decimal maxSectionLength)
		{
			if (dimensions.Count < 2)
				throw new ValidationException("There was only 1 bore dimension given. There must be at least 2.");

			if (dimensions[0].Position != 0)
				throw new ValidationException("The first dimension must be at position 0.");

			//sort by position
			dimensions.Sort();

			//create sections from the dimensions
			List<BoreSection> tempSections = new List<BoreSection>();
			for (int i = 0; i < dimensions.Count - 1; i++)
			{
				if (dimensions[i + 1].Position - dimensions[i].Position > 0)
					tempSections.Add(new BoreSection(dimensions[i].Radius, dimensions[i + 1].Radius, dimensions[i + 1].Position - dimensions[i].Position));
			}

			//use the dimensions as given - don't split up into smaller sections
			if (maxSectionLength == 0)
				sections = tempSections;
			//split up the bore into smaller segments for improved accuracy with lossy conical segments
			else
			{
				sections = new List<BoreSection>();
				foreach (BoreSection boreSection in tempSections)
				{
					//the impedance calculations are "stable" for cylindrical sections,
					//so no need to split them up
					if (!boreSection.IsCylindrical)
						sections.AddRange(BoreSection.Split(boreSection, maxSectionLength));
					else
						sections.Add(boreSection);
				}
			}
		}

		/// <summary>
		/// Calculates the impedance of this bore, for frequencies from minFrequency to maxFrequency, at intervals of frequencyInterval.
		/// Since the impedance peaks are normally quite "pointy", and are one of the most important parts of the impedance spectrum,
		/// we calculate additional frequencies around the peaks, until the percentage change of the impedance magnitude between each
		/// point is less than maxImpedanceChangeAtPeak.
		/// </summary>
		/// <param name="minFrequency">The minumum frequency to calculate</param>
		/// <param name="maxFrequency">The maximum frequency to calculate</param>
		/// <param name="frequencyInterval">The interval between frequencies</param>
		/// <param name="maxImpedanceChangeAtPeak">The maximum percent change of the impedance magnitude at the peak. .001 seems like a good value..</param>
		/// <returns></returns>
		public SortedList<double, Complex> CalculateImpedance(double minFrequency, double maxFrequency, double frequencyInterval, double maxImpedanceChangeAtPeak)
		{
			SortedList<double, Complex> impedances = new SortedList<double, Complex>();

			for (double frequency = minFrequency; frequency < maxFrequency; frequency += frequencyInterval)
			{
				Complex impedance = ImpedanceCalculator.InputImpedance(this.sections, frequency);
				impedances.Add(frequency, impedance);
				CalculatedFrequency(frequency);
			}

			//calculate additional frequencies at the peaks, until we have
			//sufficient resolution (as per maxPercentageChangeAtPeak)
			List<double> additionalFrequencies = new List<double>();
			do
			{
				additionalFrequencies.Clear();

				//determine what additional frequencies we need to calculate, if any
				for (int i = 1; i < impedances.Count - 1; i++)
				{
					double frequency = impedances.Keys[i];
					double impedanceMagnitude = impedances.Values[i].Magnitude;
					double priorFrequency = impedances.Keys[i - 1];
					double priorImpedanceMagnitude = impedances.Values[i - 1].Magnitude;
					double nextFrequency = impedances.Keys[i + 1];
					double nextImpedanceMagnitude = impedances.Values[i + 1].Magnitude;

					//if this is a peak
					if (impedanceMagnitude >= priorImpedanceMagnitude && impedanceMagnitude >= nextImpedanceMagnitude)
					{
						//if the change on the left side of the peak is greater than the maximum allowed
						if ((impedanceMagnitude - priorImpedanceMagnitude) / impedanceMagnitude > maxImpedanceChangeAtPeak)
							additionalFrequencies.Add((frequency + priorFrequency) / 2);

						//if the change on the right side of the peak is greater than the maximum allowed
						if ((impedanceMagnitude - nextImpedanceMagnitude) / impedanceMagnitude > maxImpedanceChangeAtPeak)
							additionalFrequencies.Add((frequency + nextFrequency) / 2);
					}
				}

				foreach (double frequency in additionalFrequencies)
				{
					Complex impedance = ImpedanceCalculator.InputImpedance(sections, frequency);
					impedances.Add(frequency, impedance);
					CalculatedFrequency(frequency);
				}
			} while (additionalFrequencies.Count > 0);

			return impedances;
		}


		/// <summary>
		/// This event is fired for when calculating the impedance of this bore. It is fired
		/// after each frequency is calculated.
		/// </summary>
		public event CalculatedFrequencyDelegate CalculatedFrequency;
		public delegate void CalculatedFrequencyDelegate(double frequency);


		/// <summary>
		/// An exception that arises from some problem with the dimensions supplied by the user.
		/// </summary>
		public class ValidationException : Exception
		{
			public ValidationException(string message, params object[] args)
				: base(String.Format(message, args))
			{
			}
		}
	}
}