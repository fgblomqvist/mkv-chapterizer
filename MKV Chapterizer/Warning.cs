using System;
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
            Close();
        }
    }
}
