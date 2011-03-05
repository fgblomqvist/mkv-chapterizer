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

namespace MKVChapterCreator
{
    public partial class Settings : Form
    {
        //declare private member of form1 instance
        private MKVC _MainForm { set; get; }

        public Settings()
        {
            InitializeComponent();
        }

        //  constructor that gets Form1 as parameter and set it to From2 private member which we //delcared above
        public Settings(MKVC MainForm) : this()
        {
            this._MainForm = MainForm;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

                Properties.Settings.Default.firstChap00 = chkboxFirstChapter00.Checked;
                Properties.Settings.Default.extraChapEnd = chkboxExtraChapter.Checked;
                Properties.Settings.Default.customOutputName = txtboxCustomName.Text;
                Properties.Settings.Default.defChapInterval = Int32.Parse(txtboxDefaultInterval.Text);
                Properties.Settings.Default.Save();

                if (_MainForm != null)
                {
                    _MainForm.setTrackBarValue(Int32.Parse(txtboxDefaultInterval.Text));
                    _MainForm.CalcChapNumber(MKVC.duration);
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
           

        }

        private void Settings_Load(object sender, EventArgs e)
        {

            txtboxDefaultInterval.Text = Properties.Settings.Default.defChapInterval.ToString();
            chkboxFirstChapter00.Checked = Properties.Settings.Default.firstChap00;
            chkboxExtraChapter.Checked = Properties.Settings.Default.extraChapEnd;
            txtboxCustomName.Text = Properties.Settings.Default.customOutputName;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
