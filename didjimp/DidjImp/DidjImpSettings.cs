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
using System.Configuration;

namespace DidjImp
{
	public class DidjImpSettings : ApplicationSettingsBase
	{
		[UserScopedSetting()]
		[DefaultSettingValueAttribute("meter")]
		public UnitType Units
		{
			get { return (UnitType)this["Units"]; }
			set { this["Units"] = value; }
		}

		public double UnitConversionFactor
		{
			get
			{
				switch (this.Units)
				{
					case DidjImpSettings.UnitType.millimeter:
						return .001;
					case DidjImpSettings.UnitType.centimeter:
						return .01;
					case DidjImpSettings.UnitType.meter:
						return 1;
					case DidjImpSettings.UnitType.inch:
						return .0254;
					case DidjImpSettings.UnitType.foot:
						return .3048;
					case DidjImpSettings.UnitType.yard:
						return .9144;
					default:
						return 1;
				}
			}
		}

		[UserScopedSetting()]
		[DefaultSettingValueAttribute("2")]
		public int NumberOfThreads
		{
			get { return (int)this["NumberOfThreads"]; }
			set { this["NumberOfThreads"] = value; }
		}

		public enum UnitType
		{
			millimeter=0,
			centimeter,
			meter,
			inch,
			foot,
			yard
		}
	}
}
