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
using System.IO;

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

            Properties.Settings.Default.firstChap00 = chkboxFirstChapter00.Checked;
            Properties.Settings.Default.extraChapEnd = chkboxExtraChapter.Checked;
            Properties.Settings.Default.autoUpdate = chkboxAutoUpdate.Checked;
            Properties.Settings.Default.customOutputName = txtboxCustomName.Text;
            Properties.Settings.Default.defChapInterval = Int32.Parse(txtboxDefaultInterval.Text);
            Properties.Settings.Default.customChapterName = txtChapterName.Text;
            Properties.Settings.Default.Save();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void Settings_Load(object sender, EventArgs e)
        {
            txtboxDefaultInterval.Text = Properties.Settings.Default.defChapInterval.ToString();
            chkboxFirstChapter00.Checked = Properties.Settings.Default.firstChap00;
            chkboxExtraChapter.Checked = Properties.Settings.Default.extraChapEnd;
            chkboxAutoUpdate.Checked = Properties.Settings.Default.autoUpdate;
            txtboxCustomName.Text = Properties.Settings.Default.customOutputName;
            txtChapterName.Text = Properties.Settings.Default.customChapterName;
            chkUseLocalMKVMerge.Checked = Properties.Settings.Default.customMKVMerge;
            txtMKVPath.Text = Properties.Settings.Default.customMKVMergePath;

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
            this.Close();
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

            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
