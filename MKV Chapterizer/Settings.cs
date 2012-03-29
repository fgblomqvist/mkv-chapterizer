using System;
using System.IO;
using System.Windows.Forms;

namespace MKV_Chapterizer
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkUseLocalMKVMerge.Checked)
            {
                //Check if the user has pointed to an existing file
                if (File.Exists(txtMKVPath.Text))
                {
                    Properties.Settings.Default.customMKVMerge = true;
                    Properties.Settings.Default.customMKVMergePath = txtMKVPath.Text;
                }
                else
                {
                    //The path is invalid, warn the user and break the exit
                    MessageBox.Show(this, "You have specified and invalid path to mkvmerge.exe!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                Properties.Settings.Default.customMKVMerge = false;
            }

            Properties.Settings.Default.extraChapStart = chkboxFirstChapter00.Checked;
            Properties.Settings.Default.extraChapEnd = chkboxExtraChapter.Checked;
            Properties.Settings.Default.autoUpdate = chkboxAutoUpdate.Checked;
            Properties.Settings.Default.customOutputName = txtboxCustomName.Text;
            Properties.Settings.Default.defChapInterval = Int32.Parse(txtboxDefaultInterval.Text);
            Properties.Settings.Default.customChapterName = txtChapterName.Text;
            Properties.Settings.Default.showConsole = chkboxShowConsole.Checked;
            Properties.Settings.Default.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txtboxDefaultInterval.Text = Properties.Settings.Default.defChapInterval.ToString();
            chkboxFirstChapter00.Checked = Properties.Settings.Default.extraChapStart;
            chkboxExtraChapter.Checked = Properties.Settings.Default.extraChapEnd;
            chkboxAutoUpdate.Checked = Properties.Settings.Default.autoUpdate;
            txtboxCustomName.Text = Properties.Settings.Default.customOutputName;
            txtChapterName.Text = Properties.Settings.Default.customChapterName;
            chkUseLocalMKVMerge.Checked = Properties.Settings.Default.customMKVMerge;
            txtMKVPath.Text = Properties.Settings.Default.customMKVMergePath;
            chkboxShowConsole.Checked = Properties.Settings.Default.showConsole;

            if (!chkUseLocalMKVMerge.Checked)
            {
                txtMKVPath.Enabled = false;
                lblPath.Enabled = false;
                btnBrowse.Enabled = false;
            }

            if (Properties.Settings.Default.showMKVMergeWarning)
            {
                btnRestoreWarnings.Enabled = false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkUseLocalMKVMerge_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseLocalMKVMerge.Checked)
            {
                //Warn the user
                if (Properties.Settings.Default.showMKVMergeWarning)
                {
                    Warning warning = new Warning();
                    warning.ShowDialog();
                    if (Properties.Settings.Default.showMKVMergeWarning == false)
                    {
                        btnRestoreWarnings.Enabled = true;
                    }
                }

                txtMKVPath.Enabled = true;
                lblPath.Enabled = true;
                btnBrowse.Enabled = true;
            }
            else
            {
                txtMKVPath.Enabled = false;
                lblPath.Enabled = false;
                btnBrowse.Enabled = false;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.CheckFileExists = true;
            openFileDlg.DereferenceLinks = true;
            openFileDlg.Filter = "MkvMerge|mkvmerge.exe";
            openFileDlg.Multiselect = false;
            openFileDlg.Title = "Please browse to mkvmerge";

            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtMKVPath.Text = openFileDlg.FileName;
            }
        }

        private void btnRestoreWarnings_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.showMKVMergeWarning = true;
            btnRestoreWarnings.Enabled = false;
        }
    }
}