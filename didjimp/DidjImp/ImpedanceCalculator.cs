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
	public class ImpedanceCalculator
	{
		public const double speedSound = 347.23;
		public const double airDensity = 1.1769;
		public const double airViscocity = 1.846E-5;


		/// <summary>
		/// Encapsulates calculations that are dependant on frequency
		/// </summary>
		private class FrequencyCalculations
		{
			public double angularFrequency;
			//these are partial components used in calculating the corresponding value, that
			//are only dependant on frequency;
			public double frictionLossT1;
			public double gammaT1;
			public double gammaT2;
			public double characteristicImpedanceT1;
			public Complex terminatingImpedanceT1;

			/// <summary>
			/// Creates a new FrequencyCalculations class for the given frequency
			/// </summary>
			/// <param name="frequency">The frequency in Hz</param>
			public FrequencyCalculations(double frequency)
			{
				angularFrequency = frequency * 2 * Math.PI;

				frictionLossT1 = Math.Sqrt(angularFrequency * airDensity / airViscocity);

				gammaT1 = (angularFrequency / speedSound);
				gammaT2 = gammaT1 * 1.045 / frictionLossT1;

				characteristicImpedanceT1 = airDensity * speedSound * .369 / frictionLossT1;

				terminatingImpedanceT1 = new Complex(airDensity * speedSound * gammaT1 * gammaT1 / (4 * Math.PI), .61 * airDensity * speedSound * gammaT1 / (Math.PI));
			}
		}

		private static Complex Gamma(FrequencyCalculations freq, BoreSection boreSection)
		{
			double temp = freq.gammaT2 / boreSection.AverageRadius;
			return new Complex(temp, freq.gammaT1 + temp);
		}

		private static Complex CharacteristicImpedance(FrequencyCalculations freq, BoreSection boreSection)
		{
			double temp = freq.characteristicImpedanceT1 / boreSection.SphericalAreaTimesAverageRadius;
			return new Complex((airDensity * speedSound) / boreSection.SphericalArea + temp, -1 * temp);
		}

		private static ComplexMatrix TransmissionMatrix(FrequencyCalculations freq, BoreSection boreSection)
		{
			Complex gamma = Gamma(freq, boreSection);
			Complex C, D, characteristicImpedance;

			ComplexMathUtils.CoshSinh(gamma * boreSection.SphericalDistance, out C, out D);
			characteristicImpedance = CharacteristicImpedance(freq, boreSection);

			ComplexMatrix transmissionMatrix = new ComplexMatrix();

			if (boreSection.IsCylindrical)
			{
				transmissionMatrix[0, 0] = C;
				transmissionMatrix[0, 1] = characteristicImpedance * D;
				transmissionMatrix[1, 0] = characteristicImpedance.Reciprocal() * D;
				transmissionMatrix[1, 1] = C;
			}
			else
			{
				double outputinput = boreSection.OutputXi / boreSection.InputXi;
				double inputoutput = 1 / outputinput;
				Complex dgammaoutput = D / (gamma * boreSection.OutputXi);

				transmissionMatrix[0, 0] = outputinput * (C - dgammaoutput);
				transmissionMatrix[1, 1] = ((C * inputoutput) + dgammaoutput);
				transmissionMatrix[0, 1] = inputoutput * characteristicImpedance * D;

				Complex gammaXi2Inv = 1 / (gamma * gamma * (boreSection.InputXi * boreSection.InputXi));

				transmissionMatrix[1, 0] = (1 / characteristicImpedance) *
				(
				D *
						(
							outputinput - gammaXi2Inv
						) +
						(
							C * gamma * boreSection.SphericalDistance * gammaXi2Inv
						)
				);


			}

			if (boreSection.OpeningRadius > boreSection.ClosingRadius)
				return transmissionMatrix.Inverse;

			return transmissionMatrix;
		}

		public static Complex InputImpedance(IList<BoreSection> boreSections, double freq)
		{
			FrequencyCalculations freqCalc = new FrequencyCalculations(freq);

			ComplexMatrix cascadeMatrix = ComplexMatrix.IdentityMatrix();
			foreach (BoreSection boreSection in boreSections)
				cascadeMatrix = cascadeMatrix * TransmissionMatrix(freqCalc, boreSection);


			Complex terminatingImpedance = TerminatingImpedance(freqCalc, boreSections[boreSections.Count - 1]);

			Complex inputPressure = terminatingImpedance * cascadeMatrix[0, 0] + cascadeMatrix[0, 1];
			Complex inputFlow = terminatingImpedance * cascadeMatrix[1, 0] + cascadeMatrix[1, 1];

			return ((inputPressure / inputFlow));
		}

		private static Complex TerminatingImpedance(FrequencyCalculations freq, BoreSection lastSection)
		{
			return new Complex(freq.terminatingImpedanceT1.Real, freq.terminatingImpedanceT1.Imaginary / lastSection.ClosingRadius);
		}
	}

}
