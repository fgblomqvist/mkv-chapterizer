using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using MediaInfoLib;

namespace MKV_Chapterizer
{
    public class Chapterizer
    {
        //Variables

        #region Delegates

        public delegate void FinishedEventHandler(object sender, RunWorkerCompletedEventArgs ps);
        public delegate void ProgressChangedEventHandler(object sender, ProgressChangedEventArgs pa);

        #endregion

        private int chapterizerProgress;
        private string status;
        private string error;
        private string workDir;

        private readonly BackgroundWorker chapterizeWorker = new BackgroundWorker();
        private readonly BackgroundWorker chapterfileWorker = new BackgroundWorker();
        private readonly BackgroundWorker chapterExtractorWorker = new BackgroundWorker();

        public Chapterizer()
        {
            //Bind the events for the chapterizeWorker
            chapterizeWorker.DoWork += new DoWorkEventHandler(chapterizeWorker_DoWork);
            chapterizeWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(chapterizeWorker_RunWorkerCompleted);
            chapterizeWorker.WorkerSupportsCancellation = true;

            //Bind the events for the chapterfileWorker
            chapterfileWorker.DoWork += new DoWorkEventHandler(chapterfileWorker_DoWork);
            chapterfileWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(chapterfileWorker_RunWorkerCompleted);
            chapterfileWorker.WorkerSupportsCancellation = true;

            //Bind the events for the chapterExtractorWorker
            chapterExtractorWorker.DoWork += new DoWorkEventHandler(chapterExtractorWorker_DoWork);
            chapterExtractorWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(chapterExtractorWorker_RunWorkerCompleted);
            chapterExtractorWorker.WorkerSupportsCancellation = true;
        }

        //------------------------------------------------------
        //   External used Properties and Enumerators
        //------------------------------------------------------

        #region Enums

        public enum Operations
        {
            Chapterize,
            Chapterfile,
            ExtractChapter,
        }

        public enum ChapterModes
        {
            Interval,
            File,
            ChapterSet,
        }

        #endregion

        #region Properties
        public string Status
        {
            get { return status; }

            set
            {
                status = value;
                var ps = new ProgressChangedEventArgs(0, status);
                StatusChanged(this, ps);
            }
        }

        public int Progress
        {
            get { return chapterizerProgress; }

            set
            {
                chapterizerProgress = value;
                var ps = new ProgressChangedEventArgs(chapterizerProgress, null);
                ProgressChanged(this, ps);
            }
        }

        public ChapterDBAccess.ChapterSet CustomChapterSet { get; set; }
        public List<string> Files { get; set; }
        public bool OverwriteOutput { get; set; }
        public bool IsBusy { get; set; }
        public bool ShowConsole { get; set; }
        public string MKVMergePath { get; set; }
        public string MKVExtractPath { get; set; }
        public string CustomChapterName { get; set; }
        public string CustomOutputName { get; set; }
        public bool ExtraChapterStart { get; set; }
        public bool ExtraChapterEnd { get; set; }

        /// <summary>
        /// What to do if the file has chapters.
        /// 1 = Replace; 2 = Remove; 3 = Skip File
        /// </summary>
        public int ChaptersExistAction { get; set; }

        /// <summary>
        /// Gets or sets the regex pattern used when generating raw chapter files.
        /// </summary>
        /// <value>
        /// The chapter creation pattern (regex).
        /// </value>
        public string ChapterCreationPattern { get; set; }

        /// <summary>
        /// Gets or sets the chapter interval.
        /// </summary>
        /// <value>
        /// The chapter interval in seconds.
        /// </value>
        public int ChapterInterval { get; set; }

        /// <summary>
        /// Gets or sets the custom chapter file.
        /// </summary>
        /// <value>
        /// The path to a chapter file which mkvmerge supports
        /// </value>
        public string CustomChapterFile { get; set; }

        public ChapterModes ChapterMode { get; set; }
        #endregion

        /// <summary>
        /// Log entries will be sent to this.
        /// </summary>
        public Action<string> WriteLog { get; set; }

        /// <summary>
        /// Error log entries will be sent to this.
        /// </summary>
        public Action<string> WriteError { get; set; }

        public event ProgressChangedEventHandler ProgressChanged;
        public event ProgressChangedEventHandler StatusChanged;
        public event FinishedEventHandler Finished;

        //------------------------------------------------------
        //   Constructor
        //------------------------------------------------------

        //------------------------------------------------------
        //   Accessable Functions
        //------------------------------------------------------

        public void Start(Operations operation)
        {
            if (operation == Operations.Chapterize)
            {
                //Create the working dir
                GenerateWorkingDir();

                IsBusy = true;
                chapterizeWorker.RunWorkerAsync();
            }
            else if (operation == Operations.Chapterfile)
            {
                //Output a chapterfile to the same directory as every file specified
                IsBusy = true;
                chapterfileWorker.RunWorkerAsync(Files);
            }
            else if (operation == Operations.ExtractChapter)
            {
                //Create the working dir
                GenerateWorkingDir();

                IsBusy = true;
                chapterExtractorWorker.RunWorkerAsync();
            }
        }

        public void Cancel()
        {
            chapterizeWorker.CancelAsync();
            chapterfileWorker.CancelAsync();
            chapterExtractorWorker.CancelAsync();
        }

        public void Clean()
        {
            if (Directory.Exists(workDir))
            {
                try
                {
                    Directory.Delete(workDir, true);
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

        #region Chapterizer Operations

        private void chapterizeWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> mkvlist = Files;
            int doneMovies = 0;

            if (Files.Count > 1)
            {
                //Only report done count if it is a queue you are processing
                Status = doneMovies.ToString() + "/" + Files.Count.ToString();
            }

            //for each mkv the user has added
            foreach (string s in mkvlist)
            {
                if (Files.Count > 1)
                {
                    //Only report done count if it is a queue you are processing
                    //Update status
                    doneMovies += 1;
                    Status = doneMovies.ToString() + "/" + Files.Count.ToString() + " " + Path.GetFileName(s);
                }

                var fi = new FileInfo(s);

                //check if it already has chapters
                if (ChaptersExist(s))
                {
                    //the file already has chapters, check what the user want's to do
                    FileInfo nfile;
                    switch (ChaptersExistAction)
                    {
                        case 1:
                            //Replace Them
                            WriteLog("Starting ReplaceChapters job");
                            object file;
                            file = ReplaceChapters(s);

                            if (file is string)
                            {
                                WriteLog("Job failed, either it got cancelled or an error was thrown");
                                e.Cancel = true;
                                chapterizeWorker.Dispose();

                                //Pass the new file to the complete event for cleaning
                                error = (string)file;
                                return;
                            }
                            
                            WriteLog("Succeeded replacing chapters");
                            nfile = (FileInfo)file;

                            WriteLog("Handling the finished file");
                            if (OverwriteOutput)
                            {
                                //Handle the finished file
                                HandleFinishedFile(nfile.FullName, s, OverwriteOutput);
                            }
                            else
                            {
                                HandleFinishedFile(nfile.FullName, Path.GetDirectoryName(s) + "\\" + nfile.Name, OverwriteOutput);
                            }

                            break;
                        case 2:
                            //Remove Them
                            WriteLog("Starting RemoveChapters job");
                            file = RemoveChapters(s);

                            if (file is string)
                            {
                                WriteLog("Job failed, either it got cancelled or an error was thrown");
                                e.Cancel = true;
                                chapterizeWorker.Dispose();

                                //Pass the new file to the complete event for cleaning
                                error = (string)file;
                                return;
                            }

                            WriteLog("Succeeded removing chapters");
                            nfile = (FileInfo)file;

                            //check if the user want to overwrite
                            WriteLog("Handling the finished file");
                            if (OverwriteOutput)
                            {
                                HandleFinishedFile(nfile.FullName, fi.FullName, OverwriteOutput);
                            }
                            else
                            {
                                HandleFinishedFile(nfile.FullName, Path.GetDirectoryName(s) + "\\" + nfile.Name, OverwriteOutput);
                            }
                            break;

                        case 3:
                            //Skip the file
                            break;
                    }
                }
                else
                {
                    //Insert new chapters
                    WriteLog("Starting InsertChapters job");
                    object file = InsertChapters(s);
                    if (file is string)
                    {
                        WriteLog("Job failed, either it got cancelled or an error was thrown");
                        e.Cancel = true;
                        chapterizeWorker.Dispose();

                        //Pass the new file to the complete event for cleaning
                        error = (string)file;
                        return;
                    }
                    
                    WriteLog("Succeeded inserting chapters");
                    var nfile = (FileInfo)file;

                    WriteLog("Handling the finished file");
                    if (OverwriteOutput)
                    {
                        //Rename -new file to original name
                        HandleFinishedFile(nfile.FullName, s, OverwriteOutput);
                    }
                    else
                    {
                        HandleFinishedFile(nfile.FullName, Path.GetDirectoryName(s) + "\\" + nfile.Name, OverwriteOutput);
                    }
                }
            }
        }

        private void chapterizeWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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
                    WriteError("Failed to delete leftovers: " + ex.ToString());
                    MessageBox.Show("Failed to delete leftovers!:" + Environment.NewLine + ex.Message);
                }

                ps = new RunWorkerCompletedEventArgs(null, null, false);
                Finished(this, ps);
            }
            else if (e.Cancelled & e.Error == null)
            {
                //Clean-up files

                try
                {
                    File.Delete(error);
                }
                catch (Exception ex)
                {
                    WriteError("Failed to delete leftovers:" + ex.ToString());
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

        #endregion

        #region Chapter File Operations

        private void chapterfileWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OutputChapterFiles((List<string>)e.Argument, ChapterInterval);
        }

        private void chapterfileWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedEventArgs ps;

            if (e.Cancelled == false & e.Error == null)
            {
                //The operation completed successfully
                ps = new RunWorkerCompletedEventArgs(null, null, false);
                Finished(this, ps);
            }
            else if (e.Cancelled & e.Error == null)
            {
                //The user cancelled
                ps = new RunWorkerCompletedEventArgs(null, null, true);
                Finished(this, ps);
            }
            else if (e.Cancelled == false & e.Error != null)
            {
                //There was an error
                ps = new RunWorkerCompletedEventArgs(null, e.Error, false);
                Finished(this, ps);
            }

            IsBusy = false;
        }

        #endregion

        #region Chapter Extractor Operations

        private void chapterExtractorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string> mkvlist = Files;
            int doneMovies = 0;

            if (Files.Count > 1)
            {
                //Only report done count if it is a queue you are processing
                Status = doneMovies.ToString() + "/" + Files.Count.ToString();
            }

            //for each mkv the user has added
            foreach (string s in mkvlist)
            {
                if (Files.Count > 1)
                {
                    //Only report done count if it is a queue you are processing
                    //Update status
                    doneMovies += 1;
                    Status = doneMovies.ToString() + "/" + Files.Count.ToString() + " " + Path.GetFileName(s);
                }

                //check if it has chapters
                if (ChaptersExist(s))
                {
                    //the file has chapters, extract them
                    string path = ExtractChapters(s);

                    //Format it right before you output it to the mkv's folder
                    ReFormatChapters(path);

                    //Now move them to the mkv's folder
                    try
                    {
                        File.Move(path, Path.GetDirectoryName(s) + "\\chapters.txt");
                    }
                    catch (IOException)
                    {
                        //File already exists, if overwrite, then overwrite
                        if (OverwriteOutput)
                        {
                            File.Delete(Path.GetDirectoryName(s) + "\\chapters.txt");
                            File.Move(path, Path.GetDirectoryName(s) + "\\chapters.txt");
                        }
                    }
                }

                Progress = 100;
            }
        }

        private void chapterExtractorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompletedEventArgs ps;

            if (e.Cancelled == false & e.Error == null)
            {
                //The operation completed successfully
                ps = new RunWorkerCompletedEventArgs(null, null, false);
                Finished(this, ps);
            }
            else if (e.Cancelled & e.Error == null)
            {
                //The user cancelled
                ps = new RunWorkerCompletedEventArgs(null, null, true);
                Finished(this, ps);
            }
            else if (e.Cancelled == false & e.Error != null)
            {
                //There was an error
                ps = new RunWorkerCompletedEventArgs(null, e.Error, false);
                Finished(this, ps);
            }

            IsBusy = false;
        }

        #endregion

        /// <summary>
        /// Creates a chapter set.
        /// </summary>
        /// <param name="runtime">The runtime of the movie in seconds.</param>
        /// <param name="interval">The interval in seconds.</param>
        /// <returns></returns>
        public ChapterDBAccess.ChapterSet CreateChapterSet(int runtime, int interval)
        {
            ChapterDBAccess.ChapterSet newChapterSet = new ChapterDBAccess.ChapterSet();
            ChapterDBAccess.Chapter chapter;

            decimal count = runtime / interval;

            int start;
            int extraval = 0;
            int[] time = {00, 00, 00};

            if (ExtraChapterStart)
            {
                chapter = new ChapterDBAccess.Chapter();
                chapter.Time = TimeSpan.Parse(time[0] + ":" + time[1] + ":" + time[2]);
                chapter.Name = CustomChapterName.Replace("%N", "1").Replace("%T", string.Format("{0:00}:{1:00}:{2:00}", (int)chapter.Time.TotalHours, chapter.Time.Minutes, chapter.Time.Seconds));
                newChapterSet.Chapters.Add(chapter);
                extraval = 1;
            }

            for (start = 0 + extraval; start <= count; start++)
            {
                time[2] += interval;

                while (time[2] >= 60)
                {
                    time[2] -= 60;
                    time[1] += 1;
                }

                while (time[1] >= 60)
                {
                    time[1] -= 60;
                    time[0] += 1;
                }

                chapter = new ChapterDBAccess.Chapter();
                chapter.Time = TimeSpan.Parse(time[0] + ":" + time[1] + ":" + time[2]);
                chapter.Name = CustomChapterName.Replace("%N", Convert.ToString(start + 1)).Replace("%T", string.Format("{0:00}:{1:00}:{2:00}", (int)chapter.Time.TotalHours, chapter.Time.Minutes, chapter.Time.Seconds));

                newChapterSet.Chapters.Add(chapter);
            }

            if (ExtraChapterEnd)
            {
                int hours = 0;
                int minutes = 0;
                int seconds = runtime;

                while (seconds >= 60)
                {
                    seconds -= 60;
                    minutes += 1;
                }

                while (minutes >= 60)
                {
                    minutes -= 60;
                    hours += 1;
                }

                chapter = new ChapterDBAccess.Chapter();
                chapter.Time = TimeSpan.Parse(hours.ToString() + ":" + minutes.ToString() + ":" + seconds.ToString());
                chapter.Name = CustomChapterName.Replace("%N", Convert.ToString(start + 1)).Replace("%T", string.Format("{0:00}:{1:00}:{2:00}", (int)chapter.Time.TotalHours, chapter.Time.Minutes, chapter.Time.Seconds));
            }

            return newChapterSet;
        }

        public string CreateChapterFile(ChapterDBAccess.ChapterSet chapterSet, string path)
        {
            return CreateChapterFile(chapterSet, path, true);
        }

        public string CreateChapterFile(ChapterDBAccess.ChapterSet chapterSet, string path, bool overwrite)
        {
            WriteLog("Creating chapterfile");

            if (Path.GetExtension(path) != ".xml")
            {
                path += "\\chapters.xml";
            }

            if (File.Exists(path))
            {
                //If the there already exists a file with the same name as the specified, check if we should overwrite
                if (!overwrite)
                {
                    //If we shouldn't overwrite, exit
                    return null;
                }

                //Delete the existing file
                File.Delete(path);
            }

            var xwrite = new XmlTextWriter(path, Encoding.UTF8);

            xwrite.WriteStartDocument();
            xwrite.Formatting = Formatting.Indented;
            xwrite.Indentation = 2;
            xwrite.WriteDocType("Tags", null, "matroskatags.dtd", null);
            xwrite.WriteStartElement("Chapters");
            xwrite.WriteStartElement("EditionEntry");

            foreach (ChapterDBAccess.Chapter chapter in chapterSet.Chapters)
            {
                xwrite.WriteStartElement("ChapterAtom");
                xwrite.WriteElementString("ChapterTimeStart", string.Format("{0:00}:{1:00}:{2:00}.000000000", chapter.Time.Hours, chapter.Time.Minutes, chapter.Time.Seconds));
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
            WriteLog("Checking if file contains chapters: " + file);
            var info = new MediaInfo();
            info.Open(file);

            info.Option("Inform", "XML");
            info.Option("Complete");
            if (info.Inform().Contains("<track type=\"Menu\""))
            {
                WriteLog("It contains chapters");
                return true;
            }
            
            WriteLog("It didn't contain chapters");
            return false;
        }

        private void ReFormatChapters(string path)
        {
            string chapterText = File.ReadAllText(path);
            Regex chapterRegex = new Regex(@"\d{2}:\d{2}:\d{2}.\d{3}|(?<=CHAPTER\d{2,4}NAME=).*");
            MatchCollection chapterMatches = chapterRegex.Matches(chapterText);
            Dictionary<string, string> chapters = new Dictionary<string, string>();

            //Every even match is a chapter time and the following odd match is the chapter name
            for (int i = 0; i < chapterMatches.Count; i += 2)
            {
                chapters.Add(chapterMatches[i].Value, chapterMatches[i + 1].Value.TrimEnd( '\r', '\n' ));
            }

            //Now format the chapters according to how the users has set it
            if (ChapterCreationPattern != null)
            {
                string chapterPattern = ChapterCreationPattern.Split(Convert.ToChar(0))[0];
                string chapterSeparator = ChapterCreationPattern.Split(Convert.ToChar(0))[1];
                string chaptersText = string.Empty;
                int i = 0; //Chapter number

                foreach(KeyValuePair<string,string> keys in chapters)
                {
                    i++; //Increment chapter number
                    string chapter = Regex.Replace(chapterPattern, "%N", keys.Value);
                    chapter = Regex.Replace(chapter, "%T", keys.Key);
                    chapter = Regex.Replace(chapter, "%L", Environment.NewLine);
                    chapter = Regex.Replace(chapter, "%I", i.ToString());
                    chaptersText = (chaptersText != string.Empty) ? String.Join(chapterSeparator.Replace("%L", Environment.NewLine), chaptersText, chapter) : chapter;
                }

                File.WriteAllText(path, chaptersText);
            }
        }

        #region Chapter Operations

        private object InsertChapters(string file)
        {
            var info = new FileInfo(file);
            String cpath;

            if (ChapterMode == ChapterModes.ChapterSet)
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(CustomChapterSet, workDir + "\\chapters.xml");
            }
            else if (ChapterMode == ChapterModes.File)
            {
                WriteLog("Using pre-set chapterfile: " + CustomChapterFile);
                cpath = CustomChapterFile;
            }
            else
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(CreateChapterSet(GetMovieRuntime(info.FullName), ChapterInterval), workDir + "\\chapters.xml");
            }

            string newFileName = CustomOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            var prcinfo = new ProcessStartInfo();
            var prc = new Process();

            Char q = Convert.ToChar(34);
            String newpath = q + workDir + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String args = "-o " + newpath + " --chapters " + q + cpath + q + " --compression -1:none " + oldpath;

            prcinfo.FileName = MKVMergePath;
            prcinfo.Arguments = args;
            prcinfo.RedirectStandardOutput = true;
            prcinfo.RedirectStandardError = true;

            prcinfo.CreateNoWindow = !ShowConsole;

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

                if (chapterizeWorker.CancellationPending)
                {
                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }

                    if (ChapterMode != ChapterModes.File)
                    {
                        File.Delete(cpath);
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
                    {
                        MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (ChapterMode != ChapterModes.File)
            {
                File.Delete(cpath);
            }

            return new FileInfo(workDir + "\\" + newFileName);
        }

        private object RemoveChapters(string file)
        {
            //Remove the chapters on the file and return the path to the new file

            var info = new FileInfo(file);

            String newFileName = CustomOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            Char q = Convert.ToChar(34);
            String newpath = q + workDir + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String pArgs = "-o " + newpath + " --no-chapters --compression -1:none " + oldpath;

            var prcinfo = new ProcessStartInfo();
            var prc = new Process();

            prcinfo.FileName = MKVMergePath;
            prcinfo.Arguments = pArgs;
            prcinfo.RedirectStandardOutput = true;
            prcinfo.RedirectStandardError = true;

            prcinfo.CreateNoWindow = !ShowConsole;

            prcinfo.UseShellExecute = false;

            prc.StartInfo = prcinfo;
            prc.Start();

            string str;

            while ((str = prc.StandardOutput.ReadLine()) != null)
            {
                //Check for Cancellation

                if (chapterizeWorker.CancellationPending)
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
                    {
                        MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return new FileInfo(workDir + "\\" + newFileName);
        }

        private object ReplaceChapters(string file)
        {
            //Remove the chapters

            var info = new FileInfo(file);

            String newFileName = CustomOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info.FullName)) + ".mkv";

            Char q = Convert.ToChar(34);
            String newpath = q + workDir + "\\" + newFileName + q;
            String oldpath = q + info.FullName + q;

            String args = "-o " + newpath + " --no-chapters --compression -1:none " + oldpath;

            var prcinfo = new ProcessStartInfo();
            var prc = new Process();

            prcinfo.FileName = MKVMergePath;
            prcinfo.Arguments = args;
            prcinfo.RedirectStandardOutput = true;
            prcinfo.RedirectStandardError = true;

            prcinfo.CreateNoWindow = !ShowConsole;

            prcinfo.UseShellExecute = false;

            prc.StartInfo = prcinfo;
            prc.Start();

            string str;

            while ((str = prc.StandardOutput.ReadLine()) != null)
            {
                //Check for Cancellation

                if (chapterizeWorker.CancellationPending)
                {
                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }
                    return workDir + "\\" + newFileName;
                }
                //End Check

                if (str == "")
                {
                    continue;
                }

                if (str.Contains("Progress"))
                {
                    Progress = Convert.ToInt32(parseProgress(str)) / 2;
                }
                else if (str.Contains("Error"))
                {
                    MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //Add the new

            var info2 = new FileInfo(workDir + "\\" + newFileName);
            var MI = new MediaInfo();

            MI.Open(info2.FullName);

            string cpath;

            if (ChapterMode == ChapterModes.ChapterSet)
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(CustomChapterSet, workDir + "\\chapters.xml");
            }
            else if (ChapterMode == ChapterModes.File)
            {
                WriteLog("Using pre-set chapterfile: " + CustomChapterFile);
                cpath = CustomChapterFile;
            }
            else
            {
                WriteLog("Creating chapterfile: " + workDir + "\\chapters.xml");
                cpath = CreateChapterFile(CreateChapterSet(GetMovieRuntime(info.FullName), ChapterInterval), workDir + "\\chapters.xml");
            }

            string newFileName2 = CustomOutputName.Replace("%O", Path.GetFileNameWithoutExtension(info2.FullName)) + ".mkv";

            newpath = q + workDir + "\\" + newFileName2 + q;
            oldpath = q + info2.FullName + q;

            args = "-o " + newpath + " --chapters " + q + cpath + q + " --compression -1:none " + oldpath;

            prcinfo.Arguments = args;

            prc.StartInfo = prcinfo;
            prc.Start();

            while ((str = prc.StandardOutput.ReadLine()) != null)
            {
                //Check for Cancellation

                if (chapterizeWorker.CancellationPending)
                {
                    MI.Close();

                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }

                    if (ChapterMode != ChapterModes.File)
                    {
                        File.Delete(cpath);
                    }
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
                    {
                        MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            if (ChapterMode != ChapterModes.File)
            {
                File.Delete(cpath);
            }

            File.Delete(workDir + "\\" + newFileName);
            File.Move(workDir + "\\" + newFileName2, workDir + "\\" + newFileName);

            return new FileInfo(workDir + "\\" + newFileName);
        }

        private string ExtractChapters(string mkvPath)
        {
            //Extract the chapters to a temp location and return the location
            FileInfo info = new FileInfo(mkvPath);

            Char q = Convert.ToChar(34);
            String newpath = q + workDir + "\\chapters.txt" + q;
            String oldpath = q + info.FullName + q;

            String pArgs = "chapters " + oldpath + " --simple --redirect-output " + newpath;

            ProcessStartInfo prcinfo = new ProcessStartInfo();
            Process prc = new Process();

            prcinfo.FileName = MKVExtractPath;
            prcinfo.Arguments = pArgs;
            prcinfo.RedirectStandardOutput = true;
            prcinfo.RedirectStandardError = true;
            prcinfo.CreateNoWindow = !ShowConsole;
            prcinfo.UseShellExecute = false;
            prc.StartInfo = prcinfo;
            prc.Start();

            string str;

            while ((str = prc.StandardOutput.ReadLine()) != null)
            {
                //Check for Cancellation
                if (chapterExtractorWorker.CancellationPending)
                {
                    if (!prc.WaitForExit(500))
                    {
                        prc.Kill();
                        Thread.Sleep(1000);
                    }
                    return workDir + "\\chapters.txt";
                }
                //End Check

                if (str != "")
                {
                    if (str.Contains("Progress"))
                    {
                        Progress = Convert.ToInt32(parseProgress(str));
                    }
                    else if (str.Contains("Error"))
                    {
                        MessageBox.Show(null, str, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            return workDir + "\\chapters.txt";
        }

        #endregion

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

        private void HandleFinishedFile(string path, string newpath, bool overwrite)
        {
            string inputDrive = Path.GetPathRoot(path);
            string outputDrive = Path.GetPathRoot(newpath);

            if (inputDrive == outputDrive)
            {
                WriteLog("Deleting the input file");
                File.Delete(newpath);
                WriteLog("Moving the finished file");
                File.Move(path, newpath);
                return;
            }

            WriteLog("Starting to copy the finished file");
            //Copy the file
            CopyFileCallback callback = new CopyFileCallback(CopyProgressChanged);
            try
            {
                AdvancedFileHandling.CopyFile(path, newpath, CopyFileOptions.None, callback);
            }
            catch (IOException)
            {
                //Assume the user cancelled
                return;
            }

            //Then delete the input
            File.Delete(path);
        }

        private CopyFileCallbackAction CopyProgressChanged(string source, string destination, object state, long totalFileSize, long totalBytesTransferred)
        {
            if (chapterizeWorker.CancellationPending)
            {
                return CopyFileCallbackAction.Cancel;
            }
            
            int progress = (int)Math.Round(Convert.ToDecimal(totalBytesTransferred / (decimal)totalFileSize * 100), 0, MidpointRounding.ToEven);
            
            Status = string.Format(OverwriteOutput ? "Overwriting old file: {0}%" : "Moving chapterized file: {0}%", progress.ToString());

            return CopyFileCallbackAction.Continue;
        }

        /// <summary>
        /// Gets the movie runtime in seconds
        /// </summary>
        /// <param name="movie">The movie.</param>
        /// <returns></returns>
        public int GetMovieRuntime(string movie)
        {
            var MI = new MediaInfo();
            MI.Open(movie);

            decimal dd = Math.Floor(decimal.Parse(MI.Get(StreamKind.Video, 0, "Duration")) / 1000);
            MI.Close();

            return Convert.ToInt32(dd);
        }

        public void OutputChapterFiles(List<string> mkvlist, int interval)
        {
            var progressIncrement = (int)Math.Floor(100 / (decimal)mkvlist.Count);
            Progress = 0;

            foreach (string mkv in mkvlist)
            {
                //Try to output a chapterfile at the files destination
                int runtime = GetMovieRuntime(mkv);
                ChapterDBAccess.ChapterSet chapterSet = CreateChapterSet(runtime, interval);
                CreateChapterFile(chapterSet, Path.GetDirectoryName(mkv));
                //Increment progress
                Progress += progressIncrement;
            }
        }
    }
}