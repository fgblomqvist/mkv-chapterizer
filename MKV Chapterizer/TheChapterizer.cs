﻿using System;
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
        private static bool pIsBusy = false;
        private static string pStatus;
        private static string pError;
        private static ChapterDBAccess.ChapterSet pChapterSet;
        private static string pMKVMergePath;
        private static string pCustomChapterName;
        private static string workDir;
        private static TextWriter pLogWriter;
        private static TextWriter pErrorWriter;

        private static BackgroundWorker worker = new BackgroundWorker();
        
        //------------------------------------------------------
        //   External used Properties
        //------------------------------------------------------

        // delegate declaration
        public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs pa);
        public delegate void FinishedEventHandler(object sender, RunWorkerCompletedEventArgs ps);
        // event declaration
        public event ProgressChangedEventHandler ProgressChanged;
        public event ProgressChangedEventHandler StatusChanged;
        public event FinishedEventHandler Finished;

        public string Status
        {
            get
            {
                return pStatus;
            }

            set
            {
                pStatus = value;
                ProgressChangedEventArgs ps = new ProgressChangedEventArgs(0, pStatus);
                StatusChanged(this, ps);
            }
        }

        public int Progress
        {
            get
            {
                return pProgress;
            }
 
            set
            {
                pProgress = value;
                ProgressChangedEventArgs ps = new ProgressChangedEventArgs(pProgress, null);
                ProgressChanged(this, ps); 
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

        public ChapterDBAccess.ChapterSet ChapterSet
        {
            get
            {
                return pChapterSet;
            }

            set
            {
                pChapterSet = value;
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

        public bool IsBusy
        {
            get
            {
                return pIsBusy;
            }

            set
            {
                pIsBusy = value;
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

        public string MKVMergePath
        {
            get
            {
                return pMKVMergePath;
            }

            set
            {
                pMKVMergePath = value;
            }
        }

        public string CustomChapterName
        {
            get
            {
                return pCustomChapterName;
            }

            set
            {
                pCustomChapterName = value;
            }
        }

        /// <summary>
        /// Gets or sets the TextWriter that the chapterizer should write log entries to.
        /// </summary>
        public TextWriter LogWriter
        {
            get
            {
                return pLogWriter;
            }

            set
            {
                pLogWriter = value;
            }
        }

        /// <summary>
        /// Gets or sets the TextWriter that the chapterizer should write errorlog entries to.
        /// </summary>
        public TextWriter ErrorWriter
        {
            get
            {
                return pErrorWriter;
            }

            set
            {
                pErrorWriter = value;
            }
        }
        
        //------------------------------------------------------
        //   Constructor
        //------------------------------------------------------

        public Chapterizer()
        {
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerSupportsCancellation = true;
        }

        //------------------------------------------------------
        //   Accessable Functions
        //------------------------------------------------------

        public void Start()
        {
            //Create the working dir
            GenerateWorkingDir();

            IsBusy = true;
            worker.RunWorkerAsync();
        }

        public void Cancel()
        {
            worker.CancelAsync();
        }

        public void Clean()
        {
            if (Directory.Exists(workDir))
            {
                try
                {
                    Directory.Delete(workDir);
                }
                catch (Exception ex)
                {
                    WriteError("Failed to delete working directory: " + ex.ToString());
                }
            }
        }

        //------------------------------------------------------
        //   Internal used methods
        //------------------------------------------------------

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> mkvlist = Files;
            int doneMovies = 0;

            Status = doneMovies.ToString() + "/" + Files.Count.ToString() + ";";

            //for each mkv the user has added
            foreach (string s in mkvlist)
            {
                //Update status
                doneMovies += 1;
                Status = doneMovies.ToString() + "/" + Files.Count.ToString() + ";" + Path.GetFileName(s);

                FileInfo fi = new FileInfo(s);

                //check if it already has chapters
                if (ChaptersExist(s))
                {
                    //the file already has chapters, check what the user want's to do
                    switch (ChaptersExistAction)
                    {
                        case 1:
                            //Replace Them
                            object file;
                            file = ReplaceChapters(s);

                            if (file is string)
                            {
                                e.Cancel = true;
                                worker.Dispose();

                                //Pass the new file to the complete event for cleaning
                                pError = (string)file;
                                return;
                            }
                            else
                            {
                                FileInfo nfile = (FileInfo)file;

                                if (Overwrite)
                                {
                                    //Delete the input file
                                    File.Delete(s);
                                    //Rename -new file to original name
                                    File.Move(nfile.FullName, s);
                                }
                                else
                                {
                                    //Move the -new file from workdir to orginial dir
                                    File.Move(nfile.FullName, Path.GetDirectoryName(s) + "\\" + nfile.Name);
                                }
                            }

                            break;
                        case 2:
                            //Remove Them
                            file = RemoveChapters(s);

                            if (file is string)
                            {
                                e.Cancel = true;
                                worker.Dispose();

                                //Pass the new file to the complete event for cleaning
                                pError = (string)file;
                                return;
                            }
                            else
                            {
                                FileInfo nfile = (FileInfo)file;
                                //check if the user want to overwrite
                                if (Overwrite)
                                {
                                    File.Delete(fi.FullName);
                                    File.Move(nfile.FullName, fi.FullName);
                                }
                                else
                                {
                                    //Move the -new file from workdir to orginial dir
                                    File.Move(nfile.FullName, Path.GetDirectoryName(s) + "\\" + nfile.Name);
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
                    object file = InsertChapters(s);
                    if (file is string)
                    {
                        e.Cancel = true;
                        worker.Dispose();

                        //Pass the new file to the complete event for cleaning
                        pError = (string)file;
                        return;
                    }
                    else
                    {
                        FileInfo nfile = (FileInfo)file;

                        if (Overwrite)
                        {
                            //Delete the input file
                            File.Delete(s);

                            //Rename -new file to original name
                            File.Move(nfile.FullName, s);
                        }
                        else
                        {
                            //Move the -new file from workdir to orginial dir
                            File.Move(nfile.FullName, Path.GetDirectoryName(s) + "\\" + nfile.Name);
                        }
                    }
                }
            }
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedEventArgs ps;

            if (e.Cancelled == false & e.Error == null)
            {
                
                //Delete any file in the e.Result
                try
                {
                    if (File.Exists((string)e.Result))
                    {
                        File.Delete((string)e.Result);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete leftovers!:" + Environment.NewLine + ex.Message);
                }

                ps = new RunWorkerCompletedEventArgs(null, null, false);
                Finished(this, ps);
            }
            else if (e.Cancelled == true & e.Error == null)
            {

                //Clean-up files

                try
                {
                    File.Delete(pError);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to delete leftovers!:" + Environment.NewLine + ex.Message);
                }

                ps = new RunWorkerCompletedEventArgs(null, null, true);
                Finished(this, ps);

            }
            else if (e.Cancelled == false & e.Error != null)
            {
                ps = new RunWorkerCompletedEventArgs(null, e.Error, false);
                Finished(this, ps);
            }

            IsBusy = false;
        }

        private ChapterDBAccess.ChapterSet CreateChapterSet(int runTime)
        {
            ChapterDBAccess.ChapterSet chapterSet = new ChapterDBAccess.ChapterSet();
            ChapterDBAccess.Chapter chapter;

            decimal count = runTime / ChapterInterval;

            int nmbr = Convert.ToInt32(count);
            int start;
            int extraval = 0;
            int[] time = { 00, 00 };
            int interval = ChapterInterval; // trackBar1.Value;

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

            if (Properties.Settings.Default.firstChap00)
            {
                chapter = new ChapterDBAccess.Chapter();
                chapter.Time = TimeSpan.Parse(time[0] + ":" + time[1]);
                chapter.Name = CustomChapterName.Replace("%N", "1").Replace("%T", string.Format("{0:00}:{1:00}:{2:00}", (int)chapter.Time.TotalHours, chapter.Time.Minutes, chapter.Time.Seconds));
                chapterSet.Chapters.Add(chapter);
                extraval = 1;
            }

            for (start = 0 + extraval; start <= nmbr; start++)
            {
                time[1] += interval;

                if (time[1] >= 60)
                {
                    time[0] += 1;
                    time[1] -= 60;
                }

                chapter = new ChapterDBAccess.Chapter();
                chapter.Time = TimeSpan.Parse(time[0] + ":" + time[1]);
                chapter.Name = CustomChapterName.Replace("%N", Convert.ToString(start + 1)).Replace("%T", string.Format("{0:00}:{1:00}:{2:00}", (int)chapter.Time.TotalHours, chapter.Time.Minutes, chapter.Time.Seconds));

                chapterSet.Chapters.Add(chapter);
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

                chapter = new ChapterDBAccess.Chapter();
                chapter.Time = TimeSpan.Parse(hours.ToString() + ":" + minutes.ToString());
                chapter.Name = CustomChapterName.Replace("%N", Convert.ToString(start + 1)).Replace("%T", string.Format("{0:00}:{1:00}:{2:00}", (int)chapter.Time.TotalHours, chapter.Time.Minutes, chapter.Time.Seconds));
            }

            return chapterSet;
        }

        private string CreateChapterFile(ChapterDBAccess.ChapterSet chapterSet, string path)
        {

            XmlTextWriter xwrite = new XmlTextWriter(path, System.Text.Encoding.UTF8);

            xwrite.WriteStartDocument();
            xwrite.Formatting = Formatting.Indented;
            xwrite.Indentation = 2;
            xwrite.WriteDocType("Tags", null, "matroskatags.dtd", null);
            xwrite.WriteStartElement("Chapters");
            xwrite.WriteStartElement("EditionEntry");

            foreach(ChapterDBAccess.Chapter chapter in chapterSet.Chapters)
            {
                xwrite.WriteStartElement("ChapterAtom");
                xwrite.WriteElementString("ChapterTimeStart", string.Format("{0:00}:{1:00}:{2:00}.000000000", (int)chapter.Time.Hours, chapter.Time.Minutes, chapter.Time.Seconds));
                xwrite.WriteStartElement("ChapterDisplay");
                xwrite.WriteElementString("ChapterString", chapter.Name);
                xwrite.WriteEndElement();
                xwrite.WriteEndElement();
            }

            xwrite.WriteEndElement();
            xwrite.WriteEndElement();
            xwrite.Close();

            return path;
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

        private object InsertChapters(string file)
        {

            FileInfo info = new FileInfo(file);
            MediaInfo MI = new MediaInfo();

            MI.Open(info.FullName);

            decimal dd;
            dd = Math.Floor(decimal.Parse(MI.Get(StreamKind.Video, 0, "Duration")) / 60000);

            String cpath;

            if (ChapterSet == null)
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(CreateChapterSet(Convert.ToInt32(dd)), workDir + "\\chapters.xml");
            }
            else
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(ChapterSet, workDir + "\\chapters.xml");
            }

            String newFileName = null;

            newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            ProcessStartInfo prcinfo = new ProcessStartInfo();
            Process prc = new Process();

            Char q = Convert.ToChar(34);
            String newpath = q + workDir + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String args = "-o " + newpath + " --chapters " + q + cpath + q + " --compression -1:none " + oldpath;

            prcinfo.FileName = MKVMergePath;
            prcinfo.Arguments = args;
            prcinfo.RedirectStandardOutput = true;
            prcinfo.RedirectStandardError = true;
            prcinfo.CreateNoWindow = true;
            prcinfo.UseShellExecute = false;

            prc.StartInfo = prcinfo;

            WriteLog("Starting mkvmerge.exe");
            WriteLog("Arguments: " + args);

            try
            {
                prc.Start();
            }
            catch (Exception ex)
            {
                WriteError(string.Format("Failed to start mkvmerge.exe process: {0}", ex.ToString()));
            }

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

                    File.Delete(cpath);
                    return workDir + "\\" + newFileName;

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
            return new FileInfo(workDir + "\\" + newFileName);
        }

        private object RemoveChapters(string file)
        {
            //Remove the chapters on the file and return the path to the new file

            FileInfo info = new FileInfo(file);

            String newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            Char q = Convert.ToChar(34);
            String newpath = q + workDir + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String pArgs = "-o " + newpath + " --no-chapters --compression -1:none " + oldpath;

            ProcessStartInfo prcinfo = new ProcessStartInfo();
            Process prc = new Process();

            prcinfo.FileName = MKVMergePath;
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
                    return workDir + "\\" + newFileName;
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

            return new FileInfo(workDir + "\\" + newFileName);
        }

        private object ReplaceChapters(string file)
        {

            //Remove the chapters

            FileInfo info = new FileInfo(file);

            String newFileName = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            Char q = Convert.ToChar(34);
            String newpath = q + workDir + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String args = "-o " + newpath + " --no-chapters --compression -1:none " + oldpath;

            ProcessStartInfo prcinfo = new ProcessStartInfo();
            Process prc = new Process();

            prcinfo.FileName = MKVMergePath;
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

                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }
                    return workDir + "\\" + newFileName;
                }
                //End Check


                if (str != "")
                {
                    if (str.Contains("Progress"))
                    {
                        Progress = Convert.ToInt32(parseProgress(str)) / 2;
                    }
                    else if (str.Contains("Error"))
                    { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }

            FileInfo info2 = new FileInfo(workDir + "\\" + newFileName);
            MediaInfo MI = new MediaInfo();

            MI.Open(info2.FullName);

            decimal dd;
            dd = Math.Floor(decimal.Parse(MI.Get(StreamKind.Video, 0, "Duration")) / 60000);

            string cpath;
            if (ChapterSet == null)
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(CreateChapterSet(Convert.ToInt32(dd)), workDir + "\\chapters.xml");
            }
            else
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(ChapterSet, workDir + "\\chapters.xml");
            }

            string newFileName2 = Properties.Settings.Default.customOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info2.FullName)) + ".mkv";

            newpath = q + workDir + "\\" + newFileName2 + q;
            oldpath = q + info2.FullName + q;

            args = "-o " + newpath + " --chapters " + q + cpath + q + " --compression -1:none " + oldpath;

            prcinfo.Arguments = args;

            prc.StartInfo = prcinfo;
            prc.Start();

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

                    File.Delete(cpath);
                    return workDir + "\\" + newFileName;

                }
                //End Check


                if (str != "")
                {
                    if (str.Contains("Progress"))
                    {

                        Progress = 50 + Convert.ToInt32(parseProgress(str)) / 2;
                    }
                    else if (str.Contains("Error"))
                    { MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }

            }

            File.Delete(cpath);
            File.Delete(workDir + "\\" + newFileName);
            File.Move(workDir + "\\" + newFileName2, workDir + "\\" + newFileName);

            return new FileInfo(workDir + "\\" + newFileName);
        }

        private void GenerateWorkingDir()
        {
            string tempdir = Path.GetTempPath();
            string randomString = Guid.NewGuid().ToString();
            string workdir = tempdir + randomString;

            WriteLog("Creating working directory: " + workDir);

            try
            {
                Directory.CreateDirectory(workdir);
                workDir = workdir;
            }
            catch (Exception ex)
            {
                WriteError(string.Format("Failed to create working directory: {0}", ex.ToString()));
            }
        }

        private void WriteLog(string message)
        {
            if (LogWriter != null)
            {
                LogWriter.Write(message);
            }
        }

        private void WriteError(string message)
        {
            if (ErrorWriter != null)
            {
                ErrorWriter.Write(message);
            }
        }
    }
}
