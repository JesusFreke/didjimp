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
			this.plot = new NPlot.Windows.PlotSurface2D();
			this.panel3 = new System.Windows.Forms.Panel();
			this.radioPhase = new System.Windows.Forms.RadioButton();
			this.radioImaginary = new System.Windows.Forms.RadioButton();
			this.radioReal = new System.Windows.Forms.RadioButton();
			this.radioMagnitude = new System.Windows.Forms.RadioButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
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
			this.panel1.Size = new System.Drawing.Size(147, 370);
			this.panel1.TabIndex = 0;
			// 
			// txtDimensions
			// 
			this.txtDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDimensions.Location = new System.Drawing.Point(3, 39);
			this.txtDimensions.Multiline = true;
			this.txtDimensions.Name = "txtDimensions";
			this.txtDimensions.Size = new System.Drawing.Size(141, 308);
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
			this.btnCalculate.Location = new System.Drawing.Point(3, 347);
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
			this.panel2.Controls.Add(this.plot);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(147, 45);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(543, 349);
			this.panel2.TabIndex = 1;
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
			this.plot.Location = new System.Drawing.Point(0, 0);
			this.plot.Name = "plot";
			this.plot.RightMenu = null;
			this.plot.ShowCoordinates = true;
			this.plot.Size = new System.Drawing.Size(541, 347);
			this.plot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			this.plot.SurfacePadding = 10;
			this.plot.TabIndex = 0;
			this.plot.Text = "plotSurface2D1";
			this.plot.Title = "";
			this.plot.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.plot.XAxis1 = null;
			this.plot.XAxis2 = null;
			this.plot.YAxis1 = null;
			this.plot.YAxis2 = null;
			this.plot.MouseMove += new System.Windows.Forms.MouseEventHandler(this.plot_MouseMove);
			this.plot.MouseEnter += new System.EventHandler(this.plot_MouseEnter);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.radioPhase);
			this.panel3.Controls.Add(this.radioImaginary);
			this.panel3.Controls.Add(this.radioReal);
			this.panel3.Controls.Add(this.radioMagnitude);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(147, 24);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(543, 21);
			this.panel3.TabIndex = 1;
			// 
			// radioPhase
			// 
			this.radioPhase.AutoSize = true;
			this.radioPhase.Dock = System.Windows.Forms.DockStyle.Left;
			this.radioPhase.Location = new System.Drawing.Point(360, 0);
			this.radioPhase.Name = "radioPhase";
			this.radioPhase.Size = new System.Drawing.Size(111, 21);
			this.radioPhase.TabIndex = 3;
			this.radioPhase.Text = "Impedance Phase";
			this.radioPhase.UseVisualStyleBackColor = true;
			this.radioPhase.CheckedChanged += new System.EventHandler(this.radioPhase_CheckedChanged);
			// 
			// radioImaginary
			// 
			this.radioImaginary.AutoSize = true;
			this.radioImaginary.Dock = System.Windows.Forms.DockStyle.Left;
			this.radioImaginary.Location = new System.Drawing.Point(234, 0);
			this.radioImaginary.Name = "radioImaginary";
			this.radioImaginary.Size = new System.Drawing.Size(126, 21);
			this.radioImaginary.TabIndex = 2;
			this.radioImaginary.Text = "Imaginary Impedance";
			this.radioImaginary.UseVisualStyleBackColor = true;
			this.radioImaginary.CheckedChanged += new System.EventHandler(this.radioImaginary_CheckedChanged);
			// 
			// radioReal
			// 
			this.radioReal.AutoSize = true;
			this.radioReal.Dock = System.Windows.Forms.DockStyle.Left;
			this.radioReal.Location = new System.Drawing.Point(131, 0);
			this.radioReal.Name = "radioReal";
			this.radioReal.Size = new System.Drawing.Size(103, 21);
			this.radioReal.TabIndex = 1;
			this.radioReal.Text = "Real Impedance";
			this.radioReal.UseVisualStyleBackColor = true;
			this.radioReal.CheckedChanged += new System.EventHandler(this.radioReal_CheckedChanged);
			// 
			// radioMagnitude
			// 
			this.radioMagnitude.AutoSize = true;
			this.radioMagnitude.Checked = true;
			this.radioMagnitude.Dock = System.Windows.Forms.DockStyle.Left;
			this.radioMagnitude.Location = new System.Drawing.Point(0, 0);
			this.radioMagnitude.Name = "radioMagnitude";
			this.radioMagnitude.Size = new System.Drawing.Size(131, 21);
			this.radioMagnitude.TabIndex = 0;
			this.radioMagnitude.TabStop = true;
			this.radioMagnitude.Text = "Impedance Magnitude";
			this.radioMagnitude.UseVisualStyleBackColor = true;
			this.radioMagnitude.CheckedChanged += new System.EventHandler(this.radioMagnitude_CheckedChanged);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(690, 24);
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
			// DidjImp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(691, 395);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "DidjImp";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.Text = "DidjImp";
			this.Load += new System.EventHandler(this.DidjImp_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
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
		private NPlot.Windows.PlotSurface2D plot;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.RadioButton radioPhase;
		private System.Windows.Forms.RadioButton radioImaginary;
		private System.Windows.Forms.RadioButton radioReal;
		private System.Windows.Forms.RadioButton radioMagnitude;
		private System.Windows.Forms.TextBox txtDimensions;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;





	}
}

