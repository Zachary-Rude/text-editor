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
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
            searchStart = 0;
        }
        private void btnFindNext_Click(object sender, EventArgs e)
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
            searchStart += txtSearchTerm.Text.Length + 1;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private int searchStart;

        private void txtSearchTerm_TextChanged(object sender, EventArgs e)
        {
            searchStart = 0;
        }

        private void chkMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            searchStart = 0;
        }
    }
}
