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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DidjImp
{
	public partial class ScaleBoreToFundamental : Form
	{
		public decimal SelectedFundamental
		{
			get { return (decimal)numFrequency.Value; }
		}

		public ScaleBoreToFundamental()
		{
			InitializeComponent();
			this.comboNote.SelectedIndex = 9;
		}

		public ScaleBoreToFundamental(decimal frequency)
		{
			InitializeComponent();
			if (frequency >= 5 && frequency <= 1000)
				this.numFrequency.Value = frequency;
			else
				this.comboNote.SelectedIndex = 9;
		}

		private static Regex comboNoteValueRegex = new Regex(@"\(([0-9]+\.[0-9]+)Hz\)");
		private void comboNote_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboNote.SelectedItem == null)
				return;
			Match m = comboNoteValueRegex.Match(comboNote.SelectedItem.ToString());
			if (!m.Success)
				return;
			decimal frequency;
			if (!Decimal.TryParse(m.Groups[1].Captures[0].Value, out frequency))
				return;

			numFrequency.Value = frequency;
		}

		private void numFrequency_ValueChanged(object sender, EventArgs e)
		{
			foreach (object item in comboNote.Items)
			{
				if (item.ToString().Contains(numFrequency.Value.ToString("0.00")))
				{
					comboNote.SelectedItem = item;
					return;
				}
			}
			comboNote.SelectedItem = null;
		}
	}
}
