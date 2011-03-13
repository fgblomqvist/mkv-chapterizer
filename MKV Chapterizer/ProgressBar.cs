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

/* Original by By De Dauw Jeroen - jeroendedauw@gmail.com
 * http://www.codeproject.com/KB/progress/progressbar-percentage.aspx */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace WinForms.Controls
{
    /// <summary>
    /// Control that extends the System.Windows.Forms.ProgressBar
    /// with the ability to overlay the percentage.
    /// </summary>
    [Description(
        "Control that extends the System.Windows.Forms.ProgressBar with the ability to overlay the percentage."),
    DefaultProperty("PercentageVisible"),
    DefaultEvent("PercentageVisibleChanged")]
    public class ProgressBarWithPercentage : ProgressBar
    {
        private const int WM_PAINT = 0x0F;

        /// <summary>
        /// Raised when the visibility of the percentage text is changed.
        /// </summary>
        [Description("Raised when the visibility of the percentage text is changed."),
        Category("Property Changed")]
        public event EventHandler PercentageVisibleChanged;

        private Color overlayColor;
        private bool percentageVisible;
        private StringFormat stringFormat;

        /// <summary>
        /// Create a new instance of a ProgressbarWithPercentage.
        /// </summary>
        public ProgressBarWithPercentage()
        {
            overlayColor = Color.White;
            stringFormat = new StringFormat();
            percentageVisible = true;
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;
        }

        /// <summary>
        /// Gets or sets the Color that is used to draw the text over a filled section of the progress bar.
        /// </summary>
        [Description("The Color that is used to draw the text over a filled section of the progress bar."),
        Category("Appearance"),
        DefaultValue(typeof(Color), "White")]
        public Color OverlayColor
        {
            get { return overlayColor; }
            set
            {
                if (overlayColor != value)
                    overlayColor = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the percentage will be displayed.
        /// </summary>
        [Description("Indicates whether the percentage will be displayed on the progress bar."),
        Category("Appearance"),
        DefaultValue(true)]
        public bool PercentageVisible
        {
            get { return percentageVisible; }
            set
            {
                if (percentageVisible != value)
                {
                    percentageVisible = value;
                    OnPercentageVisibleChanged(EventArgs.Empty);
                }
            }
        }

        public new int Value
        {
            get { return base.Value; }
            set
            {
                if (base.Value != value)
                {
                    base.Value = value;

                    /* Needed for XP. Downside is control will be drawn twice
                     * when value coincides with one that the system uses for
                     * repaint. Could maybe use Environment.OSVersion to check? */
                    if (percentageVisible)
                        Invalidate();
                }
            }
        }

        public override string Text
        {
            get
            {

                int val = Value;
                return val.ToString() + "%";
            }
        }

        protected virtual void OnPercentageVisibleChanged(EventArgs e)
        {
            EventHandler eh = PercentageVisibleChanged;
            if (eh != null)
                eh(this, e);
        }

        private void ShowPercentage()
        {
            using (Graphics graphics = CreateGraphics())
            {
                // Draw left side
                Region regionLeft = new Region(new RectangleF(
                    ClientRectangle.X,
                    ClientRectangle.Y,
                    (ClientRectangle.Width * base.Value) / 100,
                    ClientRectangle.Height));
                using (Brush brush = new SolidBrush(overlayColor))
                {
                    graphics.Clip = regionLeft;
                    graphics.DrawString(Text, Font, brush, ClientRectangle, stringFormat);
                }
                // Draw right side
                Region regionRight = new Region(ClientRectangle);
                regionRight.Exclude(regionLeft);
                using (Brush brush = new SolidBrush(ForeColor))
                {
                    graphics.Clip = regionRight;
                    graphics.DrawString(Text, Font, brush, ClientRectangle, stringFormat);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (percentageVisible && m.Msg == WM_PAINT)
                ShowPercentage();
        }
    }
}
