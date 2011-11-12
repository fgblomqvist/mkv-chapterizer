using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml.Linq;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace MKV_Chapterizer
{
    public partial class AutoUpdate : Form
    {

        private string[] pUpdateInfo;
        private string pMainExe;

        public delegate void DownloadProgressDelegate(int percProgress);

        BackgroundWorker bwUpdateProgram = new BackgroundWorker();
        BackgroundWorker bwCheckUpdates = new BackgroundWorker();

        public string[] UpdateInfo
        {
            get { return pUpdateInfo; }
            set { pUpdateInfo = value; }
        }

        public string MainExe
        {
            get { return pMainExe; }
            set { pMainExe = value; }
        }

        public AutoUpdate()
        {
            InitializeComponent();

            //Get command line args and save into the data

            string[] args = Environment.GetCommandLineArgs();
            string apiURL = null;

            for (int i = 0; i <= args.Count() - 1; i++)
            {
                switch (args[i])
                {
                    case "-exe":
                        MainExe = args[i + 1];
                        break;

                    case "-apiurl":
                        apiURL = args[i + 1];
                        break;
                }
            }

            bwCheckUpdates.DoWork +=new DoWorkEventHandler(bwCheckUpdates_DoWork);
            bwCheckUpdates.ProgressChanged +=new ProgressChangedEventHandler(bwCheckUpdates_ProgressChanged);
            bwCheckUpdates.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(bwCheckUpdates_RunWorkerCompleted);

            bwUpdateProgram.RunWorkerCompleted +=new RunWorkerCompletedEventHandler(bwUpdateProgram_RunWorkerCompleted);
            bwUpdateProgram.DoWork +=new DoWorkEventHandler(bwUpdateProgram_DoWork);
            bwUpdateProgram.ProgressChanged +=new ProgressChangedEventHandler(bwUpdateProgram_ProgressChanged);
            bwUpdateProgram.WorkerReportsProgress = true;
            bwUpdateProgram.WorkerSupportsCancellation = true;

            btnUpdate.Enabled = false;
            btnUpdate.Text = "Loading...";
            btnUpdate.Select();

            bwCheckUpdates.RunWorkerAsync(apiURL);
        }

        private string[] GetUpdateInfo(string APIUrl)
        {
            WebClient client = new WebClient();
            string xml = client.DownloadString(new Uri(APIUrl));
            XDocument xdoc = XDocument.Parse(xml);
            XElement xe = xdoc.Root.Element("program");

            string[] data = new string[7];

            data[0] = xe.Element("name").Value;
            data[1] = xe.Element("version").Value;
            data[2] = xe.Element("beta").Value;
            data[3] = xe.Element("changelog").Value;
            data[4] = xe.Element("downloadURL").Value;
            data[5] = xe.Element("downloadURLType").Value;
            data[6] = xe.Element("downloadPath").Value;

            return data;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            bwUpdateProgram.RunWorkerAsync();
            btnUpdate.Enabled = false;
        }

        private void bwCheckUpdates_DoWork(object sender, DoWorkEventArgs e)
        {
            FileVersionInfo fi;

            if (File.Exists(MainExe))
            {
                fi = FileVersionInfo.GetVersionInfo(MainExe);
            }
            else
            {
                return;
            }

            UpdateInfo = GetUpdateInfo((string)e.Argument);

            Version currentVersion = new Version(fi.FileVersion);
            Version newVersion = new Version(UpdateInfo[1]);

            if (newVersion > currentVersion)
            {
                //There is a new update available
                e.Result = true;
            }
            else
            {
                //The user is running the latest version
                e.Result = false;
            }
        }

        private void bwCheckUpdates_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

        private void bwCheckUpdates_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if ((bool)e.Result == true)
            {
                lblNewUpdate.Text = string.Format("Version {0} is available for download", UpdateInfo[1]);
                txtChangelog.Text = UpdateInfo[3];
                btnUpdate.Text = "Update";
                btnUpdate.Enabled = true;
            }
        }

        private void bwUpdateProgram_DoWork(object sender, DoWorkEventArgs e)
        {
            //Create workdir
            try
            {
                Directory.CreateDirectory("update");
            }
            catch (Exception)
            {
                return;
            }

            //Download the updatefile

            DownloadProgressDelegate progressUpdate = new DownloadProgressDelegate(downloadFileProgressChanged);
            bool result = false;

            //Check if the updateurl is any specific type

            switch (UpdateInfo[5])
            {
                case "freedns.afraid.org":

                    //The real URL needs to be fetched
                    UpdateInfo[4] = FetchFreeDNSURL(UpdateInfo[4]);
                    break;
            }

            try
            {

                string url = UpdateInfo[4] + UpdateInfo[6] + UpdateInfo[1] + ".zip";
                result = Download(url, "update\\" + UpdateInfo[1] + ".exe", progressUpdate);
            }
            catch (Exception ex)
            {
                e.Result = ex;
                return;
            }

            if (result == true)
            {
                //Download succeeded, notify the user and start the setup
                //Download succeeded, return to the main thread
                e.Result = UpdateInfo[1] + ".exe";
            }
                
        }

        private void bwUpdateProgram_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbarDownloadProgress.Value = e.ProgressPercentage;
        }

        private void bwUpdateProgram_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result.GetType().Name == "String")
            {
                MessageBox.Show("MKV Chapterizer will now be closed to prepare for the update, \r\nso please save all your work before proceeding!", "Update Downloaded Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Process.Start("update\\" + e.Result);
            }
            else if (e.Result.GetType().Name == "Exception")
            {
                MessageBox.Show("Error Occured: \r\n" + (Exception)e.Result, "Error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private string FetchFreeDNSURL(string url)
        {
            WebClient client = new WebClient();
            string html = client.DownloadString(url);

            //Find the frame-tag that holds the redirect URL
            Regex regex = new Regex("<frame target=\"random_name_not_taken2\" name=\"random_name_not_taken2\" src=\".+\" border=\"0\" noresize>", RegexOptions.IgnoreCase);
            Match match = regex.Match(html);

            regex = new Regex("src=\".*?\"");

            string realURL = regex.Match(match.Value).Value.Substring(5).Replace("\"", "");

            return realURL;
        }

        private void downloadFileProgressChanged(int percentage)
        {
            bwUpdateProgram.ReportProgress(percentage);
        }

        /// <summary>
        /// Downloads the specified URL. Returns true if success.
        /// </summary>
        /// <param name="uri">The URL.</param>
        /// <param name="localPath">The local path.</param>
        /// <param name="progressDelegate">The progress delegate.</param>
        /// <returns></returns>
        public static bool Download(string url, string localPath, DownloadProgressDelegate progressDelegate)
        {
            long remoteSize;
            string fullLocalPath; // Full local path including file name if only directory was provided.

            try
            {
                /// Get the name of the remote file.
                Uri remoteUri = new Uri(url);
                string fileName = Path.GetFileName(remoteUri.LocalPath);

                if (Path.GetFileName(localPath).Length == 0)
                    fullLocalPath = Path.Combine(localPath, fileName);
                else
                    fullLocalPath = localPath;

                /// Have to get size of remote object through the webrequest as not available on remote files,
                /// although it does work on local files.
                using (WebResponse response = WebRequest.Create(url).GetResponse())
                using (Stream stream = response.GetResponseStream())
                    remoteSize = response.ContentLength;

            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error connecting to URI (Exception={0})", ex.Message), ex);
            }

            int bytesRead = 0, bytesReadTotal = 0;

            try
            {
                using (WebClient client = new WebClient())
                using (Stream streamRemote = client.OpenRead(new Uri(url)))
                using (Stream streamLocal = new FileStream(fullLocalPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    byte[] byteBuffer = new byte[1024 * 1024 * 2]; // 2 meg buffer although in testing only got to 10k max usage.
                    int perc = 0;
                    while ((bytesRead = streamRemote.Read(byteBuffer, 0, byteBuffer.Length)) > 0)
                    {
                        bytesReadTotal += bytesRead;
                        streamLocal.Write(byteBuffer, 0, bytesRead);
                        int newPerc = (int)((double)bytesReadTotal / (double)remoteSize * 100);
                        if (newPerc > perc)
                        {
                            perc = newPerc;
                            if (progressDelegate != null)
                                progressDelegate(perc);
                        }
                    }
                }

                progressDelegate(100);
                return true; //File succeeded downloading
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format("Error downloading file (Exception={0})", ex.Message), ex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Clean-up
            if (CleanUp() == false)
            {
                if (MessageBox.Show("Failed to clean up update directory!", "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == System.Windows.Forms.DialogResult.Retry)
                {
                    CleanUp();
                }
            }

            this.Close();
        }

        private bool CleanUp()
        {
            if (Directory.Exists("update\\"))
            {
                try
                {
                    Directory.Delete("update\\");
                }
                catch (Exception)
                {
                    return false;
                }
            }

            //Clean-up succeeded
            return true;
        }
    }
}


