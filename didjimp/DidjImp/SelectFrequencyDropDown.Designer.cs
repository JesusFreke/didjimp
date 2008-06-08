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
	partial class SelectFrequencyDropDown
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lstResonances = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.numFrequency = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numFrequency)).BeginInit();
			this.SuspendLayout();
			// 
			// lstResonances
			// 
			this.lstResonances.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)));
			this.lstResonances.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lstResonances.FormattingEnabled = true;
			this.lstResonances.IntegralHeight = false;
			this.lstResonances.Location = new System.Drawing.Point(0, 26);
			this.lstResonances.Name = "lstResonances";
			this.lstResonances.ScrollAlwaysVisible = true;
			this.lstResonances.Size = new System.Drawing.Size(92, 99);
			this.lstResonances.TabIndex = 0;
			this.lstResonances.SelectedIndexChanged += new System.EventHandler(this.lstResonances_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Select Resonance";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(98, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(25, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "OR";
			// 
			// numFrequency
			// 
			this.numFrequency.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.numFrequency.DecimalPlaces = 2;
			this.numFrequency.Location = new System.Drawing.Point(132, 26);
			this.numFrequency.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
			this.numFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.numFrequency.Name = "numFrequency";
			this.numFrequency.Size = new System.Drawing.Size(82, 20);
			this.numFrequency.TabIndex = 3;
			this.numFrequency.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numFrequency.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numFrequency_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(129, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(85, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Enter Frequency";
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(155, 52);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(36, 23);
			this.btnOK.TabIndex = 5;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// SelectFrequencyDropDown
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.numFrequency);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstResonances);
			this.Name = "SelectFrequencyDropDown";
			this.Size = new System.Drawing.Size(218, 126);
			((System.ComponentModel.ISupportInitialize)(this.numFrequency)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox lstResonances;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown numFrequency;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button btnOK;
	}
}
