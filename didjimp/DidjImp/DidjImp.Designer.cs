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
	partial class DidjImpApp
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
			System.Windows.Forms.Panel panel1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.Panel panel2;
			System.Windows.Forms.TabPage tabPage1;
			System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
			System.Windows.Forms.Panel panel4;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Panel panel5;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.TabPage tabPage2;
			System.Windows.Forms.Panel panel3;
			System.Windows.Forms.Label label4;
			System.Windows.Forms.MenuStrip menuStrip1;
			this.txtDimensions = new System.Windows.Forms.TextBox();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.comboHarmonics = new System.Windows.Forms.ComboBox();
			this.comboImpedanceGraphType = new System.Windows.Forms.ComboBox();
			this.impedancePlot = new NPlot.Windows.PlotSurface2D();
			this.waveformPlot = new NPlot.Windows.PlotSurface2D();
			this.borePlot = new NPlot.Windows.PlotSurface2D();
			this.comboWaveformSelect = new CustomComboBox.CustomComboBox();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			panel2 = new System.Windows.Forms.Panel();
			tabPage1 = new System.Windows.Forms.TabPage();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			panel4 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			tabPage2 = new System.Windows.Forms.TabPage();
			panel3 = new System.Windows.Forms.Panel();
			label4 = new System.Windows.Forms.Label();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			panel1.SuspendLayout();
			panel2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			tabPage2.SuspendLayout();
			panel3.SuspendLayout();
			menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(this.txtDimensions);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(this.btnCalculate);
			panel1.Dock = System.Windows.Forms.DockStyle.Left;
			panel1.Location = new System.Drawing.Point(0, 24);
			panel1.Name = "panel1";
			panel1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
			panel1.Size = new System.Drawing.Size(147, 556);
			panel1.TabIndex = 0;
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
			label2.AutoSize = true;
			label2.Dock = System.Windows.Forms.DockStyle.Top;
			label2.Location = new System.Drawing.Point(3, 23);
			label2.Name = "label2";
			label2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			label2.Size = new System.Drawing.Size(89, 16);
			label2.TabIndex = 4;
			label2.Text = "(Position, Radius)";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Dock = System.Windows.Forms.DockStyle.Top;
			label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			label1.Location = new System.Drawing.Point(3, 3);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(102, 20);
			label1.TabIndex = 3;
			label1.Text = "Dimensions";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
			panel2.AutoSize = true;
			panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			panel2.Controls.Add(this.tabControl1);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(147, 24);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(828, 556);
			panel2.TabIndex = 1;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(tabPage1);
			this.tabControl1.Controls.Add(tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Enabled = false;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(826, 554);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(flowLayoutPanel1);
			tabPage1.Controls.Add(this.impedancePlot);
			tabPage1.Location = new System.Drawing.Point(4, 22);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new System.Windows.Forms.Padding(3);
			tabPage1.Size = new System.Drawing.Size(818, 528);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Impedance";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			flowLayoutPanel1.Controls.Add(panel4);
			flowLayoutPanel1.Controls.Add(panel5);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(812, 26);
			flowLayoutPanel1.TabIndex = 7;
			// 
			// panel4
			// 
			panel4.AutoSize = true;
			panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel4.Controls.Add(label3);
			panel4.Controls.Add(this.comboHarmonics);
			panel4.Location = new System.Drawing.Point(0, 0);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(210, 26);
			panel4.TabIndex = 4;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 5);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(133, 13);
			label3.TabIndex = 4;
			label3.Text = "Show Harmonics for Peak:";
			// 
			// comboHarmonics
			// 
			this.comboHarmonics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboHarmonics.FormattingEnabled = true;
			this.comboHarmonics.Location = new System.Drawing.Point(139, 2);
			this.comboHarmonics.Name = "comboHarmonics";
			this.comboHarmonics.Size = new System.Drawing.Size(68, 21);
			this.comboHarmonics.TabIndex = 5;
			this.comboHarmonics.SelectedIndexChanged += new System.EventHandler(this.comboHarmonics_SelectedIndexChanged);
			// 
			// panel5
			// 
			panel5.AutoSize = true;
			panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel5.Controls.Add(this.comboImpedanceGraphType);
			panel5.Controls.Add(label5);
			panel5.Location = new System.Drawing.Point(210, 0);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(211, 26);
			panel5.TabIndex = 5;
			// 
			// comboImpedanceGraphType
			// 
			this.comboImpedanceGraphType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboImpedanceGraphType.FormattingEnabled = true;
			this.comboImpedanceGraphType.Items.AddRange(new object[] {
            "Impedance Magnitude",
            "Real Impedance",
            "Imaginary Impedance",
            "Impedance Phase"});
			this.comboImpedanceGraphType.Location = new System.Drawing.Point(72, 2);
			this.comboImpedanceGraphType.Name = "comboImpedanceGraphType";
			this.comboImpedanceGraphType.Size = new System.Drawing.Size(136, 21);
			this.comboImpedanceGraphType.TabIndex = 1;
			this.comboImpedanceGraphType.SelectedIndexChanged += new System.EventHandler(this.comboImpedanceGraphType_SelectedIndexChanged);
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(3, 5);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(63, 13);
			label5.TabIndex = 0;
			label5.Text = "Graph Type";
			// 
			// impedancePlot
			// 
			this.impedancePlot.AutoScaleAutoGeneratedAxes = false;
			this.impedancePlot.AutoScaleTitle = false;
			this.impedancePlot.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.impedancePlot.DateTimeToolTip = false;
			this.impedancePlot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.impedancePlot.Legend = null;
			this.impedancePlot.LegendZOrder = -1;
			this.impedancePlot.Location = new System.Drawing.Point(3, 3);
			this.impedancePlot.Name = "impedancePlot";
			this.impedancePlot.RightMenu = null;
			this.impedancePlot.ShowCoordinates = true;
			this.impedancePlot.Size = new System.Drawing.Size(812, 522);
			this.impedancePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			this.impedancePlot.SurfacePadding = 10;
			this.impedancePlot.TabIndex = 3;
			this.impedancePlot.Text = "plotSurface2D1";
			this.impedancePlot.Title = "";
			this.impedancePlot.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.impedancePlot.XAxis1 = null;
			this.impedancePlot.XAxis2 = null;
			this.impedancePlot.YAxis1 = null;
			this.impedancePlot.YAxis2 = null;
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(this.waveformPlot);
			tabPage2.Controls.Add(this.borePlot);
			tabPage2.Controls.Add(panel3);
			tabPage2.Location = new System.Drawing.Point(4, 22);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3);
			tabPage2.Size = new System.Drawing.Size(818, 528);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "Bore";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// waveformPlot
			// 
			this.waveformPlot.AutoScaleAutoGeneratedAxes = false;
			this.waveformPlot.AutoScaleTitle = false;
			this.waveformPlot.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.waveformPlot.DateTimeToolTip = false;
			this.waveformPlot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.waveformPlot.Legend = null;
			this.waveformPlot.LegendZOrder = -1;
			this.waveformPlot.Location = new System.Drawing.Point(3, 173);
			this.waveformPlot.Name = "waveformPlot";
			this.waveformPlot.RightMenu = null;
			this.waveformPlot.ShowCoordinates = true;
			this.waveformPlot.Size = new System.Drawing.Size(812, 352);
			this.waveformPlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			this.waveformPlot.SurfacePadding = 10;
			this.waveformPlot.TabIndex = 6;
			this.waveformPlot.Text = "plotSurface2D1";
			this.waveformPlot.Title = "";
			this.waveformPlot.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.waveformPlot.XAxis1 = null;
			this.waveformPlot.XAxis2 = null;
			this.waveformPlot.YAxis1 = null;
			this.waveformPlot.YAxis2 = null;
			// 
			// borePlot
			// 
			this.borePlot.AutoScaleAutoGeneratedAxes = false;
			this.borePlot.AutoScaleTitle = false;
			this.borePlot.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.borePlot.DateTimeToolTip = false;
			this.borePlot.Dock = System.Windows.Forms.DockStyle.Top;
			this.borePlot.Legend = null;
			this.borePlot.LegendZOrder = -1;
			this.borePlot.Location = new System.Drawing.Point(3, 30);
			this.borePlot.Name = "borePlot";
			this.borePlot.RightMenu = null;
			this.borePlot.ShowCoordinates = true;
			this.borePlot.Size = new System.Drawing.Size(812, 143);
			this.borePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
			this.borePlot.SurfacePadding = 10;
			this.borePlot.TabIndex = 4;
			this.borePlot.Text = "j";
			this.borePlot.Title = "";
			this.borePlot.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.borePlot.XAxis1 = null;
			this.borePlot.XAxis2 = null;
			this.borePlot.YAxis1 = null;
			this.borePlot.YAxis2 = null;
			this.borePlot.Layout += new System.Windows.Forms.LayoutEventHandler(this.borePlot_Layout);
			// 
			// panel3
			// 
			panel3.AutoSize = true;
			panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel3.Controls.Add(this.comboWaveformSelect);
			panel3.Controls.Add(label4);
			panel3.Dock = System.Windows.Forms.DockStyle.Top;
			panel3.Location = new System.Drawing.Point(3, 3);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(812, 27);
			panel3.TabIndex = 5;
			// 
			// comboWaveformSelect
			// 
			this.comboWaveformSelect.AllowResizeDropDown = true;
			this.comboWaveformSelect.ControlSize = new System.Drawing.Size(1, 1);
			this.comboWaveformSelect.DropDownControl = null;
			this.comboWaveformSelect.DropDownSizeMode = CustomComboBox.CustomComboBox.SizeMode.UseControlSize;
			this.comboWaveformSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboWaveformSelect.DropSize = new System.Drawing.Size(121, 106);
			this.comboWaveformSelect.FormattingEnabled = true;
			this.comboWaveformSelect.Location = new System.Drawing.Point(168, 3);
			this.comboWaveformSelect.Name = "comboWaveformSelect";
			this.comboWaveformSelect.Size = new System.Drawing.Size(117, 21);
			this.comboWaveformSelect.TabIndex = 2;
			this.comboWaveformSelect.SelectedIndexChanged += new System.EventHandler(this.comboWaveformSelect_SelectedIndexChanged);
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new System.Drawing.Point(5, 6);
			label4.Name = "label4";
			label4.Size = new System.Drawing.Size(157, 13);
			label4.TabIndex = 0;
			label4.Text = "Show Waveform for Frequency:";
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem});
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Size = new System.Drawing.Size(975, 24);
			menuStrip1.TabIndex = 1;
			menuStrip1.Text = "menuStrip1";
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
			// DidjImpApp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(976, 581);
			this.Controls.Add(panel2);
			this.Controls.Add(panel1);
			this.Controls.Add(menuStrip1);
			this.Name = "DidjImpApp";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.Text = "DidjImp";
			this.Load += new System.EventHandler(this.DidjImp_Load);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			panel2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			panel3.ResumeLayout(false);
			panel3.PerformLayout();
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.TextBox txtDimensions;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private NPlot.Windows.PlotSurface2D impedancePlot;
		private System.Windows.Forms.ComboBox comboHarmonics;
		private CustomComboBox.CustomComboBox comboWaveformSelect;
		private NPlot.Windows.PlotSurface2D borePlot;
		private System.Windows.Forms.ComboBox comboImpedanceGraphType;
		private System.Windows.Forms.TabControl tabControl1;
		private NPlot.Windows.PlotSurface2D waveformPlot;





	}
}

