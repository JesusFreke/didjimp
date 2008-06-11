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
using System.Windows.Forms;

namespace DidjImp
{
	public partial class Options : Form
	{
		private DidjImpSettings settings;
		public Options(DidjImpSettings settings)
		{
			InitializeComponent();
			this.settings = settings;
			switch (settings.Units)
			{
				case DidjImpSettings.UnitType.millimeter:
					radio_mm.Checked = true;
					break;
				case DidjImpSettings.UnitType.centimeter:
					radio_cm.Checked = true;
					break;
				case DidjImpSettings.UnitType.meter:
					radio_m.Checked = true;
					break;
				case DidjImpSettings.UnitType.inch:
					radio_in.Checked = true;
					break;
				case DidjImpSettings.UnitType.foot:
					radio_ft.Checked = true;
					break;
				case DidjImpSettings.UnitType.yard:
					radio_yd.Checked = true;
					break;
				default:
					radio_m.Checked = true;
					break;
			}

			if (settings.NumberOfThreads < 1 || settings.NumberOfThreads > 16)
				numThreads.Value = 2;
			else
				numThreads.Value = settings.NumberOfThreads;
		}


		private void btnOK_Click(object sender, EventArgs e)
		{
			if (radio_mm.Checked)
				settings.Units = DidjImpSettings.UnitType.millimeter;
			else if (radio_cm.Checked)
				settings.Units = DidjImpSettings.UnitType.centimeter;
			else if (radio_m.Checked)
				settings.Units = DidjImpSettings.UnitType.meter;
			else if (radio_in.Checked)
				settings.Units = DidjImpSettings.UnitType.inch;
			else if (radio_ft.Checked)
				settings.Units = DidjImpSettings.UnitType.foot;
			else if (radio_yd.Checked)
				settings.Units = DidjImpSettings.UnitType.yard;

			settings.NumberOfThreads = (int)numThreads.Value;

			settings.Save();
		}
	}
}
