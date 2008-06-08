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
using System.Windows.Forms;

namespace DidjImp
{
	public partial class SelectFrequencyDropDown : UserControl
	{
		CustomComboBox.CustomComboBox parent;

		public SelectFrequencyDropDown(CustomComboBox.CustomComboBox parent, List<double> resonances)
		{
			InitializeComponent();

			this.parent = parent;
			for (int i = 0; i < resonances.Count; i++)
				lstResonances.Items.Add(new ListBoxItem(i + 1, resonances[i]));
		}

		private class ListBoxItem
		{
			public string DisplayName;
			public double Frequency;

			public ListBoxItem(int resonanceNumber, double frequency)
			{
				DisplayName = String.Format("{0}: {1:0.00}Hz", resonanceNumber, frequency);
				this.Frequency = frequency;
			}

			public override string ToString()
			{
				return DisplayName;
			}
		}

		private void lstResonances_SelectedIndexChanged(object sender, EventArgs e)
		{
			ListBoxItem item = (ListBoxItem)lstResonances.SelectedItem;
			parent.HideDropDown();
			parent.Items.Clear();
			parent.Items.Add(item.Frequency.ToString());
			parent.SelectedIndex = 0;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			parent.HideDropDown();
			parent.Items.Clear();
			parent.Items.Add(numFrequency.Value.ToString());
			parent.SelectedIndex = 0;			
		}

		private void numFrequency_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r' || e.KeyChar == '\n')
			{
				parent.HideDropDown();
				parent.Items.Clear();
				parent.Items.Add(numFrequency.Value.ToString());
				parent.SelectedIndex = 0;				
			}
		}

		public void SelectFirstResonance()
		{
			ListBoxItem item = (ListBoxItem)lstResonances.Items[0];
			parent.HideDropDown();
			parent.Items.Clear();
			parent.Items.Add(item.Frequency.ToString());
			parent.SelectedIndex = 0;
		}
	}
}
