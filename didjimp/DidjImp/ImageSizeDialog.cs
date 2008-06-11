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

using System.Windows.Forms;

namespace DidjImp
{
	public partial class ImageSizeDialog : Form
	{
		public ImageSizeDialog(int initialWidth, int initialHeight)
		{
			InitializeComponent();
			numWidth.Value = initialWidth;
			numHeight.Value = initialHeight;
		}

		public int SelectedWidth
		{
			get { return (int)numWidth.Value; }
		}

		public int SelectedHeight
		{
			get { return (int)numHeight.Value; }
		}
	}
}
