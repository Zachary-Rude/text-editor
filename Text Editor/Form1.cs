using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using Microsoft.Samples;

namespace Text_Editor
{
	public partial class Form1 : Form
	{
		string path;
		private int firstCharOnPage;
		private string currentEncoding;
		private bool openedFromDrop = false;
		TaskDialog taskDialog;
		public Form1()
		{
			InitializeComponent();
			MessageBoxManager.Yes = "&Save";
			MessageBoxManager.No = "Do&n't Save";
			TaskDialogButton saveButton = new TaskDialogButton()
			{
				ButtonId = 100,
				ButtonText = "&Save"
			};
			TaskDialogButton dontSaveButton = new TaskDialogButton()
			{
				ButtonId = 101,
				ButtonText = "Do&n't Save"
			};
			taskDialog = new TaskDialog()
			{
				WindowTitle = "Notepad.NET",
				MainInstruction = "Do you want to save ${FILE_NAME}?",
				Content = "Your changes will be lost if you don't save them.",
				CommonButtons = TaskDialogCommonButtons.Cancel,
				MainIcon = TaskDialogIcon.Warning,
				Buttons = new TaskDialogButton[] { saveButton, dontSaveButton }
			};
			mainEditor.AllowDrop = true;
			mainEditor.DragEnter += mainEditor_DragEnter;
			mainEditor.DragDrop += mainEditor_DragDrop;
			this.MinimumSize = new Size(0, 0);
			this.Icon = Properties.Resources.icon_72;
			menuItem18.Checked = Properties.Settings.Default.EnableWordWrap;
			mainEditor.WordWrap = Properties.Settings.Default.EnableWordWrap;
			mainEditor.Font = Properties.Settings.Default.Font;
			mainEditor.AutoWordSelection = false;
			mainEditor.ContextMenu = contextMenu1;
			switch (Properties.Settings.Default.RedoShortcut)
			{
				case "Ctrl+Y":
				case "Both":
					menuItem19.Shortcut = Shortcut.CtrlY;
					break;
				case "Ctrl+Shift+Z":
					menuItem19.Shortcut = Shortcut.CtrlShiftZ;
					break;
			}
			enableDisableTimer.Start();
			UpdateRecentFileList();
			path = null;
			currentEncoding = Encoding.UTF8.EncodingName;
		}

		public Form1(string fileName) : this()
		{
			if (fileName == null)
				return;

			if (!File.Exists(fileName))
			{
				DialogResult dr = MessageBox.Show(string.Format("Could not find the {0} file.\r\nDo you want to create it?", fileName), "Notepad.NET", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (dr == DialogResult.Yes)
				{
					try
					{
						File.Create(fileName).Close();
						this.Text = Path.GetFileName(fileName) + " - Notepad.NET";
						using (StreamReader sr = new StreamReader(fileName, true))
						{
							path = fileName;
							currentEncoding = Encoding.UTF8.EncodingName;
							Task<string> text = sr.ReadToEndAsync();
							mainEditor.Text = text.Result;
							this.Text = this.Text.Replace("*", "");
						}
						if (Properties.Settings.Default.SaveRecentFiles)
						{
							if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
							{
								Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
							}
							if (Properties.Settings.Default.RecentFiles.Contains(fileName))
							{
								Properties.Settings.Default.RecentFiles.Remove(fileName);
							}
							Properties.Settings.Default.RecentFiles.Insert(0, fileName);
							Properties.Settings.Default.Save();
						}
						UpdateRecentFileList();
					}
					catch (Exception e)
					{
						MessageBox.Show(e.Message, "Cannot create file", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				else
				{
					return;
				}
			}
			try
			{
				this.Text = Path.GetFileName(fileName) + " - Notepad.NET";
				using (StreamReader sr = new StreamReader(fileName))
				{
					path = fileName;
					currentEncoding = GetEncoding(fileName);
					Task<string> text = sr.ReadToEndAsync();
					mainEditor.Text = text.Result;
					this.Text = this.Text.Replace("*", "");
				}
				if (Properties.Settings.Default.SaveRecentFiles)
				{
					if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
					{
						Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
					}
					if (Properties.Settings.Default.RecentFiles.Contains(fileName))
					{
						Properties.Settings.Default.RecentFiles.Remove(fileName);
					}
					Properties.Settings.Default.RecentFiles.Insert(0, fileName);
					Properties.Settings.Default.Save();
				}
				UpdateRecentFileList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Cannot open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public string GetEncoding(string filename)
		{
			byte[] bytes = File.ReadAllBytes(filename);
			Encoding encoding = null;
			string text = null;
			// Test UTF8 with BOM. This check can easily be copied and adapted
			// to detect many other encodings that use BOMs.
			UTF8Encoding encUtf8Bom = new UTF8Encoding(true, true);
			bool couldBeUtf8 = true;
			byte[] preamble = encUtf8Bom.GetPreamble();
			int prLen = preamble.Length;
			if (bytes.Length >= prLen && preamble.SequenceEqual(bytes.Take(prLen)))
			{
				// UTF8 BOM found; use encUtf8Bom to decode.
				try
				{
					// Seems that despite being an encoding with preamble,
					// it doesn't actually skip said preamble when decoding...
					text = encUtf8Bom.GetString(bytes, prLen, bytes.Length - prLen);
					encoding = encUtf8Bom;
				}
				catch (ArgumentException)
				{
					// Confirmed as not UTF-8!
					couldBeUtf8 = false;
				}
			}
			// use boolean to skip this if it's already confirmed as incorrect UTF-8 decoding.
			if (couldBeUtf8 && encoding == null)
			{
				// test UTF-8 on strict encoding rules. Note that on pure ASCII this will
				// succeed as well, since valid ASCII is automatically valid UTF-8.
				UTF8Encoding encUtf8NoBom = new UTF8Encoding(false, true);
				try
				{
					text = encUtf8NoBom.GetString(bytes);
					encoding = encUtf8NoBom;
				}
				catch (ArgumentException)
				{
					// Confirmed as not UTF-8!
				}
			}
			// fall back to default ANSI encoding.
			if (encoding == null)
			{
				encoding = Encoding.GetEncoding(1252);
				text = encoding.GetString(bytes);
			}

			return encoding.EncodingName;
		}

		private void UpdateRecentFileList()
		{
			if (menuItem40.MenuItems.Count > 0)
			{
				menuItem40.MenuItems.Clear();
				if (Properties.Settings.Default.RecentFiles.Count > 0)
				{
					int index = 0;
					var uniques = Properties.Settings.Default.RecentFiles.Cast<IEnumerable>();
					var unique = uniques.Distinct();
					foreach (string fldr in unique)
					{
						if (!string.IsNullOrEmpty(fldr) || !string.IsNullOrWhiteSpace(fldr))
						{
							index++;
							MenuItem item = new MenuItem(fldr);
							item.Click += recentFile_Click;
							item.Index = index;
							menuItem40.MenuItems.Add(item);
						}
					}
					MenuItem separator = new MenuItem("-");
					menuItem40.MenuItems.Add(separator);
				}
			}
			menuItem40.MenuItems.Add(menuClear);
			menuClear.Enabled = Properties.Settings.Default.RecentFiles.Count != 0;
		}

		private void recentFile_Click(object sender, EventArgs e)
		{
			if (this.Text.StartsWith("*"))
			{
				taskDialog.MainInstruction = $"Do you want to save {Path.GetFileName(path) ?? "Untitled"}";
				DialogResult dr = (DialogResult)taskDialog.Show(this, out _, out _);
				if (dr == (DialogResult)100)
				{
					menuItem5.PerformClick();
				}
				else if (dr == DialogResult.Cancel)
				{
					return;
				}
			}
			try
			{
				using (StreamReader sr = new StreamReader(((MenuItem)sender).Text))
				{
					path = ((MenuItem)sender).Text;
					Task<string> text = sr.ReadToEndAsync();
					mainEditor.Text = text.Result;
					currentEncoding = GetEncoding(((MenuItem)sender).Text);
					this.Text = this.Text.Replace("*", "");
				}
				this.Text = Path.GetFileName(((MenuItem)sender).Text) + " - Notepad.NET";
				if (Properties.Settings.Default.SaveRecentFiles)
				{
					if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
					{
						Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
					}
					if (Properties.Settings.Default.RecentFiles.Contains(((MenuItem)sender).Text))
					{
						Properties.Settings.Default.RecentFiles.Remove(((MenuItem)sender).Text);
					}
					Properties.Settings.Default.RecentFiles.Insert(0, ((MenuItem)sender).Text);
					Properties.Settings.Default.Save();
				}
				UpdateRecentFileList();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Cannot open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
				if (!File.Exists(((MenuItem)sender).Text))
				{
					Properties.Settings.Default.RecentFiles.Remove(((MenuItem)sender).Text);
					Properties.Settings.Default.Save();
					UpdateRecentFileList();
				}
			}
		}

		private void menuClear_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.RecentFiles.Clear();
			Properties.Settings.Default.Save();
			UpdateRecentFileList();
		}

		public static void QuickReplace(RichTextBox rtb, string word, string word2)
		{
			rtb.Text = rtb.Text.Replace(word, word2);
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			if (this.Text.StartsWith("*"))
			{
				taskDialog.MainInstruction = $"Do you want to save {Path.GetFileName(path) ?? "Untitled"}";
				DialogResult dr = (DialogResult)taskDialog.Show(this, out _, out _);
				if (dr == (DialogResult)100)
				{
					menuItem5.PerformClick();
				}
				else if (dr == DialogResult.Cancel)
				{
					return;
				}
			}
			this.Text = "Untitled - Notepad.NET";
			path = null;
			mainEditor.Clear();
			this.Text = this.Text.Replace("*", "");
			currentEncoding = Encoding.UTF8.EncodingName;
		}

		private void menuItem3_Click(object sender, EventArgs e)
		{
			Process.Start(Application.ExecutablePath);
		}

		private void menuItem4_Click(object sender, EventArgs e)
		{
			if (this.Text.StartsWith("*"))
			{
				taskDialog.MainInstruction = $"Do you want to save {Path.GetFileName(path) ?? "Untitled"}";
				DialogResult dr = (DialogResult)taskDialog.Show(this, out _, out _);
				if (dr == (DialogResult)100)
				{
					menuItem5.PerformClick();
				}
				else if (dr == DialogResult.Cancel)
				{
					return;
				}
			}
			mainEditor.TextChanged -= mainEditor_TextChanged;
			using (OpenFileDialog ofd = new OpenFileDialog() { DefaultExt = ".txt", Filter = "Text Files|*.txt|All Files|*.*", ValidateNames = true, Multiselect = false })
			{
				if (ofd.ShowDialog() == DialogResult.OK)
				{
					try
					{
						this.Text = Path.GetFileName(ofd.FileName) + " - Notepad.NET";
						using (StreamReader sr = new StreamReader(ofd.FileName))
						{
							path = ofd.FileName;
							currentEncoding = GetEncoding(ofd.FileName);
							Task<string> text = sr.ReadToEndAsync();
							mainEditor.Text = text.Result;
						}
						if (Properties.Settings.Default.SaveRecentFiles)
						{
							if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
							{
								Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
							}
							if (Properties.Settings.Default.RecentFiles.Contains(ofd.FileName))
							{
								Properties.Settings.Default.RecentFiles.Remove(ofd.FileName);
							}
							Properties.Settings.Default.RecentFiles.Insert(0, ofd.FileName);
							Properties.Settings.Default.Save();
						}
						UpdateRecentFileList();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Cannot open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			mainEditor.TextChanged += mainEditor_TextChanged;
		}

		private async void menuItem5_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(path))
			{
				using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Files|*.txt|All Files|*.*", ValidateNames = true, FileName = path ?? "Untitled.txt" })
				{
					if (sfd.ShowDialog() == DialogResult.OK)
					{
						try
						{
							path = sfd.FileName;
							using (StreamWriter sw = new StreamWriter(sfd.FileName))
							{
								await sw.WriteAsync(mainEditor.Text.Replace("\n", "\r\n")); // Write data to text file
								this.Text = Path.GetFileName(sfd.FileName) + " - Notepad.NET";
							}
							if (Properties.Settings.Default.SaveRecentFiles)
							{
								if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
								{
									Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
								}
								if (Properties.Settings.Default.RecentFiles.Contains(sfd.FileName))
								{
									Properties.Settings.Default.RecentFiles.Remove(sfd.FileName);
								}
								Properties.Settings.Default.RecentFiles.Insert(0, sfd.FileName);
								Properties.Settings.Default.Save();
							}
							UpdateRecentFileList();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.Message, "Cannot save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
				}
			}
			else
			{
				try
				{
					using (StreamWriter sw = new StreamWriter(path))
					{
						await sw.WriteAsync(mainEditor.Text.Replace("\n", "\r\n")); // Write data to text file
						var regex = new Regex(Regex.Escape("*"));
						this.Text = regex.Replace(this.Text, "", 1);
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Cannot save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private async void menuItem6_Click(object sender, EventArgs e)
		{
			using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Files|*.txt|All Files|*.*", ValidateNames = true, FileName = path ?? "Untitled.txt" })
			{
				if (sfd.ShowDialog() == DialogResult.OK)
				{
					try
					{
						path = sfd.FileName;
						using (StreamWriter sw = new StreamWriter(sfd.FileName))
						{
							await sw.WriteAsync(mainEditor.Text.Replace("\n", "\r\n")); // Write data to text file
							this.Text = Path.GetFileName(sfd.FileName) + " - Notepad.NET";
						}
						if (Properties.Settings.Default.SaveRecentFiles)
						{
							if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
							{
								Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
							}
							if (Properties.Settings.Default.RecentFiles.Contains(sfd.FileName))
							{
								Properties.Settings.Default.RecentFiles.Remove(sfd.FileName);
							}
							Properties.Settings.Default.RecentFiles.Insert(0, sfd.FileName);
							Properties.Settings.Default.Save();
						}
						UpdateRecentFileList();
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Cannot save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void menuItem8_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void menuItem10_Click(object sender, EventArgs e)
		{
			mainEditor.Undo();
		}

		private void menuItem12_Click(object sender, EventArgs e)
		{
			mainEditor.Cut();
		}

		private void menuItem13_Click(object sender, EventArgs e)
		{
			mainEditor.Copy();
		}

		private void menuItem14_Click(object sender, EventArgs e)
		{
			mainEditor.Paste(DataFormats.GetFormat("Text"));
		}

		private void menuItem16_Click(object sender, EventArgs e)
		{
			mainEditor.SelectAll();
		}

		private void menuItem18_Click(object sender, EventArgs e)
		{
			menuItem18.Checked = !menuItem18.Checked;
			Properties.Settings.Default.EnableWordWrap = menuItem18.Checked;
			Properties.Settings.Default.Save();
			mainEditor.WordWrap = Properties.Settings.Default.EnableWordWrap;
		}

		private void menuItem20_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;
		}

		private void menuItem21_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void menuItem22_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void menuItem24_Click(object sender, EventArgs e)
		{
			AboutForm aboutForm = new AboutForm();
			aboutForm.ShowDialog();
		}

		private void menuItem26_Click(object sender, EventArgs e)
		{
			new SearchForm().Show(this);
		}

		private void menuItem27_Click(object sender, EventArgs e)
		{
			//This will give us the full name path of the executable file:
			//i.e. C:\Program Files\MyApplication\MyApplication.exe
			string strExeFilePath = Assembly.GetExecutingAssembly().Location;
			//This will strip just the working path name:
			//C:\Program Files\MyApplication
			string strWorkPath = Path.GetDirectoryName(strExeFilePath);
			Help.ShowHelp(this, Path.Combine(strWorkPath, "textedit-help.chm"));
		}

		private void mainEditor_TextChanged(object sender, EventArgs e)
		{
			if (openedFromDrop)
			{
				openedFromDrop = false;
				return;
			}
			if (!this.Text.StartsWith("*"))
			{
				this.Text = "*" + this.Text;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (this.Text.StartsWith("*"))
			{

				switch (e.CloseReason)
				{
					case CloseReason.UserClosing:
					case CloseReason.WindowsShutDown:
						taskDialog.MainInstruction = $"Do you want to save {Path.GetFileName(path) ?? "Untitled"}";
						DialogResult dr = (DialogResult)taskDialog.Show(this, out _, out _);
						if (dr == (DialogResult)100)
						{
							menuItem5.PerformClick();
						}
						else if (dr == DialogResult.Cancel)
						{
							e.Cancel = true;
						}
						break;
				}
			}
			enableDisableTimer.Stop();
		}

		private void menuItem29_Click(object sender, EventArgs e)
		{
			using (FontDialog fd = new FontDialog() { Font = Properties.Settings.Default.Font })
			{
				if (fd.ShowDialog() == DialogResult.OK)
				{
					Properties.Settings.Default.Font = fd.Font;
					Properties.Settings.Default.Save();
					mainEditor.TextChanged -= mainEditor_TextChanged;
					mainEditor.Font = Properties.Settings.Default.Font;
					mainEditor.TextChanged += mainEditor_TextChanged;
					//if (!this.Text.StartsWith("*"))
					//{
					//    this.Text = this.Text.Replace("*", "");
					//}
				}
			}
		}

		private void menuItem19_Click(object sender, EventArgs e)
		{
			mainEditor.Redo();
		}

		private void menuItem30_Click(object sender, EventArgs e)
		{
			new ReplaceForm().Show(this);
		}

		private void menuItem32_Click(object sender, EventArgs e)
		{
			mainEditor.ZoomFactor += 0.1F;
		}

		private void menuItem33_Click(object sender, EventArgs e)
		{
			mainEditor.ZoomFactor -= 0.1F;
		}

		private void menuItem34_Click(object sender, EventArgs e)
		{
			mainEditor.ZoomFactor = 1.0F;
		}

		private void enableDisableTimer_Tick(object sender, EventArgs e)
		{
			// Enable the menu items for undo and redo only when you are able to undo and redo (gray them out otherwise)
			menuItem10.Enabled = mainEditor.CanUndo;
			menuItem19.Enabled = mainEditor.CanRedo;
			menuItem20.Enabled = menuItem10.Enabled;
			menuItem21.Enabled = menuItem19.Enabled;

			// Disable the menu items for cut, copy, and delete if no text is selected
			menuItem12.Enabled = mainEditor.SelectedText.Length > 0;
			menuItem13.Enabled = menuItem12.Enabled;
			menuItem35.Enabled = menuItem12.Enabled;
			menuItem36.Enabled = menuItem12.Enabled;
			menuItem49.Enabled = menuItem12.Enabled;
			menuItem50.Enabled = menuItem12.Enabled;

			// Disable the menu item for zooming in if the user has reached the maximum zoom level
			menuItem32.Enabled = mainEditor.ZoomFactor < 5.0F;

			// Disable the menu item for zooming out if the user has reached the minimum zoom level
			menuItem33.Enabled = mainEditor.ZoomFactor > 1.0F;

			// Automatically show/hide the status bar based on the user's preferences
			statusBar1.Visible = Properties.Settings.Default.ShowStatusBar;
			menuItem48.Checked = Properties.Settings.Default.ShowStatusBar;

			// Set the main RichTextBox's dock style to Fill if the status bar is visible; otherwise, disable the docking for it and instead set the anchor to all 4 sides
			if (statusBar1.Visible)
			{
				mainEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
				mainEditor.Dock = DockStyle.None;
			}
			else
			{
				mainEditor.Anchor = AnchorStyles.Top | AnchorStyles.Left;
				mainEditor.Dock = DockStyle.Fill;
			}

			#region Status Bar
			sbpLineCol.Text = string.Format("Ln {0}, Col {1}", mainEditor.CurrentLine, mainEditor.CurrentColumn);
			sbpZoomPercent.Text = string.Format("{0}%", (int)(mainEditor.ZoomFactor * 100));
			sbpTextEncoding.Text = currentEncoding;
			#endregion
		}

		private void menuItem43_Click(object sender, EventArgs e)
		{
			new SettingsForm().ShowDialog();
			switch (Properties.Settings.Default.RedoShortcut)
			{
				case "Ctrl+Y":
				case "Both":
					menuItem19.Shortcut = Shortcut.CtrlY;
					break;
				case "Ctrl+Shift+Z":
					menuItem19.Shortcut = Shortcut.CtrlShiftZ;
					break;
			}
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			switch (keyData)
			{
				case Keys.Control | Keys.Shift | Keys.Z:
					if ((Properties.Settings.Default.RedoShortcut == "Both" || Properties.Settings.Default.RedoShortcut == "Ctrl+Shift+Z") && mainEditor.CanRedo)
					{
						mainEditor.Redo();
					}
					return true;
				case Keys.Control | Keys.Y:
					if (Properties.Settings.Default.RedoShortcut == "Ctrl+Shift+Z")
					{
						return true;
					}
					return false;
				case Keys.Shift | Keys.Insert:
					mainEditor.Paste(DataFormats.GetFormat("Text"));
					return true;
				case Keys.Control | Keys.Oemplus:
					if (mainEditor.ZoomFactor < 5.0F)
						mainEditor.ZoomFactor += 0.1F;
					return true;
				case Keys.Control | Keys.OemMinus:
					if (mainEditor.ZoomFactor > 1.0F)
						mainEditor.ZoomFactor -= 0.1F;
					return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			// Start at the beginning of the text
			firstCharOnPage = 0;
		}

		private void printDocument1_EndPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			// Clean up cached information
			mainEditor.FormatRangeDone();
		}

		private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			firstCharOnPage = mainEditor.FormatRange(false, e, firstCharOnPage, mainEditor.TextLength);
			// check if there are more pages to print
			if (firstCharOnPage < mainEditor.TextLength)
				e.HasMorePages = true;
			else
				e.HasMorePages = false;
		}

		private void menuItem46_Click(object sender, EventArgs e)
		{
			using (PrintDialog print = new PrintDialog() { Document = printDocument1 })
			{
				print.ShowDialog();
			}
		}

		private void menuItem45_Click(object sender, EventArgs e)
		{
			using (PageSetupDialog pageSetup = new PageSetupDialog() { Document = printDocument1 })
			{
				pageSetup.ShowDialog();
			}
		}

		private void menuItem48_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.ShowStatusBar ^= true;
		}

		private void Form1_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		}

		private void Form1_DragDrop(object sender, DragEventArgs e)
		{
			mainEditor.TextChanged -= mainEditor_TextChanged;
			object filename = e.Data.GetData("FileDrop");
			if (this.Text.StartsWith("*"))
			{
				taskDialog.MainInstruction = $"Do you want to save {Path.GetFileName(path) ?? "Untitled"}";
				DialogResult dr = (DialogResult)taskDialog.Show(this, out _, out _);
				if (dr == (DialogResult)100)
				{
					menuItem5.PerformClick();
				}
				else if (dr == DialogResult.Cancel)
				{
					return;
				}
			}
			if (filename != null)
			{
				if (filename is string[] list && !string.IsNullOrWhiteSpace(list[0]))
				{
					openedFromDrop = true;
					path = list[0];
					mainEditor.LoadFile(list[0], RichTextBoxStreamType.PlainText);
					this.Text = this.Text.Replace("*", "");
					this.Text = string.Format("{0} - Notepad.NET", Path.GetFileName(list[0]));
					if (Properties.Settings.Default.SaveRecentFiles)
					{
						if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
						{
							Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
						}
						if (Properties.Settings.Default.RecentFiles.Contains(list[0]))
						{
							Properties.Settings.Default.RecentFiles.Remove(list[0]);
						}
						Properties.Settings.Default.RecentFiles.Insert(0, list[0]);
						Properties.Settings.Default.Save();
					}
					UpdateRecentFileList();
				}
			}
			mainEditor.TextChanged += mainEditor_TextChanged;
		}

		private void mainEditor_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
		}

		private void mainEditor_DragDrop(object sender, DragEventArgs e)
		{
			mainEditor.TextChanged -= mainEditor_TextChanged;
			object filename = e.Data.GetData("FileDrop");
			if (filename != null)
			{
				if (filename is string[] list && !string.IsNullOrWhiteSpace(list[0]))
				{
					openedFromDrop = true;
					path = list[0];
					mainEditor.Clear();
					mainEditor.LoadFile(list[0], RichTextBoxStreamType.PlainText);
					this.Text = this.Text.Replace("*", "");
					this.Text = string.Format("{0} - Notepad.NET", Path.GetFileName(list[0]));
					if (Properties.Settings.Default.SaveRecentFiles)
					{
						if (Properties.Settings.Default.RecentFiles.Count > Properties.Settings.Default.MaxRecentFiles - 1)
						{
							Properties.Settings.Default.RecentFiles.RemoveAt(Properties.Settings.Default.MaxRecentFiles - 1);
						}
						if (Properties.Settings.Default.RecentFiles.Contains(list[0]))
						{
							Properties.Settings.Default.RecentFiles.Remove(list[0]);
						}
						Properties.Settings.Default.RecentFiles.Insert(0, list[0]);
						Properties.Settings.Default.Save();
					}
					UpdateRecentFileList();
				}
			}
			mainEditor.TextChanged += mainEditor_TextChanged;
		}

		private void menuItem49_Click(object sender, EventArgs e)
		{
			mainEditor.SelectedText = "";
		}

		private void menuItem51_Click(object sender, EventArgs e)
		{
			string currentDate = DateTime.Now.ToString("h:mm tt M/dd/yyyy");
			int selectionIndex = mainEditor.SelectionStart;
			if (mainEditor.SelectionLength != 0)
			{
				mainEditor.SelectedText = currentDate;
			}
			else
			{
				mainEditor.Text = mainEditor.Text.Insert(selectionIndex, currentDate);
			}
			mainEditor.SelectionStart = selectionIndex + currentDate.Length;
		}
	}
}
