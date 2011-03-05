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

namespace MKV_Chapterizer
{
    public partial class ChaptersExist : Form
    {
        private int theResult = 0;
        private MKVC _MainForm { set; get; }

        public int Result
        {
            get
            {
                return theResult;
            }
            set
            {
                theResult = value;
            }
        }

        public ChaptersExist(MKVC MainForm) : this()
        {
            this._MainForm = MainForm;
        }

        public ChaptersExist()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Remove
            theResult = 1;
            this.Close();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            //Replace
            theResult = 2;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Cancel
            theResult = 0;
            this.Close();
        }

        private void ChaptersExist_Load(object sender, EventArgs e)
        {

        }
    }
}
