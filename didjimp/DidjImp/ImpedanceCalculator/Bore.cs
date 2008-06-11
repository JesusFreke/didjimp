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
	/// This class represents an arbitrary bore
	/// </summary>
	public class Bore
	{
		private List<BoreSection> boreSections;
		private List<BoreDimension> boreDimensions;

		public IList<BoreSection> BoreSections
		{
			get { return boreSections.AsReadOnly(); }
		}

		public IList<BoreDimension> BoreDimensions
		{
			get { return boreDimensions.AsReadOnly(); }
		}

		private Object boreSectionsSplitForInputImpedanceLock = new Object();
		private List<BoreSection> boreSectionsSplitForInputImpedance;
		private List<BoreSection> BoreSectionsSplitForInputImpedance
		{
			get
			{
				lock (boreSectionsSplitForInputImpedanceLock)
				{
					if (boreSectionsSplitForInputImpedance == null)
					{
						boreSectionsSplitForInputImpedance = new List<BoreSection>();
						foreach (BoreSection boreSection in boreSections)
						{
							if (boreSection.IsCylindrical)
								boreSectionsSplitForInputImpedance.Add(boreSection);
							else
								boreSectionsSplitForInputImpedance.AddRange(BoreSection.Split(boreSection, .002m));
						}
					}
					return boreSectionsSplitForInputImpedance;
				}
			}
		}


		private Object boreSectionsSplitForPressureWaveformLock = new Object();
		private List<BoreSection> boreSectionsSplitForPressureWaveform;
		private List<BoreSection> BoreSectionsSplitForPressureWaveform
		{
			get
			{
				lock (boreSectionsSplitForPressureWaveformLock)
				{
					if (boreSectionsSplitForPressureWaveform == null)
					{
						boreSectionsSplitForPressureWaveform = new List<BoreSection>();
						foreach (BoreSection boreSection in boreSections)
							boreSectionsSplitForPressureWaveform.AddRange(BoreSection.Split(boreSection, .002m));
					}
					return boreSectionsSplitForPressureWaveform;
				}
			}
		}

		private SortedList<double, Complex> impedances = new SortedList<double, Complex>();
		public SortedList<double, Complex> InputImpedance
		{
			get
			{
				return impedances;
			}
		}

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
			this.boreDimensions = new List<BoreDimension>(dimensions);
			if (boreDimensions.Count < 2)
				throw new ValidationException("There was only 1 bore dimension given. There must be at least 2.");

			if (boreDimensions[0].Position != 0)
				throw new ValidationException("The first dimension must be at position 0.");

			//create sections from the dimensions
			boreSections = new List<BoreSection>();
			for (int i = 0; i < boreDimensions.Count - 1; i++)
			{
				if (boreDimensions[i + 1].Position - boreDimensions[i].Position > 0)
				{
					BoreSection boreSection = new BoreSection(boreDimensions[i], boreDimensions[i + 1]);
					length += boreSection.Length;
					boreSections.Add(boreSection);					
				}
			}
		}

		/// <summary>
		/// Calculates the impedance of this bore, for frequencies from minFrequency to maxFrequency, at intervals of frequencyInterval.
		/// </summary>
		/// <param name="minFrequency">The minumum frequency to calculate</param>
		/// <param name="maxFrequency">The maximum frequency to calculate</param>
		/// <param name="frequencyInterval">The interval between frequencies</param>
		public void CalculateInputImpedance(double minFrequency, double maxFrequency, double frequencyInterval)
		{
			SortedList<double, Complex> returnImpedances = new SortedList<double, Complex>();

			lock (impedances)
			{
				foreach (KeyValuePair<double, Complex> value in returnImpedances)
					impedances[value.Key] = value.Value;
			}

			for (double frequency = minFrequency; frequency < maxFrequency; frequency += frequencyInterval)
			{
				Complex impedance;

				impedance = ImpedanceCalculator.InputImpedance(BoreSectionsSplitForInputImpedance, frequency);
				returnImpedances[frequency] = impedance;
				CalculatedFrequency(frequency);
			}

			lock (impedances)
			{
				foreach (KeyValuePair<double, Complex> value in returnImpedances)
					impedances[value.Key] = value.Value;
			}
		}

		/// <summary>
		/// This function looks at the existing input impedance data for this bore
		/// and calculates the impedance additional frequencies, to pinpoint the
		/// impedance peaks, to the number of decimal places specified
		/// </summary>
		public void FindResonances(int decimalPlaces)
		{
			for (int i = 1; i < impedances.Count-1; i++)
			{
				if (impedances.Values[i].Magnitude > impedances.Values[i-1].Magnitude && impedances.Values[i].Magnitude > impedances.Values[i+1].Magnitude)
				{
					//find the resonance to within decimalPlaces. This will add some items to impedances, so we need to keep
					//track of how many were added, and increment our loop variable correspondingly
					i+=FindResonance(impedances.Keys[i-1], impedances.Keys[i], impedances.Keys[i + 1], decimalPlaces);
				}
			}
		}

		private int FindResonance(double leftFrequency, double centerFrequency, double rightFrequency, int decimalPlaces)
		{
			return FindResonance((decimal)leftFrequency, (decimal)centerFrequency, (decimal)rightFrequency, impedances[leftFrequency], impedances[centerFrequency], impedances[rightFrequency], decimalPlaces, 1.0m/(decimal)Math.Pow(10.0, decimalPlaces));
		}

		//TODO: make this iterative, instead of recursive
		private int FindResonance(decimal leftFrequency, decimal centerFrequency, decimal rightFrequency, Complex leftImpedance, Complex centerImpedance, Complex rightImpedance, int decimalPlaces, decimal accuracy)
		{
			if (centerFrequency - leftFrequency <= accuracy && rightFrequency - centerFrequency <= accuracy)
				return 0;

			decimal nextFrequency;

			if ((centerFrequency - leftFrequency) / (rightFrequency - centerFrequency) > 10)
			{
				//the center point is too far to the right, let's try a frequency that is
				//to the left of center, twice as far away as the center is from the right
				nextFrequency = centerFrequency - (rightFrequency - centerFrequency) * 2;
				nextFrequency = Math.Round(nextFrequency, decimalPlaces);
			}
			else if ((rightFrequency - centerFrequency) / (centerFrequency - leftFrequency) > 10)
			{
				//the center point is too far to the left, let's try a frequency that is
				//to the right of center, twice as far away as the center is from the left
				nextFrequency = centerFrequency + (centerFrequency - leftFrequency) * 2;
				nextFrequency = Math.Round(nextFrequency, decimalPlaces);
			}
			else
			{
				//find the approximate center, given 3 points. 

				//first, find the line that goes through the lowest point and the center point, then find
				//the line with the opposite slope that goes through the remaining point.
				//these two lines will intersect approximately at the maximum, assuming that the peak is
				//mostly symmetrical. 

				if (leftImpedance.Magnitude < rightImpedance.Magnitude)
				{
					decimal slope = ((decimal)centerImpedance.Magnitude - (decimal)leftImpedance.Magnitude) / (centerFrequency - leftFrequency);
					nextFrequency = (slope * rightFrequency + (decimal)rightImpedance.Magnitude - (decimal)leftImpedance.Magnitude + slope * leftFrequency) / (2 * slope);
					nextFrequency = Math.Round(nextFrequency, decimalPlaces);
				}
				else
				{
					decimal slope = ((decimal)rightImpedance.Magnitude - (decimal)centerImpedance.Magnitude) / (rightFrequency - centerFrequency);
					nextFrequency = (slope * leftFrequency + (decimal)leftImpedance.Magnitude - (decimal)rightImpedance.Magnitude + slope * rightFrequency) / (2 * slope);
					nextFrequency = Math.Round(nextFrequency, decimalPlaces);
				}

				if (nextFrequency == centerFrequency)
				{
					//if the above procedure is giving us the same center point
					//that we already had, just try a binary search in whichever
					//direction is biggest.
					if (centerFrequency - leftFrequency > rightFrequency - centerFrequency)
						nextFrequency = (centerFrequency + leftFrequency) / 2;
					else
						nextFrequency = (centerFrequency + rightFrequency) / 2;
					nextFrequency = Math.Round(nextFrequency, decimalPlaces);
				}
			}

			Complex complex = GetImpedance((double)nextFrequency);
			if (complex.Magnitude > centerImpedance.Magnitude)
			{
				if (nextFrequency > centerFrequency)
					return 1 + FindResonance(centerFrequency, nextFrequency, rightFrequency, centerImpedance, complex, rightImpedance, decimalPlaces, accuracy);
				else
					return 1 + FindResonance(leftFrequency, nextFrequency, centerFrequency, leftImpedance, complex, centerImpedance, decimalPlaces, accuracy);
			}
			else
			{
				if (nextFrequency > centerFrequency)
					return 1 + FindResonance(leftFrequency, centerFrequency, nextFrequency, leftImpedance, centerImpedance, complex, decimalPlaces, accuracy);
				else
					return 1 + FindResonance(nextFrequency, centerFrequency, rightFrequency, complex, centerImpedance, rightImpedance, decimalPlaces, accuracy);
			}
		}


		/// <summary>
		/// Calculates the impedance of this bore, for frequencies from minFrequency to maxFrequency, at intervals of frequencyInterval.
		/// It also calculates additional frequencies around the peaks in order to get better resolution there. This is controled by the
		/// maxImpedanceChangeAtPeak parameter, which denotes the maximum allowed percange change in the impedance magnitude between
		/// the the peak and each point on either side
		/// </summary>
		/// <param name="minFrequency">The minumum frequency to calculate</param>
		/// <param name="maxFrequency">The maximum frequency to calculate</param>
		/// <param name="frequencyInterval">The interval between frequencies</param>
		/// <param name="maxImpedanceChangeAtPeak">The maximum percent change of the impedance magnitude at the peak. .001 seems like a good value..</param>
		/// <returns></returns>
		/*public SortedList<double, Complex> CalculateInputImpedance(double minFrequency, double maxFrequency, double frequencyInterval, double maxImpedanceChangeAtPeak)
		{
			SortedList<double, Complex> returnImpedances = new SortedList<double, Complex>();

			List<BoreSection> splitSections = new List<BoreSection>();
			foreach (BoreSection boreSection in boreSections)
			{
				if (boreSection.IsCylindrical)
					splitSections.Add(boreSection);
				else
					splitSections.AddRange(BoreSection.Split(boreSection, .002m));
			}

			CalculateInputImpedance(splitSections, minFrequency, maxFrequency, frequencyInterval, returnImpedances);

			//TODO: use the imaginary impedance to pinpoint the peak more accurately
			//calculate additional frequencies at the peaks, until we have
			//sufficient resolution (as per maxPercentageChangeAtPeak)
			List<double> additionalFrequencies = new List<double>();
			do
			{
				additionalFrequencies.Clear();

				//determine what additional frequencies we need to calculate, if any
				for (int i = 1; i < returnImpedances.Count - 1; i++)
				{
					double frequency = returnImpedances.Keys[i];
					double impedanceMagnitude = returnImpedances.Values[i].Magnitude;
					double priorFrequency = returnImpedances.Keys[i - 1];
					double priorImpedanceMagnitude = returnImpedances.Values[i - 1].Magnitude;
					double nextFrequency = returnImpedances.Keys[i + 1];
					double nextImpedanceMagnitude = returnImpedances.Values[i + 1].Magnitude;

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
					Complex impedance;
					
					impedance = ImpedanceCalculator.InputImpedance(splitSections, frequency);
					returnImpedances[frequency] = impedance;
					CalculatedFrequency(frequency);
				}
			} while (additionalFrequencies.Count > 0);

			lock (impedances)
			{
				foreach (KeyValuePair<double, Complex> value in returnImpedances)
					impedances[value.Key] = value.Value;
			}

			return returnImpedances;
		}*/

		private Complex GetImpedance(double frequency)
		{
			Complex impedance = null;
			if (!impedances.TryGetValue(frequency, out impedance))
			{
				List<BoreSection> splitSections = new List<BoreSection>();
				foreach (BoreSection boreSection in boreSections)
				{
					if (boreSection.IsCylindrical)
						splitSections.Add(boreSection);
					else
						splitSections.AddRange(BoreSection.Split(boreSection, .002m));
				}

				impedance = ImpedanceCalculator.InputImpedance(splitSections, frequency);
				impedances[frequency] = impedance;
			}
			return impedance;
		}

		public SortedList<double, Complex> CalculateWaveform(double frequency, decimal maxSectionLength)
		{
			SortedList<double, Complex> pressures = new SortedList<double, Complex>();
			List<BoreSection> splitSections = new List<BoreSection>();
			foreach (BoreSection boreSection in boreSections)
				splitSections.AddRange(BoreSection.Split(boreSection, maxSectionLength));

			return ImpedanceCalculator.CalculatePressure(frequency, splitSections, GetImpedance(frequency));
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

		private double length = 0;
		public double Length
		{
			get { return length; }
		}
	}
}