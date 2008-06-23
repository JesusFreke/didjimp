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
			System.Windows.Forms.TableLayoutPanel tblTreeAndDimension;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DidjImpApp));
			System.Windows.Forms.TabPage tabPageImpedance;
			System.Windows.Forms.Panel pnlImpedancePlot;
			DidjImp.ImpedancePlot.ImpedancePlotContextMenu impedancePlotContextMenu2 = new DidjImp.ImpedancePlot.ImpedancePlotContextMenu();
			System.Windows.Forms.FlowLayoutPanel pnlImpedanceTools;
			System.Windows.Forms.Panel pnlImpedanceShowHarmonics;
			System.Windows.Forms.Label lblShowHarmonics;
			System.Windows.Forms.Panel pnlImpedanceGraphType;
			System.Windows.Forms.Label lblGraphType;
			System.Windows.Forms.TabPage tabBore;
			System.Windows.Forms.Panel pnlBoreTools;
			System.Windows.Forms.MenuStrip menuMain;
			System.Windows.Forms.ToolStripMenuItem mnuFile;
			System.Windows.Forms.ToolStripMenuItem mnuHelp;
			this.treeDidgeHistory = new System.Windows.Forms.TreeView();
			this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
			this.btnCalculate = new System.Windows.Forms.Button();
			this.txtDimensions = new System.Windows.Forms.TextBox();
			this.impedancePlot = new DidjImp.ImpedancePlot();
			this.comboHarmonics = new CustomComboBox.CustomComboBox();
			this.comboImpedanceGraphType = new System.Windows.Forms.ComboBox();
			this.borePlot = new DidjImp.BorePlot();
			this.comboWaveformSelect = new CustomComboBox.CustomComboBox();
			this.chkWaveform = new System.Windows.Forms.CheckBox();
			this.waveformPlot = new NPlot.Windows.PlotSurface2D();
			this.saveDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.splitMain = new System.Windows.Forms.SplitContainer();
			this.tabPlots = new System.Windows.Forms.TabControl();
			this.textBoxContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.mnuResetText = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuInterpolate = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuScaleBoreByFactor = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuScaleToFundamental = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			tblTreeAndDimension = new System.Windows.Forms.TableLayoutPanel();
			tabPageImpedance = new System.Windows.Forms.TabPage();
			pnlImpedancePlot = new System.Windows.Forms.Panel();
			pnlImpedanceTools = new System.Windows.Forms.FlowLayoutPanel();
			pnlImpedanceShowHarmonics = new System.Windows.Forms.Panel();
			lblShowHarmonics = new System.Windows.Forms.Label();
			pnlImpedanceGraphType = new System.Windows.Forms.Panel();
			lblGraphType = new System.Windows.Forms.Label();
			tabBore = new System.Windows.Forms.TabPage();
			pnlBoreTools = new System.Windows.Forms.Panel();
			menuMain = new System.Windows.Forms.MenuStrip();
			mnuFile = new System.Windows.Forms.ToolStripMenuItem();
			mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
			tblTreeAndDimension.SuspendLayout();
			tabPageImpedance.SuspendLayout();
			pnlImpedancePlot.SuspendLayout();
			pnlImpedanceTools.SuspendLayout();
			pnlImpedanceShowHarmonics.SuspendLayout();
			pnlImpedanceGraphType.SuspendLayout();
			tabBore.SuspendLayout();
			pnlBoreTools.SuspendLayout();
			menuMain.SuspendLayout();
			this.splitMain.Panel1.SuspendLayout();
			this.splitMain.Panel2.SuspendLayout();
			this.splitMain.SuspendLayout();
			this.tabPlots.SuspendLayout();
			this.textBoxContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(316, 6);
			// 
			// tblTreeAndDimension
			// 
			tblTreeAndDimension.ColumnCount = 1;
			tblTreeAndDimension.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tblTreeAndDimension.Controls.Add(this.treeDidgeHistory, 0, 0);
			tblTreeAndDimension.Controls.Add(this.btnCalculate, 0, 3);
			tblTreeAndDimension.Controls.Add(this.txtDimensions, 0, 2);
			tblTreeAndDimension.Dock = System.Windows.Forms.DockStyle.Fill;
			tblTreeAndDimension.Location = new System.Drawing.Point(0, 0);
			tblTreeAndDimension.Margin = new System.Windows.Forms.Padding(0);
			tblTreeAndDimension.Name = "tblTreeAndDimension";
			tblTreeAndDimension.RowCount = 4;
			tblTreeAndDimension.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tblTreeAndDimension.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblTreeAndDimension.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			tblTreeAndDimension.RowStyles.Add(new System.Windows.Forms.RowStyle());
			tblTreeAndDimension.Size = new System.Drawing.Size(186, 517);
			tblTreeAndDimension.TabIndex = 6;
			// 
			// treeDidgeHistory
			// 
			this.treeDidgeHistory.AllowDrop = true;
			this.treeDidgeHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeDidgeHistory.HideSelection = false;
			this.treeDidgeHistory.ImageIndex = 0;
			this.treeDidgeHistory.ImageList = this.treeViewImageList;
			this.treeDidgeHistory.Location = new System.Drawing.Point(1, 3);
			this.treeDidgeHistory.Margin = new System.Windows.Forms.Padding(1, 3, 0, 1);
			this.treeDidgeHistory.Name = "treeDidgeHistory";
			this.treeDidgeHistory.SelectedImageIndex = 0;
			this.treeDidgeHistory.ShowRootLines = false;
			this.treeDidgeHistory.Size = new System.Drawing.Size(185, 243);
			this.treeDidgeHistory.TabIndex = 6;
			this.treeDidgeHistory.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeDidgeHistory_DragDrop);
			this.treeDidgeHistory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDidgeHistory_AfterSelect);
			this.treeDidgeHistory.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeDidgeHistory_DragEnter);
			this.treeDidgeHistory.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeDidgeHistory_BeforeSelect);
			this.treeDidgeHistory.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeDidgeHistory_ItemDrag);
			this.treeDidgeHistory.DragOver += new System.Windows.Forms.DragEventHandler(this.treeDidgeHistory_DragOver);
			// 
			// treeViewImageList
			// 
			this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
			this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.treeViewImageList.Images.SetKeyName(0, "Untitled.ico");
			this.treeViewImageList.Images.SetKeyName(1, "didge.ico");
			// 
			// btnCalculate
			// 
			this.btnCalculate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.btnCalculate.Enabled = false;
			this.btnCalculate.Location = new System.Drawing.Point(0, 494);
			this.btnCalculate.Margin = new System.Windows.Forms.Padding(0);
			this.btnCalculate.Name = "btnCalculate";
			this.btnCalculate.Size = new System.Drawing.Size(186, 23);
			this.btnCalculate.TabIndex = 8;
			this.btnCalculate.Text = "Calculate";
			this.btnCalculate.UseVisualStyleBackColor = true;
			this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
			// 
			// txtDimensions
			// 
			this.txtDimensions.AcceptsReturn = true;
			this.txtDimensions.AcceptsTab = true;
			this.txtDimensions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtDimensions.Location = new System.Drawing.Point(0, 248);
			this.txtDimensions.Margin = new System.Windows.Forms.Padding(0, 1, 0, 2);
			this.txtDimensions.Multiline = true;
			this.txtDimensions.Name = "txtDimensions";
			this.txtDimensions.Size = new System.Drawing.Size(186, 244);
			this.txtDimensions.TabIndex = 9;
			this.txtDimensions.TextChanged += new System.EventHandler(this.txtDimensions_TextChanged);
			// 
			// tabPageImpedance
			// 
			tabPageImpedance.Controls.Add(pnlImpedancePlot);
			tabPageImpedance.Controls.Add(pnlImpedanceTools);
			tabPageImpedance.Location = new System.Drawing.Point(4, 20);
			tabPageImpedance.Margin = new System.Windows.Forms.Padding(0);
			tabPageImpedance.Name = "tabPageImpedance";
			tabPageImpedance.Size = new System.Drawing.Size(779, 493);
			tabPageImpedance.TabIndex = 0;
			tabPageImpedance.Text = "Impedance";
			tabPageImpedance.UseVisualStyleBackColor = true;
			// 
			// pnlImpedancePlot
			// 
			pnlImpedancePlot.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pnlImpedancePlot.Controls.Add(this.impedancePlot);
			pnlImpedancePlot.Dock = System.Windows.Forms.DockStyle.Fill;
			pnlImpedancePlot.Location = new System.Drawing.Point(0, 26);
			pnlImpedancePlot.Margin = new System.Windows.Forms.Padding(0);
			pnlImpedancePlot.Name = "pnlImpedancePlot";
			pnlImpedancePlot.Size = new System.Drawing.Size(779, 467);
			pnlImpedancePlot.TabIndex = 9;
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
			this.impedancePlot.RightMenu = impedancePlotContextMenu2;
			this.impedancePlot.SelectedFrequency = 0;
			this.impedancePlot.ShowCoordinates = true;
			this.impedancePlot.Size = new System.Drawing.Size(775, 463);
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
			// pnlImpedanceTools
			// 
			pnlImpedanceTools.AutoSize = true;
			pnlImpedanceTools.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			pnlImpedanceTools.Controls.Add(pnlImpedanceShowHarmonics);
			pnlImpedanceTools.Controls.Add(pnlImpedanceGraphType);
			pnlImpedanceTools.Dock = System.Windows.Forms.DockStyle.Top;
			pnlImpedanceTools.Location = new System.Drawing.Point(0, 0);
			pnlImpedanceTools.Name = "pnlImpedanceTools";
			pnlImpedanceTools.Size = new System.Drawing.Size(779, 26);
			pnlImpedanceTools.TabIndex = 7;
			// 
			// pnlImpedanceShowHarmonics
			// 
			pnlImpedanceShowHarmonics.AutoSize = true;
			pnlImpedanceShowHarmonics.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			pnlImpedanceShowHarmonics.Controls.Add(this.comboHarmonics);
			pnlImpedanceShowHarmonics.Controls.Add(lblShowHarmonics);
			pnlImpedanceShowHarmonics.Location = new System.Drawing.Point(0, 0);
			pnlImpedanceShowHarmonics.Margin = new System.Windows.Forms.Padding(0);
			pnlImpedanceShowHarmonics.Name = "pnlImpedanceShowHarmonics";
			pnlImpedanceShowHarmonics.Size = new System.Drawing.Size(229, 26);
			pnlImpedanceShowHarmonics.TabIndex = 4;
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
			// lblShowHarmonics
			// 
			lblShowHarmonics.AutoSize = true;
			lblShowHarmonics.Location = new System.Drawing.Point(3, 5);
			lblShowHarmonics.Name = "lblShowHarmonics";
			lblShowHarmonics.Size = new System.Drawing.Size(158, 13);
			lblShowHarmonics.TabIndex = 4;
			lblShowHarmonics.Text = "Show Harmonics for Frequency:";
			// 
			// pnlImpedanceGraphType
			// 
			pnlImpedanceGraphType.AutoSize = true;
			pnlImpedanceGraphType.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			pnlImpedanceGraphType.Controls.Add(this.comboImpedanceGraphType);
			pnlImpedanceGraphType.Controls.Add(lblGraphType);
			pnlImpedanceGraphType.Location = new System.Drawing.Point(229, 0);
			pnlImpedanceGraphType.Margin = new System.Windows.Forms.Padding(0);
			pnlImpedanceGraphType.Name = "pnlImpedanceGraphType";
			pnlImpedanceGraphType.Size = new System.Drawing.Size(207, 26);
			pnlImpedanceGraphType.TabIndex = 5;
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
			// lblGraphType
			// 
			lblGraphType.AutoSize = true;
			lblGraphType.Location = new System.Drawing.Point(3, 5);
			lblGraphType.Name = "lblGraphType";
			lblGraphType.Size = new System.Drawing.Size(66, 13);
			lblGraphType.TabIndex = 0;
			lblGraphType.Text = "Graph Type:";
			// 
			// tabBore
			// 
			tabBore.Controls.Add(this.borePlot);
			tabBore.Controls.Add(pnlBoreTools);
			tabBore.Controls.Add(this.waveformPlot);
			tabBore.Location = new System.Drawing.Point(4, 20);
			tabBore.Name = "tabBore";
			tabBore.Padding = new System.Windows.Forms.Padding(3);
			tabBore.Size = new System.Drawing.Size(779, 493);
			tabBore.TabIndex = 1;
			tabBore.Text = "Bore";
			tabBore.UseVisualStyleBackColor = true;
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
			this.borePlot.Size = new System.Drawing.Size(773, 461);
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
			// pnlBoreTools
			// 
			pnlBoreTools.AutoSize = true;
			pnlBoreTools.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			pnlBoreTools.Controls.Add(this.comboWaveformSelect);
			pnlBoreTools.Controls.Add(this.chkWaveform);
			pnlBoreTools.Dock = System.Windows.Forms.DockStyle.Top;
			pnlBoreTools.Location = new System.Drawing.Point(3, 3);
			pnlBoreTools.Name = "pnlBoreTools";
			pnlBoreTools.Size = new System.Drawing.Size(773, 26);
			pnlBoreTools.TabIndex = 5;
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
			// menuMain
			// 
			menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            mnuFile,
            this.mnuOptions,
            this.mnuTools,
            mnuHelp});
			menuMain.Location = new System.Drawing.Point(0, 0);
			menuMain.Name = "menuMain";
			menuMain.Size = new System.Drawing.Size(975, 24);
			menuMain.TabIndex = 1;
			menuMain.Text = "menuStrip1";
			// 
			// mnuFile
			// 
			mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveDimensionsToolStripMenuItem,
            this.loadDimensionsToolStripMenuItem});
			mnuFile.Name = "mnuFile";
			mnuFile.Size = new System.Drawing.Size(35, 20);
			mnuFile.Text = "&File";
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
			// mnuOptions
			// 
			this.mnuOptions.Name = "mnuOptions";
			this.mnuOptions.Size = new System.Drawing.Size(56, 20);
			this.mnuOptions.Text = "&Options";
			this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
			// 
			// mnuTools
			// 
			this.mnuTools.Name = "mnuTools";
			this.mnuTools.Size = new System.Drawing.Size(44, 20);
			this.mnuTools.Text = "&Tools";
			// 
			// mnuHelp
			// 
			mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout});
			mnuHelp.Name = "mnuHelp";
			mnuHelp.Size = new System.Drawing.Size(40, 20);
			mnuHelp.Text = "&Help";
			// 
			// mnuAbout
			// 
			this.mnuAbout.Name = "mnuAbout";
			this.mnuAbout.Size = new System.Drawing.Size(114, 22);
			this.mnuAbout.Text = "&About";
			this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
			// 
			// splitMain
			// 
			this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitMain.Location = new System.Drawing.Point(0, 24);
			this.splitMain.Margin = new System.Windows.Forms.Padding(0);
			this.splitMain.Name = "splitMain";
			// 
			// splitMain.Panel1
			// 
			this.splitMain.Panel1.Controls.Add(tblTreeAndDimension);
			this.splitMain.Panel1MinSize = 80;
			// 
			// splitMain.Panel2
			// 
			this.splitMain.Panel2.Controls.Add(this.tabPlots);
			this.splitMain.Panel2MinSize = 120;
			this.splitMain.Size = new System.Drawing.Size(975, 517);
			this.splitMain.SplitterDistance = 186;
			this.splitMain.SplitterWidth = 2;
			this.splitMain.TabIndex = 2;
			// 
			// tabPlots
			// 
			this.tabPlots.Controls.Add(tabPageImpedance);
			this.tabPlots.Controls.Add(tabBore);
			this.tabPlots.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabPlots.Enabled = false;
			this.tabPlots.Location = new System.Drawing.Point(0, 0);
			this.tabPlots.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.tabPlots.Name = "tabPlots";
			this.tabPlots.Padding = new System.Drawing.Point(6, 0);
			this.tabPlots.SelectedIndex = 0;
			this.tabPlots.Size = new System.Drawing.Size(787, 517);
			this.tabPlots.TabIndex = 1;
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
			// DidjImpApp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(976, 542);
			this.Controls.Add(this.splitMain);
			this.Controls.Add(menuMain);
			this.Name = "DidjImpApp";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 1, 1);
			this.Text = "DidjImp";
			this.Load += new System.EventHandler(this.DidjImpApp_Load);
			tblTreeAndDimension.ResumeLayout(false);
			tblTreeAndDimension.PerformLayout();
			tabPageImpedance.ResumeLayout(false);
			tabPageImpedance.PerformLayout();
			pnlImpedancePlot.ResumeLayout(false);
			pnlImpedanceTools.ResumeLayout(false);
			pnlImpedanceTools.PerformLayout();
			pnlImpedanceShowHarmonics.ResumeLayout(false);
			pnlImpedanceShowHarmonics.PerformLayout();
			pnlImpedanceGraphType.ResumeLayout(false);
			pnlImpedanceGraphType.PerformLayout();
			tabBore.ResumeLayout(false);
			tabBore.PerformLayout();
			pnlBoreTools.ResumeLayout(false);
			menuMain.ResumeLayout(false);
			menuMain.PerformLayout();
			this.splitMain.Panel1.ResumeLayout(false);
			this.splitMain.Panel2.ResumeLayout(false);
			this.splitMain.ResumeLayout(false);
			this.tabPlots.ResumeLayout(false);
			this.textBoxContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStripMenuItem saveDimensionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadDimensionsToolStripMenuItem;
		private CustomComboBox.CustomComboBox comboWaveformSelect;
		private BorePlot borePlot;
		private System.Windows.Forms.ComboBox comboImpedanceGraphType;
		private System.Windows.Forms.TabControl tabPlots;
		private NPlot.Windows.PlotSurface2D waveformPlot;
		private ImpedancePlot impedancePlot;
		private System.Windows.Forms.CheckBox chkWaveform;
		private CustomComboBox.CustomComboBox comboHarmonics;
		private System.Windows.Forms.ContextMenuStrip textBoxContextMenu;
		private System.Windows.Forms.ToolStripMenuItem mnuResetText;
		private System.Windows.Forms.ToolStripMenuItem mnuInterpolate;
		private System.Windows.Forms.ToolStripMenuItem mnuScaleBoreByFactor;
		private System.Windows.Forms.ToolStripMenuItem mnuScaleToFundamental;
		private System.Windows.Forms.ToolStripMenuItem mnuTools;
		private System.Windows.Forms.ToolStripMenuItem mnuAbout;
		private System.Windows.Forms.SplitContainer splitMain;
		private System.Windows.Forms.TreeView treeDidgeHistory;
		private System.Windows.Forms.ToolStripMenuItem mnuOptions;
		private System.Windows.Forms.ImageList treeViewImageList;
		private System.Windows.Forms.Button btnCalculate;
		private System.Windows.Forms.TextBox txtDimensions;
	}
}

