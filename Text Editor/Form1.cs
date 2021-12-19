﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Text_Editor
{
    public partial class Form1 : Form
    {
        string path;
        public Form1()
        {
            InitializeComponent();
            menuItem18.Checked = Properties.Settings.Default.EnableWordWrap;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.Text = "Untitled - Text Editor";
            path = string.Empty;
            mainEditor.Clear();
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
    }
}