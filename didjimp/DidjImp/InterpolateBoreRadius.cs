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
using System.ComponentModel;
using System.Windows.Forms;

namespace DidjImp
{
	public partial class InterpolateBoreRadius : Form
	{
		private DidjImpApp app;

		public InterpolateBoreRadius()
		{
			InitializeComponent();
		}

		public InterpolateBoreRadius(DidjImpApp app)
			: this()
		{
			this.app = app;
		}

		private bool CalculateRadiusAtPosition(decimal position)
		{
			if (app == null || app.Bore == null || position < 0 || position > (decimal)app.Bore.Length)
				return false;

			decimal radius = -1;
			for (int i = 0; i < app.Bore.BoreDimensions.Count; i++)
			{
				if ((decimal)app.Bore.BoreDimensions[i].Position == position)
				{
					radius = (decimal)app.Bore.BoreDimensions[i].Radius;
					break;
				}
				if (position < (decimal)app.Bore.BoreDimensions[i].Position)
				{
					decimal position1 = (decimal)app.Bore.BoreDimensions[i-1].Position;
					decimal radius1 = (decimal)app.Bore.BoreDimensions[i-1].Radius;
					decimal position2 = (decimal)app.Bore.BoreDimensions[i].Position;
					decimal radius2 = (decimal)app.Bore.BoreDimensions[i].Radius;

					radius = ((radius2 - radius1) / (position2 - position1)) * (position - position1) + radius1;
					break;
				}
			}
			if (radius == -1)
				return false;

			txtRadius.Text = radius.ToString("0.00###");
			return true;
		}

		private void txtPosition_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				this.ActiveControl = null;
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(txtRadius.Text);
		}

		private void txtPosition_Leave(object sender, EventArgs e)
		{
			decimal position;
			if (!Decimal.TryParse(txtPosition.Text, out position))
				return;

			CalculateRadiusAtPosition(position);
		}

		private void InterpolateBoreRadius_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				this.ActiveControl = null;
			}

			//escape
			if (e.KeyChar == (char)27)
			{
				e.Handled = true;
				this.Close();				
			}
		}
	}
}
