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
	partial class DidjImp
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.txtDimensions = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.comboPeaks = new System.Windows.Forms.ComboBox();
			this.radioMagnitude = new System.Windows.Forms.RadioButton();
			this.radioReal = new System.Windows.Forms.RadioButton();
			this.radioImaginary = new System.Windows.Forms.RadioButton();
			this.radioPhase = new System.Windows.Forms.RadioButton();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.plot = new NPlot.Windows.PlotSurface2D();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.panel4.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.txtDimensions);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btnCalculate);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 24);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.panel1.Size = new System.Drawing.Size(147, 556);
			this.panel1.TabIndex = 0;
			// 
			// txtDimensions
			// 
			this.txtDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDimensions.Location = new System.Drawing.Point(3, 39);
			this.txtDimensions.Multiline = true;
			this.txtDimensions.Name = "txtDimensions";
			this.txtDimensions.Size = new System.Drawing.Size(141, 494);
			this.txtDimensions.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(3, 23);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.label2.Size = new System.Drawing.Size(89, 16);
			this.label2.TabIndex = 4;
			this.label2.Text = "(Position, Radius)";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(102, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Dimensions";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCalculate
			// 
			this.btnCalculate.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnCalculate.Location = new System.Drawing.Point(3, 533);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.Size = new System.Drawing.Size(141, 23);
			this.btnCalculate.TabIndex = 2;
			this.btnCalculate.Text = "Calculate";
			this.btnCalculate.UseVisualStyleBackColor = true;
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// panel2
			// 
			this.panel2.AutoSize = true;
			this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.tabControl1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(147, 24);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(828, 556);
			this.panel2.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(826, 554);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.flowLayoutPanel1);
			this.tabPage1.Controls.Add(this.plot);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(818, 528);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Impedance";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel1
			// 
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.panel4);
			this.flowLayoutPanel1.Controls.Add(this.radioMagnitude);
			this.flowLayoutPanel1.Controls.Add(this.radioReal);
			this.flowLayoutPanel1.Controls.Add(this.radioImaginary);
			this.flowLayoutPanel1.Controls.Add(this.radioPhase);
			this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(812, 26);
			this.flowLayoutPanel1.TabIndex = 7;
			// 
			// panel4
			// 
			this.panel4.AutoSize = true;
			this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panel4.Controls.Add(this.label3);
			this.panel4.Controls.Add(this.comboPeaks);
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Margin = new System.Windows.Forms.Padding(0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(210, 26);
			this.panel4.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(3, 5);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(133, 13);
			this.label3.TabIndex = 4;
			this.label3.Text = "Show Harmonics for Peak:";
			// 
			// comboPeaks
			// 
			this.comboPeaks.FormattingEnabled = true;
			this.comboPeaks.Location = new System.Drawing.Point(139, 2);
			this.comboPeaks.Name = "comboPeaks";
			this.comboPeaks.Size = new System.Drawing.Size(68, 21);
			this.comboPeaks.TabIndex = 5;
			this.comboPeaks.Validating += new System.ComponentModel.CancelEventHandler(this.comboPeaks_Validating);
			this.comboPeaks.SelectedIndexChanged += new System.EventHandler(this.comboPeaks_SelectedIndexChanged);
			// 
			// radioMagnitude
			// 
			this.radioMagnitude.AutoSize = true;
			this.radioMagnitude.Checked = true;
			this.radioMagnitude.Location = new System.Drawing.Point(213, 3);
			this.radioMagnitude.Name = "radioMagnitude";
			this.radioMagnitude.Size = new System.Drawing.Size(131, 17);
			this.radioMagnitude.TabIndex = 0;
			this.radioMagnitude.TabStop = true;
			this.radioMagnitude.Text = "Impedance Magnitude";
			this.radioMagnitude.UseVisualStyleBackColor = true;
			this.radioMagnitude.CheckedChanged += new System.EventHandler(this.radioMagnitude_CheckedChanged);
			// 
			// radioReal
			// 
			this.radioReal.AutoSize = true;
			this.radioReal.Location = new System.Drawing.Point(350, 3);
			this.radioReal.Name = "radioReal";
			this.radioReal.Size = new System.Drawing.Size(103, 17);
			this.radioReal.TabIndex = 1;
			this.radioReal.Text = "Real Impedance";
			this.radioReal.UseVisualStyleBackColor = true;
			this.radioReal.CheckedChanged += new System.EventHandler(this.radioReal_CheckedChanged);
			// 
			// radioImaginary
			// 
			this.radioImaginary.AutoSize = true;
			this.radioImaginary.Location = new System.Drawing.Point(459, 3);
			this.radioImaginary.Name = "radioImaginary";
			this.radioImaginary.Size = new System.Drawing.Size(126, 17);
			this.radioImaginary.TabIndex = 2;
			this.radioImaginary.Text = "Imaginary Impedance";
			this.radioImaginary.UseVisualStyleBackColor = true;
			this.radioImaginary.CheckedChanged += new System.EventHandler(this.radioImaginary_CheckedChanged);
			// 
			// radioPhase
			// 
			this.radioPhase.AutoSize = true;
			this.radioPhase.Location = new System.Drawing.Point(591, 3);
			this.radioPhase.Name = "radioPhase";
			this.radioPhase.Size = new System.Drawing.Size(111, 17);
			this.radioPhase.TabIndex = 3;
			this.radioPhase.Text = "Impedance Phase";
			this.radioPhase.UseVisualStyleBackColor = true;
			this.radioPhase.CheckedChanged += new System.EventHandler(this.radioPhase_CheckedChanged);
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(818, 528);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Bore";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(975, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDimensionsToolStripMenuItem,
            this.loadDimensionsToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// saveDimensionsToolStripMenuItem
			// 
			this.saveDimensionsToolStripMenuItem.Name = "saveDimensionsToolStripMenuItem";
			this.saveDimensionsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.saveDimensionsToolStripMenuItem.Text = "&Save Dimensions";
			this.saveDimensionsToolStripMenuItem.Click += new System.EventHandler(this.saveDimensionsToolStripMenuItem_Click);
			// 
			// loadDimensionsToolStripMenuItem
			// 
			this.loadDimensionsToolStripMenuItem.Name = "loadDimensionsToolStripMenuItem";
			this.loadDimensionsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.loadDimensionsToolStripMenuItem.Text = "&Load Dimensions";
			this.loadDimensionsToolStripMenuItem.Click += new System.EventHandler(this.loadDimensionsToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// plot
			// 
			this.plot.AutoScaleAutoGeneratedAxes = false;
			this.plot.AutoScaleTitle = false;
			this.plot.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.plot.DateTimeToolTip = false;
			this.plot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plot.Legend = null;
			this.plot.LegendZOrder = -1;
			this.plot.Location = new System.Drawing.Point(3, 3);
			this.plot.Name = "plot";
			this.plot.RightMenu = null;
			this.plot.ShowCoordinates = true;
			this.plot.Size = new System.Drawing.Size(812, 522);
			this.plot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			this.plot.SurfacePadding = 10;
			this.plot.TabIndex = 3;
			this.plot.Text = "plotSurface2D1";
			this.plot.Title = "";
			this.plot.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.plot.XAxis1 = null;
			this.plot.XAxis2 = null;
			this.plot.YAxis1 = null;
			this.plot.YAxis2 = null;
			// 
			// DidjImp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(976, 581);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "DidjImp";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.Text = "DidjImp";
			this.Load += new System.EventHandler(this.DidjImp_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtDimensions;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private NPlot.Windows.PlotSurface2D plot;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.RadioButton radioPhase;
		private System.Windows.Forms.RadioButton radioReal;
		private System.Windows.Forms.RadioButton radioMagnitude;
		private System.Windows.Forms.RadioButton radioImaginary;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboPeaks;





	}
}

