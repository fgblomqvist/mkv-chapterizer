﻿namespace MKV_Chapterizer
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblMinute = new System.Windows.Forms.Label();
            this.lblDefaultInterval = new System.Windows.Forms.Label();
            this.txtboxDefaultInterval = new System.Windows.Forms.TextBox();
            this.chkboxFirstChapter00 = new System.Windows.Forms.CheckBox();
            this.chkboxExtraChapter = new System.Windows.Forms.CheckBox();
            this.txtboxCustomName = new System.Windows.Forms.TextBox();
            this.lblCustomName2 = new System.Windows.Forms.Label();
            this.lblCustomName = new System.Windows.Forms.Label();
            this.lblMkv = new System.Windows.Forms.Label();
            this.infoTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(30, 180);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(111, 180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblMinute
            // 
            this.lblMinute.AutoSize = true;
            this.lblMinute.Location = new System.Drawing.Point(177, 16);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(23, 13);
            this.lblMinute.TabIndex = 6;
            this.lblMinute.Text = "min";
            // 
            // lblDefaultInterval
            // 
            this.lblDefaultInterval.AutoSize = true;
            this.lblDefaultInterval.Location = new System.Drawing.Point(9, 16);
            this.lblDefaultInterval.Name = "lblDefaultInterval";
            this.lblDefaultInterval.Size = new System.Drawing.Size(122, 13);
            this.lblDefaultInterval.TabIndex = 7;
            this.lblDefaultInterval.Text = "Default Chapter Interval:";
            // 
            // txtboxDefaultInterval
            // 
            this.txtboxDefaultInterval.Location = new System.Drawing.Point(137, 13);
            this.txtboxDefaultInterval.MaxLength = 2;
            this.txtboxDefaultInterval.Name = "txtboxDefaultInterval";
            this.txtboxDefaultInterval.Size = new System.Drawing.Size(34, 20);
            this.txtboxDefaultInterval.TabIndex = 8;
            this.txtboxDefaultInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkboxFirstChapter00
            // 
            this.chkboxFirstChapter00.AutoSize = true;
            this.chkboxFirstChapter00.Location = new System.Drawing.Point(12, 50);
            this.chkboxFirstChapter00.Name = "chkboxFirstChapter00";
            this.chkboxFirstChapter00.Size = new System.Drawing.Size(157, 17);
            this.chkboxFirstChapter00.TabIndex = 9;
            this.chkboxFirstChapter00.Text = "Create first chapter at 00:00";
            this.chkboxFirstChapter00.UseVisualStyleBackColor = true;
            // 
            // chkboxExtraChapter
            // 
            this.chkboxExtraChapter.AutoSize = true;
            this.chkboxExtraChapter.Location = new System.Drawing.Point(12, 73);
            this.chkboxExtraChapter.Name = "chkboxExtraChapter";
            this.chkboxExtraChapter.Size = new System.Drawing.Size(122, 17);
            this.chkboxExtraChapter.TabIndex = 10;
            this.chkboxExtraChapter.Text = "Extra chapter at end";
            this.infoTip.SetToolTip(this.chkboxExtraChapter, "Example:\r\nVideo is 1 hour 23 minutes.\r\nThe last chapter will be at \r\n1 hour 23 mi" +
                    "nutes, ignoring\r\nthe chapter interval.");
            this.chkboxExtraChapter.UseVisualStyleBackColor = true;
            // 
            // txtboxCustomName
            // 
            this.txtboxCustomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxCustomName.Location = new System.Drawing.Point(15, 140);
            this.txtboxCustomName.Name = "txtboxCustomName";
            this.txtboxCustomName.Size = new System.Drawing.Size(130, 20);
            this.txtboxCustomName.TabIndex = 12;
            // 
            // lblCustomName2
            // 
            this.lblCustomName2.AutoSize = true;
            this.lblCustomName2.Location = new System.Drawing.Point(12, 124);
            this.lblCustomName2.Name = "lblCustomName2";
            this.lblCustomName2.Size = new System.Drawing.Size(93, 13);
            this.lblCustomName2.TabIndex = 13;
            this.lblCustomName2.Text = "%O = Old filename";
            // 
            // lblCustomName
            // 
            this.lblCustomName.AutoSize = true;
            this.lblCustomName.Location = new System.Drawing.Point(12, 102);
            this.lblCustomName.Name = "lblCustomName";
            this.lblCustomName.Size = new System.Drawing.Size(133, 13);
            this.lblCustomName.TabIndex = 14;
            this.lblCustomName.Text = "Custom name for new files:";
            // 
            // lblMkv
            // 
            this.lblMkv.AutoSize = true;
            this.lblMkv.Location = new System.Drawing.Point(148, 143);
            this.lblMkv.Name = "lblMkv";
            this.lblMkv.Size = new System.Drawing.Size(33, 13);
            this.lblMkv.TabIndex = 15;
            this.lblMkv.Text = ". mkv";
            // 
            // infoTip
            // 
            this.infoTip.AutoPopDelay = 8000;
            this.infoTip.InitialDelay = 1000;
            this.infoTip.ReshowDelay = 100;
            this.infoTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 215);
            this.Controls.Add(this.lblMkv);
            this.Controls.Add(this.lblCustomName);
            this.Controls.Add(this.lblCustomName2);
            this.Controls.Add(this.txtboxCustomName);
            this.Controls.Add(this.chkboxExtraChapter);
            this.Controls.Add(this.chkboxFirstChapter00);
            this.Controls.Add(this.txtboxDefaultInterval);
            this.Controls.Add(this.lblDefaultInterval);
            this.Controls.Add(this.lblMinute);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblMinute;
        private System.Windows.Forms.Label lblDefaultInterval;
        private System.Windows.Forms.TextBox txtboxDefaultInterval;
        private System.Windows.Forms.CheckBox chkboxFirstChapter00;
        private System.Windows.Forms.CheckBox chkboxExtraChapter;
        private System.Windows.Forms.TextBox txtboxCustomName;
        private System.Windows.Forms.Label lblCustomName2;
        private System.Windows.Forms.Label lblCustomName;
        private System.Windows.Forms.Label lblMkv;
        private System.Windows.Forms.ToolTip infoTip;
    }
}