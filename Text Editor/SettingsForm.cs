using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text_Editor
{
	public partial class SettingsForm : Form
	{
		public SettingsForm()
		{
			InitializeComponent();
			chkSaveRecentFiles.Checked = Properties.Settings.Default.SaveRecentFiles;
			chkRecentAutoLoad.Checked = Properties.Settings.Default.SaveRecentFiles;
			numRecentFilesMax.Value = Properties.Settings.Default.MaxRecentFiles;
			switch (Properties.Settings.Default.RedoShortcut)
			{
				case "Ctrl+Y":
					rbCtrlY.Checked = true;
					break;
				case "Ctrl+Shift+Z":
					rbCtrlShiftZ.Checked = true;
					break;
				case "Both":
					rbAcceptBoth.Checked = true;
					break;
			}
		}

		private void chkSaveRecentFiles_CheckedChanged(object sender, EventArgs e)
		{
			lblMaxRecent.Enabled = chkSaveRecentFiles.Checked;
			numRecentFilesMax.Enabled = chkSaveRecentFiles.Checked;
		}

		private void RedoRadioButton_CheckedChanged(object sender, EventArgs e)
		{
			// Check of the raiser of the event is a checked Checkbox.
			// Of course we also need to to cast it first.
			if (((RadioButton)sender).Checked)
			{
				// This is the correct control.
				redoShortcutValue = ((RadioButton)sender).Text;
			}
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			switch (redoShortcutValue)
			{
				case "Accept both":
					Properties.Settings.Default.RedoShortcut = "Both";
					break;
				default:
					Properties.Settings.Default.RedoShortcut = redoShortcutValue;
					break;
			}
			Properties.Settings.Default.MaxRecentFiles = (int)numRecentFilesMax.Value;
			Properties.Settings.Default.SaveRecentFiles = chkSaveRecentFiles.Checked;
			Properties.Settings.Default.Save();
			this.Close();
		}

		private void chkRecentAutoLoad_CheckedChanged(object sender, EventArgs e)
		{
			Properties.Settings.Default.AutoLoadRecentFiles = chkRecentAutoLoad.Checked;
		}
		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private string redoShortcutValue;
    }
}
