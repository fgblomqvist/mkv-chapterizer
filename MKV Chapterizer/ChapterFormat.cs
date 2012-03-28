using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MKV_Chapterizer
{
    public partial class ChapterFormat : Form
    {
        DataTable chapterPatterns = new DataTable();

        public ChapterFormat()
        {
            InitializeComponent();
        }

        private void txtPattern_TextChanged(object sender, EventArgs e)
        {
            PrintExample();
        }

        private void PrintExample()
        {
            string exampleText = null;
            txtExample.Text = string.Empty;
            for(int i = 0; i < 4; i++)
            {
                string chapterText = Regex.Replace(txtPattern.Text, "%N", "Chapter " + i.ToString());
                chapterText = Regex.Replace(chapterText, "%T", string.Format("00:{0}:00.000", (i * 5).ToString("00")));
                chapterText = Regex.Replace(chapterText, "%L", Environment.NewLine);
                chapterText = Regex.Replace(chapterText, "%I", i.ToString()); 
                exampleText = (exampleText != null) ? String.Join(txtSeparator.Text.Replace("%L", Environment.NewLine), exampleText, chapterText) : chapterText;
            }

            txtExample.Text = exampleText;
        }

        private void txtSeparator_TextChanged(object sender, EventArgs e)
        {
            PrintExample();
        }

        private void ChapterFormat_Load(object sender, EventArgs e)
        {
            chapterPatterns.Columns.Add("name");
            chapterPatterns.Columns.Add("pattern");
            chapterPatterns.Columns.Add("separator");

            chapterPatterns.Rows.Add("OGG", "CHAPTER%I=%T" + Environment.NewLine + "CHAPTER%INAME=%N", "%L");
            chapterPatterns.Rows.Add("Custom", "");

            cboxPatterns.DataSource = chapterPatterns;
            cboxPatterns.DisplayMember = "name";
            cboxPatterns.ValueMember = "pattern";

            cboxPatterns.SelectedIndex = -1;

            txtPattern.Text = Properties.Settings.Default.chapterPattern.Split((char)0)[0];
            txtSeparator.Text = Properties.Settings.Default.chapterPattern.Split((char)0)[1];
        }

        private void cboxPatterns_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPattern.Text = (string)cboxPatterns.SelectedValue;
                txtSeparator.Text = chapterPatterns.Rows[cboxPatterns.SelectedIndex]["separator"].ToString();
            }
            catch
            {
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!txtPattern.Text.Contains("%T") && !txtPattern.Text.Contains("%N") && !txtPattern.Text.Contains("%I"))
            {
                if (MessageBox.Show("You haven't used any of the chapter variables, are you sure you want to continue and use this pattern?", "No variables found!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                {
                    return;
                }      
            }

            Properties.Settings.Default.chapterPattern = String.Join(Convert.ToChar(0).ToString(), txtPattern.Text, txtSeparator.Text);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
