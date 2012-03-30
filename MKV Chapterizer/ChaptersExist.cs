using System;
using System.Windows.Forms;

namespace MKV_Chapterizer
{
    public partial class ChaptersExist : Form
    {
        private int theResult;
        private MKVC _MainForm { set; get; }

        public int Result
        {
            get
            {
                return theResult;
            }
            set
            {
                theResult = value;
            }
        }

        public ChaptersExist(MKVC MainForm) : this()
        {
            _MainForm = MainForm;
        }

        public ChaptersExist()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Remove
            theResult = 1;
            Close();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            //Replace
            theResult = 2;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Cancel
            theResult = 0;
            Close();
        }

        private void ChaptersExist_Load(object sender, EventArgs e)
        {

        }
    }
}
