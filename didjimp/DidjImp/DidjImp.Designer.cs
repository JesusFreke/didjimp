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
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
			System.Windows.Forms.Panel panel2;
			System.Windows.Forms.MenuStrip menuStrip1;
			System.Windows.Forms.Panel panel1;
			System.Windows.Forms.Label label2;
			System.Windows.Forms.Label label1;
			System.Windows.Forms.TabPage tabPage1;
			System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
			System.Windows.Forms.Panel panel4;
			System.Windows.Forms.Label label3;
			System.Windows.Forms.Panel panel5;
			System.Windows.Forms.Label label5;
			System.Windows.Forms.TabPage tabPage2;
			System.Windows.Forms.Panel panel3;
			DidjImp.ImpedancePlot.ImpedancePlotContextMenu impedancePlotContextMenu1 = new DidjImp.ImpedancePlot.ImpedancePlotContextMenu();
			this.textBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuResetText = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuInterpolate = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuScaleBoreByFactor = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuScaleToFundamental = new System.Windows.Forms.ToolStripMenuItem();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.txtCurrentDimensions = new System.Windows.Forms.TextBox();
			this.txtDimensions = new System.Windows.Forms.TextBox();
			this.btnCalculate = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.panel6 = new System.Windows.Forms.Panel();
			this.comboImpedanceGraphType = new System.Windows.Forms.ComboBox();
			this.chkWaveform = new System.Windows.Forms.CheckBox();
			this.impedancePlot = new DidjImp.ImpedancePlot();
			this.comboHarmonics = new CustomComboBox.CustomComboBox();
			this.borePlot = new DidjImp.BorePlot();
			this.comboWaveformSelect = new CustomComboBox.CustomComboBox();
			this.waveformPlot = new NPlot.Windows.PlotSurface2D();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			panel2 = new System.Windows.Forms.Panel();
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			panel1 = new System.Windows.Forms.Panel();
			label2 = new System.Windows.Forms.Label();
			label1 = new System.Windows.Forms.Label();
			tabPage1 = new System.Windows.Forms.TabPage();
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			panel4 = new System.Windows.Forms.Panel();
			label3 = new System.Windows.Forms.Label();
			panel5 = new System.Windows.Forms.Panel();
			label5 = new System.Windows.Forms.Label();
			tabPage2 = new System.Windows.Forms.TabPage();
			panel3 = new System.Windows.Forms.Panel();
			this.textBoxContextMenu.SuspendLayout();
			panel2.SuspendLayout();
			menuStrip1.SuspendLayout();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.tabControl1.SuspendLayout();
			tabPage1.SuspendLayout();
			this.panel6.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			panel4.SuspendLayout();
			panel5.SuspendLayout();
			tabPage2.SuspendLayout();
			panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxContextMenu
			// 
			this.textBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuResetText,
            toolStripSeparator2,
            this.mnuInterpolate,
            this.mnuScaleBoreByFactor,
            this.mnuScaleToFundamental});
			this.textBoxContextMenu.Name = "textBoxContextMenu";
			this.textBoxContextMenu.ShowImageMargin = false;
			this.textBoxContextMenu.ShowItemToolTips = false;
			this.textBoxContextMenu.Size = new System.Drawing.Size(320, 98);
			this.textBoxContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.textBoxContextMenu_Opening);
			// 
			// mnuResetText
			// 
			this.mnuResetText.Name = "mnuResetText";
			this.mnuResetText.Size = new System.Drawing.Size(319, 22);
			this.mnuResetText.Text = "&Reset Dimensions";
			this.mnuResetText.Click += new System.EventHandler(this.mnuResetText_Click);
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(316, 6);
			// 
			// mnuInterpolate
			// 
			this.mnuInterpolate.Name = "mnuInterpolate";
			this.mnuInterpolate.Size = new System.Drawing.Size(319, 22);
			this.mnuInterpolate.Text = "Interpolate Bore Radius at Arbitrary Position";
			this.mnuInterpolate.Click += new System.EventHandler(this.mnuInterpolate_Click);
			// 
			// mnuScaleBoreByFactor
			// 
			this.mnuScaleBoreByFactor.Name = "mnuScaleBoreByFactor";
			this.mnuScaleBoreByFactor.Size = new System.Drawing.Size(319, 22);
			this.mnuScaleBoreByFactor.Text = "&Scale Entire Bore";
			this.mnuScaleBoreByFactor.Click += new System.EventHandler(this.mnuScaleBoreByPercent_Click);
			// 
			// mnuScaleToFundamental
			// 
			this.mnuScaleToFundamental.Name = "mnuScaleToFundamental";
			this.mnuScaleToFundamental.Size = new System.Drawing.Size(319, 22);
			this.mnuScaleToFundamental.Text = "Scale Entire Bore to Specified &Fundamental Frequency";
			this.mnuScaleToFundamental.Click += new System.EventHandler(this.mnuScaleToFundamental_Click);
			// 
			// panel2
			// 
			panel2.AutoSize = true;
			panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel2.Controls.Add(this.splitContainer1);
			panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			panel2.Location = new System.Drawing.Point(0, 24);
			panel2.Margin = new System.Windows.Forms.Padding(0);
			panel2.Name = "panel2";
			panel2.Size = new System.Drawing.Size(975, 517);
			panel2.TabIndex = 1;
			// 
			// menuStrip1
			// 
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.mnuTools,
            this.helpToolStripMenuItem});
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
            this.loadDimensionsToolStripMenuItem});
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
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem.Text = "&Options";
			this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
			// 
			// mnuTools
			// 
			this.mnuTools.Name = "mnuTools";
			this.mnuTools.Size = new System.Drawing.Size(44, 20);
			this.mnuTools.Text = "&Tools";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(panel1);
			this.splitContainer1.Panel1MinSize = 80;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
			this.splitContainer1.Panel2MinSize = 120;
			this.splitContainer1.Size = new System.Drawing.Size(975, 517);
			this.splitContainer1.SplitterDistance = 119;
			this.splitContainer1.SplitterWidth = 2;
			this.splitContainer1.TabIndex = 2;
			// 
			// panel1
			// 
			panel1.Controls.Add(this.tableLayoutPanel1);
			panel1.Controls.Add(label2);
			panel1.Controls.Add(label1);
			panel1.Controls.Add(this.btnCalculate);
			panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			panel1.Location = new System.Drawing.Point(0, 0);
			panel1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(119, 517);
			panel1.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Controls.Add(this.txtCurrentDimensions, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtDimensions, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
			this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(119, 458);
			this.tableLayoutPanel1.TabIndex = 6;
			// 
			// txtCurrentDimensions
			// 
			this.txtCurrentDimensions.ContextMenuStrip = this.textBoxContextMenu;
			this.txtCurrentDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtCurrentDimensions.Location = new System.Drawing.Point(1, 3);
			this.txtCurrentDimensions.Margin = new System.Windows.Forms.Padding(1, 3, 0, 1);
			this.txtCurrentDimensions.Multiline = true;
			this.txtCurrentDimensions.Name = "txtCurrentDimensions";
			this.txtCurrentDimensions.ReadOnly = true;
			this.txtCurrentDimensions.Size = new System.Drawing.Size(118, 225);
			this.txtCurrentDimensions.TabIndex = 5;
			// 
			// txtDimensions
			// 
			this.txtDimensions.AcceptsReturn = true;
			this.txtDimensions.AcceptsTab = true;
			this.txtDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDimensions.Location = new System.Drawing.Point(1, 230);
			this.txtDimensions.Margin = new System.Windows.Forms.Padding(1, 1, 0, 3);
			this.txtDimensions.Multiline = true;
			this.txtDimensions.Name = "txtDimensions";
			this.txtDimensions.Size = new System.Drawing.Size(118, 225);
			this.txtDimensions.TabIndex = 5;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Dock = System.Windows.Forms.DockStyle.Top;
			label2.Location = new System.Drawing.Point(0, 20);
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
			label1.Location = new System.Drawing.Point(0, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(102, 20);
			label1.TabIndex = 3;
			label1.Text = "Dimensions";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnCalculate
			// 
			this.btnCalculate.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnCalculate.Location = new System.Drawing.Point(0, 494);
			this.btnCalculate.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.Size = new System.Drawing.Size(119, 23);
			this.btnCalculate.TabIndex = 2;
			this.btnCalculate.Text = "Calculate";
			this.btnCalculate.UseVisualStyleBackColor = true;
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(tabPage1);
			this.tabControl1.Controls.Add(tabPage2);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Enabled = false;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.Padding = new System.Drawing.Point(6, 0);
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(854, 517);
			this.tabControl1.TabIndex = 1;
			// 
			// tabPage1
			// 
			tabPage1.Controls.Add(this.panel6);
			tabPage1.Controls.Add(flowLayoutPanel1);
			tabPage1.Location = new System.Drawing.Point(4, 20);
			tabPage1.Margin = new System.Windows.Forms.Padding(0);
			tabPage1.Name = "tabPage1";
			tabPage1.Size = new System.Drawing.Size(846, 493);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "Impedance";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// panel6
			// 
			this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel6.Controls.Add(this.impedancePlot);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel6.Location = new System.Drawing.Point(0, 26);
			this.panel6.Margin = new System.Windows.Forms.Padding(0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(846, 467);
			this.panel6.TabIndex = 9;
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			flowLayoutPanel1.Controls.Add(panel4);
			flowLayoutPanel1.Controls.Add(panel5);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(846, 26);
			flowLayoutPanel1.TabIndex = 7;
			// 
			// panel4
			// 
			panel4.AutoSize = true;
			panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel4.Controls.Add(this.comboHarmonics);
			panel4.Controls.Add(label3);
			panel4.Location = new System.Drawing.Point(0, 0);
			panel4.Margin = new System.Windows.Forms.Padding(0);
			panel4.Name = "panel4";
			panel4.Size = new System.Drawing.Size(229, 26);
			panel4.TabIndex = 4;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(3, 5);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(158, 13);
			label3.TabIndex = 4;
			label3.Text = "Show Harmonics for Frequency:";
			// 
			// panel5
			// 
			panel5.AutoSize = true;
			panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel5.Controls.Add(this.comboImpedanceGraphType);
			panel5.Controls.Add(label5);
			panel5.Location = new System.Drawing.Point(229, 0);
			panel5.Margin = new System.Windows.Forms.Padding(0);
			panel5.Name = "panel5";
			panel5.Size = new System.Drawing.Size(207, 26);
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
			this.comboImpedanceGraphType.Location = new System.Drawing.Point(68, 2);
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
			label5.Size = new System.Drawing.Size(66, 13);
			label5.TabIndex = 0;
			label5.Text = "Graph Type:";
			// 
			// tabPage2
			// 
			tabPage2.Controls.Add(this.borePlot);
			tabPage2.Controls.Add(panel3);
			tabPage2.Controls.Add(this.waveformPlot);
			tabPage2.Location = new System.Drawing.Point(4, 20);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new System.Windows.Forms.Padding(3);
			tabPage2.Size = new System.Drawing.Size(844, 491);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "Bore";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// panel3
			// 
			panel3.AutoSize = true;
			panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			panel3.Controls.Add(this.comboWaveformSelect);
			panel3.Controls.Add(this.chkWaveform);
			panel3.Dock = System.Windows.Forms.DockStyle.Top;
			panel3.Location = new System.Drawing.Point(3, 3);
			panel3.Name = "panel3";
			panel3.Size = new System.Drawing.Size(838, 26);
			panel3.TabIndex = 5;
			// 
			// chkWaveform
			// 
			this.chkWaveform.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkWaveform.Location = new System.Drawing.Point(3, 3);
			this.chkWaveform.Name = "chkWaveform";
			this.chkWaveform.Size = new System.Drawing.Size(179, 20);
			this.chkWaveform.TabIndex = 3;
			this.chkWaveform.Text = "Show Waveform for Frequency:";
			this.chkWaveform.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			this.chkWaveform.UseVisualStyleBackColor = true;
			this.chkWaveform.CheckedChanged += new System.EventHandler(this.chkWaveform_CheckedChanged);
			// 
			// impedancePlot
			// 
			this.impedancePlot.AutoScaleAutoGeneratedAxes = false;
			this.impedancePlot.AutoScaleTitle = false;
			this.impedancePlot.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.impedancePlot.DateTimeToolTip = false;
			this.impedancePlot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.impedancePlot.ImpedanceData = null;
			this.impedancePlot.ImpedancePlotType = DidjImp.ImpedancePlotType.Magnitude;
			this.impedancePlot.Legend = null;
			this.impedancePlot.LegendZOrder = -1;
			this.impedancePlot.Location = new System.Drawing.Point(0, 0);
			this.impedancePlot.Name = "impedancePlot";
			this.impedancePlot.RightMenu = impedancePlotContextMenu1;
			this.impedancePlot.SelectedFrequency = 0;
			this.impedancePlot.ShowCoordinates = true;
			this.impedancePlot.Size = new System.Drawing.Size(842, 463);
			this.impedancePlot.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			this.impedancePlot.SurfacePadding = 10;
			this.impedancePlot.TabIndex = 8;
			this.impedancePlot.Text = "impedancePlot";
			this.impedancePlot.Title = "";
			this.impedancePlot.TitleFont = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
			this.impedancePlot.XAxis1 = null;
			this.impedancePlot.XAxis2 = null;
			this.impedancePlot.YAxis1 = null;
			this.impedancePlot.YAxis2 = null;
			// 
			// comboHarmonics
			// 
			this.comboHarmonics.AllowResizeDropDown = false;
			this.comboHarmonics.ControlSize = new System.Drawing.Size(1, 1);
			this.comboHarmonics.DropDownControl = null;
			this.comboHarmonics.DropDownSizeMode = CustomComboBox.CustomComboBox.SizeMode.UseControlSize;
			this.comboHarmonics.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboHarmonics.DropSize = new System.Drawing.Size(121, 106);
			this.comboHarmonics.FormattingEnabled = true;
			this.comboHarmonics.Location = new System.Drawing.Point(160, 2);
			this.comboHarmonics.Name = "comboHarmonics";
			this.comboHarmonics.Size = new System.Drawing.Size(66, 21);
			this.comboHarmonics.TabIndex = 5;
			this.comboHarmonics.SelectedIndexChanged += new System.EventHandler(this.comboHarmonics_SelectedIndexChanged);
			// 
			// borePlot
			// 
			this.borePlot.AutoScaleAutoGeneratedAxes = false;
			this.borePlot.AutoScaleTitle = false;
			this.borePlot.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.borePlot.Bore = null;
			this.borePlot.DateTimeToolTip = false;
			this.borePlot.Dock = System.Windows.Forms.DockStyle.Fill;
			this.borePlot.Legend = null;
			this.borePlot.LegendZOrder = -1;
			this.borePlot.Location = new System.Drawing.Point(3, 29);
			this.borePlot.Name = "borePlot";
			this.borePlot.RightMenu = null;
			this.borePlot.SelectedFrequency = 0;
			this.borePlot.ShowCoordinates = true;
			this.borePlot.ShowWaveformPlot = false;
			this.borePlot.Size = new System.Drawing.Size(838, 459);
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
			// 
			// comboWaveformSelect
			// 
			this.comboWaveformSelect.AllowResizeDropDown = true;
			this.comboWaveformSelect.ControlSize = new System.Drawing.Size(1, 1);
			this.comboWaveformSelect.DropDownControl = null;
			this.comboWaveformSelect.DropDownSizeMode = CustomComboBox.CustomComboBox.SizeMode.UseControlSize;
			this.comboWaveformSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboWaveformSelect.DropSize = new System.Drawing.Size(121, 106);
			this.comboWaveformSelect.Enabled = false;
			this.comboWaveformSelect.FormattingEnabled = true;
			this.comboWaveformSelect.Location = new System.Drawing.Point(178, 1);
			this.comboWaveformSelect.Name = "comboWaveformSelect";
			this.comboWaveformSelect.Size = new System.Drawing.Size(117, 21);
			this.comboWaveformSelect.TabIndex = 2;
			this.comboWaveformSelect.SelectedIndexChanged += new System.EventHandler(this.comboWaveformSelect_SelectedIndexChanged);
			// 
			// waveformPlot
			// 
			this.waveformPlot.AutoScaleAutoGeneratedAxes = false;
			this.waveformPlot.AutoScaleTitle = false;
			this.waveformPlot.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.waveformPlot.DateTimeToolTip = false;
			this.waveformPlot.Legend = null;
			this.waveformPlot.LegendZOrder = -1;
			this.waveformPlot.Location = new System.Drawing.Point(3, 353);
			this.waveformPlot.Name = "waveformPlot";
			this.waveformPlot.RightMenu = null;
			this.waveformPlot.ShowCoordinates = true;
			this.waveformPlot.Size = new System.Drawing.Size(812, 172);
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
			// DidjImpApp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(976, 542);
			this.Controls.Add(panel2);
			this.Controls.Add(menuStrip1);
			this.Name = "DidjImpApp";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.Text = "DidjImp";
			this.Load += new System.EventHandler(this.DidjImpApp_Load);
			this.textBoxContextMenu.ResumeLayout(false);
			panel2.ResumeLayout(false);
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.ResumeLayout(false);
			panel1.ResumeLayout(false);
			panel1.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			tabPage1.ResumeLayout(false);
			tabPage1.PerformLayout();
			this.panel6.ResumeLayout(false);
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			panel4.ResumeLayout(false);
			panel4.PerformLayout();
			panel5.ResumeLayout(false);
			panel5.PerformLayout();
			tabPage2.ResumeLayout(false);
			tabPage2.PerformLayout();
			panel3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.TextBox txtDimensions;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private CustomComboBox.CustomComboBox comboWaveformSelect;
		private BorePlot borePlot;
		private System.Windows.Forms.ComboBox comboImpedanceGraphType;
		private System.Windows.Forms.TabControl tabControl1;
		private NPlot.Windows.PlotSurface2D waveformPlot;
		private ImpedancePlot impedancePlot;
		private System.Windows.Forms.CheckBox chkWaveform;
		private CustomComboBox.CustomComboBox comboHarmonics;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.TextBox txtCurrentDimensions;
		private System.Windows.Forms.ContextMenuStrip textBoxContextMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuResetText;
		private System.Windows.Forms.ToolStripMenuItem mnuInterpolate;
		private System.Windows.Forms.ToolStripMenuItem mnuScaleBoreByFactor;
		private System.Windows.Forms.ToolStripMenuItem mnuScaleToFundamental;
		private System.Windows.Forms.ToolStripMenuItem mnuTools;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.Panel panel6;
	}
}

