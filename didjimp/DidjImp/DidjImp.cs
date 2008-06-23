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

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace DidjImp
{
	public partial class DidjImpApp : Form
	{
		private ImpedanceData impedanceData = null;
		private Progress progressDialog;
		private DidjImpSettings settings = new DidjImpSettings();
		private Bore bore = null;
		
		private int calculatedFrequencyCount;
		private int finishedThreads = 0;

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new DidjImpApp());
		}

		public DidjImpApp()
		{
			InitializeComponent();
			impedancePlot.RightMenu = new ImpedancePlot.ImpedancePlotContextMenu();
			borePlot.RightMenu = new BorePlot.BorePlotContextMenu();
			mnuTools.DropDown = textBoxContextMenu;
		}

		public Bore Bore
		{
			get { return bore; }
		}

		private void ShowError(string message, params object[] values)
		{
			MessageBox.Show(this, String.Format(message, values));
		}

		private Regex dimensionRegex = new Regex(@"^\s*([0-9.]+)(?:(?:\s+)|(?:\s*,\s*))([0-9.]+)\s*(?:;.*)?$");
		private void btnCalculate_Click(object sender, EventArgs e)
		{
			tabPlots.Enabled = true;
			verticalLines.Clear();

			calculatedFrequencyCount = 0;
			List<BoreDimension> dimensions = new List<BoreDimension>();
			int lineNumber = 0;
			double previousPosition = 0;
			foreach (string dimensionLine in txtDimensions.Text.Replace('\r', '\n').Replace("\n\n", "\n").Split('\n'))
			{
				string line = dimensionLine.Trim();
				//ignore blank lines and lines with only a comment
				if (line.Length == 0 || line[0] == ';')
					continue;

				Match match = dimensionRegex.Match(line);
				if (!match.Success)
				{
					ShowError("Line {0}: \"{1}\" - The line is not in a valid format. Expecting 2 positive numbers.", lineNumber, dimensionLine);
					return;
				}

				double position;
				if (!Double.TryParse(match.Groups[1].Captures[0].Value, out position))
				{
					ShowError("Line {0}: \"{1}\" - The position \"{2}\" is not a valid number.", lineNumber, dimensionLine, match.Groups[1].Captures[0].Value);
					return;
				}

				if (position < previousPosition)
				{
					ShowError("Line {0}: \"{1}\" - The position \"{2}\" must be greater than or equal to the previous position \"{3}\"", lineNumber, dimensionLine, position, previousPosition);
					return;
				}
				previousPosition = position;

				double radius;
				if (!Double.TryParse(match.Groups[2].Captures[0].Value, out radius))
				{
					ShowError("Line {0}: \"{1}\" - The radius \"{2}\" is not a valid number.", lineNumber, dimensionLine, match.Groups[2].Captures[0].Value);
					return;
				}

				if (radius == 0)
				{
					ShowError("Line {0}: \"{1}\" - The radius cannot be 0", lineNumber, dimensionLine);
					return;
				}
				dimensions.Add(new BoreDimension(position * settings.UnitConversionFactor, radius * settings.UnitConversionFactor));

				lineNumber++;
			}

			if (dimensions.Count == 1)
			{
				ShowError("There must be at least 2 dimensions entered");
				return;
			}

			bore = new Bore(dimensions, 1.0m / 500);
			if (bore.Length == 0)
			{
				ShowError("The bore has a length of 0.");
				return;				
			}
			bore.CalculatedFrequency += new Bore.CalculatedFrequencyDelegate(bore_CalculatedFrequency);

			finishedThreads = 0;
			
			ParameterizedThreadStart threadStart = (threadNum) =>
			{
				//split the work up among however many threads we have
				int startFrequency = (int)Math.Round((2000.0 / settings.NumberOfThreads) * ((int)threadNum) + 1.0, 0);
				int endFrequency = (int)Math.Round((2000.0 / settings.NumberOfThreads) * ((int)threadNum + 1.0), 0);

				bore.CalculateInputImpedance(startFrequency, endFrequency, 1);
				SetImpedanceData();
			};


			for (int i=0; i<settings.NumberOfThreads; i++)
			{
				Thread thread = new Thread(threadStart);
				thread.Start(i);
			}

			progressDialog = new Progress();
			progressDialog.ShowDialog(this);
		}

		private void bore_CalculatedFrequency(double frequency)
		{
			InvokeUtil.InvokeIfRequired(this, new InvokeUtil.VoidDelegate(delegate()
			{
				calculatedFrequencyCount++;
				progressDialog.SetProgress(Math.Min(100, (int)((calculatedFrequencyCount / 2000.0) * 100)));
			}));
		}

		private object SetImpedanceDataLock = new object();
		private void SetImpedanceData()
		{
			lock (SetImpedanceDataLock)
			{
				finishedThreads++;

				if (finishedThreads < settings.NumberOfThreads)
					return;
			}

			InvokeUtil.InvokeIfRequired(this, new InvokeUtil.VoidDelegate(delegate()
			{
				bore.FindResonances(2);
				progressDialog.Close();
				btnCalculate.Enabled = false;
			
				this.impedanceData = new ImpedanceData(bore.InputImpedance);

				DidgeData data = new DidgeData();
				data.bore = this.bore;
				data.dimensions = txtDimensions.Text;
				data.impedanceData = this.impedanceData;
				treeDidgeHistory.SelectedNode.Tag = data;

				treeDidgeHistory.SelectedNode.Text = treeDidgeHistory.SelectedNode.Text.Substring(1);

				UpdateDisplayWithCurrentData();				
			}));
		}

		private void UpdateDisplayWithCurrentData()
		{
			impedancePlot.ImpedanceData = this.impedanceData;

			IList<double> resonances = this.impedanceData.ImpedancePeakFrequencies;

			SelectFrequencyDropDown dropDown = new SelectFrequencyDropDown(comboHarmonics, resonances);
			comboHarmonics.DropDownControl = dropDown;
			dropDown.SelectFirstResonance();

			borePlot.Bore = bore;
			chkWaveform.Checked = false;
		}

		private void DidjImpApp_Load(object sender, EventArgs e)
		{		
			comboWaveformSelect.AllowResizeDropDown = false;
			comboImpedanceGraphType.SelectedIndex = 0;
			impedancePlot.ImpedanceData = this.impedanceData;

			TreeNode node = new TreeNode("Untitled Workspace", 0, 0);
			treeDidgeHistory.Nodes.Add(node);
			treeDidgeHistory.SelectedNode = node;
			node.Tag = new WorkspaceData();

		}


		private void saveDimensionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			/*SaveFileDialog dlg = new SaveFileDialog();
			dlg.Filter = "Text Files (*.txt;*.csv) | *.txt;*.csv";
			DialogResult res = dlg.ShowDialog(this);
			if (res == DialogResult.OK)
			{
				File.WriteAllText(dlg.FileName, txtDimensions.Text);
			}*/
		}

		private void loadDimensionsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			/*OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Text File (*.txt)|*.txt";
			dlg.CheckFileExists = true;
			DialogResult res = dlg.ShowDialog(this);
			if (res == DialogResult.OK)
			{
				txtDimensions.Text = File.ReadAllText(dlg.FileName);
			}*/
		}

		private void mnuAbout_Click(object sender, EventArgs e)
		{
			About about = new About();
			about.ShowDialog();
		}

		private void mnuOptions_Click(object sender, EventArgs e)
		{
			Options options = new Options(settings);
			DialogResult dr = options.ShowDialog();
		}

		private List<VerticalLine> verticalLines = new List<VerticalLine>();
		private void comboHarmonics_SelectedIndexChanged(object sender, EventArgs e)
		{
			double selectedFrequency;
			if (!Double.TryParse((string)comboHarmonics.SelectedItem, out selectedFrequency))
				return;
			impedancePlot.SelectedFrequency = selectedFrequency;
		}

		private void comboImpedanceGraphType_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboImpedanceGraphType.Text)
			{
				case "Impedance Magnitude":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Magnitude;
					break;
				case "Real Impedance":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Real;
					break;
				case "Imaginary Impedance":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Imaginary;
					break;
				case "Impedance Phase":
					impedancePlot.ImpedancePlotType = ImpedancePlotType.Phase;
					break;
			}
		}

		private void comboWaveformSelect_SelectedIndexChanged(object sender, EventArgs e)
		{
			double selectedFrequency;

			if (!double.TryParse((string)comboWaveformSelect.SelectedItem, out selectedFrequency))
				return;

			borePlot.SelectedFrequency = selectedFrequency;
		}

		private void chkWaveform_CheckedChanged(object sender, EventArgs e)
		{
			if (chkWaveform.Checked)
			{
				comboWaveformSelect.DropDownControl = new SelectFrequencyDropDown(comboWaveformSelect, impedanceData.ImpedancePeakFrequencies);
				comboWaveformSelect.Enabled = true;
				((SelectFrequencyDropDown)comboWaveformSelect.DropDownControl).SelectFirstResonance();
				borePlot.ShowWaveformPlot = true;
			}
			else
			{
				comboWaveformSelect.Enabled = false;
				comboWaveformSelect.Items.Clear();
				borePlot.ShowWaveformPlot = false;
			}
		}

		private void mnuResetText_Click(object sender, EventArgs e)
		{
		}

		private void mnuInterpolate_Click(object sender, EventArgs e)
		{
			InterpolateBoreRadius dialog = new InterpolateBoreRadius(this);
			dialog.Show(this);
		}

		private void mnuScaleBoreByPercent_Click(object sender, EventArgs e)
		{
			/*ScaleBoreByFactorDialog dlg = new ScaleBoreByFactorDialog();
			DialogResult dr = dlg.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				decimal scaleFactor = dlg.ScaleFactor;
				StringBuilder sb = new StringBuilder();
				foreach (BoreDimension boreDimension in bore.BoreDimensions)
					sb.AppendFormat("{0}\t{1}\r\n", (decimal)boreDimension.Position * scaleFactor, boreDimension.Radius);
				txtDimensions.Text = sb.ToString();
			}*/
		}

		private void mnuScaleToFundamental_Click(object sender, EventArgs e)
		{
			/*ScaleBoreToFundamental dlg = new ScaleBoreToFundamental((decimal)impedanceData.ImpedancePeakFrequencies[0]);
			DialogResult dr = dlg.ShowDialog(this);
			decimal targetFundamental = dlg.SelectedFundamental;

			if (dr == DialogResult.OK)
			{
				Bore previousBore = bore;
				decimal currentFundamental = (decimal)impedanceData.ImpedancePeakFrequencies[0];
				while (currentFundamental != targetFundamental)
				{
					decimal scaleAmount = currentFundamental / targetFundamental;
					List<BoreDimension> newBoreDimensions = new List<BoreDimension>();
					foreach (BoreDimension boreDimension in previousBore.BoreDimensions)
						newBoreDimensions.Add(new BoreDimension((double)((decimal)boreDimension.Position * scaleAmount), boreDimension.Radius));
					Bore newBore = new Bore(newBoreDimensions, 1.0m/500);
					if (targetFundamental > currentFundamental)
					{
						Complex lastImpedance = newBore.GetImpedance((double)currentFundamental);
						for (decimal freq = currentFundamental + 1; true; freq++)
						{
							Complex currentImpedance = newBore.GetImpedance((double)freq);
							if (currentImpedance.Magnitude < lastImpedance.Magnitude)
							{
								if (newBore.InputImpedance.Count == 2)
								{
									newBore.GetImpedance((double)currentFundamental - 1);
								}
								newBore.FindResonances(2);
								ImpedanceData tempImpedanceData = new ImpedanceData(newBore.InputImpedance);
								currentFundamental = (decimal)tempImpedanceData.ImpedancePeakFrequencies[0];
								previousBore = newBore;
								break;
							}
							lastImpedance = currentImpedance;
						}
					}
					else
					{
						Complex lastImpedance = newBore.GetImpedance(1);
						for (decimal freq = 2; true; freq++)
						{
							Complex currentImpedance = newBore.GetImpedance((double)freq);
							if (currentImpedance.Magnitude < lastImpedance.Magnitude)
							{
								newBore.FindResonances(2);
								ImpedanceData tempImpedanceData = new ImpedanceData(newBore.InputImpedance);
								currentFundamental =(decimal)tempImpedanceData.ImpedancePeakFrequencies[0];
								previousBore = newBore;
								break;
							}
							lastImpedance = currentImpedance;
						}
					}
				}

				StringBuilder sb = new StringBuilder();
				foreach (BoreDimension boreDimension in previousBore.BoreDimensions)
					sb.AppendFormat("{0:0.00######}\t{1:0.00######}\r\n", boreDimension.Position, boreDimension.Radius);

				txtDimensions.Text = sb.ToString();

			}*/
		}

		private void textBoxContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			/*if (txtCurrentDimensions.Text.Length == 0)
			{
				foreach (ToolStripItem item in textBoxContextMenu.Items)
					item.Enabled = false;
			}
			else
			{
				foreach (ToolStripItem item in textBoxContextMenu.Items)
					item.Enabled = true;
			}*/
		}

		private void treeDidgeHistory_ItemDrag(object sender, ItemDragEventArgs e)
		{
			treeDidgeHistory.DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void treeDidgeHistory_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
				e.Effect = DragDropEffects.Move;
			else
				e.Effect = DragDropEffects.None;
		}

		private void treeDidgeHistory_DragOver(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
				return;

			TreeNode dragNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode", true);
			TreeNode targetNode = treeDidgeHistory.GetNodeAt(treeDidgeHistory.PointToClient(new System.Drawing.Point(e.X, e.Y)));

			if (treeDidgeHistory.SelectedNode != targetNode)
			{
				treeDidgeHistory.SelectedNode = targetNode;

				//make sure the node we're dragging over isn't a child of the node that we're dragging
				while (targetNode != null)
				{
					if (targetNode == dragNode)
					{
						e.Effect = DragDropEffects.None;
						return;
					}
					targetNode = targetNode.Parent;
				}

				e.Effect = DragDropEffects.Move;
			}
		}

		private void treeDidgeHistory_DragDrop(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent("System.Windows.Forms.TreeNode", true))
				return;

			TreeNode dragNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode", true);
			TreeNode targetNode = treeDidgeHistory.SelectedNode;

			dragNode.Remove();
			if (targetNode == null)
				treeDidgeHistory.Nodes.Add(dragNode);
			else
				targetNode.Nodes.Add(dragNode);

			dragNode.EnsureVisible();
			treeDidgeHistory.SelectedNode = dragNode;
		}

		private void txtDimensions_TextChanged(object sender, EventArgs e)
		{
			//do nothing if this is an "unsaved" didge
			if (treeDidgeHistory.SelectedNode.Tag is DidgeData)
			{
				if (((DidgeData)treeDidgeHistory.SelectedNode.Tag).bore == null)
					return;
				if (((DidgeData)treeDidgeHistory.SelectedNode.Tag).dimensions == txtDimensions.Text)
					return;
			}

			if (treeDidgeHistory.SelectedNode.Tag is WorkspaceData)
			{
				if (txtDimensions.Text.Length == 0)
					return;
			}


			//otherwise, we need to create a new child node of the currently selected node
			TreeNode node = new TreeNode(GetNextDidgeNodeName(), 1, 1);
			treeDidgeHistory.SelectedNode.Nodes.Add(node);
			treeDidgeHistory.SelectedNode = node;
			node.Tag = new DidgeData();
			btnCalculate.Enabled = true;
		}

		private string GetNextDidgeNodeName()
		{
			int didgeI = 1;
			do
			{
				string name = String.Format("Didge{0}", didgeI);
				string starredName = "*" + name;

				bool found = false;
				foreach (TreeNode node in treeDidgeHistory.Nodes)
				{
					if (FindNode(node, name, starredName))
					{
						found = true;
						break;
					}
				}
				if (!found)
					return starredName;
				didgeI++;
			}while (true);
		}

		private bool FindNode(TreeNode root, string name, string starredName)
		{
			if (root.Text == name || root.Text == starredName)
				return true;

			foreach (TreeNode node in root.Nodes)
			{
				if (FindNode(node, name, starredName))
					return true;
			}
			return false;
		}

		private void treeDidgeHistory_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeDidgeHistory.SelectedNode == null)
				return;

			treeDidgeHistory.SelectedNode.EnsureVisible();

			if (treeDidgeHistory.SelectedNode.Tag == null)
				return;
			
			if (treeDidgeHistory.SelectedNode.Tag is WorkspaceData)
			{
				txtDimensions.Text = "";
				btnCalculate.Enabled = false;
				impedancePlot.Clear();
				borePlot.Clear();
				tabPlots.Enabled = false;
				return;
			}

			DidgeData data = (DidgeData)treeDidgeHistory.SelectedNode.Tag;

			if (data.bore != null)
			{
				txtDimensions.Text = data.dimensions;
				bore = data.bore;
				impedanceData = data.impedanceData;
				UpdateDisplayWithCurrentData();
				btnCalculate.Enabled = false;
				tabPlots.Enabled = true;
			}
			else
			{
				txtDimensions.Text = data.dimensions;
				btnCalculate.Enabled = true;
				impedancePlot.Clear();
				borePlot.Clear();
				tabPlots.Enabled = false;
			}
		}

		private void treeDidgeHistory_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (treeDidgeHistory.SelectedNode == null || treeDidgeHistory.SelectedNode.Tag is WorkspaceData || ((DidgeData)treeDidgeHistory.SelectedNode.Tag).bore != null)
				return;
			else
				((DidgeData)treeDidgeHistory.SelectedNode.Tag).dimensions = txtDimensions.Text;
		}		
	}

	public class WorkspaceData
	{
		public string WorkspaceXMLPath;
	}

	public class DidgeData
	{
		public Bore bore = null;
		public ImpedanceData impedanceData = null;
		public string dimensions = null;
	}
}
