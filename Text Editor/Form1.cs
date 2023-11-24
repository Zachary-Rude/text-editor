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

namespace Text_Editor
{
    public partial class Form1 : Form
    {
        string path;
        private int firstCharOnPage;

        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = new Size(0, 0);
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
                        using (StreamReader sr = new StreamReader(fileName))
                        {
                            path = fileName;
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
                    Task<string> text = sr.ReadToEndAsync();
                    mainEditor.Text = text.Result;
                    this.Text = this.Text.Replace("*", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cannot open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                this.Text = Path.GetFileName(((MenuItem)sender).Text) + " - Notepad.NET";
                using (StreamReader sr = new StreamReader(((MenuItem)sender).Text))
                {
                    path = ((MenuItem)sender).Text;
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
                DialogResult dr = MessageBox.Show(Path.GetFileName(path) + " has unsaved changes.\r\nDo you want to save them?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (dr == DialogResult.Yes)
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
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Process.Start(Application.ExecutablePath);
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
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
                                await sw.WriteLineAsync(mainEditor.Text.Replace("\n", "\r\n"));//Write data to text file
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
                        await sw.WriteLineAsync(mainEditor.Text.Replace("\n", "\r\n"));//Write data to text file
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
                            await sw.WriteLineAsync(mainEditor.Text); // Write data to text file
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
                        string fileNameOld = this.Text.Replace(" - Notepad.NET", "");
                        var regex = new Regex(Regex.Escape("*"));
                        string fileName = regex.Replace(fileNameOld, "", 1);
                        DialogResult dr = MessageBox.Show(fileName + " has unsaved changes.\r\nDo you want to save them?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (dr == DialogResult.Yes)
                        {
                            this.menuItem5.PerformClick();
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
            mainEditor.ZoomFactor += 1.0F;
        }

        private void menuItem33_Click(object sender, EventArgs e)
        {
            mainEditor.ZoomFactor -= 1.0F;
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

            // Disable the menu items for cut and copy if no text is selected
            menuItem12.Enabled = mainEditor.SelectedText.Length > 0;
            menuItem13.Enabled = menuItem12.Enabled;
            menuItem35.Enabled = menuItem12.Enabled;
            menuItem36.Enabled = menuItem12.Enabled;

            // Disable the menu item for zooming in if the user has reached the maximum zoom level
            menuItem32.Enabled = mainEditor.ZoomFactor < 63.0F;

            // Disable the menu item for zooming out if the user has reached the minimum zoom level
            menuItem33.Enabled = mainEditor.ZoomFactor > 1.0F;
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
                    if (Properties.Settings.Default.RedoShortcut == "Both" && mainEditor.CanRedo)
                    {
                        mainEditor.Redo();
                    }
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
    }
}
