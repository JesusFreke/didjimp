/*
 * CustomComboBox, A Customizable ComboBox Drop-Down (http://www.codeproject.com/KB/combobox/CustomDDComboBox.aspx)
 * Copyright (C) 2008 "lhayes00" (handle on CodeProject)
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
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace CustomComboBox
{
	/// <summary>
	/// <c>CustomComboBox</c> is an extension of <c>ComboBox</c> which provides drop-down customization.
	/// </summary>
	[SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class CustomComboBox : ComboBox
	{
		#region Construction and destruction

		public CustomComboBox()
			: base()
		{
			m_sizeCombo = new Size(base.DropDownWidth, base.DropDownHeight);
			m_dropDown.Closing += new ToolStripDropDownClosingEventHandler(m_dropDown_Closing);
		}

		void m_dropDown_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			m_lastHideTime = DateTime.Now;
		}

		public CustomComboBox(Control dropControl)
			: this()
		{
			DropDownControl = dropControl;
		}

		#endregion

		#region ComboBox overrides

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (m_dropDown != null)
				{
					m_dropDown.Dispose();
					m_dropDown = null;
				}
				if (m_dropDownHost != null)
				{
					m_dropDownHost.Dispose();
					m_dropDownHost = null;
				}
				if (m_timerAutoFocus != null)
				{
					m_timerAutoFocus.Dispose();
					m_timerAutoFocus = null;
				}
			}
			base.Dispose(disposing);
		}

		#endregion

		#region Event handlers

		private void timerAutoFocus_Tick(object sender, EventArgs e)
		{
			if (m_dropDownHost.IsOnDropDown && !DropDownControl.Focused)
			{
				DropDownControl.Focus();
				m_timerAutoFocus.Enabled = false;
			}
		}

		private void m_dropDown_LostFocus(object sender, EventArgs e)
		{
			m_lastHideTime = DateTime.Now;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Prepare custom container to hold drop-down control.
		/// </summary>
		/// <param name="control">Control to contain.</param>
		/// <returns>Specialized container.</returns>
		protected virtual Control PrepareDropDown(Control control)
		{
			if (AllowResizeDropDown)
			{
				// Encapsulate contorl within a resizable container.
				return new ResizableDropDownContainer(control);
			}
			else
			{
				// By default simply return the drop down control.
				return control;
			}
		}

		/// <summary>
		/// Displays drop-down area of combo box, if not already shown.
		/// </summary>
		public virtual void ShowDropDown()
		{
			if (DropDownContainer != null && m_dropDownHost != null && !IsDroppedDown)
			{
				// Restore original container size.
				AutoSizeDropDown();

				// Display drop-down control.
				m_dropDown.Show(this, 0, this.Height);
				m_bDroppedDown = true;

				// Initialize automatic focus timer?
				if (m_timerAutoFocus == null)
				{
					m_timerAutoFocus = new Timer();
					m_timerAutoFocus.Interval = 10;
					m_timerAutoFocus.Tick += new EventHandler(timerAutoFocus_Tick);
				}
				// Enable the timer!
				m_timerAutoFocus.Enabled = true;
				m_sShowTime = DateTime.Now;
			}
		}

		/// <summary>
		/// Hides drop-down area of combo box, if shown.
		/// </summary>
		public virtual void HideDropDown()
		{
			if (DropDownContainer != null && m_dropDownHost != null && IsDroppedDown)
			{
				// Hide drop-down control.
				m_dropDown.Hide();
				m_bDroppedDown = false;

				// Disable automatic focus timer.
				if (m_timerAutoFocus != null && m_timerAutoFocus.Enabled)
					m_timerAutoFocus.Enabled = false;
			}
		}

		/// <summary>
		/// Automatically resize drop-down from properties.
		/// </summary>
		protected void AutoSizeDropDown()
		{
			if (m_dropDownHost != null)
			{
				m_dropDownHost.Margin = m_dropDownHost.Padding = new Padding(0);

				switch (DropDownSizeMode)
				{
					case SizeMode.UseComboSize:
						DropDownContainer.Size = new Size(Width, m_sizeCombo.Height);
						break;

					case SizeMode.UseControlSize:
						DropDownContainer.Size = new Size(m_sizeOriginal.Width, m_sizeOriginal.Height);
						break;

					case SizeMode.UseDropDownSize:
						DropDownContainer.Size = m_sizeCombo;
						break;
				}
			}
		}

		/// <summary>
		/// Assigns control to custom drop-down area of combo box.
		/// </summary>
		/// <param name="control">Control to be used as drop-down. Please note that this control must not be contained elsewhere.</param>
		protected virtual void AssignControl(Control control)
		{
			// If specified control is different then...
			if (control != DropDownControl)
			{
				// Preserve original container size.
				m_sizeOriginal = control.Size;

				// Reference the user-specified drop down control.
				m_dropDownCtrl = control;
				UpdateDropDownHost(control);
			}
		}

		/// <summary>
		/// Updates drop-down control host.
		/// </summary>
		/// <param name="control">Control being used as drop-down.</param>
		protected void UpdateDropDownHost(Control control)
		{
			// Clear previous control.
			m_dropDown.Items.Clear();

			Size prevControlSize = control.Size;

			// A new drop-down container will be required to store the newly specified
			// control. If no container is specified, this will be equal to the newly
			// specified control!
			if (DropDownContainer != null && DropDownContainer != DropDownControl)
			{
				m_dropDownCnt.Dispose();
				m_dropDownCnt = null;
			}
			m_dropDownCnt = PrepareDropDown(control);

			// A new drop-down host is required to accomodate the newly specified control.
			if (m_dropDownHost != null)
			{
				m_dropDownHost.Dispose();
				m_dropDownHost = null;
			}
			m_dropDownHost = new ToolStripControlHost(DropDownContainer);
			m_dropDownHost.LostFocus += new EventHandler(m_dropDown_LostFocus);
			m_dropDownHost.AutoSize = false;
			m_dropDown.Items.Add(m_dropDownHost);

			// Calculate required drop-down size.
			IDropDownContainer dropDownCnt = DropDownContainer as IDropDownContainer;
			if (dropDownCnt != null)
				m_sizeOriginal = dropDownCnt.CalculateContainerSize(prevControlSize);
			else
				m_sizeOriginal = prevControlSize;

			AutoSizeDropDown();
		}

		#endregion

		#region Win32 message handlers

		public const uint WM_COMMAND = 0x0111;
		public const uint WM_USER = 0x0400;
		public const uint WM_REFLECT = WM_USER + 0x1C00;
		public const uint WM_LBUTTONDOWN = 0x0201;

		public const uint CBN_DROPDOWN = 7;
		public const uint CBN_CLOSEUP = 8;

		public static uint HIWORD(int n)
		{
			return (uint)(n >> 16) & 0xffff;
		}

		public override bool PreProcessMessage(ref Message m)
		{
			if (m.Msg == (WM_REFLECT + WM_COMMAND))
			{
				if (HIWORD((int)m.WParam) == CBN_DROPDOWN)
					return false;
			}
			return base.PreProcessMessage(ref m);
		}

		private static DateTime m_sShowTime = DateTime.Now;

		private void AutoDropDown()
		{
			if (m_dropDown != null && m_dropDown.Visible)
				HideDropDown();
			else if ((DateTime.Now - m_lastHideTime).Milliseconds > 50)
				ShowDropDown();
		}

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == WM_LBUTTONDOWN)
			{
				AutoDropDown();
				return;
			}

			if (m.Msg == (WM_REFLECT + WM_COMMAND))
			{
				switch (HIWORD((int)m.WParam))
				{
					case CBN_DROPDOWN:
						AutoDropDown();
						return;

					case CBN_CLOSEUP:
						if ((DateTime.Now - m_sShowTime).Seconds > 1)
							HideDropDown();
						return;
				}
			}

			base.WndProc(ref m);
		}

		#endregion

		#region Enumerations

		public enum SizeMode
		{
			UseComboSize,
			UseControlSize,
			UseDropDownSize,
		}

		#endregion

		#region Properties

		/// <summary>
		/// Drop-down container. Equals drop-down control when no container is specified.
		/// </summary>
		[Browsable(false)]
		public Control DropDownContainer
		{
			get { return this.m_dropDownCnt; }
		}

		/// <summary>
		/// Actual drop-down control itself.
		/// </summary>
		[Browsable(false)]
		public Control DropDownControl
		{
			get { return m_dropDownCtrl; }
			set { AssignControl(value); }
		}

		/// <summary>
		/// Indicates if drop-down is currently shown.
		/// </summary>
		[Browsable(false)]
		public bool IsDroppedDown
		{
			get { return this.m_bDroppedDown && DropDownContainer.Visible; }
		}

		/// <summary>
		/// Indicates if drop-down is resizable.
		/// </summary>
		[Category("Custom Drop-Down"), Description("Indicates if drop-down is resizable.")]
		public bool AllowResizeDropDown
		{
			get { return this.m_bIsResizable; }
			set
			{
				if (value != this.m_bIsResizable)
				{
					this.m_bIsResizable = value;
					if (m_dropDownHost != null)
						UpdateDropDownHost(DropDownControl);
				}
			}
		}

		/// <summary>
		/// Indicates current sizing mode.
		/// </summary>
		[Category("Custom Drop-Down"), Description("Indicates current sizing mode."), DefaultValue(SizeMode.UseComboSize)]
		public SizeMode DropDownSizeMode
		{
			get { return this.m_sizeMode; }
			set
			{
				if (value != this.m_sizeMode)
				{
					this.m_sizeMode = value;
					AutoSizeDropDown();
				}
			}
		}

		[Category("Custom Drop-Down")]
		public Size DropSize
		{
			get { return m_sizeCombo; }
			set
			{
				m_sizeCombo = value;
				if (DropDownSizeMode == SizeMode.UseDropDownSize)
					AutoSizeDropDown();
			}
		}

		[Category("Custom Drop-Down"), Browsable(false)]
		public Size ControlSize
		{
			get { return m_sizeOriginal; }
			set
			{
				m_sizeOriginal = value;
				if (DropDownSizeMode == SizeMode.UseControlSize)
					AutoSizeDropDown();
			}
		}

		#endregion

		#region Hide some unwanted properties

		[Browsable(false)]
		public new ObjectCollection Items
		{
			get { return base.Items; }
		}

		[Browsable(false)]
		public new int ItemHeight
		{
			get { return base.ItemHeight; }
			set { base.ItemHeight = value; }
		}

		#endregion

		#region Attributes

		/// <summary>
		/// Drop-down control host.
		/// </summary>
		ToolStripControlHost m_dropDownHost;
		/// <summary>
		/// Toolstrip drop-down container.
		/// </summary>
		ToolStripDropDown m_dropDown = new ToolStripDropDown();
		/// <summary>
		/// Drop-down container. Equals drop-down control when no container is specified.
		/// </summary>
		Control m_dropDownCnt;
		/// <summary>
		/// Actual drop-down control itself.
		/// </summary>
		Control m_dropDownCtrl;
		/// <summary>
		/// Indicates if drop-down is currently shown.
		/// </summary>
		bool m_bDroppedDown = false;
		/// <summary>
		/// Indicates current sizing mode.
		/// </summary>
		SizeMode m_sizeMode = SizeMode.UseComboSize;
		/// <summary>
		/// Time drop-down was last hidden.
		/// </summary>
		DateTime m_lastHideTime = DateTime.Now;

		/// <summary>
		/// Automatic focus timer helps make sure drop-down control is focused for user
		/// input upon drop-down.
		/// </summary>
		Timer m_timerAutoFocus;
		/// <summary>
		/// Original size of control dimensions when first assigned.
		/// </summary>
		Size m_sizeOriginal = new Size(1, 1);
		/// <summary>
		/// Original size of combo box dropdown when first assigned.
		/// </summary>
		Size m_sizeCombo;
		/// <summary>
		/// Indicates if drop-down is resizable.
		/// </summary>
		bool m_bIsResizable = true;

		#endregion
	}

	/// <summary>
	/// Interface of a drop down container.
	/// </summary>
	public interface IDropDownContainer
	{
		Size CalculateContainerSize(Size controlSize);
	}

	/// <summary>
	/// Provides resizable drop-down container for use in <c>CustomComboBox</c>.
	/// </summary>
	public class ResizableDropDownContainer : Control, IDropDownContainer
	{
		#region Construction and destruction

		public ResizableDropDownContainer(Control childControl)
		{
			if (childControl == null)
				throw new NullReferenceException();
			this.Controls.Add(childControl);
			m_ctrlChild = childControl;

			// The minimum size of this container must allow for resizable corner.
			this.MinimumSize = new Size(childControl.MinimumSize.Width, childControl.MinimumSize.Height + 18);
		}

		#endregion

		#region IDropDownContainer Members

		public Size CalculateContainerSize(Size controlSize)
		{
			controlSize.Height += 18;
			return controlSize;
		}

		#endregion

		#region Control overrides

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			// Draw resize grip.
			Rectangle rectGrip = new Rectangle(Right - 16, Bottom - 16, 16, 16);
			ControlPaint.DrawSizeGrip(e.Graphics, BackColor, rectGrip);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			// Position child control.
			ChildControl.Left = ChildControl.Top = 0;
			ChildControl.Width = DisplayRectangle.Width;
			ChildControl.Height = DisplayRectangle.Height - 18;
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button == MouseButtons.Left)
			{
				Rectangle rectGrip = CalculateGripRectangle();

				if (rectGrip.Contains(e.Location))
				{
					m_bDragGrip = Capture = true;
					UpdateCursor(e.Location);

					// Store anchor position.
					m_ptAnchor = e.Location;
				}
			}
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.Button == MouseButtons.Left)
			{
				if (IsDraggingGrip)
				{
					// Adjust container dimensions.
					Size = new Size(Width + e.X - m_ptAnchor.X, Height + e.Y - m_ptAnchor.Y);
					m_ptAnchor = e.Location;

					// Invalidate container!
					Invalidate();
				}
			}
			if (!IsDraggingGrip)
				UpdateCursor(e.Location);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			m_bDragGrip = Capture = false;
			UpdateCursor(e.Location);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);

			// Use default cursor by default.
			Cursor = Cursors.Default;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Update control cursor according to the location specified.
		/// </summary>
		/// <param name="location">Cursor location.</param>
		protected void UpdateCursor(Point location)
		{
			// Use cursor for bottom-right grip.
			Rectangle rectGrip = CalculateGripRectangle();
			if (rectGrip.Contains(location))
				Cursor = Cursors.SizeNWSE;
			else
				Cursor = Cursors.Default;
		}

		/// <summary>
		/// Calculate display rectangle of control grip handle.
		/// </summary>
		/// <returns>Grip handle rectangle.</returns>
		protected Rectangle CalculateGripRectangle()
		{
			return new Rectangle(Right - 16, Bottom - 16, 16, 16);
		}

		#endregion

		#region Properties

		/// <summary>
		/// Child control.
		/// </summary>
		[Browsable(false)]
		public Control ChildControl
		{
			get { return this.m_ctrlChild; }
		}

		/// <summary>
		/// Indicates if resizer grip is currently being dragged.
		/// </summary>
		public bool IsDraggingGrip
		{
			get { return this.m_bDragGrip; }
		}

		#endregion

		#region Attributes

		/// <summary>
		/// Child control.
		/// </summary>
		Control m_ctrlChild;

		/// <summary>
		/// Anchor position used to determine distance whilst dragging resize corner.
		/// </summary>
		Point m_ptAnchor;

		/// <summary>
		/// Indicates if resizer grip is currently being dragged.
		/// </summary>
		bool m_bDragGrip = false;

		#endregion
	}
}
