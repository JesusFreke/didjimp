using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DidjImp
{
	[DefaultEvent("CollapseExpand")]
	public partial class ExpandControl : Control
	{
		public ExpandControl()
		{
			InitializeComponent();
			this.Size = new Size(9, 9);
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			return new Size(9, 9);
		}

		private bool collapsed = true;
		public bool Collapsed
		{
			get { return collapsed; }
			set
			{
				collapsed = value;
				this.Invalidate();
				if (CollapseExpand != null)
					CollapseExpand(new EventArgs());
			}
		}

		public delegate void CollapseExpandDelegate(EventArgs e);
		public event CollapseExpandDelegate CollapseExpand;

		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			this.Collapsed = !Collapsed;
		}
	
		protected override void OnPaint(PaintEventArgs pe)
		{
			pe.Graphics.FillRectangle(SystemBrushes.Window, 0, 0, 8, 8);
			pe.Graphics.DrawRectangle(SystemPens.ControlText, 0, 0, 8, 8);

			//For some reason, DrawLine doesn't act the same on the local machine
			//as in a remote desktop session. the line is 1 pixel longer on the
			//local machine. Using 6.5 instead of 6 or 7 seems to make it work
			//consistently

			pe.Graphics.DrawLine(SystemPens.ControlText, 2.0f, 4.0f, 6.5f, 4.0f);

			if (this.Collapsed)
			{
				pe.Graphics.DrawLine(SystemPens.ControlText, 4.0f, 2.0f, 4.0f, 6.5f);
			}
		}
	}
}
