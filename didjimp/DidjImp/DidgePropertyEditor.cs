using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DidjImp
{
	public partial class DidgePropertyEditor : UserControl
	{
		public DidgePropertyEditor()
		{
			InitializeComponent();
		}

		private bool doDimensionChangedEvent = true;
		private void DisableDimensionChangedEvent()
		{
			doDimensionChangedEvent = false;
		}
		private void EnableDimensionChangedEvent()
		{
			doDimensionChangedEvent = true;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DidgeDesignProperties DidgeDesignProperties
		{
			get
			{
				DidgeDesignProperties didge = new DidgeDesignProperties();
				didge.DidgeName = this.DidgeName;
				didge.DidgeComments = this.DidgeComments;
				didge.BoreSections = this.BoreSections;
				return didge;
			}

			set
			{
				DisableDimensionChangedEvent();
				this.DidgeName = value.DidgeName;
				this.DidgeComments = value.DidgeComments;
				this.BoreSections = value.BoreSections;
				this.lblPositionRadiusError.Text = "";
				this.lblPositionRadiusError.Visible = false;
				this.lblRadiusLengthError.Text = "";
				this.lblRadiusLengthError.Visible = false;
				EnableDimensionChangedEvent();
			}
		}

		public string DidgeName
		{
			get { return txtDidgeName.Text; }
			set { txtDidgeName.Text = value; }
		}

		public string DidgeComments
		{
			get { return txtDidgeComments.Text; }
			set { txtDidgeComments.Text = value; }
		}

		private List<BoreSection> boreSections = new List<BoreSection>();
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public List<BoreSection> BoreSections
		{
			get { return boreSections; }
			set
			{
				boreSections = value;
				UpdatePositionAndRadiusDimensions();
				UpdateRadiusAndLengthDimensions();
			}
		}

		public string PositionAndRadiusDimensions
		{
			get { return txtPositionRadiusDimensions.Text; }
			set
			{
				boreSections = ParsePositionAndRadiusDimensions(value);
				UpdateRadiusAndLengthDimensions();
			}
		}

		public string RadiusAndLengthDimensions
		{
			get { return txtRadiusLengthDimensions.Text; }
			set
			{
				boreSections = ParseRadiusAndLengthDimensions(value);
				UpdatePositionAndRadiusDimensions();
			}
		}

		private void btnCollapseExpandComments_CollapseExpand(EventArgs e)
		{
			txtDidgeComments.Visible = !btnCollapseExpandComments.Collapsed;

			if (btnCollapseExpandComments.Collapsed)
				tblDidgeProperties.RowStyles[1].SizeType = SizeType.AutoSize;
			else
			{
				tblDidgeProperties.RowStyles[1].SizeType = SizeType.Percent;
				tblDidgeProperties.RowStyles[1].Height = 100;
				txtDidgeComments.Focus();
			}
		}

		private void btnCollapseExpandPositionRadius_CollapseExpand(EventArgs e)
		{
			txtPositionRadiusDimensions.Visible = !btnCollapseExpandPositionRadius.Collapsed;
			if (lblPositionRadiusError.Text.Length > 0)
				lblPositionRadiusError.Visible = !btnCollapseExpandPositionRadius.Collapsed;
			else
				lblPositionRadiusError.Visible = false;

			if (btnCollapseExpandPositionRadius.Collapsed)
				tblDidgeProperties.RowStyles[2].SizeType = SizeType.AutoSize;
			else
			{
				tblDidgeProperties.RowStyles[2].SizeType = SizeType.Percent;
				tblDidgeProperties.RowStyles[2].Height = 100;
				if (txtPositionRadiusDimensions.Enabled)
					txtPositionRadiusDimensions.Focus();
			}

			this.PerformLayout();
		}

		private void btnCollapseExpandRadiusLength_CollapseExpand(EventArgs e)
		{
			txtRadiusLengthDimensions.Visible = !btnCollapseExpandRadiusLength.Collapsed;
			if (lblRadiusLengthError.Text.Length > 0)
				lblRadiusLengthError.Visible = !btnCollapseExpandRadiusLength.Collapsed;
			else
				lblRadiusLengthError.Visible = false;

			if (btnCollapseExpandRadiusLength.Collapsed)
				tblDidgeProperties.RowStyles[3].SizeType = SizeType.AutoSize;
			else
			{
				tblDidgeProperties.RowStyles[3].SizeType = SizeType.Percent;
				tblDidgeProperties.RowStyles[3].Height = 100;
				if (txtPositionRadiusDimensions.Enabled)
					txtRadiusLengthDimensions.Focus();
			}
			this.PerformLayout();
		}


		private class PositionAndRadius
		{
			public decimal Position;
			public decimal Radius;

			public PositionAndRadius(decimal position, decimal radius)
			{
				this.Position = position;
				this.Radius = radius;
			}

			private Regex positionsAndRadiusesRegex = new Regex(@"^\s*([0-9.]+)(?:(?:\s+)|(?:\s*,\s*))([0-9.]+)\s*$");
			public PositionAndRadius(string str)
			{
				Match m = positionsAndRadiusesRegex.Match(str);
				if (!m.Success)
					throw new ValidationException("The line is not in a valid format. Expecting 2 positive numbers.", str);

				if (!Decimal.TryParse(m.Groups[1].Captures[0].Value, out Position))
					throw new ValidationException("The position \"{0}\" is not a valid number.", m.Groups[1].Captures[0].Value);

				if (!Decimal.TryParse(m.Groups[2].Captures[0].Value, out Radius))
					throw new ValidationException("The radius \"{1}\" is not a valid number.", m.Groups[2].Captures[0].Value);
			}
		}


		private static List<BoreSection> ParsePositionAndRadiusDimensions(string dimensionString)
		{
			List<BoreSection> boreSections;
			List<PositionAndRadius> dimensions = new List<PositionAndRadius>();
			int lineNumber = 0;
			decimal previousPosition = 0;
			foreach (string dimensionLine in dimensionString.Replace('\r', '\n').Replace("\n\n", "\n").Split('\n'))
			{
				string line = dimensionLine.Trim();
				//ignore blank lines
				if (line.Length == 0)
					continue;

				PositionAndRadius positionAndRadius;
				try
				{
					positionAndRadius = new PositionAndRadius(line);
				}
				catch (ValidationException ex)
				{
					throw new ValidationException("Line {0}: \"{1}\" - {2}", lineNumber, line, ex.Message);
				}

				if (positionAndRadius.Position < previousPosition)
					throw new ValidationException("Line {0}: \"{1}\" - The position \"{2}\" must be greater than or equal to the previous position \"{3}\"", lineNumber, line, positionAndRadius.Position, previousPosition);
				previousPosition = positionAndRadius.Position;

				if (positionAndRadius.Radius == 0)
					throw new ValidationException("Line {0}: \"{1}\" - The radius cannot be 0", lineNumber, line);
				dimensions.Add(positionAndRadius);

				lineNumber++;
			}

			if (dimensions.Count == 1)
				throw new ValidationException("There must be at least 2 dimensions entered");

			boreSections = new List<BoreSection>();
			for (int i = 0; i < dimensions.Count - 1; i++)
			{
				if (dimensions[i + 1].Position > dimensions[i].Position)
				{
					BoreSection boreSection = new BoreSection(dimensions[i].Radius, dimensions[i + 1].Radius, (dimensions[i + 1].Position - dimensions[i].Position));
					boreSections.Add(boreSection);
				}
			}
			return boreSections;
		}


		private class RadiusAndLength
		{
			public decimal Radius;
			public decimal Length;

			public RadiusAndLength(decimal radius, decimal length)
			{
				this.Radius = radius;
				this.Length = length;
			}

			private Regex radiusesAndLengthsRegex = new Regex(@"^\s*([0-9.]+)(?:(?:(?:\s+)|(?:\s*,\s*))([0-9.]+))?\s*$");
			public RadiusAndLength(string str)
			{
				Match m = radiusesAndLengthsRegex.Match(str);
				if (!m.Success)
					throw new ValidationException("The line is not in a valid format. Expecting 2 positive numbers (or just 1 for the last line).", str);

				if (!Decimal.TryParse(m.Groups[1].Captures[0].Value, out Radius))
					throw new ValidationException("The radius \"{0}\" is not a valid number.", m.Groups[1].Captures[0].Value);

				if (m.Groups[2].Success)
				{
					if (!Decimal.TryParse(m.Groups[2].Captures[0].Value, out Length))
						throw new ValidationException("The length \"{1}\" is not a valid number.", m.Groups[2].Captures[0].Value);
				}
				else
				{
					Length = -1;
				}
			}
		}


		private static List<BoreSection> ParseRadiusAndLengthDimensions(string dimensionString)
		{
			List<BoreSection> boreSections = new List<BoreSection>();
			if (dimensionString.Trim().Length == 0)
				return boreSections;

			List<RadiusAndLength> dimensions = new List<RadiusAndLength>();
			int lineNumber = 0;
			bool foundLastLine = false;
			foreach (string dimensionLine in dimensionString.Replace('\r', '\n').Replace("\n\n", "\n").Split('\n'))
			{
				string line = dimensionLine.Trim();
				//ignore blank lines
				if (line.Length == 0)
					continue;

				if (foundLastLine)
					throw new ValidationException("Line {0}: \"{1}\" - Not expecting another line. You probably didn't put a length value on the previous line", lineNumber, line);

				RadiusAndLength radiusAndLength;
				try
				{
					radiusAndLength = new RadiusAndLength(line);
				}
				catch (ValidationException ex)
				{
					throw new ValidationException("Line {0}: \"{1}\" - {2}", lineNumber, line, ex.Message);
				}

				if (radiusAndLength.Length == -1)
					foundLastLine = true;

				if (radiusAndLength.Radius == 0)
					throw new ValidationException("Line {0}: \"{1}\" - The radius cannot be 0", lineNumber, line);
				dimensions.Add(radiusAndLength);

				lineNumber++;
			}

			if (dimensions.Count == 1)
				throw new ValidationException("There must be at least 2 lines. The last line must contain a single number, which is the radius at the end of the bore");

			if (dimensions[dimensions.Count - 1].Length != -1)
				throw new ValidationException("The last line cannot contain a length value. It must be a single number, which is the radius at the end of the bore");

			boreSections = new List<BoreSection>();
			for (int i = 1; i < dimensions.Count; i++)
			{
				BoreSection boreSection = new BoreSection(dimensions[i - 1].Radius, dimensions[i].Radius, dimensions[i - 1].Length);
				boreSections.Add(boreSection);
			}
			return boreSections;
		}


		private void UpdatePositionAndRadiusDimensions()
		{
			StringBuilder sb = new StringBuilder();
			bool first = true;
			decimal position = 0;
			BoreSection previousSection = null;
			foreach (BoreSection boreSection in boreSections)
			{
				if (first)
				{
					sb.AppendFormat("{0}\t{1}\r\n", position, boreSection.OpeningRadius);
					position += (decimal)boreSection.Length;
					sb.AppendFormat("{0}\t{1}", position, boreSection.ClosingRadius);
					first = false;
				}
				else
				{
					sb.AppendLine();
					if (previousSection.ClosingRadius != boreSection.OpeningRadius)
						sb.AppendFormat("{0}\t{1}\r\n", position, boreSection.OpeningRadius);
					position += (decimal)boreSection.Length;
					sb.AppendFormat("{0}\t{1}", position, boreSection.ClosingRadius);
				}
				previousSection = boreSection;
			}
			txtPositionRadiusDimensions.Text = sb.ToString();
		}

		private void UpdateRadiusAndLengthDimensions()
		{
			StringBuilder sb = new StringBuilder();
			bool first = true;
			BoreSection previousSection = null;
			foreach (BoreSection boreSection in boreSections)
			{
				if (first)
				{
					sb.AppendFormat("{0}\t{1}\r\n", boreSection.OpeningRadius, boreSection.Length);
					first = false;
				}
				else
				{
					if (previousSection.ClosingRadius != boreSection.OpeningRadius)
						sb.AppendFormat("{0}\t{1}\r\n", previousSection.ClosingRadius, 0);
					sb.AppendFormat("{0}\t{1}\r\n", boreSection.OpeningRadius, boreSection.Length);
				}
				previousSection = boreSection;
			}
			if (previousSection != null)
				sb.Append(previousSection.ClosingRadius);
			txtRadiusLengthDimensions.Text = sb.ToString();
		}

		public delegate void DimensionsChangedDelegate();
		public event DimensionsChangedDelegate DimensionsChanged;

		public delegate void DidgeNameChangedDelegate();
		public event DidgeNameChangedDelegate DidgeNameChanged;

		public delegate void UnvalidDelegate();
		public event UnvalidDelegate Unvalid;

		public delegate void ValidDelegate();
		public event ValidDelegate Valid;

		private void dimensions_TextChanged(object sender, EventArgs e)
		{
			if (DimensionsChanged != null && doDimensionChangedEvent)
				DimensionsChanged();
		}

		private void txtDidgeName_TextChanged(object sender, EventArgs e)
		{
			if (DidgeNameChanged != null)
				DidgeNameChanged();
		}

		private void DidgePropertyEditor_Load(object sender, EventArgs e)
		{
			lblPositionRadiusError.Text = "";
			lblRadiusLengthError.Text = "";
		}

		private void txtRadiusLengthDimensions_Leave(object sender, EventArgs e)
		{
			try
			{
				BoreSections = ParseRadiusAndLengthDimensions(txtRadiusLengthDimensions.Text);
			}
			catch (ValidationException ex)
			{
				try
				{
					lblRadiusLengthError.Text = ex.Message;
					lblRadiusLengthError.Visible = true;
					txtPositionRadiusDimensions.Enabled = false;
					if (Unvalid != null)
						Unvalid();
				}
				catch { }
				return;
			}

			lblRadiusLengthError.Text = "";
			lblRadiusLengthError.Visible = false;
			txtPositionRadiusDimensions.Enabled = true;
			if (Valid != null)
				Valid();
		}

		private void txtPositionRadiusDimensions_Leave(object sender, EventArgs e)
		{
			try
			{
				ParsePositionAndRadiusDimensions(txtPositionRadiusDimensions.Text);
			}
			catch (ValidationException ex)
			{
				try
				{
					lblPositionRadiusError.Text = ex.Message.Replace('\t', ' ').Replace("\r\n", "");
					lblPositionRadiusError.Visible = true;
					txtRadiusLengthDimensions.Enabled = false;
					if (Unvalid != null)
						Unvalid();
				}
				catch { }
				return;
			}

			lblPositionRadiusError.Text = "";
			lblPositionRadiusError.Visible = false;
			BoreSections = ParsePositionAndRadiusDimensions(txtPositionRadiusDimensions.Text);
			txtRadiusLengthDimensions.Enabled = true;
			if (Valid != null)
				Valid();
		}
	}
}
