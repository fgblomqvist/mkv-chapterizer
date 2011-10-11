namespace MKV_Chapterizer
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
            this.lblChapterName = new System.Windows.Forms.Label();
            this.lblChapterVariables = new System.Windows.Forms.Label();
            this.chkUseLocalMKVMerge = new System.Windows.Forms.CheckBox();
            this.lblPath = new System.Windows.Forms.Label();
            this.txtMKVPath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(245, 182);
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
            this.btnCancel.Location = new System.Drawing.Point(326, 182);
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
            this.txtboxCustomName.Location = new System.Drawing.Point(229, 143);
            this.txtboxCustomName.Name = "txtboxCustomName";
            this.txtboxCustomName.Size = new System.Drawing.Size(130, 20);
            this.txtboxCustomName.TabIndex = 12;
            this.infoTip.SetToolTip(this.txtboxCustomName, "Example:\r\n%O-new\r\nAvatar-new.mkv");
            // 
            // lblCustomName2
            // 
            this.lblCustomName2.AutoSize = true;
            this.lblCustomName2.Location = new System.Drawing.Point(226, 127);
            this.lblCustomName2.Name = "lblCustomName2";
            this.lblCustomName2.Size = new System.Drawing.Size(93, 13);
            this.lblCustomName2.TabIndex = 13;
            this.lblCustomName2.Text = "%O = Old filename";
            // 
            // lblCustomName
            // 
            this.lblCustomName.AutoSize = true;
            this.lblCustomName.Location = new System.Drawing.Point(226, 105);
            this.lblCustomName.Name = "lblCustomName";
            this.lblCustomName.Size = new System.Drawing.Size(133, 13);
            this.lblCustomName.TabIndex = 14;
            this.lblCustomName.Text = "Custom name for new files:";
            // 
            // lblMkv
            // 
            this.lblMkv.AutoSize = true;
            this.lblMkv.Location = new System.Drawing.Point(362, 146);
            this.lblMkv.Name = "lblMkv";
            this.lblMkv.Size = new System.Drawing.Size(33, 13);
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
            this.txtChapterName.Location = new System.Drawing.Point(15, 160);
            this.txtChapterName.Name = "txtChapterName";
            this.txtChapterName.Size = new System.Drawing.Size(166, 20);
            this.txtChapterName.TabIndex = 17;
            this.infoTip.SetToolTip(this.txtChapterName, "Example:\r\nChapter %N - %T\r\nChapter 3 - 05:00");
            // 
            // lblChapterName
            // 
            this.lblChapterName.AutoSize = true;
            this.lblChapterName.Location = new System.Drawing.Point(12, 105);
            this.lblChapterName.Name = "lblChapterName";
            this.lblChapterName.Size = new System.Drawing.Size(133, 13);
            this.lblChapterName.TabIndex = 16;
            this.lblChapterName.Text = "Custom name for chapters:";
            // 
            // lblChapterVariables
            // 
            this.lblChapterVariables.AutoSize = true;
            this.lblChapterVariables.Location = new System.Drawing.Point(12, 131);
            this.lblChapterVariables.Name = "lblChapterVariables";
            this.lblChapterVariables.Size = new System.Drawing.Size(124, 26);
            this.lblChapterVariables.TabIndex = 18;
            this.lblChapterVariables.Text = "%N = Chapter Number\r\n%T =  Chapter Timecode";
            // 
            // chkUseLocalMKVMerge
            // 
            this.chkUseLocalMKVMerge.AutoSize = true;
            this.chkUseLocalMKVMerge.Location = new System.Drawing.Point(229, 12);
            this.chkUseLocalMKVMerge.Name = "chkUseLocalMKVMerge";
            this.chkUseLocalMKVMerge.Size = new System.Drawing.Size(135, 30);
            this.chkUseLocalMKVMerge.TabIndex = 19;
            this.chkUseLocalMKVMerge.Text = "Try to use MKV Merge \r\n from local mkvtoolnix";
            this.chkUseLocalMKVMerge.UseVisualStyleBackColor = true;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(226, 54);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(32, 13);
            this.lblPath.TabIndex = 20;
            this.lblPath.Text = "Path:";
            // 
            // txtMKVPath
            // 
            this.txtMKVPath.Location = new System.Drawing.Point(229, 70);
            this.txtMKVPath.Name = "txtMKVPath";
            this.txtMKVPath.Size = new System.Drawing.Size(172, 20);
            this.txtMKVPath.TabIndex = 21;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 217);
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
    }
}