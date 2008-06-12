using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DidjImp
{
	public partial class ScaleBoreByPercentDialog : Form
	{
		public decimal ScaleFactor
		{
			get { return (decimal)numScaleFactor.Value; }
		}

		public ScaleBoreByPercentDialog()
		{
			InitializeComponent();
		}
	}
}
