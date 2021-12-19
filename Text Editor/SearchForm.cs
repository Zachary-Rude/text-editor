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
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
            string searchString = txtSearchString.Text;
            string replaceString = txtReplaceString.Text;
        }

        public string searchString;
        public string replaceString;
    }
}
