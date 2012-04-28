/* 
 
  MKV Chapterizer
  ---------------------------
 
  Copyright © 2010-2012 Fredrik Blomqvist

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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using MediaInfoLib;
using System.Net;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Logger;

namespace MKV_Chapterizer
{

    public partial class MKVC : Form
    {

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr ptr);

        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

        private const int LOGPIXELSX = 88;
        private readonly IntPtr NULL = IntPtr.Zero;

        private static int duration;
        private static float screenMultiplier = 1.0F;
        private static int queueAction = 1;
        private static ChapterDBAccess.ChapterSet chapterSet;
        private static UIModes pUIMode;

        private delegate void ProcessChangedHandler(int percentage);
        private delegate void ObjectDelegate2(string status);

        private readonly Chapterizer thechapterizer;
        private readonly Log log = new Log("mkvc.log");

        public MKVC()
        {
            InitializeComponent();
            AddDragHandlers(this);

            foreach (Control ctl in Controls)
            {
                ctl.AllowDrop = true;
            }

            WriteLog("Initializing chapterizer");
            thechapterizer = new Chapterizer();
            thechapterizer.WriteLog = WriteLog;
            thechapterizer.WriteError = WriteError;

            thechapterizer.ProgressChanged += thechapterizer_ProgressChanged;
            thechapterizer.StatusChanged += thechapterizer_StatusChanged;
            thechapterizer.Finished += thechapterizer_Finished;

        }

        public bool ShowModeChange
        {
            set
            {
                //Change the ability to change mode
                btnSwitch.Enabled = value;

                if (value)
                {
                    SemiEnableLabel(lblMode);
                    SemiEnableLabel(lblModeValue);
                    ttInfo.SetToolTip(lblMode, "");
                    ttInfo.SetToolTip(lblModeValue, "");
                }
                else
                {
                    SemiDisableLabel(lblMode);
                    SemiDisableLabel(lblModeValue);

                    ttInfo.SetToolTip(lblMode, "You can only change mode when chapterizing single files!");
                    ttInfo.SetToolTip(lblModeValue, "You can only change mode when chapterizing single files!");
                }
            }
        }

        public enum ChapterMode
        {
            Interval,
            ChapterDB,
            ChapterFile
        }

        public enum UIModes
        {
            Simple,
            Advanced,
        }

        public enum UIStatuses
        {
            Input,
            AwaitFileDrop,
            RemoveChapters,
            Working,
        }

        public UIStatuses UIStatus
        {
            set
            {
                switch (value)
                {
                    case UIStatuses.Input:

                        WriteLog("Setting UI to Input mode");
                        lblChapterCount.Enabled = true;
                        lblNumOfChapters.Enabled = true;
                        lblChapterInterval.Enabled = true;
                        lblTrackbarValue.Enabled = true;
                        cboxUnit.Enabled = true;
                        chkboxOverwrite.Enabled = true;
                        tbarInterval.Enabled = true;
                        btnMerge.Enabled = true;
                        pnlChapterDB.Enabled = true;

                        lblStatus.Text = string.Empty;

                        ShowTutorialMessage = false;
                        ShowProgressBar = false;
                        pnlModeChange.Enabled = true;

                        btnAdd.Enabled = true;
                        btnRemove.Enabled = true;
                        btnAddFolder.Enabled = true;
                        btnImport.Enabled = true;
                        btnExport.Enabled = true;
                        lboxFiles.Enabled = true;

                        grpboxChapterFile.Enabled = true;

                        cboxUnit.SelectedIndex = 1;

                        grpboxMKVHasChapters.Enabled = !chkboxOutputChapterfile.Checked;
                        ShowModeChange = (UIMode == UIModes.Simple);

                        btnMerge.Text = "Chapterize";
                        progressBar.Value = 0;

                        break;

                    case UIStatuses.AwaitFileDrop:

                        WriteLog("Setting UI to await file drop");
                        tbarInterval.Enabled = false;
                        btnMerge.Enabled = false;
                        chkboxOverwrite.Enabled = false;

                        lblNumOfChapters.Enabled = false;
                        lblChapterCount.Text = string.Empty;
                        lblStatus.Text = string.Empty;
                        lblChapterInterval.Enabled = false;
                        lblTrackbarValue.Enabled = false;
                        lblStatus.Visible = false;

                        lboxFiles.Items.Clear();

                        ShowTutorialMessage = true;
                        ShowModeChange = false;
                        ShowProgressBar = false;

                        cboxUnit.SelectedIndex = 1;
                        cboxUnit.Enabled = false;

                        //Change mode to interval
                        Mode = ChapterMode.Interval;

                        //Set the tut message
                        lblTutorial.Text = "Start by either dropping a MKV file on me or switching to advanced mode";

                        btnMerge.Text = "Chapterize";
                        progressBar.Value = 0;

                        break;

                    case UIStatuses.RemoveChapters:

                        lblChapterCount.Enabled = false;
                        lblNumOfChapters.Enabled = false;
                        lblChapterInterval.Enabled = false;
                        lblTrackbarValue.Enabled = false;
                        cboxUnit.Enabled = false;
                        chkboxOverwrite.Enabled = true;
                        tbarInterval.Enabled = false;
                        btnMerge.Enabled = true;

                        ShowTutorialMessage = false;
                        pnlModeChange.Enabled = true;

                        btnAdd.Enabled = true;
                        btnRemove.Enabled = true;
                        btnAddFolder.Enabled = true;
                        btnImport.Enabled = true;
                        btnExport.Enabled = true;
                        lboxFiles.Enabled = true;
                        grpboxChapterFile.Enabled = true;
                        grpboxMKVHasChapters.Enabled = true;
                        ShowModeChange = false;

                        break;

                    case UIStatuses.Working:

                        tbarInterval.Enabled = false;
                        cboxUnit.Enabled = false;
                        chkboxOverwrite.Enabled = false;
                        btnAdd.Enabled = false;
                        btnRemove.Enabled = false;
                        lboxFiles.Enabled = false;
                        btnAdd.Enabled = false;
                        btnRemove.Enabled = false;
                        btnAddFolder.Enabled = false;
                        btnImport.Enabled = false;
                        btnExport.Enabled = false;
                        pnlChapterDB.Enabled = false;

                        grpboxChapterFile.Enabled = false;
                        grpboxMKVHasChapters.Enabled = false;

                        pnlModeChange.Enabled = false;

                        lblStatus.Text = string.Empty;
                        lblStatus.Visible = true;

                        //Show progressbar
                        ShowProgressBar = true;

                        btnMerge.Text = "Cancel";

                        break;
                }
            }
        }

        public UIModes UIMode
        {
            get { return pUIMode; }

            set
            {
                pUIMode = value;

                switch (value)
                {
                    case UIModes.Advanced:

                        WriteLog("Setting UI to Advanced mode");
                        tabControl.HideTabs = false;
                        lblChapterCount.Visible = false;
                        lblNumOfChapters.Visible = false;
                        chkboxOverwrite.Text = "Overwrite old files";
                        lblUIMode.Text = "Simple Mode";

                        break;

                    case UIModes.Simple:

                        WriteLog("Setting UI to Simple mode");
                        tabControl.HideTabs = true;
                        lblChapterCount.Visible = true;
                        lblNumOfChapters.Visible = true;
                        chkboxOverwrite.Text = "Overwrite old file";
                        lblUIMode.Text = "Advanced Mode";

                        //Navigate back to first tab in-case the user is on another tab
                        tabControl.SelectedIndex = 0;

                        break;

                }

                lboxFiles.Items.Clear();
            }
        }

        public ChapterMode Mode
        {
            get
            {
                if (pnlChapterDB.Visible)
                {
                    return ChapterMode.ChapterDB;
                }

                return pnlChapterFile.Visible ? ChapterMode.ChapterFile : ChapterMode.Interval;
            }

            set
            {
                if (value == ChapterMode.Interval)
                {
                    pnlInterval.Visible = true;
                    pnlInterval.BringToFront();
                    pnlChapterDB.Visible = false;
                    pnlChapterFile.Visible = false;
                    lblModeValue.Text = "Interval";
                }
                else if (value == ChapterMode.ChapterDB)
                {
                    pnlChapterDB.Visible = true;
                    pnlChapterDB.BringToFront();
                    pnlInterval.Visible = false;
                    pnlChapterFile.Visible = false;
                    lblModeValue.Text = "ChapterDB";
                }
                else if (value == ChapterMode.ChapterFile)
                {
                    pnlChapterFile.Visible = true;
                    pnlChapterFile.BringToFront();
                    pnlChapterDB.Visible = false;
                    pnlInterval.Visible = false;
                    lblModeValue.Text = "Chapter file";
                }
            }
        }

        public bool ShowTutorialMessage
        {
            get { return lblTutorial.Visible; }

            set
            {
                lblTutorial.Visible = value;
                pnlModeChange.Visible = !value;
            }
        }

        public bool ShowProgressBar
        {
            set
            {
                if (value)
                {
                    float y = 254 * screenMultiplier;
                    Size = new Size(Size.Width, (int)y);
                }
                else
                {
                    float y = 225 * screenMultiplier;
                    Size = new Size(Size.Width, (int)y);
                }
            }
        }

        public bool Portable
        {
            get
            {
                //Check if the regkey with the path to the program exists, if it doesn't it is portable
                RegistryKey key = Registry.LocalMachine.OpenSubKey
                    (Environment.Is64BitOperatingSystem ? "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\MKV Chapterizer" : "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\MKV Chapterizer");

                string value = null;

                try
                {
                    if (key != null)
                    {
                        value = (string) key.GetValue("InstallLocation", null);
                    }
                    else
                    {
                        return true;
                    }
                }
                catch
                {
                }

                return (value == null);
            }
        }

        public int ChapterCount
        {
            get { return Convert.ToInt32(lblChapterCount); }

            set
            {
                if (value < 0)
                {
                    MessageBox.Show("Too high interval!");
                    tbarInterval.Value = Properties.Settings.Default.defChapInterval;
                }
                else
                {
                    lblChapterCount.Text = value.ToString();
                }
            }
        }


        private void DragEnterHandler(object sender, DragEventArgs e)
        {

            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (UIMode == UIModes.Simple)
            {
                if (s != null && s.Count() != 1)
                {
                    e.Effect = DragDropEffects.None;
                    return;
                }
            }

            if (s == null)
            {
                return;
            }

            foreach (FileInfo fi in s.Select(i => new FileInfo(i)))
            {
                if (fi.Extension.ToLower() == ".mkv")
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

        private void DragDropHandler(object sender, DragEventArgs e)
        {
            WriteLog("User dropped mkv(s) on the UI");

            //temp: Clear the lboxFiles if no queue
            if (UIMode == UIModes.Simple)
            {
                lboxFiles.Items.Clear();
            }

            string[] s = (string[]) e.Data.GetData(DataFormats.FileDrop);
            bool exist = false;

            foreach (string str in s)
            {
                foreach (string itm in lboxFiles.Items)
                {
                    if (itm == str)
                    {
                        exist = true;
                    }
                }

                if (exist)
                {
                    continue;
                }

                lboxFiles.Items.Add(str);
                if (UIMode == UIModes.Advanced)
                {
                    tabControl.SelectedIndex = 1;
                }
            }

            FileInfo fi = new FileInfo(s[0]);

            //Get runtime of movie with MediaInfo
            WriteLog("Retrieving movie runtime");

            try
            {
                duration = thechapterizer.GetMovieRuntime(fi.FullName);
            }
            catch (Exception)
            {
                //The value fetched from MediaInfo is invalid, the mkv is probably corrupt or invalid
                MessageBox.Show("The mkv file you dropped is either corrupt or invalid, please choose another!", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UIMode == UIModes.Simple)
            {
                if (ChaptersExist(fi.FullName))
                {
                    ChaptersExist f = new ChaptersExist(this);

                    f.ShowDialog(this);

                    switch (f.Result)
                    {
                        case 0:

                            //Cancel
                            WriteLog("User chose to cancel mkv drop");
                            return;

                        case 1:

                            //Remove
                            WriteLog("User chose to de-chapterize the mkv");

                            btnMerge.Text = "De-Chapterize";
                            UIStatus = UIStatuses.RemoveChapters;

                            break;

                        case 2:

                            //Remove and Insert New
                            WriteLog("User chose to re-chapterize the mkv");

                            UIStatus = UIStatuses.Input;
                            btnMerge.Text = "Re-Chapterize";

                            //Calculate the number of chapters with default chapter interval
                            ChapterCount = CalcChapterCount(duration, ConvertToSeconds(tbarInterval.Value, cboxUnit.Text));
                            break;
                    }

                }
                else
                {
                    UIStatus = UIStatuses.Input;
                    //Calculate the number of chapters with default chapter interval
                    ChapterCount = CalcChapterCount(duration, ConvertToSeconds(tbarInterval.Value, cboxUnit.Text));
                }
            }
            else
            {
                UIStatus = UIStatuses.Input;
            }
        }

        private void EnableControls(bool RemoveMode)
        {
            if (RemoveMode)
            {
                tbarInterval.Enabled = false;
                lblChapterInterval.Enabled = false;
                lblTrackbarValue.Enabled = false;
                cboxUnit.Enabled = false;
                lblNumOfChapters.Enabled = false;
                lblChapterCount.Enabled = false;
            }
            else
            {
                tbarInterval.Enabled = true;
                lblChapterInterval.Enabled = true;
                lblTrackbarValue.Enabled = true;
                cboxUnit.Enabled = true;
                lblNumOfChapters.Enabled = true;
                lblChapterCount.Enabled = true;
            }

            btnMerge.Enabled = true;
            chkboxOverwrite.Enabled = true;
        }

        private bool ChaptersExist(String file)
        {
            MediaInfo info = new MediaInfo();
            info.Open(file);

            info.Option("Inform", "XML");
            info.Option("Complete");
            return info.Inform().Contains("<track type=\"Menu\"");
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            //Check if any file is selected
            if (lboxFiles.Items.Count < 1)
            {
                MessageBox.Show("Your queue is empty, please add at least one mkv to it!", "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                return;
            }

            if (UIMode == UIModes.Simple && ModifierKeys == Keys.Shift)
            {
                if (Mode == ChapterMode.ChapterFile)
                {
                    MessageBox.Show("Why would you want to create a chapter file out of a chapter file? duh!");
                    return;
                }

                if (CreateChapterFile(lboxFiles.Items[0].ToString()))
                {
                    UIStatus = UIStatuses.AwaitFileDrop;
                }
            }
            else
            {
                UseChapterizer();
            }
        }

        private bool CreateChapterFile(string moviePath)
        {
            //Only produce a chapter-file

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.AddExtension = true;
            dlg.CheckPathExists = true;
            dlg.DefaultExt = "xml";
            dlg.DereferenceLinks = true;
            dlg.FileName = "chapters.xml";
            dlg.Filter = "(*.xml)|*.xml";
            dlg.InitialDirectory = Path.GetDirectoryName(lboxFiles.Items[0].ToString());
            dlg.Title = "Please choose where to save the chapterfile";
            dlg.ValidateNames = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //Create a chapterset
                if (Mode == ChapterMode.Interval)
                {
                    //Create a chapterfile at chosen destination
                    thechapterizer.CustomChapterName = Properties.Settings.Default.customChapterName;
                    thechapterizer.CreateChapterFile(
                        thechapterizer.CreateChapterSet(thechapterizer.GetMovieRuntime(moviePath),
                                                        ConvertToSeconds(tbarInterval.Value, cboxUnit.Text)),
                        dlg.FileName);
                }
                else if (Mode == ChapterMode.ChapterDB && chapterSet != null)
                {
                    thechapterizer.CustomChapterSet = chapterSet;
                    thechapterizer.CreateChapterFile(chapterSet, dlg.FileName);
                }
                return true;
            }
            return false;
        }

        private int ConvertToSeconds(int p, string currentUnit)
        {
            switch (currentUnit)
            {
                case "seconds":
                    return p;
                case "minutes":
                    return p*60;
                case "hours":
                    return p*3600;
                default:
                    return p;
            }
        }

        private void UseChapterizer()
        {
            if (Properties.Settings.Default.customMKVMerge)
            {
                WriteLog(string.Format("Setting mkvmerge path: {0}", Properties.Settings.Default.customMKVMergePath));
                thechapterizer.MKVMergePath = Properties.Settings.Default.customMKVMergePath;
            }
            else
            {
                WriteLog(@"Setting mkvmerge path: mkvmerge\mkvmerge.exe");
                thechapterizer.MKVMergePath = "mkvmerge\\mkvmerge.exe";
            }

            thechapterizer.ChapterCreationPattern = Properties.Settings.Default.chapterPattern;
            thechapterizer.MKVExtractPath = "mkvmerge\\mkvextract.exe";
            thechapterizer.CustomChapterName = Properties.Settings.Default.customChapterName;
            thechapterizer.ShowConsole = Properties.Settings.Default.showConsole;
            thechapterizer.CustomOutputName = Properties.Settings.Default.customOutputName;
            thechapterizer.ExtraChapterEnd = Properties.Settings.Default.extraChapEnd;
            thechapterizer.ExtraChapterStart = Properties.Settings.Default.extraChapStart;

            if (btnMerge.Text == "Chapterize")
            {
                UIStatus = UIStatuses.Working;

                List<string> mkvList = lboxFiles.Items.Cast<string>().ToList();

                thechapterizer.Files = mkvList;
                thechapterizer.ChaptersExistAction = queueAction;

                if (Mode == ChapterMode.ChapterDB && chapterSet != null)
                {
                    thechapterizer.CustomChapterSet = chapterSet;
                    thechapterizer.ChapterMode = Chapterizer.ChapterModes.ChapterSet;
                }
                else if (Mode == ChapterMode.ChapterFile && !string.IsNullOrWhiteSpace(txtChapterFileLocation.Text))
                {
                    thechapterizer.CustomChapterFile = txtChapterFileLocation.Text;
                    thechapterizer.ChapterMode = Chapterizer.ChapterModes.File;
                }
                else
                {
                    thechapterizer.ChapterInterval = ConvertToSeconds(tbarInterval.Value, cboxUnit.Text);
                    thechapterizer.ChapterMode = Chapterizer.ChapterModes.Interval;
                }

                thechapterizer.OverwriteOutput = chkboxOverwrite.Checked;

            }
            else if (btnMerge.Text == "Re-Chapterize")
            {
                UIStatus = UIStatuses.Working;

                List<string> mkvList = lboxFiles.Items.Cast<string>().ToList();

                thechapterizer.Files = mkvList;
                thechapterizer.ChaptersExistAction = 1;

                if (pnlChapterDB.Visible && chapterSet != null)
                {
                    thechapterizer.CustomChapterSet = chapterSet;
                }
                else
                {
                    thechapterizer.ChapterInterval = ConvertToSeconds(tbarInterval.Value, cboxUnit.Text);
                }

                thechapterizer.OverwriteOutput = chkboxOverwrite.Checked;

            }
            else if (btnMerge.Text == "De-Chapterize")
            {
                UIStatus = UIStatuses.Working;

                List<string> mkvList = lboxFiles.Items.Cast<string>().ToList();

                thechapterizer.Files = mkvList;
                thechapterizer.ChaptersExistAction = 2;
                thechapterizer.ChapterInterval = ConvertToSeconds(tbarInterval.Value, cboxUnit.Text);
                thechapterizer.OverwriteOutput = chkboxOverwrite.Checked;

            }
            else if (btnMerge.Text == "Cancel")
            {
                //Cancel the ongoing merge
                WriteLog("User chose to cancel current operation");

                btnMerge.Text = "Cancelling...";
                thechapterizer.Cancel();
                return;
            }

            WriteLog("Starting chapterizer");

            if (UIMode == UIModes.Advanced)
            {
                if (!chkboxOutputChapterfile.Checked)
                {
                    thechapterizer.Start(Chapterizer.Operations.Chapterize);
                }
                else
                {
                    thechapterizer.Start(rbtnExtractChapters.Checked ? Chapterizer.Operations.ExtractChapter : Chapterizer.Operations.Chapterfile);
                }
            }
            else
            {
                //Chapterize is the only mode availbe in single mode
                thechapterizer.Start(Chapterizer.Operations.Chapterize);
            }
        }

        private void thechapterizer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateProgressbar(e.ProgressPercentage);
        }

        private void UpdateProgressbar(int percentage)
        {
            if (InvokeRequired)
            {
                ProcessChangedHandler method = new ProcessChangedHandler(UpdateProgressbar);
                Invoke(method, percentage);
                return;
            }

            progressBar.Value = percentage;
        }

        private void thechapterizer_StatusChanged(object sender, ProgressChangedEventArgs e)
        {
            UpdateStatus((string) e.UserState);
        }

        private void UpdateStatus(string status)
        {
            if (InvokeRequired)
            {
                ObjectDelegate2 method = new ObjectDelegate2(UpdateStatus);
                Invoke(method, status);
                return;
            }

            lblStatus.Text = status;
            ReFormatStatus();
        }

        private void thechapterizer_Finished(object sender, RunWorkerCompletedEventArgs e)
        {
            if (UIMode == UIModes.Simple)
            {
                UIStatus = UIStatuses.AwaitFileDrop;
            }
            else
            {
                lboxFiles.Items.Clear();
                UIStatus = UIStatuses.Input;
            }

            if (e.Cancelled == false && e.Error == null)
            {
                MessageBox.Show("Finished without errors", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (e.Cancelled == false && e.Error != null)
            {
                MessageBox.Show("Finished with this error:" + e.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            thechapterizer.Clean();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            lblTrackbarValue.Text = tbarInterval.Value.ToString();
            ChapterCount = CalcChapterCount(duration, ConvertToSeconds(tbarInterval.Value, cboxUnit.Text));
        }

        private void MKVC_Load(object sender, EventArgs e)
        {
            WriteLog(string.Format("Starting MKV Chapterizer {0}", Application.ProductVersion));

            if (Properties.Settings.Default.autoUpdate)
            {
                CheckUpdates();
            }

            WriteLog("Getting screen scale multiplier");

            screenMultiplier = GetScreenScaleMulitplier();

            WriteLog(string.Format("Screen scale multiplier = {0}", screenMultiplier));

            //Set to default mode
            switch (Properties.Settings.Default.defaultMode)
            {
                case "Simple":
                    UIMode = UIModes.Simple;
                    UIStatus = UIStatuses.AwaitFileDrop;
                    break;
                case "Advanced":
                    UIMode = UIModes.Advanced;
                    UIStatus = UIStatuses.Input;
                    break;
            }

            tbarInterval.Value = Properties.Settings.Default.defChapInterval;

            lblTrackbarValue.Text = tbarInterval.Value.ToString();
            lblVersion.Text = "v" + Convert.ToString(GetVersion(Version.Parse(Application.ProductVersion)));

            //Check if any format is set
            if (String.IsNullOrWhiteSpace(Properties.Settings.Default.chapterPattern))
            {
                Properties.Settings.Default.chapterPattern = string.Join(((char)0).ToString(), "CHAPTER%I=%T" + Environment.NewLine + "CHAPTER%INAME=%N", "%L");
            }

            if (Properties.Settings.Default.rememberOverwrite)
            {
                chkboxOverwrite.Checked = Properties.Settings.Default.overwrite;
            }

        }

        private void CheckUpdates()
        {
            if (Portable == false)
            {
                Process prc = Process.GetCurrentProcess();
                try
                {
                    string args = string.Format("-version {2} -pids {0} -apiurls \"{1}\"", prc.Id.ToString(),
                                                "http://fredrikblomqvist.developer.se/dev/getupdate.php?name=mkvc|http://mumble.codecafe.com/dev/getupdate.php?name=mkvc",
                                                Application.ProductVersion.ToString());
                    Process.Start("SharpDate.exe", args);
                }
                catch (Win32Exception)
                {
                    MessageBox.Show(
                        "Failed to check for updates: \r\nYou are missing an important component, please reinstall!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to check for updates: " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                bwCheckUpdates.RunWorkerAsync();
            }
        }

        private void MKVC_Closing(object sender, FormClosingEventArgs e)
        {
            WriteLog("Exiting MKV Chapterizer");

            if (thechapterizer.IsBusy)
            {
                btnMerge.Text = "Cancelling...";
                thechapterizer.Cancel();

                while (!thechapterizer.IsBusy)
                {
                    Application.DoEvents();
                }
            }

            //Save the settings
            Properties.Settings.Default.Save();
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

        /// <summary>
        /// Calcs the chapter count.
        /// </summary>
        /// <param name="runtime">The runtime of the movie in seconds.</param>
        /// <param name="interval">The interval in seconds.</param>
        /// <returns></returns>
        public int CalcChapterCount(int runtime, int interval)
        {

            int count = Convert.ToInt32(Math.Floor((decimal)runtime/interval));

            if (Properties.Settings.Default.extraChapStart)
            {
                count += 1;
            }

            if (Properties.Settings.Default.extraChapEnd)
            {
                count += 1;
            }

            return count;

        }

        private void websiteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Process.Start("http://cyb3rh4xter.wordpress.com");

        }

        public void setTrackBarValue(int value)
        {
            if (tbarInterval.Enabled)
            {
                return;
            }

            tbarInterval.Value = value;
            lblTrackbarValue.Text = tbarInterval.Value.ToString();
        }

        private void pnlSettings_Click(object sender, EventArgs e)
        {
            Settings f = new Settings();
            f.ShowDialog();
        }

        private float GetScreenScaleMulitplier()
        {
            IntPtr hdcScreen = GetDC(NULL);
            int iDPI = GetDeviceCaps(hdcScreen, LOGPIXELSX);
            ReleaseDC(NULL, hdcScreen);

            float multiplier = (float)iDPI/96;
            return multiplier;

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int index = lboxFiles.SelectedIndex;

            if (index != -1)
            {
                lboxFiles.Items.RemoveAt(lboxFiles.SelectedIndex);
            }
            else
            {
                return;
            }

            if (lboxFiles.Items.Count > 0)
            {
                lboxFiles.SelectedIndex = index - 1;
            }

            if (lboxFiles.Items.Count == 0)
            {
                UIStatus = UIStatuses.Input;
            }
        }

        private void AddDragHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control.Controls.Count > 0)
                    AddDragHandlers(control);

                control.DragDrop += new DragEventHandler(DragDropHandler);
                control.DragEnter += new DragEventHandler(DragEnterHandler);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Copyright 2010-2012 © Fredrik Blomqvist" +
                Environment.NewLine +
                Environment.NewLine +
                "Thanks to Jarret Vance (ChapterDB)" +
                Environment.NewLine +
                "http://www.jvance.com" +
                Environment.NewLine +
                Environment.NewLine +
                "MKVMerge Copyright © Moritz Bunkus" +
                Environment.NewLine +
                "http://www.bunkus.org/videotools/mkvtoolnix/");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (openMKVdlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            //Enable Chaterizing
            EnableControls(false);

            foreach (string file in openMKVdlg.FileNames)
            {
                lboxFiles.Items.Add(file);
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
            WriteLog("Checking for updates");

            WebClient client = new WebClient();
            Version newVersion;

            try
            {
                newVersion = Version.Parse(client.DownloadString("http://fredrikblomqvist.developer.se/dev/getversion.php?name=mkvc"));
            }
            catch (Exception ex)
            {
                WriteLog("Failed to check for updates");
                MessageBox.Show("Failed to check for updates: " + ex.Message, "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            Version curVersion = Version.Parse(Application.ProductVersion);

            if (newVersion > curVersion)
            {
                WriteLog("Found update");
                e.Result = true;
            }
            else
            {
                WriteLog("The current version is the latest");
                e.Result = false;
            }
        }

        private void bwCheckUpdates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null && (bool)e.Result)
            {
                MessageBox.Show("There is a new version available at http://code.google.com/p/mkv-chapterizer/!",
                                "New Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            SwitchChapterMode();
        }

        private void SwitchChapterMode()
        {
            if (Mode == ChapterMode.ChapterDB)
            {
                Mode = ChapterMode.ChapterFile;
            }
            else if (Mode == ChapterMode.Interval)
            {
                Mode = ChapterMode.ChapterDB;
            }
            else if (Mode == ChapterMode.ChapterFile)
            {
                Mode = ChapterMode.Interval;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ChapterDB chapterDB = new ChapterDB();
            ChapterDBAccess.ChapterSet result = chapterDB.ShowDialog(txtSearch.Text);

            if (result != null)
            {
                chapterSet = result;
            }
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            e.SuppressKeyPress = true;
            ChapterDB chapterDB = new ChapterDB();
            ChapterDBAccess.ChapterSet result = chapterDB.ShowDialog(txtSearch.Text);

            if (result != null)
            {
                chapterSet = result;
            }
        }

        private void ReFormatStatus()
        {
            //Strip down the text to the max number of characters (25)
            int newStatusLength = lblStatus.Text.Length;
            if (newStatusLength <= 30)
            {
                return;
            }
            string[] parts = lblStatus.Text.Split(Convert.ToChar("."));
            string extension = parts[parts.Length - 1];

            string newStatus = lblStatus.Text.Remove(27 - extension.Length);
            newStatus = String.Join("...", newStatus, extension);
            lblStatus.Text = newStatus;
        }

        private void WriteLog(string message)
        {
            if (log != null)
            {
                log.Write(message);
            }
        }

        private void WriteError(string message)
        {
            if (log != null)
            {
                log.Write(message, Log.Type.Error);
            }
        }

        private void SemiDisableLabel(Control lbl)
        {
            lbl.ForeColor = SystemColors.AppWorkspace;
        }

        private void SemiEnableLabel(Control lbl)
        {
            lbl.ForeColor = SystemColors.ControlText;
        }

        private void chkboxOutputChapterfile_CheckedChanged(object sender, EventArgs e)
        {
            grpboxMKVHasChapters.Enabled = !chkboxOutputChapterfile.Checked;
            rbtnGenerateNew.Enabled = chkboxOutputChapterfile.Checked;
            rbtnExtractChapters.Enabled = chkboxOutputChapterfile.Checked;
        }

        private void cboxUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblNumOfChapters.Enabled)
            {
                ChapterCount = CalcChapterCount(duration, ConvertToSeconds(tbarInterval.Value, cboxUnit.Text));
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            //Ask if we should scan recursively
            if (
                MessageBox.Show("Do you want to scan recursively?", "", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IEnumerable<string> mkvs = FindMKV(dlg.SelectedPath, new List<string>());
                foreach (string file in mkvs)
                {
                    lboxFiles.Items.Add(file);
                }
            }
            else
            {
                foreach (string file in Directory.GetFiles(dlg.SelectedPath))
                {
                    if (Path.GetExtension(file) == ".mkv")
                    {
                        lboxFiles.Items.Add(file);
                    }
                }
            }
        }

        private IEnumerable<string> FindMKV(string selectedPath, List<string> ongoingList)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(selectedPath))
                {
                    if (d.EndsWith("Converted"))
                    {
                        continue;
                    }

                    ongoingList.AddRange(Directory.GetFiles(d).Where(f => Path.GetExtension(f) == ".mkv"));
                    FindMKV(d, ongoingList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return ongoingList;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lboxFiles.Items.Count < 1)
            {
                MessageBox.Show("You don't have anything to export!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.CheckPathExists = true;
            dlg.DereferenceLinks = true;
            dlg.OverwritePrompt = true;
            dlg.Title = "Please choose where to export the list to";
            dlg.ValidateNames = true;
            dlg.Filter = "Text file (*.txt)|*.txt";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string export = String.Join(Environment.NewLine, lboxFiles.Items.Cast<string>());
                File.WriteAllText(dlg.FileName, export);
                MessageBox.Show("Successfully exported the files to a list!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            dlg.DereferenceLinks = true;
            dlg.Title = "Please choose a file containing a list of files";
            dlg.ValidateNames = true;
            dlg.Filter = "Text file (*.txt)|*.txt|All (*.*)|*.*";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                string[] files = File.ReadAllText(dlg.FileName).Split(new string[] {Environment.NewLine} , StringSplitOptions.RemoveEmptyEntries);
                lboxFiles.Items.AddRange(files);
            }
        }

        private void MouseEnter_ToHand(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void MouseLeave_ToDefault(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void lblUIMode_Click(object sender, EventArgs e)
        {
            if (UIMode == UIModes.Simple)
            {
                //Change to advanced mode
                UIMode = UIModes.Advanced;
                UIStatus = UIStatuses.Input;
            }
            else
            {
                UIMode = UIModes.Simple;
                UIStatus = UIStatuses.AwaitFileDrop;
            }
        }

        private void btnChapterFormat_Click(object sender, EventArgs e)
        {
            ChapterFormat chapterFormat = new ChapterFormat();
            chapterFormat.ShowDialog();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (odlgChooseChapterFile.ShowDialog() == DialogResult.OK)
            {
                txtChapterFileLocation.Text = odlgChooseChapterFile.FileName;
            }
        }

        private void cboxOverwrite_CheckedChanged(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.rememberOverwrite)
            {
                Properties.Settings.Default.overwrite = chkboxOverwrite.Checked;
                Properties.Settings.Default.Save();
            }
        }
    }
}