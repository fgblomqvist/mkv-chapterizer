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
            this.txtChapterName = new System.Windows.Forms.TextBox();
            this.chkboxShowConsole = new System.Windows.Forms.CheckBox();
            this.lblChapterName = new System.Windows.Forms.Label();
            this.lblChapterVariables = new System.Windows.Forms.Label();
            this.chkUseLocalMKVMerge = new System.Windows.Forms.CheckBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtMKVPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnRestoreWarnings = new System.Windows.Forms.Button();
            this.chkboxAutoUpdate = new System.Windows.Forms.CheckBox();
            this.chkboxRememberOverwrite = new System.Windows.Forms.CheckBox();
            this.lblDefaultMode = new System.Windows.Forms.Label();
            this.cboxDefaultMode = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(245, 280);
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
            this.btnCancel.Location = new System.Drawing.Point(326, 280);
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
            this.lblMinute.Location = new System.Drawing.Point(177, 20);
            this.lblMinute.Name = "lblMinute";
            this.lblMinute.Size = new System.Drawing.Size(25, 13);
            this.lblMinute.TabIndex = 6;
            this.lblMinute.Text = "min";
            // 
            // lblDefaultInterval
            // 
            this.lblDefaultInterval.AutoSize = true;
            this.lblDefaultInterval.Location = new System.Drawing.Point(9, 16);
            this.lblDefaultInterval.Name = "lblDefaultInterval";
            this.lblDefaultInterval.Size = new System.Drawing.Size(127, 26);
            this.lblDefaultInterval.TabIndex = 7;
            this.lblDefaultInterval.Text = "Default Chapter Interval:\r\n(Restart required)";
            // 
            // txtboxDefaultInterval
            // 
            this.txtboxDefaultInterval.Location = new System.Drawing.Point(137, 17);
            this.txtboxDefaultInterval.MaxLength = 2;
            this.txtboxDefaultInterval.Name = "txtboxDefaultInterval";
            this.txtboxDefaultInterval.Size = new System.Drawing.Size(34, 20);
            this.txtboxDefaultInterval.TabIndex = 8;
            this.txtboxDefaultInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // chkboxFirstChapter00
            // 
            this.chkboxFirstChapter00.AutoSize = true;
            this.chkboxFirstChapter00.Location = new System.Drawing.Point(12, 60);
            this.chkboxFirstChapter00.Name = "chkboxFirstChapter00";
            this.chkboxFirstChapter00.Size = new System.Drawing.Size(163, 17);
            this.chkboxFirstChapter00.TabIndex = 9;
            this.chkboxFirstChapter00.Text = "Create first chapter at 00:00";
            this.chkboxFirstChapter00.UseVisualStyleBackColor = true;
            // 
            // chkboxExtraChapter
            // 
            this.chkboxExtraChapter.AutoSize = true;
            this.chkboxExtraChapter.Location = new System.Drawing.Point(12, 83);
            this.chkboxExtraChapter.Name = "chkboxExtraChapter";
            this.chkboxExtraChapter.Size = new System.Drawing.Size(125, 17);
            this.chkboxExtraChapter.TabIndex = 10;
            this.chkboxExtraChapter.Text = "Extra chapter at end";
            this.infoTip.SetToolTip(this.chkboxExtraChapter, "Example:\r\nVideo is 1 hour 23 minutes.\r\nThe last chapter will be at \r\n1 hour 23 mi" +
        "nutes, ignoring\r\nthe chapter interval.");
            this.chkboxExtraChapter.UseVisualStyleBackColor = true;
            // 
            // txtboxCustomName
            // 
            this.txtboxCustomName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtboxCustomName.Location = new System.Drawing.Point(229, 127);
            this.txtboxCustomName.Name = "txtboxCustomName";
            this.txtboxCustomName.Size = new System.Drawing.Size(150, 20);
            this.txtboxCustomName.TabIndex = 12;
            this.infoTip.SetToolTip(this.txtboxCustomName, "Example:\r\n%O-new\r\nAvatar-new.mkv");
            // 
            // lblCustomName2
            // 
            this.lblCustomName2.AutoSize = true;
            this.lblCustomName2.Location = new System.Drawing.Point(226, 150);
            this.lblCustomName2.Name = "lblCustomName2";
            this.lblCustomName2.Size = new System.Drawing.Size(101, 13);
            this.lblCustomName2.TabIndex = 13;
            this.lblCustomName2.Text = "%O = Old filename";
            // 
            // lblCustomName
            // 
            this.lblCustomName.AutoSize = true;
            this.lblCustomName.Location = new System.Drawing.Point(226, 98);
            this.lblCustomName.Name = "lblCustomName";
            this.lblCustomName.Size = new System.Drawing.Size(138, 26);
            this.lblCustomName.TabIndex = 14;
            this.lblCustomName.Text = "Custom name for new files\r\nwhen not overwriting:";
            // 
            // lblMkv
            // 
            this.lblMkv.AutoSize = true;
            this.lblMkv.Location = new System.Drawing.Point(384, 130);
            this.lblMkv.Name = "lblMkv";
            this.lblMkv.Size = new System.Drawing.Size(34, 13);
            this.lblMkv.TabIndex = 15;
            this.lblMkv.Text = ". mkv";
            // 
            // infoTip
            // 
            this.infoTip.AutoPopDelay = 8000;
            this.infoTip.InitialDelay = 500;
            this.infoTip.ReshowDelay = 100;
            // 
            // txtChapterName
            // 
            this.txtChapterName.Location = new System.Drawing.Point(12, 156);
            this.txtChapterName.Name = "txtChapterName";
            this.txtChapterName.Size = new System.Drawing.Size(166, 20);
            this.txtChapterName.TabIndex = 17;
            this.infoTip.SetToolTip(this.txtChapterName, "Example:\r\nChapter %N - %T\r\nChapter 3 - 05:00");
            // 
            // chkboxShowConsole
            // 
            this.chkboxShowConsole.AutoSize = true;
            this.chkboxShowConsole.Location = new System.Drawing.Point(229, 183);
            this.chkboxShowConsole.Name = "chkboxShowConsole";
            this.chkboxShowConsole.Size = new System.Drawing.Size(153, 30);
            this.chkboxShowConsole.TabIndex = 25;
            this.chkboxShowConsole.Text = "Show mkvmerge console \r\nwhen chapterizing";
            this.infoTip.SetToolTip(this.chkboxShowConsole, "Don\'t enable this if you don\'t know what it means");
            this.chkboxShowConsole.UseVisualStyleBackColor = true;
            this.chkboxShowConsole.Visible = false;
            // 
            // lblChapterName
            // 
            this.lblChapterName.AutoSize = true;
            this.lblChapterName.Location = new System.Drawing.Point(12, 111);
            this.lblChapterName.Name = "lblChapterName";
            this.lblChapterName.Size = new System.Drawing.Size(140, 13);
            this.lblChapterName.TabIndex = 16;
            this.lblChapterName.Text = "Custom name for chapters:";
            // 
            // lblChapterVariables
            // 
            this.lblChapterVariables.AutoSize = true;
            this.lblChapterVariables.Location = new System.Drawing.Point(12, 127);
            this.lblChapterVariables.Name = "lblChapterVariables";
            this.lblChapterVariables.Size = new System.Drawing.Size(130, 26);
            this.lblChapterVariables.TabIndex = 18;
            this.lblChapterVariables.Text = "%N = Chapter Number\r\n%T =  Chapter Timecode";
            // 
            // chkUseLocalMKVMerge
            // 
            this.chkUseLocalMKVMerge.AutoSize = true;
            this.chkUseLocalMKVMerge.Location = new System.Drawing.Point(229, 12);
            this.chkUseLocalMKVMerge.Name = "chkUseLocalMKVMerge";
            this.chkUseLocalMKVMerge.Size = new System.Drawing.Size(139, 30);
            this.chkUseLocalMKVMerge.TabIndex = 19;
            this.chkUseLocalMKVMerge.Text = "Try to use MKV Merge \r\n from local mkvtoolnix";
            this.chkUseLocalMKVMerge.UseVisualStyleBackColor = true;
            this.chkUseLocalMKVMerge.CheckedChanged += new System.EventHandler(this.chkUseLocalMKVMerge_CheckedChanged);
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(226, 44);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(32, 13);
            this.lblPath.TabIndex = 20;
            this.lblPath.Text = "Path:";
            // 
            // txtMKVPath
            // 
            this.txtMKVPath.Location = new System.Drawing.Point(229, 60);
            this.txtMKVPath.Name = "txtMKVPath";
            this.txtMKVPath.Size = new System.Drawing.Size(159, 20);
            this.txtMKVPath.TabIndex = 21;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(394, 58);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(24, 23);
            this.btnBrowse.TabIndex = 22;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnRestoreWarnings
            // 
            this.btnRestoreWarnings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestoreWarnings.Location = new System.Drawing.Point(12, 280);
            this.btnRestoreWarnings.Name = "btnRestoreWarnings";
            this.btnRestoreWarnings.Size = new System.Drawing.Size(106, 23);
            this.btnRestoreWarnings.TabIndex = 23;
            this.btnRestoreWarnings.Text = "Restore Warnings";
            this.btnRestoreWarnings.UseVisualStyleBackColor = true;
            this.btnRestoreWarnings.Click += new System.EventHandler(this.btnRestoreWarnings_Click);
            // 
            // chkboxAutoUpdate
            // 
            this.chkboxAutoUpdate.AutoSize = true;
            this.chkboxAutoUpdate.Location = new System.Drawing.Point(12, 190);
            this.chkboxAutoUpdate.Name = "chkboxAutoUpdate";
            this.chkboxAutoUpdate.Size = new System.Drawing.Size(198, 17);
            this.chkboxAutoUpdate.TabIndex = 24;
            this.chkboxAutoUpdate.Text = "Auto-search for updates on Launch";
            this.chkboxAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // chkboxRememberOverwrite
            // 
            this.chkboxRememberOverwrite.AutoSize = true;
            this.chkboxRememberOverwrite.Location = new System.Drawing.Point(229, 235);
            this.chkboxRememberOverwrite.Name = "chkboxRememberOverwrite";
            this.chkboxRememberOverwrite.Size = new System.Drawing.Size(176, 17);
            this.chkboxRememberOverwrite.TabIndex = 26;
            this.chkboxRememberOverwrite.Text = "Remember \"Overwrite\" setting";
            this.chkboxRememberOverwrite.UseVisualStyleBackColor = true;
            // 
            // lblDefaultMode
            // 
            this.lblDefaultMode.AutoSize = true;
            this.lblDefaultMode.Location = new System.Drawing.Point(12, 219);
            this.lblDefaultMode.Name = "lblDefaultMode";
            this.lblDefaultMode.Size = new System.Drawing.Size(75, 13);
            this.lblDefaultMode.TabIndex = 27;
            this.lblDefaultMode.Text = "Default mode:";
            // 
            // cboxDefaultMode
            // 
            this.cboxDefaultMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxDefaultMode.FormattingEnabled = true;
            this.cboxDefaultMode.Items.AddRange(new object[] {
            "Simple",
            "Advanced"});
            this.cboxDefaultMode.Location = new System.Drawing.Point(12, 235);
            this.cboxDefaultMode.Name = "cboxDefaultMode";
            this.cboxDefaultMode.Size = new System.Drawing.Size(190, 21);
            this.cboxDefaultMode.TabIndex = 28;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 315);
            this.Controls.Add(this.cboxDefaultMode);
            this.Controls.Add(this.lblDefaultMode);
            this.Controls.Add(this.chkboxRememberOverwrite);
            this.Controls.Add(this.chkboxShowConsole);
            this.Controls.Add(this.chkboxAutoUpdate);
            this.Controls.Add(this.btnRestoreWarnings);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtMKVPath);
            this.Controls.Add(this.lblPath);
            this.Controls.Add(this.chkUseLocalMKVMerge);
            this.Controls.Add(this.lblChapterVariables);
            this.Controls.Add(this.txtChapterName);
            this.Controls.Add(this.lblChapterName);
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
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
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
        private System.Windows.Forms.Label lblChapterName;
        private System.Windows.Forms.TextBox txtChapterName;
        private System.Windows.Forms.Label lblChapterVariables;
        private System.Windows.Forms.CheckBox chkUseLocalMKVMerge;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.TextBox txtMKVPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnRestoreWarnings;
        private System.Windows.Forms.CheckBox chkboxAutoUpdate;
        private System.Windows.Forms.CheckBox chkboxShowConsole;
        private System.Windows.Forms.CheckBox chkboxRememberOverwrite;
        private System.Windows.Forms.Label lblDefaultMode;
        private System.Windows.Forms.ComboBox cboxDefaultMode;
    }
}