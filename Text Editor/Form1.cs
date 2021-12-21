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

namespace Text_Editor
{
    public partial class Form1 : Form
    {
        string path;
        public Form1()
        {
            InitializeComponent();
            menuItem18.Checked = Properties.Settings.Default.EnableWordWrap;
            mainEditor.WordWrap = Properties.Settings.Default.EnableWordWrap;
        }

        public Form1(string fileName) : this()
        {
            if (fileName == null)
                return;

            if (!File.Exists(fileName))
            {
                MessageBox.Show("Invalid file name.", "Cannot open file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                this.Text = Path.GetFileName(fileName) + " - Text Editor";
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

        public static void QuickReplace(RichTextBox rtb, String word, String word2)
        {
            rtb.Text = rtb.Text.Replace(word, word2);
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Text = "Untitled - Text Editor";
            path = string.Empty;
            mainEditor.Clear();
            this.Text = this.Text.Replace("*", "");
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
        }

        private void menuItem4_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { DefaultExt = ".txt", Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        this.Text = Path.GetFileName(ofd.FileName) + " - Text Editor";
                        using (StreamReader sr = new StreamReader(ofd.FileName))
                        {
                            path = ofd.FileName;
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
            }
        }

        private async void menuItem5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(path))
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*", ValidateNames = true, FileName = "*.txt" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            path = sfd.FileName;
                            using (StreamWriter sw = new StreamWriter(sfd.FileName))
                            {
                                await sw.WriteLineAsync(mainEditor.Text);//Write data to text file
                                this.Text = Path.GetFileName(sfd.FileName) + " - Text Editor";
                            }
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
                        await sw.WriteLineAsync(mainEditor.Text);//Write data to text file
                        this.Text = this.Text.Replace("*", "");
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
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*", ValidateNames = true, FileName = "*.txt" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        path = sfd.FileName;
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            await sw.WriteLineAsync(mainEditor.Text);//Write data to text file
                            this.Text = Path.GetFileName(sfd.FileName) + " - Text Editor";
                        }
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
            if (mainEditor.CanUndo)
            {
                mainEditor.Undo();
            }
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
            mainEditor.Paste();
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
            using (SearchForm searchForm = new SearchForm())
            {
                if (searchForm.ShowDialog() == DialogResult.OK)
                {
                    QuickReplace(mainEditor, searchForm.txtSearchString.Text, searchForm.txtReplaceString.Text);
                }
            }
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
                        string fileNameOld = this.Text.Replace(" - Text Editor", "");
                        var regex = new Regex(Regex.Escape("*"));
                        string fileName = regex.Replace(fileNameOld, "", 1);
                        DialogResult dr = MessageBox.Show(fileName + " has unsaved changes. Do you want to save them?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
        }
    }
}
