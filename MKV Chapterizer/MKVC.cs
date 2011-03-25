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

                    bwFixChapters.RunWorkerAsync(mkvList);

            }
            else if (btnMerge.Text == "Re-Chapterize")
            {
                PrepareForRun();

                //Start the replacing process
                bwRemoveChapters.RunWorkerAsync(sargs);
            }
            else if (btnMerge.Text == "De-Chapterize")
            {
                PrepareForRun();

                //Start the removing process
                bwRemoveChapters.RunWorkerAsync(sargs);
            }
            else if (btnMerge.Text == "Cancel")
            {

                //Cancel the ongoing merge

                btnMerge.Text = "Cancelling...";
                bwAddChapters.CancelAsync();
                bwRemoveChapters.CancelAsync();

            }

            
        }

        private void CreateChapterFile()
        {

                string path = "chapters.xml";
                int nmbr = int.Parse(lblChapterCount.Text);
                int start;
                int extraval = 0;
                int[] time = { 00, 00 };
                int interval = tbarVal; // trackBar1.Value;

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

            lblTrackbarValue.Text = trackBar1.Value.ToString();

            label9.Text = "v" + Convert.ToString(GetVersion(Version.Parse(Application.ProductVersion))) + " Beta";

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {

            if (bwAddChapters.IsBusy)
            {

                btnMerge.Text = "Cancelling...";
                bwAddChapters.CancelAsync();

                while (bwAddChapters.IsBusy == true)
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

        private void bwAddChapters_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> nmbr = (List<string>)e.Argument;
            int qnumber = nmbr.Count();

            foreach (string itm in (List<string>)e.Argument)
            {
  
                //Create chapter file
                CreateChapterFile();

                FileInfo info = new FileInfo(itm);

                MediaInfo MI = new MediaInfo();

                MI.Open(info.FullName);

                String cpath = "chapters.xml";
                String newFileName = null;

                newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";


                ProcessStartInfo prcinfo = new ProcessStartInfo();
                Process prc = new Process();

                Char q = Convert.ToChar(34);
                String newpath = q + info.DirectoryName + "\\" + newFileName + q;
                String oldpath = q + info.FullName + q;

                String args = "-o " + newpath + " --chapters " + q + cpath + q + " --compression -1:none " + oldpath;

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

                    if (bwAddChapters.CancellationPending == true)
                    {

                        e.Cancel = true;
                        e.Result = info;
                        MI.Close();

                        if (!prc.WaitForExit(500))
                        {
                            prc.Kill();
                            Thread.Sleep(1000);
                        }

                        bwAddChapters.Dispose();
                        return;

                    }
                    //End Check


                    if (str != "")
                    {
                        if (str.Contains("Progress"))
                        {

                            bwAddChapters.ReportProgress(Convert.ToInt32(parseProgress(str)));
                        }
                        else if (str.Contains("Error"))
                        { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                }

                File.Delete(cpath);

                if (cboxOverwrite.Checked)
                {

                    //Delete the input file
                    string input = info.FullName.Replace(Properties.Settings.Default.customOutputName.Replace("%O", ""), "");
                    File.Delete(input);

                    if (mode == "replace")
                    {

                        //Delete -new file
                        File.Delete(info.FullName);

                        //Rename the -new-new file to the old files name
                        File.Move(info.DirectoryName + "\\" + newFileName, input);
 
                    }
                    else if (mode == "add")
                    {

                        //Rename -new file to original name
                        File.Move(Properties.Settings.Default.customOutputName.Replace("%O", info.FullName.Replace(".mkv", "")) + ".mkv", input);

                    }
                }
                else if (cboxOverwrite.Checked == false && mode == "replace")
                {
                    //Delete -new file
                    File.Delete(info.FullName);

                    //Rename the -new-new file to the old files name
                    File.Move(info.DirectoryName + "\\" + newFileName, info.FullName);
                }

                qnumber -= 1;
            }
        }


        private void bwAddChapters_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //temp fix
            FileInfo theFile = new FileInfo(lboxFiles.Items[0].ToString());

            //Set the form to its normal size

            DePrepareForRun();

            btnMerge.Text = "Chapterize";

            if (mode != "queue")
            {
                //Reset the progressbar
                progressBar.Value = 0;
            }

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

            //Set its message

            lblTutorial.Text = "Start by dropping a MKV file on me";

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
            else if (e.Cancelled == false & e.Error != null)
            {

                MessageBox.Show("Error Occured:" + Environment.NewLine + e.Error.Message);

            }

        }

        private void bwAddChapters_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (mode == "add")
            {
                progressBar.Value = e.ProgressPercentage;
            }
            else if (mode == "replace")
            {
                progressBar.Value = e.ProgressPercentage / 2 + 50;
            }
            else if (mode == "queue")
            {
                int number = lboxFiles.Items.Count;
                progressBar.Value = e.ProgressPercentage / number;
            }
        }


        private void lblSettings_Click(object sender, EventArgs e)
        {

            // passing the original instance of Form1 to Form2 Constructor 
            Settings f = new Settings(this);
            f.ShowDialog(this);

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

        private void bwRemoveChapters_DoWork(object sender, DoWorkEventArgs e)
        {

            string[] args = e.Argument as string[];
            FileInfo info = new FileInfo(args[0]);

            String newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            Char q = Convert.ToChar(34);
            String newpath = q + info.DirectoryName + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String pArgs = "-o " + newpath + " --no-chapters --compression -1:none " + oldpath;

            ProcessStartInfo prcinfo = new ProcessStartInfo();
            Process prc = new Process();

            prcinfo.FileName = "mkvmerge.exe";
            prcinfo.Arguments = pArgs;
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

                if (bwRemoveChapters.CancellationPending == true)
                {

                    e.Cancel = true;
                    e.Result = info;

                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }

                    bwRemoveChapters.Dispose();
                    return;

                }
                //End Check


                if (str != "")
                {
                    if (str.Contains("Progress"))
                    {

                        bwRemoveChapters.ReportProgress(Convert.ToInt32(parseProgress(str)));
                    }
                    else if (str.Contains("Error"))
                    { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

            }

            if (mode == "remove")
            {

                if (cboxOverwrite.Checked)
                {
                    File.Delete(info.FullName);
                    File.Move(info.DirectoryName + "\\" + newFileName, info.FullName);
                }
            }
            else if (mode == "replace")
            {

                //Don't overwrite any file, keep the new (choosable) tag
                return;

            }

        }

        private void bwRemoveChapters_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (mode == "replace")
            {
                progressBar.Value = e.ProgressPercentage / 2;
            }
            else
            {
                progressBar.Value = e.ProgressPercentage;
            }

        }

        private void bwRemoveChapters_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //temp fix
            FileInfo theFile = new FileInfo(lboxFiles.Items[0].ToString());

            if (mode == "remove")
            {

                DePrepareForRun();

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

                //Set its message

                lblTutorial.Text = "Start by dropping a MKV file on me";

                //Display a messagebox with success message or error info

                if (e.Cancelled == false & e.Error == null)
                {

                    MessageBox.Show(this, "Done!" + Environment.NewLine + "Your de-chapterized mkv will be located in the same" + Environment.NewLine + "directory as the old one.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else if (e.Cancelled == true & e.Error == null)
                {

                    //Clean-up files

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
                else if (e.Cancelled == false & e.Error != null)
                {

                    MessageBox.Show("Error Occured:" + Environment.NewLine + e.Error.Message);

                }

            }
            else if (mode == "replace")
            {

                String newFile = theFile.DirectoryName + "\\" + Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(theFile.FullName)) + ".mkv";

                //Continue with the adding of new chapters
                List<string> mkv = new List<string>();
                mkv.Add(newFile);
                bwAddChapters.RunWorkerAsync(mkv);

            }

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
            /* Enable the controls on which you set the chapterproperties and disable the ability
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
            /* Enable the controls on which you set the chapterproperties and disable the ability
             * to add new files to the queu and remove files from the queu. */
            trackBar1.Enabled = true;
            cboxOverwrite.Enabled = true;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
            lboxFiles.Enabled = true;
            btnAdd.Enabled = true;
            btnRemove.Enabled = true;
            lboxFiles.Items.Clear();

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

        private void bwFixChapters_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> mkvlist = (List<string>)e.Argument;

            //for each mkv the user has added
            foreach (string s in mkvlist)
            {
                FileInfo fi = new FileInfo(s);

                //check if it already has chapters
                if (ChaptersExist(s))
                {
                    //the file already has chapters, check what the user want's to do
                    switch (queueAction)
                    {
                        case 1:
                            //Replace Them
                            InsertChapters(RemoveChapters(s));
                            break;
                        case 2:
                            //Remove Them
                            string file = RemoveChapters(s);
                            if (file == "0")
                            {
                                e.Cancel = true;
                                bwFixChapters.Dispose();
                            }
                            else
                            {
                                String newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(fi.FullName)) + ".mkv";

                                if (cboxOverwrite.Checked)
                                {
                                    File.Delete(fi.FullName);
                                    File.Move(fi.DirectoryName + "\\" + newFileName, fi.FullName);
                                }
                            }
                            break;

                        case 3:
                            //Skip the file
                            break;
                    }
                }
                else
                {
                    string file = InsertChapters(s);
                    if (file == "0")
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        if (cboxOverwrite.Checked)
                        {

                            //Delete the input file
                            File.Delete(s);

                            //Rename -new file to original name
                            File.Move(file, s);

                        }
                    }
                }
            }
            DePrepareForRun();
        }

        private void bwFixChapters_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

                progressBar.Value = e.ProgressPercentage;

        }

        private void bwFixChapters_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            //temp fix
            FileInfo theFile = new FileInfo(lboxFiles.Items[0].ToString());

                DePrepareForRun();

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

                //Set its message

                lblTutorial.Text = "Start by dropping a MKV file on me";

                //Display a messagebox with success message or error info

                if (e.Cancelled == false & e.Error == null)
                {

                    MessageBox.Show(this, "Done!" + Environment.NewLine + "Your queue has now been processed.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                }
                else if (e.Cancelled == true & e.Error == null)
                {

                    //Clean-up files

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
                else if (e.Cancelled == false & e.Error != null)
                {

                    MessageBox.Show("Error Occured:" + Environment.NewLine + e.Error.Message);

                }
        }

        private string RemoveChapters(string file)
        {
            //Remove the chapters on the file and return the path to the new file

            FileInfo info = new FileInfo(file);

            String newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            Char q = Convert.ToChar(34);
            String newpath = q + info.DirectoryName + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String pArgs = "-o " + newpath + " --no-chapters --compression -1:none " + oldpath;

            ProcessStartInfo prcinfo = new ProcessStartInfo();
            Process prc = new Process();

            prcinfo.FileName = "mkvmerge.exe";
            prcinfo.Arguments = pArgs;
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

                if (bwFixChapters.CancellationPending == true)
                {

                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }
                    return "0";
                }
                //End Check


                if (str != "")
                {
                    if (str.Contains("Progress"))
                    {

                        bwFixChapters.ReportProgress(Convert.ToInt32(parseProgress(str)));
                    }
                    else if (str.Contains("Error"))
                    { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

            }

            return file;
        }

        private string InsertChapters(string file)
        {

                //Create chapter file
                CreateChapterFile();

                FileInfo info = new FileInfo(file);

                MediaInfo MI = new MediaInfo();

                MI.Open(info.FullName);

                String cpath = "chapters.xml";
                String newFileName = null;

                newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

                ProcessStartInfo prcinfo = new ProcessStartInfo();
                Process prc = new Process();

                Char q = Convert.ToChar(34);
                String newpath = q + info.DirectoryName + "\\" + newFileName + q;
                String oldpath = q + info.FullName + q;

                String args = "-o " + newpath + " --chapters " + q + cpath + q + " --compression -1:none " + oldpath;

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

                    if (bwFixChapters.CancellationPending == true)
                    {

                        MI.Close();

                        if (!prc.WaitForExit(500))
                        {
                            prc.Kill();
                            Thread.Sleep(1000);
                        }

                        bwAddChapters.Dispose();
                        return "0";

                    }
                    //End Check


                    if (str != "")
                    {
                        if (str.Contains("Progress"))
                        {

                            bwFixChapters.ReportProgress(Convert.ToInt32(parseProgress(str)));
                        }
                        else if (str.Contains("Error"))
                        { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                    }

                }

                File.Delete(cpath);
                return info.DirectoryName + "\\" + newFileName;
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

        }

    }

}

