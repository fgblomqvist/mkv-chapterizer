using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MKV_Chapterizer
{
    public partial class Warning : Form
    {
        public Warning()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkboxNotAgain.Checked)
            {
                Properties.Settings.Default.showMKVMergeWarning = false;
            }
            this.Close();
        }
    }
}
