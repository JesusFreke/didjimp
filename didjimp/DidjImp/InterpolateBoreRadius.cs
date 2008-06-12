using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DidjImp
{
	public partial class InterpolateBoreRadius : Form
	{
		private DidjImpApp app;

		public DidjImpApp App
		{
			get { return app; }
			set { app = value; }
		}

		public InterpolateBoreRadius()
		{
			InitializeComponent();
		}

		public InterpolateBoreRadius(DidjImpApp app)
			: this()
		{
			this.app = app;
		}

		private void txtPosition_Validating(object sender, CancelEventArgs e)
		{
			decimal position;
			if (!Decimal.TryParse(txtPosition.Text, out position))
			{
				e.Cancel = true;
				return;
			}

			if (!CalculateRadiusAtPosition(position))
			{
				e.Cancel = true;
				return;
			}
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

			txtRadius.Text = radius.ToString("0.00###########");
			return true;
		}

		private void txtPosition_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
				this.Validate();
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetDataObject(txtRadius.Text);
		}
	}
}
