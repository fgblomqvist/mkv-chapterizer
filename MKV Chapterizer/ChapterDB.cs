using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;

namespace MKV_Chapterizer
{
    public partial class ChapterDB : Form
    {
        ChapterDBAccess chapterDBAccess = new ChapterDBAccess();

        public ChapterDB()
        {
            InitializeComponent();
        }

        private void ChapterDB_Load(object sender, EventArgs e)
        {
            
        }

        public void SearchChapters(string movieName)
        {
            txtboxSearchName.Text = movieName;
            this.Show();
            ArrayList result = chapterDBAccess.GrabChapters(movieName);

            DataTable table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("ChapterSet", typeof(ChapterDBAccess.ChapterSet));

            lboxResults.DataSource = table;
            lboxResults.DisplayMember = "Name";
            lboxResults.ValueMember = "ChapterSet";

            foreach (ChapterDBAccess.ChapterSet chapterSet in result)
            {
                table.Rows.Add(chapterSet.Name, chapterSet);
            }

            ChapterDBAccess.ChapterSet selected = (ChapterDBAccess.ChapterSet)lboxResults.SelectedValue;
            LoadChapters(selected);
        }

        private void LoadChapters(ChapterDBAccess.ChapterSet chapterSet)
        {
            lviewChapters.Items.Clear();

            foreach (ChapterDBAccess.Chapter chapter in chapterSet)
            {

                string time = chapter.Time.ToString().Remove(12);

                lviewChapters.Items.Add(new ListViewItem(new string[]{chapter.Name, time}));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtboxSearchName.Text != null)
            {
                SearchChapters(txtboxSearchName.Text);
            }
        }

        private void lboxResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ChapterDBAccess.ChapterSet selected = (ChapterDBAccess.ChapterSet)lboxResults.SelectedValue;
            LoadChapters(selected);
        }

        private void txtboxSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtboxSearchName.Text != null)
                {
                    SearchChapters(txtboxSearchName.Text);
                }
            }
        }
    }
}
