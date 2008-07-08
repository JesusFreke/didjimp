using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DidjImp
{
	public partial class SurfaceAreaDialog : Form
	{
		private DidjImpApp app;
		public SurfaceAreaDialog()
		{
			InitializeComponent();
		}

		public SurfaceAreaDialog(DidjImpApp app)
			:this()
		{
			this.app = app;
		}

		private void btnCalculate_Click(object sender, EventArgs e)
		{
			DidgeDesignProperties didgeDesignProperties = app.CurrentDidgeDesignProperties;

			if (didgeDesignProperties == null || didgeDesignProperties.BoreSections == null || didgeDesignProperties.BoreSections.Count == 0)
				return;

			double surfaceArea = 0;
			double wallThickness = (double)numWallThickness.Value;

			foreach (BoreSection boreSection in didgeDesignProperties.BoreSections)
				surfaceArea += boreSection.SurfaceArea(wallThickness);

			txtSurfaceArea.Text = Math.Round(surfaceArea, 5).ToString();
		}

		private void SurfaceAreaDialog_KeyPress(object sender, KeyPressEventArgs e)
		{
			//escape
			if (e.KeyChar == (char)27)
			{
				e.Handled = true;
				this.Close();
			}
		}
	}
}
