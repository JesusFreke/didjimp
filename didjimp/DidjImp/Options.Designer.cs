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

namespace DidjImp
{
	partial class Options
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.radio_mm = new System.Windows.Forms.RadioButton();
			this.radio_cm = new System.Windows.Forms.RadioButton();
			this.radio_m = new System.Windows.Forms.RadioButton();
			this.radio_in = new System.Windows.Forms.RadioButton();
			this.radio_ft = new System.Windows.Forms.RadioButton();
			this.radio_yd = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radio_m);
			this.groupBox1.Controls.Add(this.radio_cm);
			this.groupBox1.Controls.Add(this.radio_yd);
			this.groupBox1.Controls.Add(this.radio_ft);
			this.groupBox1.Controls.Add(this.radio_in);
			this.groupBox1.Controls.Add(this.radio_mm);
			this.groupBox1.Location = new System.Drawing.Point(13, 13);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(115, 91);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Units";
			// 
			// radio_mm
			// 
			this.radio_mm.AutoSize = true;
			this.radio_mm.Location = new System.Drawing.Point(6, 19);
			this.radio_mm.Name = "radio_mm";
			this.radio_mm.Size = new System.Drawing.Size(41, 17);
			this.radio_mm.TabIndex = 0;
			this.radio_mm.TabStop = true;
			this.radio_mm.Text = "mm";
			this.radio_mm.UseVisualStyleBackColor = true;
			// 
			// radio_cm
			// 
			this.radio_cm.AutoSize = true;
			this.radio_cm.Location = new System.Drawing.Point(6, 43);
			this.radio_cm.Name = "radio_cm";
			this.radio_cm.Size = new System.Drawing.Size(39, 17);
			this.radio_cm.TabIndex = 1;
			this.radio_cm.TabStop = true;
			this.radio_cm.Text = "cm";
			this.radio_cm.UseVisualStyleBackColor = true;
			// 
			// radio_m
			// 
			this.radio_m.AutoSize = true;
			this.radio_m.Location = new System.Drawing.Point(5, 66);
			this.radio_m.Name = "radio_m";
			this.radio_m.Size = new System.Drawing.Size(33, 17);
			this.radio_m.TabIndex = 1;
			this.radio_m.TabStop = true;
			this.radio_m.Text = "m";
			this.radio_m.UseVisualStyleBackColor = true;
			// 
			// radio_in
			// 
			this.radio_in.AutoSize = true;
			this.radio_in.Location = new System.Drawing.Point(75, 19);
			this.radio_in.Name = "radio_in";
			this.radio_in.Size = new System.Drawing.Size(33, 17);
			this.radio_in.TabIndex = 0;
			this.radio_in.TabStop = true;
			this.radio_in.Text = "in";
			this.radio_in.UseVisualStyleBackColor = true;
			// 
			// radio_ft
			// 
			this.radio_ft.AutoSize = true;
			this.radio_ft.Location = new System.Drawing.Point(75, 43);
			this.radio_ft.Name = "radio_ft";
			this.radio_ft.Size = new System.Drawing.Size(31, 17);
			this.radio_ft.TabIndex = 0;
			this.radio_ft.TabStop = true;
			this.radio_ft.Text = "ft";
			this.radio_ft.UseVisualStyleBackColor = true;
			// 
			// radio_yd
			// 
			this.radio_yd.AutoSize = true;
			this.radio_yd.Location = new System.Drawing.Point(75, 66);
			this.radio_yd.Name = "radio_yd";
			this.radio_yd.Size = new System.Drawing.Size(36, 17);
			this.radio_yd.TabIndex = 0;
			this.radio_yd.TabStop = true;
			this.radio_yd.Text = "yd";
			this.radio_yd.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(145, 53);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(145, 81);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// Options
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(229, 113);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Options";
			this.Text = "Options";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radio_m;
		private System.Windows.Forms.RadioButton radio_cm;
		private System.Windows.Forms.RadioButton radio_yd;
		private System.Windows.Forms.RadioButton radio_ft;
		private System.Windows.Forms.RadioButton radio_in;
		private System.Windows.Forms.RadioButton radio_mm;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}