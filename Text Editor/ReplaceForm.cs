using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Text_Editor
{
	public partial class ReplaceForm : Form
	{
		public ReplaceForm()
		{
			InitializeComponent();
			searchStart = 0;
			timer1.Start();
		}
		private void btnFindNext_Click(object sender, EventArgs e)
		{
			searchStart = ((Form1)this.Owner).mainEditor.Find(txtSearchTerm.Text, searchStart, (chkMatchCase.Checked ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None) | (chkWholeWord.Checked ? RichTextBoxFinds.WholeWord : RichTextBoxFinds.None));

			if (searchStart == -1)
			{
				MessageBox.Show("Cannot find \"" + txtSearchTerm.Text + "\"", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				searchStart = 0;
				return;
			}
			((Form1)this.Owner).mainEditor.Select(searchStart, txtSearchTerm.Text.Length);
			((Form1)this.Owner).mainEditor.ScrollToCaret();
			((Form1)this.Owner).Focus();
			searchStart += txtSearchTerm.Text.Length + 1;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void txtSearchTerm_TextChanged(object sender, EventArgs e)
		{
			searchStart = 0;
		}

		private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
		{
			searchStart = 0;
		}

		private void chkWholeWord_CheckedChanged(object sender, EventArgs e)
		{
			searchStart = 0;
		}

		private void btnReplace_Click(object sender, EventArgs e)
		{
			searchStart = ((Form1)this.Owner).mainEditor.Find(txtSearchTerm.Text, searchStart, chkMatchCase.Checked ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None);

			if (searchStart == -1)
			{
				MessageBox.Show("Cannot find \"" + txtSearchTerm.Text + "\"", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				searchStart = 0;
				return;
			}
			((Form1)this.Owner).mainEditor.Select(searchStart, txtSearchTerm.Text.Length);
			((Form1)this.Owner).mainEditor.ScrollToCaret();
			if (((Form1)this.Owner).mainEditor.SelectedText.Length != 0)
			{
				((Form1)this.Owner).mainEditor.SelectedText = txtReplacementText.Text;
			}
			((Form1)this.Owner).Focus();
			searchStart += txtReplacementText.Text.Length + 1;
		}

		private void btnReplaceAll_Click(object sender, EventArgs e)
		{
			((Form1)this.Owner).mainEditor.Text = ((Form1)this.Owner).mainEditor.Text.Replace(txtSearchTerm.Text, txtReplacementText.Text);
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			btnFindNext.Enabled = !string.IsNullOrEmpty(txtSearchTerm.Text);
			btnReplace.Enabled = btnFindNext.Enabled;
			btnReplaceAll.Enabled = btnFindNext.Enabled;
		}

		private int searchStart;
	}
}
