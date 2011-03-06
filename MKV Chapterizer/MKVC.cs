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
        public static FileInfo theFile;
        public static int chapterCount;
        public static float screenMultiplier = 1.0F;

        public MKVC()
        {
            InitializeComponent();
               
      foreach (Control ctl in this.Controls)
      {
          ctl.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDrop);
      }

      foreach (Control ctl in this.Controls)
      {
          ctl.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter);
      } 

          
        }

        private void DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
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
            }
            }
        }

        private void DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {

            //Save the fileinfo in theFile

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
            FileInfo fi = new FileInfo(s[0]);

            theFile = fi;

            //Get runtime of movie with MediaInfo

            MediaInfo MI = new MediaInfo();

            MI.Open(fi.FullName);

            if (ChaptersExist(fi.FullName))
            {

                ChaptersExist f = new ChaptersExist(this);
                f.ShowDialog();

                switch (f.Result)
                {
                    case 0:
                        //Cancel
                    case 1:
                        //Remove

                        break;
                    case 2:
                        //Replace

                        break;
                }

            }

            decimal dd;
            dd = Math.Floor(decimal.Parse(MI.Get(StreamKind.Video, 0, "Duration")) / 60000);

            //Save the runtime in minutes as integer in duration variable

            duration = Convert.ToInt32(dd);

            //Calculate the number of chapters with default chapter interval 5 minutes

            CalcChapNumber(duration);

            //Enable the controls for the merging process

            trackBar1.Enabled = true;
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
            if (info.Inform().Contains("<track type=\"Menu\">"))
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

                //Show progressbar

                float y = 197 * screenMultiplier;

                Size = new Size((int)this.Size.Width, (int)y);
                btnMerge.Text = "Cancel";

                //Create chapter file

                string path = "chapters.xml";
                int nmbr = int.Parse(lblChapterCount.Text);
                int start;
                int extraval = 0;
                int[] time = { 00, 00 };
                int interval = trackBar1.Value;

                XmlTextWriter xwrite = new XmlTextWriter(path, System.Text.Encoding.UTF8);

                xwrite.WriteStartDocument();
                xwrite.Formatting = Formatting.Indented;
                xwrite.Indentation = 2;
                xwrite.WriteDocType("Tags", null, "matroskatags.dtd", null);
                xwrite.WriteStartElement("Chapters");
                xwrite.WriteStartElement("EditionEntry");

                if (Properties.Settings.Default.firstChap00)
                {
                    xwrite.WriteStartElement("ChapterAtom");
                    xwrite.WriteElementString("ChapterTimeStart", string.Format("{0:00}:{1:00}", time[0], time[1]) + ":00.000000000");
                    xwrite.WriteStartElement("ChapterDisplay");
                    xwrite.WriteElementString("ChapterString", "Chapter " + Convert.ToString(1));
                    xwrite.WriteEndElement();
                    xwrite.WriteEndElement();

                    extraval = 1;
                }

                for (start = 0 + extraval; start < int.Parse(lblChapterCount.Text); start++)
                {

                    time[1] += interval;

                    if (time[1] >= 60)
                    {
                        time[0] += 1;
                        time[1] -= 60;

                    }


                    xwrite.WriteStartElement("ChapterAtom");
                    xwrite.WriteElementString("ChapterTimeStart", string.Format("{0:00}:{1:00}", time[0], time[1]) + ":00.000000000");
                    xwrite.WriteStartElement("ChapterDisplay");
                    xwrite.WriteElementString("ChapterString", "Chapter " + Convert.ToString(start + 1));
                    xwrite.WriteEndElement();
                    xwrite.WriteEndElement();

                }

                if (Properties.Settings.Default.extraChapEnd)
                {
                    
                    int hours = 0;
                    int minutes = duration;

                    while (minutes >= 60)
                    {
                        minutes -= 60;
                        hours += 1;
                    }
  
                    xwrite.WriteStartElement("ChapterAtom");
                    xwrite.WriteElementString("ChapterTimeStart", string.Format("{0:00}:{1:00}", hours.ToString(), minutes.ToString()) + ":00.000000000");
                    xwrite.WriteStartElement("ChapterDisplay");
                    xwrite.WriteElementString("ChapterString", "Chapter " + Convert.ToString(start + 1));
                    xwrite.WriteEndElement();
                    xwrite.WriteEndElement();
                    }

                xwrite.WriteEndElement();
                xwrite.WriteEndElement();

                xwrite.Close();

                //Start the merging process

                backgroundWorker.RunWorkerAsync(theFile);


                }
                else if (btnMerge.Text == "Cancel")
                {

                    //Cancel the ongoing merge

                    btnMerge.Text = "Cancelling...";
                    backgroundWorker.CancelAsync();

                }

            
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

                lblTrackbarValue.Text = trackBar1.Value.ToString();

            CalcChapNumber(duration);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            screenMultiplier = GetScreenScaleMulitplier();
            trackBar1.Value = Properties.Settings.Default.defChapInterval;

            lblTrackbarValue.Text = trackBar1.Value.ToString();

            label9.Text = "v" + Convert.ToString(GetVersion(Version.Parse(Application.ProductVersion)));

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {

            if (backgroundWorker.IsBusy)
            {

                btnMerge.Text = "Cancelling...";
                backgroundWorker.CancelAsync();

                while (backgroundWorker.IsBusy == true)
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

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            FileInfo info = e.Argument as FileInfo;

            MediaInfo MI = new MediaInfo();

            MI.Open(info.FullName);

            String cpath = "chapters.xml";
            String newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            ProcessStartInfo prcinfo = new ProcessStartInfo();
            Process prc = new Process();

            Char q = Convert.ToChar(34);
            String newpath = q + info.DirectoryName + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String args = "-o " + newpath + " --chapters " + q + cpath + q + " " + oldpath;

            prcinfo.FileName = "mkvmerge.exe";
            prcinfo.Arguments = args;
            prcinfo.RedirectStandardOutput = true;
            prcinfo.RedirectStandardError = true;
            prcinfo.CreateNoWindow = true;
            prcinfo.UseShellExecute = false;

            prc.StartInfo = prcinfo;
            prc.Start();

            string str;

            while ((str = prc.StandardOutput.ReadLine()) != null)
            {

                //Check for Cancellation

                if (backgroundWorker.CancellationPending == true)
                {

                    e.Cancel = true;
                    e.Result = info;
                    MI.Close();

                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }
                  
                    backgroundWorker.Dispose();
                    return;
                    
                }
                //End Check


                if (str != "")
                {
                    if (str.Contains("Progress"))
                    {

                        backgroundWorker.ReportProgress(Convert.ToInt32(parseProgress(str)));
                    }
                    else if (str.Contains("Error"))
                    { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

            }

            File.Delete(cpath);

            if (cboxOverwrite.Checked)
            {
                File.Delete(info.FullName);
                File.Move(info.DirectoryName + "\\" + newFileName, info.FullName);
            }

        }


        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //Set the form to its normal size
            //double x = Math.Floor(372 * screenMultiplier);
            //double y = Math.Floor(165 * screenMultiplier);

           // Size sizesmall = new Size((int)x, (int)y);

            //Size = sizesmall;

            btnMerge.Text = "Chapterize";

            //Reset the progressbar
            progressBar.Value = 0;

            //Disable controls until new drop

            trackBar1.Enabled = false;
            label2.Enabled = false;
            lblTrackbarValue.Enabled = false;
            btnMerge.Enabled = false;
            cboxOverwrite.Enabled = false;
            lblMin.Enabled = false;
            label3.Enabled = false;

            //Reset chapter count

            lblChapterCount.Text = "";

            //Enable tutorial message

            lblTutorial.Visible = true;

            //Display a messagebox with success message or error info

            if (e.Cancelled == false & e.Error == null)
            {

                MessageBox.Show(this, "Done!" + Environment.NewLine + "Your new mkv will be located in the same" + Environment.NewLine + "directory as the old one.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            }
            else if (e.Cancelled == true & e.Error == null)
            {

                //Clean-up files
                File.Delete("chapters.xml");

                String newFile = theFile.DirectoryName + "\\" + Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(theFile.FullName)) + ".mkv";

                try
                {
                    File.Delete(newFile);
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Failed to delete leftovers!:" + Environment.NewLine + ex.Message);
                }

            }

        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

         progressBar.Value = e.ProgressPercentage;
            
        }


        private void lblSettings_Click(object sender, EventArgs e)
        {

            // passing the original instance of Form1 to Form2 Constructor 
            Settings f = new Settings(this);
            f.ShowDialog();

        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

    }

}

