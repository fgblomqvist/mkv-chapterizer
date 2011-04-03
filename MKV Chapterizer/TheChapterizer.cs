using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using MediaInfoLib;
using System.Net;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace MKV_Chapterizer
{
    public class Chapterizer
    {
        
        //Variables
        private static int pProgress;
        private static List<string> pFiles;
        private static int pChaptersExistAction;
        private static bool pOverwrite;
        private static int pChapterInterval;
        private static bool pFinished = true;

        private static BackgroundWorker worker = new BackgroundWorker();

        //------------------------------------------------------
        //   External used Properties
        //------------------------------------------------------

        // delegate declaration
        public delegate void ChangingHandler(object sender, ProgressArgs pa);

        // event declaration
        public event ChangingHandler ProgressChanged;


        public void SetProgress(int p)
        {
            pProgress = p;

            ProgressArgs pa = new ProgressArgs(pProgress);
            ProgressChanged(this, pa); 
        }

        //--------------------

        public int Progress
        {
            get
            {
                return pProgress;
            }
 
            set
            {
                pProgress = value;
                ProgressArgs pa = new ProgressArgs(pProgress);
                ProgressChanged(this, pa); 
            }
        }

        public int ChapterInterval
        {
            get
            {
                return pChapterInterval;
            }

            set
            {
                pChapterInterval = value;
            }
        }

        public List<string> Files
        {
            get
            {
                return pFiles;
            }

            set
            {
                pFiles = value;
            }
        }

        public bool Overwrite
        {
            get
            {
                return pOverwrite;
            }

            set
            {
                pOverwrite = value;
            }
        }

        public bool Finished
        {
            get
            {
                return pFinished;
            }

            set
            {
                pFinished = value;
            }
        }

        /// <summary>
        /// What to do if the file has chapters.
        /// 1 = Replace; 2 = Remove; 3 = Skip File
        /// </summary>
        public int ChaptersExistAction
        {
            get
            {
                return pChaptersExistAction;
            }

            set
            {
                pChaptersExistAction = value;
            }
        }

        //------------------------------------------------------
        //   Accessable Functions
        //------------------------------------------------------

        public void Start()
        {

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerSupportsCancellation = true;

            Finished = false;
            worker.RunWorkerAsync();
            
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> mkvlist = Files;

            //for each mkv the user has added
            foreach (string s in mkvlist)
            {
                FileInfo fi = new FileInfo(s);

                //check if it already has chapters
                if (ChaptersExist(s))
                {
                    //the file already has chapters, check what the user want's to do
                    switch (ChaptersExistAction)
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
                                //The task was cancelled
                                e.Cancel = true;
                                worker.Dispose();
                            }
                            else
                            {
                                //check if the user want to overwrite
                                if (Overwrite)
                                {
                                    String newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(fi.FullName)) + ".mkv";
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
                        worker.Dispose();
                    }
                    else
                    {
                        if (Overwrite)
                        {

                            //Delete the input file
                            File.Delete(s);

                            //Rename -new file to original name
                            File.Move(file, s);

                        }
                    }
                }
            }
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress = e.ProgressPercentage;
            SetProgress(e.ProgressPercentage);
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Finished = true;

                //Display a messagebox with success message or error info

                if (e.Cancelled == false & e.Error == null)
                {

                    MessageBox.Show("DONE");

                }
                else if (e.Cancelled == true & e.Error == null)
                {

                    //Clean-up files
                    //String newFile = theFile.DirectoryName + "\\" + Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(theFile.FullName)) + ".mkv";

                    try
                    {
                        //File.Delete(newFile);
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

        public void Cancel()
        {
            worker.CancelAsync();
        }

        //------------------------------------------------------
        //   Internal used methods
        //------------------------------------------------------

        private void CreateChapterFile(int runTime)
        {
           decimal count = runTime / ChapterInterval;

            if (count < 0)
            {

                MessageBox.Show("Too high interval!");

            }
            else
            {

                count = Math.Floor(count);
                
                if (Properties.Settings.Default.firstChap00)
                {
                    count += 1;
                }

            }

            string path = "chapters.xml";
            int nmbr = Convert.ToInt32(count);
            int start;
            int extraval = 0;
            int[] time = { 00, 00 };
            int interval = ChapterInterval; // trackBar1.Value;

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

            for (start = 0 + extraval; start < nmbr; start++)
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
                int minutes = runTime;

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

        private string parseProgress(String Text)
        {

            String newText = Text.Replace("Progress: ", "").Replace("%", "");

            return newText;

        }

        private bool ChaptersExist(String file)
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

        private string InsertChapters(string file)
        {

            FileInfo info = new FileInfo(file);
            MediaInfo MI = new MediaInfo();

            MI.Open(info.FullName);

            decimal dd;
            dd = Math.Floor(decimal.Parse(MI.Get(StreamKind.Video, 0, "Duration")) / 60000);

            //Create chapter file
            CreateChapterFile(Convert.ToInt32(dd));

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

                if (worker.CancellationPending == true)
                {

                    MI.Close();

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

                        Progress = Convert.ToInt32(parseProgress(str));
                    }
                    else if (str.Contains("Error"))
                    { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

            }

            File.Delete(cpath);
            return info.DirectoryName + "\\" + newFileName;
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

                if (worker.CancellationPending == true)
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
                        Progress = Convert.ToInt32(parseProgress(str));
                    }
                    else if (str.Contains("Error"))
                    { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            return file;
        }
    }

    public class ProgressArgs : System.EventArgs
    {
        private int percentage;

        public ProgressArgs(int m)
        {
            percentage = m;
        }

        public int Percentage()
        {
            return percentage;
        }
    } 
}
