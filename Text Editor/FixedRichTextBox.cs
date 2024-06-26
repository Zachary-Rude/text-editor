﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Text_Editor
{
	public class FixedRichTextBox : RichTextBox
	{
		[StructLayout(LayoutKind.Sequential)]
		private struct STRUCT_RECT
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct STRUCT_CHARRANGE
		{
			public int cpMin;
			public int cpMax;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct STRUCT_FORMATRANGE
		{
			public IntPtr hdc;
			public IntPtr hdcTarget;
			public STRUCT_RECT rc;
			public STRUCT_RECT rcPage;
			public STRUCT_CHARRANGE chrg;
		}

		[DllImport("user32.dll")]
		private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

		private const int WM_USER = 0x400;
		private const int EM_FORMATRANGE = WM_USER + 57;
		private const int EM_GETCHARFORMAT = WM_USER + 58;
		private const int EM_SETCHARFORMAT = WM_USER + 68;

		/// <summary>
		/// Calculate or render the contents of our RichTextBox for printing
		/// </summary>
		/// <param name="measureOnly">If true, only the calculation is performed, otherwise the text is rendered as well</param>
		/// <param name="e">The PrintPageEventArgs object from the PrintPage event</param>
		/// <param name="charFrom">Index of first character to be printed</param>
		/// <param name="charTo">Index of last character to be printed</param>
		/// <returns> (Index of last character that fitted on the page) + 1</returns>
		public int FormatRange(bool measureOnly, PrintPageEventArgs e, int charFrom, int charTo)
		{
			// Specify which characters to print
			STRUCT_CHARRANGE cr = default;
			cr.cpMin = charFrom;
			cr.cpMax = charTo;

			// Specify the area inside page margins
			STRUCT_RECT rc = default;
			rc.top = HundredthInchToTwips(e.MarginBounds.Top);
			rc.bottom = HundredthInchToTwips(e.MarginBounds.Bottom);
			rc.left = HundredthInchToTwips(e.MarginBounds.Left);
			rc.right = HundredthInchToTwips(e.MarginBounds.Right);

			// Specify the page area
			STRUCT_RECT rcPage = default;
			rcPage.top = HundredthInchToTwips(e.PageBounds.Top);
			rcPage.bottom = HundredthInchToTwips(e.PageBounds.Bottom);
			rcPage.left = HundredthInchToTwips(e.PageBounds.Left);
			rcPage.right = HundredthInchToTwips(e.PageBounds.Right);

			// Get device context of output device
			IntPtr hdc = default;
			hdc = e.Graphics.GetHdc();

			// Fill in the FORMATRANGE structure
			STRUCT_FORMATRANGE fr = default;
			fr.chrg = cr;
			fr.hdc = hdc;
			fr.hdcTarget = hdc;
			fr.rc = rc;
			fr.rcPage = rcPage;

			// Non-Zero wParam means render, Zero means measure
			int wParam = default;
			if (measureOnly)
			{
				wParam = 0;
			}
			else
			{
				wParam = 1;
			}

			// Allocate memory for the FORMATRANGE struct and
			// copy the contents of our struct to this memory
			IntPtr lParam = default;
			lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
			Marshal.StructureToPtr(fr, lParam, false);

			// Send the actual Win32 message
			int res = 0;
			res = SendMessage(Handle, EM_FORMATRANGE, wParam, lParam);

			// Free allocated memory
			Marshal.FreeCoTaskMem(lParam);

			// and release the device context
			e.Graphics.ReleaseHdc(hdc);

			return res;
		}

		/// <summary>
		/// Convert between 1/100 inch (unit used by the .NET framework)
		/// and twips (1/1440 inch, used by Win32 API calls)
		/// </summary>
		/// <param name="n">Value in 1/100 inch</param>
		/// <returns>Value in twips</returns>
		private int HundredthInchToTwips(int n)
		{
			return Convert.ToInt32(n * 14.4);
		}

		/// <summary>
		/// Free cached data from rich edit control after printing
		/// </summary>
		public void FormatRangeDone()
		{
			IntPtr lParam = new IntPtr(0);
			SendMessage(Handle, EM_FORMATRANGE, 0, lParam);
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (!AutoWordSelection)
			{
				AutoWordSelection = true;
				AutoWordSelection = false;
			}
			this.MouseWheel += this.OnMouseWheel;
		}

		private const int WM_SCROLL = 276; // Horizontal scroll 
		private const int SB_LINELEFT = 0; // Scrolls one cell left 
		private const int SB_LINERIGHT = 1; // Scrolls one line right

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

		private void OnMouseWheel(object sender, MouseEventArgs e)
		{
			if (ModifierKeys == Keys.Shift)
			{
				var direction = e.Delta > 0 ? SB_LINELEFT : SB_LINERIGHT;

				SendMessage(this.Handle, WM_SCROLL, (IntPtr)direction, IntPtr.Zero);
			}
		}

		#region Line and Column

		public event EventHandler CursorPositionChanged;

		protected virtual void OnCursorPositionChanged(EventArgs e)
		{
			CursorPositionChanged?.Invoke(this, e);
		}

		protected override void OnSelectionChanged(EventArgs e)
		{
			if (SelectionLength == 0)
				OnCursorPositionChanged(e);
			else
				base.OnSelectionChanged(e);
		}

		[Browsable(false)]
		public int CurrentColumn
		{
			get { return CursorPosition.Column(this, SelectionStart); }
		}

		[Browsable(false)]
		public int CurrentLine
		{
			get { return CursorPosition.Line(this, SelectionStart); }
		}

		[Browsable(false)]
		public int CurrentPosition
		{
			get { return this.SelectionStart; }
		}

		[Browsable(false)]
		public int SelectionEnd
		{
			get { return SelectionStart + SelectionLength; }
		}
		#endregion
	}

	internal class CursorPosition
	{
		[DllImport("user32")]
		public static extern int GetCaretPos(ref Point lpPoint);

		private static int GetCorrection(RichTextBox e, int index)
		{
			Point pt1 = Point.Empty;
			GetCaretPos(ref pt1);
			Point pt2 = e.GetPositionFromCharIndex(index);

			if (pt1 != pt2)
				return 1;
			else
				return 0;
		}

		public static int Line(RichTextBox e, int index)
		{
			int correction = GetCorrection(e, index);
			return e.GetLineFromCharIndex(index) - correction + 1;
		}

		public static int Column(RichTextBox e, int index1)
		{
			int correction = GetCorrection(e, index1);
			Point p = e.GetPositionFromCharIndex(index1 - correction);

			if (p.X == 1)
				return 1;

			p.X = 0;
			int index2 = e.GetCharIndexFromPosition(p);

			int col = index1 - index2 + 1;

			return col;
		}
	}
}
