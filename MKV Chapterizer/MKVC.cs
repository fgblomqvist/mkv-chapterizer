/* 
 
  MKV Chapterizer
  ---------------------------
 
  Copyright © 2010-2011 Fredrik Blomqvist

  This file is part of MKV Chapterizer.

    MKV Chapterizer is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    MKV Chapterizer is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with MKV Chapterizer.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using MediaInfoLib;
using System.Net;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;


namespace MKV_Chapterizer
{

    public partial class MKVC : Form
    {

            [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        private int LOGPIXELSX = 88;
        private IntPtr NULL = IntPtr.Zero;

        public static string var;
        public static int duration;
        public static FileInfo[] theFiles;
        public static int chapterCount;
        public static float screenMultiplier = 1.0F;
        public static bool pQueueMode;
        public static string[] sargs;
        public static int tbarVal = 5;
        public int queueAction = 1;
        public int queueProgress;

        private delegate void ObjectDelegate(int percentage);
        private delegate void ObjectDelegate2(string status);
        private delegate void ObjectDelegate3();

        Chapterizer thechapterizer = new Chapterizer();

        public MKVC()
        {
            InitializeComponent();
            AddDragHandlers(this);

            foreach (Control ctl in this.Controls)
            {
                ctl.AllowDrop = true;
            }

            thechapterizer.ProgressChanged += new Chapterizer.ProgressChangedEventHandler(thechapterizer_ProgressChanged);
            thechapterizer.StatusChanged += new Chapterizer.ProgressChangedEventHandler(thechapterizer_StatusChanged);
            thechapterizer.Finished += new Chapterizer.FinishedEventHandler(thechapterizer_Finished);
            
        }

        public bool QueueMode
        {
            get
            {
                return Properties.Settings.Default.queueMode;
            }

            set
            {
                Properties.Settings.Default.queueMode = value;

                if (value)
                {
                    //Empty files
                    lboxFiles.Items.Clear();
                    //Enable queueUI
                    lblNumOfChapters.Visible = false;
                    lblChapterCount.Visible = false;
                    tabControl.HideTabs = false;
                    progressBar.Location = new System.Drawing.Point(Convert.ToInt32(7 * screenMultiplier), Convert.ToInt32(149 * screenMultiplier));
                    this.Show();
                }
                else
                {
                    //Empty files
                    lboxFiles.Items.Clear();
                    //Disable queueUI
                    lblNumOfChapters.Visible = true;
                    lblChapterCount.Visible = true;
                    tabControl.HideTabs = true;
                    tabControl.Size = new System.Drawing.Size(Convert.ToInt32(379 * screenMultiplier), Convert.ToInt32(145 * screenMultiplier));
                    progressBar.Location = new System.Drawing.Point(Convert.ToInt32(7 * screenMultiplier), Convert.ToInt32(149 * screenMultiplier));
                    this.Show();
                    this.Size = new System.Drawing.Size(Convert.ToInt32(385 * screenMultiplier), Convert.ToInt32(173 * screenMultiplier));
                }
            }
        }    

        private void DragEnterHandler(object sender, System.Windows.Forms.DragEventArgs e)
        {

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (!QueueMode)
            {
                if (s.Count() != 1)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
            }

            FileInfo fi;

            foreach (string i in s)
            {
            
            fi = new FileInfo(i);

            if (fi.Extension == ".mkv")
            {
          
            e.Effect = DragDropEffects.All;

            }
            else
            {
                e.Effect = DragDropEffects.None;
                break;
            }
            }
        }

        private void DragDropHandler(object sender, System.Windows.Forms.DragEventArgs e)
        {

            //temp: Clear the lboxFiles if no queue
            if (Properties.Settings.Default.queueMode == false)
            {
                lboxFiles.Items.Clear();
            }

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
            bool exist = false;

            foreach (string str in s)
            {
                foreach (string itm in lboxFiles.Items)
                {
                    if (itm == str) { exist = true; }
                }

                if (exist != true)
                {
                    lboxFiles.Items.Add(str);
                }
            }

            FileInfo fi = new FileInfo(s[0]);

            //Get runtime of movie with MediaInfo

            MediaInfo MI = new MediaInfo();

            MI.Open(fi.FullName);

            if (Properties.Settings.Default.queueMode == false)
            {
                if (ChaptersExist(fi.FullName))
                {

                    ChaptersExist f = new ChaptersExist(this);

                    f.ShowDialog(this);

                    switch (f.Result)
                    {

                        case 0:

                            //Cancel
                            return;

                        case 1:

                            //Remove

                            sargs = new string[] { fi.FullName, "false" };

                            trackBar1.Enabled = false;
                            btnMerge.Text = "De-Chapterize";

                            break;

                        case 2:

                            //Remove and Insert New

                            sargs = new string[] { fi.FullName, "true" };

                            trackBar1.Enabled = true;
                            btnMerge.Text = "Re-Chapterize";

                            break;
                    }

                }
                else
                {
                    trackBar1.Enabled = true;
                }
            }
            else
            {
                trackBar1.Enabled = true;
            }

            decimal dd;
            dd = Math.Floor(decimal.Parse(MI.Get(StreamKind.Video, 0, "Duration")) / 60000);

            //Save the runtime in minutes as integer in duration variable

            duration = Convert.ToInt32(dd);

            //Calculate the number of chapters with default chapter interval 5 minutes

            CalcChapNumber(duration);

            //Enable the controls for the merging process

            lblChapterInterval.Enabled = true;
            lblTrackbarValue.Enabled = true;
            lblMin.Enabled = true;
            btnMerge.Enabled = true;
            cboxOverwrite.Enabled = true;
            lblNumOfChapters.Enabled = true;

            //Hide the tutorial message
            lblTutorial.Visible = false;
        }

        bool ChaptersExist(String file)
        {

            MediaInfo info = new MediaInfo();
            info.Open(file);
            int lol = info.Count_Get(StreamKind.Chapters);

            info.Option("Inform", "XML");
            info.Option("Complete");
            if (info.Inform().Contains("<track type=\"Menu\""))
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        private void button1_Click(object sender,EventArgs e)
        {

            if (btnMerge.Text == "Chapterize")
            {

                PrepareForRun();

                List<string> mkvList = new List<string>();
                    foreach (string itm in lboxFiles.Items)
                    {
                        mkvList.Add(itm);
                    }

                    thechapterizer.Files = mkvList;
                    thechapterizer.ChaptersExistAction = queueAction;
                    thechapterizer.ChapterInterval = trackBar1.Value;
                    thechapterizer.Overwrite = cboxOverwrite.Checked;
               
            }
            else if (btnMerge.Text == "Re-Chapterize")
            {
                PrepareForRun();

                List<string> mkvList = new List<string>();
                foreach (string itm in lboxFiles.Items)
                {
                    mkvList.Add(itm);
                }
                
                thechapterizer.Files = mkvList;
                thechapterizer.ChaptersExistAction = 1;
                thechapterizer.ChapterInterval = trackBar1.Value;
                thechapterizer.Overwrite = cboxOverwrite.Checked;

            }
            else if (btnMerge.Text == "De-Chapterize")
            {
                PrepareForRun();

                List<string> mkvList = new List<string>();
                foreach (string itm in lboxFiles.Items)
                {
                    mkvList.Add(itm);
                }

                thechapterizer.Files = mkvList;
                thechapterizer.ChaptersExistAction = 2;
                thechapterizer.ChapterInterval = trackBar1.Value;
                thechapterizer.Overwrite = cboxOverwrite.Checked;

            }
            else if (btnMerge.Text == "Cancel")
            {

                //Cancel the ongoing merge

                btnMerge.Text = "Cancelling...";
                thechapterizer.Cancel();
                return;

            }

            thechapterizer.Start();

        }

        private void thechapterizer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
                UpdateProgressbar(e.ProgressPercentage);
        }

        private void UpdateProgressbar(int percentage)
        {
            if (InvokeRequired)
            {
                ObjectDelegate method = new ObjectDelegate(UpdateProgressbar);
                Invoke(method, percentage);
                return;
            }

            progressBar.Value = percentage;
        }

        private void thechapterizer_StatusChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateStatus((string)e.UserState);
        }

        private void UpdateStatus(string status)
        {
            if (InvokeRequired)
            {
                ObjectDelegate2 method = new ObjectDelegate2(UpdateStatus);
                Invoke(method, status);
                return;
            }

            string[] sStatus = status.Split(Convert.ToChar(";"));

            lblStatus.Text = sStatus[0] + "    " + sStatus[1];

        }

        private void thechapterizer_Finished(object sender, RunWorkerCompletedEventArgs e)
        {
                Restore();
                MessageBox.Show("DONE");
        }

        private void Restore()
        {
            if (InvokeRequired)
            {
                ObjectDelegate3 method = new ObjectDelegate3(Restore);
                Invoke(method);
                return;
            }

            DePrepareForRun();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            lblTrackbarValue.Text = trackBar1.Value.ToString();
            tbarVal = trackBar1.Value;
            CalcChapNumber(duration);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bwCheckUpdates.RunWorkerAsync();

            screenMultiplier = GetScreenScaleMulitplier();

            trackBar1.Value = Properties.Settings.Default.defChapInterval;
            if (Properties.Settings.Default.queueMode)
            {
                QueueMode = true;
            }
            else
            {
                QueueMode = false;
            }
            lblTrackbarValue.Text = trackBar1.Value.ToString();

            lblVersion.Text = "v" + Convert.ToString(GetVersion(Version.Parse(Application.ProductVersion))) + " Beta 3";
            

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (thechapterizer.IsBusy)
            {
                btnMerge.Text = "Cancelling...";
                thechapterizer.Cancel();

                while (!thechapterizer.IsBusy)
                {
                    Application.DoEvents();
                }
            }

            //Clean up after the chapterizer
            thechapterizer.Clean();
        }

        private object GetVersion(Version version)
        {

        return (string.Format("{0}.{1}{2}{3}",

                                  version.Major,

                                  version.Minor,

                                  version.Build == 0 && version.Revision == 0

                                    ? string.Empty

                                    : "." + version.Build.ToString(),

                                  version.Revision == 0

                                    ? string.Empty

                                    : "." + version.Revision.ToString()));
        }

        public void CalcChapNumber(int runtime)
        {

            decimal count = runtime / trackBar1.Value;

            if (count < 0)
            {

                MessageBox.Show("Too high interval!");
                trackBar1.Value = Properties.Settings.Default.defChapInterval;

            }

            else
            {

                decimal roundoff = Math.Floor(count);
                
                if (Properties.Settings.Default.firstChap00)
                {
                    roundoff += 1;
                }

                lblChapterCount.Text = roundoff.ToString();

            }

        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process.Start("http://cyb3rh4xter.wordpress.com");

        }
        public void setTrackBarValue(int value)
        {
            if (trackBar1.Enabled == false)
            {
                trackBar1.Value = value;
                lblTrackbarValue.Text = trackBar1.Value.ToString();
            }

        }

        private void lblSettings_Click(object sender, EventArgs e)
        {

            // passing the original instance of Form1 to Form2 Constructor 
            Settings f = new Settings(this);
            f.ShowDialog(this);

        }

        float GetScreenScaleMulitplier()
        {
            IntPtr hdcScreen = GetDC(NULL);
            int iDPI = -1; // assume failure
            if (hdcScreen != null)
            {
                iDPI = GetDeviceCaps(hdcScreen, LOGPIXELSX);
                ReleaseDC(NULL, hdcScreen);
            }

            float multiplier = (float)iDPI / (float)96;
            return multiplier;
            
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lboxFiles.SelectedIndex != -1)
            {
                lboxFiles.Items.RemoveAt(lboxFiles.SelectedIndex);
            }

            if (lboxFiles.Items.Count == 0)
            {
                DePrepareForRun();
            }
        }
        private void AddDragHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                    AddDragHandlers(control);

                control.DragDrop += new DragEventHandler(this.DragDropHandler);
                control.DragEnter += new DragEventHandler(this.DragEnterHandler);
            }
        }

        private void PrepareForRun()
        {
            /* Disable the controls on which you set the chapterproperties and disable the ability
             * to add new files to the queu and remove files from the queu.
             * TODO: The ability of adding files to the queue during run and deleting files from the queu aswell */
            trackBar1.Enabled = false;
            cboxOverwrite.Enabled = false;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;
            lboxFiles.Enabled = false;
            btnAdd.Enabled = false;
            btnRemove.Enabled = false;

            if (QueueMode)
            {
                lblStatus.Visible = true;
            }

            //Show progressbar
            float y;
            y = 205 * screenMultiplier;
            Size = new Size((int)this.Size.Width, (int)y);

            btnMerge.Text = "Cancel";
        }

        private void DePrepareForRun()
        {
            /* Prepare for new drop*/

            trackBar1.Enabled = false;
            cboxOverwrite.Enabled = false;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
            lboxFiles.Enabled = true;
            lblChapterInterval.Enabled = false;
            lblTrackbarValue.Enabled = false;
            btnMerge.Enabled = false;
            lblMin.Enabled = false;
            lblNumOfChapters.Enabled = false;

            //Reset chapter count
            lblChapterCount.Text = "";

            //Enable tutorial message
            lblTutorial.Visible = true;

            //Set its message

            lblTutorial.Text = "Start by dropping a MKV file on me";

            lboxFiles.Items.Clear();

            btnMerge.Text = "Chapterize";
            progressBar.Value = 0;

            //Hide progressbar
            float y;
            if (QueueMode)
            {
                lblStatus.Visible = false;
            }

            y = 173 * screenMultiplier;
            Size = new Size((int)this.Size.Width, (int)y);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Copyright 2010-2011 © Fredrik Blomqvist");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (openMKVdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //Enable Chaterizing
                lblChapterInterval.Enabled = true;
                lblTrackbarValue.Enabled = true;
                lblMin.Enabled = true;
                btnMerge.Enabled = true;
                cboxOverwrite.Enabled = true;
                lblNumOfChapters.Enabled = true;
                trackBar1.Enabled = true;

                //Hide the tutorial message
                lblTutorial.Visible = false;

                foreach (string file in openMKVdlg.FileNames)
                {
                    lboxFiles.Items.Add(file);
                }
            }
        }

        private void queueToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (queueToolStripMenuItem.Checked)
            {
                QueueMode = true;
            }
            else
            {
                QueueMode = false;
            }
        }

        private void rbtnReplaceThem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnReplaceThem.Checked)
            {
                queueAction = 1;
            }
        }

        private void rbtnRemoveThem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnRemoveThem.Checked)
            {
                queueAction = 2;
            }
        }

        private void rbtnDoNothing_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDoNothing.Checked)
            {
                queueAction = 3;
            }
        }

        private void bwCheckUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            WebClient client = new WebClient();
            Version version = Version.Parse(client.DownloadString("http://flytandekvave.se/files/projects/mkvc/version.txt"));
            Version curVersion = Version.Parse(Application.ProductVersion);

            if (version > curVersion)
            {
                e.Result = true;
            }
            else
            {
                e.Result = false;
            }
        }

        private void bwCheckUpdates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result == true)
            {
                MessageBox.Show("There is a new version available at http://code.google.com/p/mkv-chapterizer/!");
            }
        }
    }
}