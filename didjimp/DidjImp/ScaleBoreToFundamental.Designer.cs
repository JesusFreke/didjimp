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
	partial class ScaleBoreToFundamental
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
			this.label1 = new System.Windows.Forms.Label();
			this.comboNote = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.numFrequency = new System.Windows.Forms.NumericUpDown();
			this.OK = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.numFrequency)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(260, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Scale Bore to Specified Fundamental Note:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// comboNote
			// 
			this.comboNote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.comboNote.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboNote.FormattingEnabled = true;
			this.comboNote.Items.AddRange(new object[] {
            "High A (110.00Hz)",
            "G# (103.83Hz)",
            "G (98.00Hz)",
            "F# (92.50Hz)",
            "F (87.31Hz)",
            "E (82.41Hz)",
            "Eb (77.78Hz)",
            "D (73.42Hz)",
            "C# (69.30Hz)",
            "C (65.41Hz)",
            "B (61.74Hz)",
            "A# (58.27Hz)",
            "A (55.00Hz)",
            "Low G# (51.91Hz)",
            "Low G (49.00Hz)",
            "Low F# (46.25Hz)",
            "Low F (43.65Hz)",
            "Low E (41.20Hz)",
            "Low Eb (38.89Hz)"});
			this.comboNote.Location = new System.Drawing.Point(15, 56);
			this.comboNote.Name = "comboNote";
			this.comboNote.Size = new System.Drawing.Size(111, 21);
			this.comboNote.TabIndex = 1;
			this.comboNote.SelectedIndexChanged += new System.EventHandler(this.comboNote_SelectedIndexChanged);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(33, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Note:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(145, 40);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(25, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "OR";
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(187, 40);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Frequency:";
			// 
			// numFrequency
			// 
			this.numFrequency.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.numFrequency.DecimalPlaces = 2;
			this.numFrequency.Location = new System.Drawing.Point(190, 57);
			this.numFrequency.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numFrequency.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numFrequency.Name = "numFrequency";
			this.numFrequency.Size = new System.Drawing.Size(77, 20);
			this.numFrequency.TabIndex = 5;
			this.numFrequency.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numFrequency.ValueChanged += new System.EventHandler(this.numFrequency_ValueChanged);
			// 
			// OK
			// 
			this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OK.Location = new System.Drawing.Point(116, 95);
			this.OK.Name = "OK";
			this.OK.Size = new System.Drawing.Size(75, 23);
			this.OK.TabIndex = 6;
			this.OK.Text = "OK";
			this.OK.UseVisualStyleBackColor = true;
			// 
			// Cancel
			// 
			this.Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.Location = new System.Drawing.Point(197, 95);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 7;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// ScaleBoreToFundamental
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 130);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.OK);
			this.Controls.Add(this.numFrequency);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.comboNote);
			this.Controls.Add(this.label1);
			this.Name = "ScaleBoreToFundamental";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Scale Bore";
			((System.ComponentModel.ISupportInitialize)(this.numFrequency)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboNote;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numFrequency;
		private System.Windows.Forms.Button OK;
		private System.Windows.Forms.Button Cancel;
	}
}