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
        public static string mode = "add";
        public static string[] sargs;
        public static int tbarVal = 5;
        public int queueAction;
        public int queueProgress;

        public MKVC()
        {
            InitializeComponent();

            AddDragHandlers(this);

            TheChapterizer someForm;
            someForm = new TheChapterizer();

            foreach (Control ctl in this.Controls)
            {
                ctl.AllowDrop = true;
            } 
            
        }

        private void DragEnterHandler(object sender, System.Windows.Forms.DragEventArgs e)
        {

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
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
            if (Properties.Settings.Default.queue == false)
            {
                lboxFiles.Items.Clear();
            }

            //Save the fileinfo in theFile

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

            if (s.Count() == 1)
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

                            mode = "remove";
                            sargs = new string[] { fi.FullName, "false" };

                            trackBar1.Enabled = false;
                            btnMerge.Text = "De-Chapterize";

                            break;

                        case 2:

                            //Remove and Insert New

                            mode = "replace";
                            sargs = new string[] { fi.FullName, "true" };

                            trackBar1.Enabled = true;
                            btnMerge.Text = "Re-Chapterize";

                            break;
                    }

                }
                else
                {
                    mode = "add";
                    trackBar1.Enabled = true;
                }
            }
            else
            {
                mode = "queue";
                trackBar1.Enabled = true;
            }

            decimal dd;
            dd = Math.Floor(decimal.Parse(MI.Get(StreamKind.Video, 0, "Duration")) / 60000);

            //Save the runtime in minutes as integer in duration variable

            duration = Convert.ToInt32(dd);

            //Calculate the number of chapters with default chapter interval 5 minutes

            CalcChapNumber(duration);

            //Enable the controls for the merging process

            label2.Enabled = true;
            lblTrackbarValue.Enabled = true;
            lblMin.Enabled = true;
            btnMerge.Enabled = true;
            cboxOverwrite.Enabled = true;
            label3.Enabled = true;

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


        private object parseProgress(String Text)
        {

            String newText = Text.Replace("Progress: ", "").Replace("%", "");

            return newText as string;

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

                    //bwFixChapters.RunWorkerAsync(mkvList);
                    TheChapterizer.Files = mkvList;
                    TheChapterizer.ChaptersExistAction = queueAction;
                    TheChapterizer.ChapterInterval = trackBar1.Value;
                    TheChapterizer.Overwrite = cboxOverwrite.Checked;

                    tmrProgress.Start();
                    TheChapterizer.Start();
                    //tmrProgress.Stop();
                    //DePrepareForRun();
               
            }
            else if (btnMerge.Text == "Re-Chapterize")
            {
                PrepareForRun();

                List<string> mkvList = new List<string>();
                foreach (string itm in lboxFiles.Items)
                {
                    mkvList.Add(itm);
                }

                TheChapterizer.Files = mkvList;
                TheChapterizer.ChaptersExistAction = 1;
                TheChapterizer.ChapterInterval = trackBar1.Value;
                TheChapterizer.Overwrite = cboxOverwrite.Checked;

                tmrProgress.Start();
                TheChapterizer.Start();
            }
            else if (btnMerge.Text == "De-Chapterize")
            {
                PrepareForRun();

                List<string> mkvList = new List<string>();
                foreach (string itm in lboxFiles.Items)
                {
                    mkvList.Add(itm);
                }

                //bwFixChapters.RunWorkerAsync(mkvList);
                TheChapterizer.Files = mkvList;
                TheChapterizer.ChaptersExistAction = 2;
                TheChapterizer.ChapterInterval = trackBar1.Value;
                TheChapterizer.Overwrite = cboxOverwrite.Checked;

                tmrProgress.Start();
                TheChapterizer.Start();

            }
            else if (btnMerge.Text == "Cancel")
            {

                //Cancel the ongoing merge

                btnMerge.Text = "Cancelling...";
                TheChapterizer.Cancel();

            }

        }



        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            lblTrackbarValue.Text = trackBar1.Value.ToString();
            tbarVal = trackBar1.Value;
            CalcChapNumber(duration);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            screenMultiplier = GetScreenScaleMulitplier();
            trackBar1.Value = Properties.Settings.Default.defChapInterval;
            if (Properties.Settings.Default.queue)
            {
                tabControl.HideTabs = false;
            }
            else
            {
                tabControl.HideTabs = true;
            }
            lblTrackbarValue.Text = trackBar1.Value.ToString();

            label9.Text = "v" + Convert.ToString(GetVersion(Version.Parse(Application.ProductVersion))) + " Beta";

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            if (!TheChapterizer.Finished)
            {
                btnMerge.Text = "Cancelling...";
                TheChapterizer.Cancel();

                while (!TheChapterizer.Finished)
                {
                    Application.DoEvents();
                }
            }
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

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

            String connString = "Database=dev;Data Source=minecraft.homeserver.com;User Id=xbmc;Password=xbmc;";
            MySqlConnection con = new MySqlConnection();

            con.ConnectionString = connString;

        try
        {
            con.Open();
        }
        catch (MySqlException)
            {

            MessageBox.Show(this ,"Failed to check for updates:" + Environment.NewLine + "Unable to connect to updateserver!","Connection Failed", MessageBoxButtons.OK,MessageBoxIcon.Error );
            return;
            }
        catch (Exception ex)
        {

            MessageBox.Show(this,"Failed to check for updates:" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error );
            return;

        }

        String newVersion;
        String query = "SELECT * FROM versions WHERE name = 'mkvc'";
        MySqlCommand cmd = new MySqlCommand(query, con);
        MySqlDataReader reader;

        reader = cmd.ExecuteReader();

        while (reader.Read())
        {

            newVersion = reader.GetString(1);

            if (Version.Parse(reader.GetString(1)) > System.Reflection.Assembly.GetExecutingAssembly().GetName().Version)
            {

                MessageBox.Show("There's a new version available at http://cyb3rh4xter.wordpress.com/mkvcc!", "Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {

                MessageBox.Show(this,"There is no new version available at the moment.","Update Check", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }
        con.Close();

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

            //Show progressbar
            float y = 228 * screenMultiplier;
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
            label2.Enabled = false;
            lblTrackbarValue.Enabled = false;
            btnMerge.Enabled = false;
            lblMin.Enabled = false;
            label3.Enabled = false;

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
            float y = 196 * screenMultiplier;
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
                foreach (string file in openMKVdlg.FileNames)
                {
                    lboxFiles.Items.Add(file);
                }
            }
        }

        private void queueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.queue = queueToolStripMenuItem.Checked;
            if (queueToolStripMenuItem.Checked)
            {
                tabControl.HideTabs = false;
            }
            else
            {
                tabControl.HideTabs = true;
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
            if (rbtnReplaceThem.Checked)
            {
                queueAction = 2;
            }
        }

        private void rbtnDoNothing_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnReplaceThem.Checked)
            {
                queueAction = 3;
            }
        }

        private void tmrProgress_Tick(object sender, EventArgs e)
        {
            if (!TheChapterizer.Finished)
            {
                progressBar.Value = TheChapterizer.Progress;
            }
            else
            {
                DePrepareForRun();
                tmrProgress.Stop();
            }
        }

    }

}