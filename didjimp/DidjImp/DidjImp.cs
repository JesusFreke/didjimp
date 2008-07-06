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
		private Progress progressDialog;
		private DidjImpSettings settings = new DidjImpSettings();

		private bool didgeValid = false;
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
			get
			{
				if (treeDidgeHistory.SelectedNode == null)
					return null;
				if (!(treeDidgeHistory.SelectedNode.Tag is DidgeData))
					return null;
				return ((DidgeData)treeDidgeHistory.SelectedNode.Tag).bore;
			}
		}

		public ImpedanceData ImpedanceData
		{
			get
			{
				return ((DidgeData)treeDidgeHistory.SelectedNode.Tag).impedanceData;
			}
		}

		private void ShowError(string message, params object[] values)
		{
			MessageBox.Show(this, String.Format(message, values));
		}

		private Regex dimensionRegex = new Regex(@"^\s*([0-9.]+)(?:(?:\s+)|(?:\s*,\s*))([0-9.]+)\s*(?:;.*)?$");
		private void btnCalculate_Click(object sender, EventArgs e)
		{
			if (!didgeValid)
			{
				ShowError("There is an error with the entered dimensions.");
				return;
			}

			tabPlots.Enabled = true;
			verticalLines.Clear();

			calculatedFrequencyCount = 0;

			DidgeDesignProperties didgeProperties = didgePropertyEditor.DidgeDesignProperties;

			Bore bore = new Bore(didgeProperties.BoreSections);
			if (bore.Length == 0)
			{
				ShowError("The bore has a length of 0.");
				return;				
			}
			bore.CalculatedFrequency += new Bore.CalculatedFrequencyDelegate(bore_CalculatedFrequency);

			DidgeData data = (DidgeData)treeDidgeHistory.SelectedNode.Tag;
			data.didge = didgePropertyEditor.DidgeDesignProperties;
			data.bore = bore;


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
				Bore.FindResonances(2);
				progressDialog.Close();
				btnCalculate.Enabled = false;

				DidgeData data = (DidgeData)treeDidgeHistory.SelectedNode.Tag;
				data.impedanceData = new ImpedanceData(Bore.InputImpedance);
				treeDidgeHistory.SelectedNode.Tag = data;

				treeDidgeHistory.SelectedNode.Text = treeDidgeHistory.SelectedNode.Text.Substring(1);

				UpdateDisplayWithCurrentData();
				data.Calculated = true;
			}));
		}

		private void UpdateDisplayWithCurrentData()
		{
			impedancePlot.ImpedanceData = this.ImpedanceData;

			IList<double> resonances = this.ImpedanceData.ImpedancePeakFrequencies;

			SelectFrequencyDropDown dropDown = new SelectFrequencyDropDown(comboHarmonics, resonances);
			comboHarmonics.DropDownControl = dropDown;
			dropDown.SelectFirstResonance();

			borePlot.Bore = Bore;
			chkWaveform.Checked = false;
		}

		private void DidjImpApp_Load(object sender, EventArgs e)
		{		
			comboWaveformSelect.AllowResizeDropDown = false;
			comboImpedanceGraphType.SelectedIndex = 0;

			TreeNode node = new TreeNode("Untitled Workspace", 0, 0);
			treeDidgeHistory.Nodes.Add(node);
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
				comboWaveformSelect.DropDownControl = new SelectFrequencyDropDown(comboWaveformSelect, ImpedanceData.ImpedancePeakFrequencies);
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

		private void mnuInterpolate_Click(object sender, EventArgs e)
		{
			InterpolateBoreRadius dialog = new InterpolateBoreRadius(this);
			dialog.Show(this);
		}

		private void mnuScaleBoreByPercent_Click(object sender, EventArgs e)
		{
			ScaleBoreByFactorDialog dlg = new ScaleBoreByFactorDialog();
			//ensure that the didgePropertyEditor loses focus first, so
			//that it will perform it's validation
			this.ActiveControl = null;
			//if the didgePropertyEditor couldn't validate the entered didge,
			//then DidgeValid will be false
			if (!didgeValid)
				return;

			DialogResult dr = dlg.ShowDialog(this);
			if (dr == DialogResult.OK)
			{
				didgePropertyEditor_DimensionsChanged();

				decimal scaleFactor = dlg.ScaleFactor;
				StringBuilder sb = new StringBuilder();
				List<BoreSection> newBore = new List<BoreSection>();
				foreach (BoreSection boreSection in didgePropertyEditor.BoreSections)
					newBore.Add(new BoreSection(boreSection.OpeningRadius, boreSection.ClosingRadius, boreSection.Length * scaleFactor));
				didgePropertyEditor.BoreSections = newBore;
				
			}
		}

		private void mnuScaleToFundamental_Click(object sender, EventArgs e)
		{
			if (Bore == null)
			{
				ShowError("You must calculate the impedance first.");
				return;
			}

			ScaleBoreToFundamental dlg = new ScaleBoreToFundamental((decimal)ImpedanceData.ImpedancePeakFrequencies[0]);
			DialogResult dr = dlg.ShowDialog(this);
			decimal targetFundamental = dlg.SelectedFundamental;

			if (dr == DialogResult.OK)
			{
				Bore previousBore = Bore;
				decimal currentFundamental = (decimal)ImpedanceData.ImpedancePeakFrequencies[0];
				while (currentFundamental != targetFundamental)
				{
					decimal scaleAmount = currentFundamental / targetFundamental;
					List<BoreSection> newBoreSections = new List<BoreSection>();
					foreach (BoreSection boreSection in previousBore.BoreSections)
						newBoreSections.Add(new BoreSection(boreSection.OpeningRadius, boreSection.ClosingRadius, boreSection.Length * scaleAmount));
					Bore newBore = new Bore(newBoreSections);
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

				didgePropertyEditor_DimensionsChanged();

				List<BoreSection> boreSections = new List<BoreSection>();
				foreach (BoreSection section in previousBore.BoreSections)
					boreSections.Add(new BoreSection(section.OpeningRadius, section.ClosingRadius, Math.Round(section.Length, 5)));
				didgePropertyEditor.BoreSections = boreSections;
			}
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
					return name;
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
				btnCalculate.Enabled = false;
				impedancePlot.Clear();
				borePlot.Clear();
				tabPlots.Enabled = false;
				return;
			}

			DidgeData data = (DidgeData)treeDidgeHistory.SelectedNode.Tag;

			if (data.didge != null)
			{
				didgePropertyEditor.DidgeDesignProperties = data.didge;
				if (data.Calculated)
				{
					UpdateDisplayWithCurrentData();
					btnCalculate.Enabled = false;
					tabPlots.Enabled = true;
				}
				else
				{
					btnCalculate.Enabled = true;
					impedancePlot.Clear();
					borePlot.Clear();
					tabPlots.Enabled = false;

				}
			}

		}

		private void didgePropertyEditor_DimensionsChanged()
		{
			//do nothing if this is an "unsaved" didge
			if (treeDidgeHistory.SelectedNode.Tag is DidgeData)
			{
				DidgeData didgeData = (DidgeData)treeDidgeHistory.SelectedNode.Tag;

				if (!didgeData.Calculated)
					return;
			}

			//otherwise, we need to create a new child node of the currently selected node
			string didgeName = GetNextDidgeNodeName();
			TreeNode node = new TreeNode("*" + didgeName, 1, 1);
			treeDidgeHistory.SelectedNode.Nodes.Add(node);
			treeDidgeHistory.SelectedNode = node;
			DidgeData data = new DidgeData();
			data.didge.DidgeName = didgeName;
			node.Tag = data;
			didgePropertyEditor.DidgeName = didgeName;
			btnCalculate.Enabled = true;
		}

		private void didgePropertyEditor_Unvalid()
		{
			didgeValid = false;
		}

		private void didgePropertyEditor_Valid()
		{
			didgeValid = true;
		}

		private void didgePropertyEditor_DidgeNameChanged()
		{
			if (((DidgeData)treeDidgeHistory.SelectedNode.Tag).Calculated)
				treeDidgeHistory.SelectedNode.Text = didgePropertyEditor.DidgeName;
			else
				treeDidgeHistory.SelectedNode.Text = "*" + didgePropertyEditor.DidgeName;
		}

		private void treeDidgeHistory_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			if (treeDidgeHistory.SelectedNode != null && treeDidgeHistory.SelectedNode.Tag is DidgeData)
			{
				DidgeData data = (DidgeData)treeDidgeHistory.SelectedNode.Tag;
				data.didge = didgePropertyEditor.DidgeDesignProperties;
			}
		}
	}

	public class WorkspaceData
	{
		public string WorkspaceXMLPath;
	}

	public class DidgeData
	{
		public DidgeDesignProperties didge = new DidgeDesignProperties();
		public ImpedanceData impedanceData = null;
		public Bore bore = null;

		private bool calculated = false;
		public bool Calculated
		{
			get { return calculated; }
			set { calculated = value; }
		}
	}

	public class DidgeDesignProperties
	{
		private string didgeName;
		public string DidgeName
		{
			get { return didgeName; }
			set { didgeName = value; }
		}

		private string didgeComments;
		public string DidgeComments
		{
			get { return didgeComments; }
			set { didgeComments = value; }
		}

		private List<BoreSection> boreSections = new List<BoreSection>();
		public List<BoreSection> BoreSections
		{
			get { return boreSections; }
			set { boreSections = value; }
		}
	}
}
