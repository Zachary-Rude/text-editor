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
        }
        private void btnFindNext_Click(object sender, EventArgs e)
        {

            int StartPosition = ((Form1)this.ParentForm).mainEditor.SelectionStart + 2;
            CompareMethod SearchType = chkMatchCase.Checked ? CompareMethod.Binary : CompareMethod.Text;

            StartPosition = Strings.InStr(StartPosition, ((Form1)this.ParentForm).mainEditor.Text, txtSearchTerm.Text, SearchType);

            if (StartPosition == 0)
            {
                MessageBox.Show("Cannot find \"" + txtSearchTerm.Text.ToString() + "\"", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                return;
            }

            ((Form1)this.ParentForm).mainEditor.Select(StartPosition - 1, txtSearchTerm.Text.Length);
            ((Form1)this.ParentForm).mainEditor.ScrollToCaret();
            ((Form1)this.ParentForm).Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
