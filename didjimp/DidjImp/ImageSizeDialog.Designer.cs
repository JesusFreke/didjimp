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
	partial class ImageSizeDialog
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
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.Label label5;
			this.numWidth = new System.Windows.Forms.NumericUpDown();
			this.numHeight = new System.Windows.Forms.NumericUpDown();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			label3 = new System.Windows.Forms.Label();
			label4 = new System.Windows.Forms.Label();
			label5 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new System.Drawing.Point(12, 35);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(35, 13);
			label1.TabIndex = 1;
			label1.Text = "Width";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(12, 67);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(38, 13);
			label2.TabIndex = 1;
			label2.Text = "Height";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(117, 35);
			label3.Margin = new System.Windows.Forms.Padding(0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(18, 13);
			label3.TabIndex = 2;
			label3.Text = "px";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(117, 67);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(18, 13);
			label4.TabIndex = 3;
			label4.Text = "px";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(12, 9);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(190, 13);
			label5.TabIndex = 4;
			label5.Text = "Choose the size of the exported image.";
			// 
			// numWidth
			// 
			this.numWidth.Location = new System.Drawing.Point(56, 33);
			this.numWidth.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numWidth.Name = "numWidth";
			this.numWidth.Size = new System.Drawing.Size(62, 20);
			this.numWidth.TabIndex = 0;
			this.numWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// numHeight
			// 
			this.numHeight.Location = new System.Drawing.Point(56, 65);
			this.numHeight.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.numHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numHeight.Name = "numHeight";
			this.numHeight.Size = new System.Drawing.Size(62, 20);
			this.numHeight.TabIndex = 0;
			this.numHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(128, 95);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(43, 95);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// ImageSizeDialog
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(215, 130);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(label5);
			this.Controls.Add(this.numHeight);
			this.Controls.Add(this.numWidth);
			this.Controls.Add(label4);
			this.Controls.Add(label3);
			this.Controls.Add(label2);
			this.Controls.Add(label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "ImageSizeDialog";
			this.Text = "Choose Image Size";
			((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown numWidth;
		private System.Windows.Forms.NumericUpDown numHeight;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
	}
}