using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace MKV_Chapterizer
{
    public partial class ChapterDB : Form
    {
        private ChapterDBAccess chapterDBAccess = new ChapterDBAccess();
        private BackgroundWorker bwSearch = new BackgroundWorker();
        private ChapterDBAccess.ChapterSet LoadedChapterSet;

        public ChapterDB()
        {
            InitializeComponent();
        }

        public string MovieName { get; set; }
        public ChapterDBAccess.ChapterSet ChosenChapter { get; set; }

        private void ChapterDB_Load(object sender, EventArgs e)
        {
            lblStatus.TextAlign = ContentAlignment.MiddleCenter;
            bwSearch.DoWork += new DoWorkEventHandler(bwSearch_DoWork);
            bwSearch.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSearch_RunWorkerCompleted);
            bwSearch.WorkerSupportsCancellation = true;

            bwSearch.RunWorkerAsync(MovieName);
        }

        public ChapterDBAccess.ChapterSet ShowDialog(string movieName)
        {
            MovieName = movieName;
            txtboxSearchName.Text = movieName;
            lblStatus.Text = "Searching...";
            return ShowDialog() == DialogResult.OK ? ChosenChapter : null;
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
            LoadedChapterSet = chapterSet;
            lviewChapters.Items.Clear();

            foreach (ChapterDBAccess.Chapter chapter in chapterSet)
            {
                string time = chapter.Time.ToString();

                if (chapter.Time.ToString().Length >= 12)
                {
                    time = time.Remove(12);
                }

                lviewChapters.Items.Add(new ListViewItem(new string[] {chapter.Name, time}));
            }

            lblSelected.Text = string.Format("You have selected {0} chapters", chapterSet.Chapters.Count);
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

        private List<ChapterDBAccess.ChapterSet> Filter(List<ChapterDBAccess.ChapterSet> list)
        {
            foreach (ChapterDBAccess.ChapterSet chapterSet in list)
            {
                int totalsum = chapterSet.Chapters.Count * 2;
                double goodsum = totalsum;

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
                    if (chapter.Time == TimeSpan.Zero || chapter.Time.ToString() == "00:00:00")
                    {
                        goodsum--;
                    }
                }

                chapterSet.Quality = (int)Math.Round(((decimal)goodsum / totalsum) * 5, 0, MidpointRounding.ToEven);
            }
            return list;
        }

        private void dgViewResults_SelectionChanged(object sender, EventArgs e)
        {
            if (dgViewResults.SelectedRows.Count < 1)
            {
                return;
            }

            ChapterDBAccess.ChapterSet selected = (ChapterDBAccess.ChapterSet)dgViewResults.SelectedRows[0].Cells[2].Value;
            LoadChapters(selected);
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            if (lviewChapters.SelectedItems.Count == 0)
            {
                ChosenChapter = (ChapterDBAccess.ChapterSet)dgViewResults.SelectedRows[0].Cells[2].Value;
            }
            else
            {
                //User has selected only a few of the chapters of the selected set
                ChapterDBAccess.ChapterSet newSet = new ChapterDBAccess.ChapterSet();
                foreach(ListViewItem item in lviewChapters.SelectedItems)
                {
                    newSet.Chapters.Add(new ChapterDBAccess.Chapter(item.Text, TimeSpan.Parse(item.SubItems[1].Text)));
                }

                ChosenChapter = newSet;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void bwSearch_DoWork(object sender, DoWorkEventArgs e)
        {
            List<ChapterDBAccess.ChapterSet> result;
            try
            {
                result = chapterDBAccess.GrabChapters((string)e.Argument);

                if (bwSearch.CancellationPending)
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
                table.Columns.Add("Name", typeof (string));
                table.Columns.Add("Quality", typeof (string));
                table.Columns.Add("ChapterSet", typeof (ChapterDBAccess.ChapterSet));

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
            else if (e.Cancelled)
            {
                //It got cancelled
            }
        }

        private void ChapterDB_FormClosing(object sender, FormClosingEventArgs e)
        {
            bwSearch.CancelAsync();
        }

        private void lviewChapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lviewChapters.SelectedItems.Count > 0)
            {
                lblSelected.Text = string.Format("You have selected {0} chapters", lviewChapters.SelectedItems.Count);
            }
            else
            {
                lblSelected.Text = string.Format("You have selected {0} chapters", lviewChapters.Items.Count);
            }
        }

        private void useAsBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lviewChapters.SelectedIndices.Count != 1)
            {
                MessageBox.Show("Only one chapter can be the base, neither more or less!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                LoadChapters(ReCalcBase(LoadedChapterSet, lviewChapters.SelectedIndices[0]));
            }
        }

        private ChapterDBAccess.ChapterSet ReCalcBase(ChapterDBAccess.ChapterSet chapterSet, int index)
        {
            //Recalculate all the chapters times with the index chapter as base (00:00)
            //Get the difference in time all chapters should be adjusted
            TimeSpan diff = chapterSet.Chapters[index].Time;

            //Then subtract that difference from all chapters
            foreach (ChapterDBAccess.Chapter t in chapterSet.Chapters)
            {
                t.Time = t.Time.Subtract(diff);
            }

            //Return the modified chapterSet
            return chapterSet;
        }
    }
}