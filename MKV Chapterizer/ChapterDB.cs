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
using System.Threading;

namespace MKV_Chapterizer
{
    public partial class ChapterDB : Form
    {
        ChapterDBAccess chapterDBAccess = new ChapterDBAccess();
        BackgroundWorker bwSearch = new BackgroundWorker();
        private string pMovieName;
        private ChapterDBAccess.ChapterSet pChosenChapter;

        public ChapterDB()
        {
            InitializeComponent();
        }

        public string MovieName
        {
            get
            {
                return pMovieName;
            }

            set
            {
                pMovieName = value;
            }
        }

        public ChapterDBAccess.ChapterSet ChosenChapter
        {
            get
            {
                return pChosenChapter;
            }

            set
            {
                pChosenChapter = value;
            }
        }

        private void ChapterDB_Load(object sender, EventArgs e)
        {
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            bwSearch.DoWork +=new DoWorkEventHandler(bwSearch_DoWork);
            bwSearch.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(bwSearch_RunWorkerCompleted);
            bwSearch.WorkerSupportsCancellation = true;

            bwSearch.RunWorkerAsync(MovieName);
        }

        public ChapterDBAccess.ChapterSet ShowDialog(string movieName)
        {
            MovieName = movieName;
            txtboxSearchName.Text = movieName;
            lblStatus.Text = "Searching...";
            if (this.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return ChosenChapter;
            }
            else
            {
                return null;
            }
        }

        private void ApplyColors()
        {
            foreach (DataGridViewRow dgRow in dgViewResults.Rows)
            {
                DataGridViewCellStyle styleRed = new DataGridViewCellStyle();
                styleRed.ForeColor = Color.Red;

                DataGridViewCellStyle styleGreen = new DataGridViewCellStyle();
                styleGreen.ForeColor = Color.Green;

                DataGridViewCellStyle styleYellow = new DataGridViewCellStyle();
                styleYellow.ForeColor = Color.Orange;

                if (Convert.ToInt32(dgRow.Cells[1].Value) <= 3)
                {
                    dgRow.DefaultCellStyle = styleRed;
                }
                else if (Convert.ToInt32(dgRow.Cells[1].Value) == 5)
                {
                    dgRow.DefaultCellStyle = styleGreen;
                }
                else
                {
                    dgRow.DefaultCellStyle = styleYellow;
                }
            }
        }

        private void dgViewResults_Sorted(object sender, EventArgs e)
        {
            ApplyColors();
        }

        private void LoadChapters(ChapterDBAccess.ChapterSet chapterSet)
        {
            lviewChapters.Items.Clear();
            string time = null;

            foreach (ChapterDBAccess.Chapter chapter in chapterSet)
            {

                time = chapter.Time.ToString();

                if (chapter.Time.ToString().Length >= 12)
                {
                    time = time.Remove(12);
                }

                lviewChapters.Items.Add(new ListViewItem(new string[]{chapter.Name, time}));
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtboxSearchName.Text != null)
            {
                lblStatus.Text = "Searching...";
                if (bwSearch.IsBusy == false)
                {
                    bwSearch.RunWorkerAsync(txtboxSearchName.Text);
                }
            }
        }


        private void txtboxSearchName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtboxSearchName.Text != null)
                {
                    lblStatus.Text = "Searching...";
                    if (bwSearch.IsBusy == false)
                    {
                        bwSearch.RunWorkerAsync(txtboxSearchName.Text);
                    }
                }
            }
        }

        private void CenterLabel(Label label)
        {
            int formMiddle = this.Width;
            label.Location = new Point(formMiddle - label.Width / 2, label.Location.Y);
        }

        private List<ChapterDBAccess.ChapterSet> Filter(List<ChapterDBAccess.ChapterSet> list)
        {
            int totalsum;
            double goodsum;

            foreach (ChapterDBAccess.ChapterSet chapterSet in list)
            {
                totalsum = chapterSet.Chapters.Count * 2;
                goodsum = totalsum;

                foreach (ChapterDBAccess.Chapter chapter in chapterSet)
                {
                    //for each chapter that misses the time and/or name decrement goodsum
                    if (string.IsNullOrEmpty(chapter.Name))
                    {
                        goodsum--;
                    }
                    else if (chapter.Name.Contains("Chapter"))
                    {
                        goodsum -= 0.5; //Not that bad, but still bad
                    }
                    if (chapter.Time == null || chapter.Time.ToString() == "00:00:00")
                    {
                        goodsum--;
                    }
                }

                chapterSet.Quality = (int)Math.Round(((decimal)goodsum / (decimal)totalsum) * 5, 0, MidpointRounding.ToEven); 
            }
            return list;
        }

        private void dgViewResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgViewResults.SelectedRows.Count >= 1)
            {
                ChapterDBAccess.ChapterSet selected = (ChapterDBAccess.ChapterSet)dgViewResults.SelectedRows[0].Cells[2].Value;
                LoadChapters(selected);
            }
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            ChosenChapter = (ChapterDBAccess.ChapterSet)dgViewResults.SelectedRows[0].Cells[2].Value;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            List<ChapterDBAccess.ChapterSet> result = null;
            try
            {
                result = chapterDBAccess.GrabChapters((string)e.Argument);

                if (bwSearch.CancellationPending == true)
                {
                    //Just set the e.Cancel to true
                    e.Cancel = true;
                    return;
                }
            }
            catch (ChapterDBAccess.NoResultsException)
            {
                result = new List<ChapterDBAccess.ChapterSet>();
            }

            e.Result = Filter(result);

        }

        private void bwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null && e.Cancelled == false)
            {
                List<ChapterDBAccess.ChapterSet> result = (List<ChapterDBAccess.ChapterSet>)e.Result;

                lblStatus.Text = string.Format("{0} sets of chapters found", result.Count.ToString());

                DataTable table = new DataTable();
                table.Columns.Add("Name", typeof(string));
                table.Columns.Add("Quality", typeof(string));
                table.Columns.Add("ChapterSet", typeof(ChapterDBAccess.ChapterSet));

                dgViewResults.DataSource = table;
                dgViewResults.Columns["ChapterSet"].Visible = false;

                foreach (ChapterDBAccess.ChapterSet chapterSet in result)
                {
                    table.Rows.Add(chapterSet.Name, chapterSet.Quality, chapterSet);
                }

                //Sort and apply colors
                dgViewResults.Sort(dgViewResults.Columns["Quality"], ListSortDirection.Descending);
                ApplyColors();
            }
            else if (e.Error != null)
            {
                lblStatus.Text = e.Error.Message;
            }
            else if (e.Cancelled == true)
            {
                //It got cancelled
            }
        }

        private void ChapterDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            bwSearch.CancelAsync();
        }
    }
}
