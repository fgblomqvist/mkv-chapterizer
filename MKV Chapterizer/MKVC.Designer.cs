namespace MKV_Chapterizer
{
    partial class MKVC
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MKVC));
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.queueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMKVdlg = new System.Windows.Forms.OpenFileDialog();
            this.bwCheckUpdates = new System.ComponentModel.BackgroundWorker();
            this.tabControl = new Dotnetrix.Samples.CSharp.TabControl();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.pnlInterval = new System.Windows.Forms.Panel();
            this.lblChapterInterval = new System.Windows.Forms.Label();
            this.lblChapterCount = new System.Windows.Forms.Label();
            this.lblNumOfChapters = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.lblTrackbarValue = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlChapterDB = new System.Windows.Forms.Panel();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.lblModeValue = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSettings = new System.Windows.Forms.Label();
            this.btnMerge = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.cboxOverwrite = new System.Windows.Forms.CheckBox();
            this.lblTutorial = new System.Windows.Forms.Label();
            this.tpQueue = new System.Windows.Forms.TabPage();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lboxFiles = new System.Windows.Forms.ListBox();
            this.tpAdvSettings = new System.Windows.Forms.TabPage();
            this.grpboxMKVHasChapters = new System.Windows.Forms.GroupBox();
            this.rbtnDoNothing = new System.Windows.Forms.RadioButton();
            this.rbtnRemoveThem = new System.Windows.Forms.RadioButton();
            this.rbtnReplaceThem = new System.Windows.Forms.RadioButton();
            this.progressBar = new WinForms.Controls.ProgressBarWithPercentage();
            this.contextMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.pnlInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.pnlMain.SuspendLayout();
            this.tpQueue.SuspendLayout();
            this.tpAdvSettings.SuspendLayout();
            this.grpboxMKVHasChapters.SuspendLayout();
            this.SuspendLayout();
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.websiteToolStripMenuItem.Text = "Website";
            this.websiteToolStripMenuItem.Click += new System.EventHandler(this.websiteToolStripMenuItem_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.websiteToolStripMenuItem,
            this.queueToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(144, 48);
            // 
            // queueToolStripMenuItem
            // 
            this.queueToolStripMenuItem.CheckOnClick = true;
            this.queueToolStripMenuItem.Name = "queueToolStripMenuItem";
            this.queueToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.queueToolStripMenuItem.Text = "Queue Mode";
            this.queueToolStripMenuItem.Click += new System.EventHandler(this.queueToolStripMenuItem_Click);
            // 
            // openMKVdlg
            // 
            this.openMKVdlg.Filter = "MKV Video Files|*.mkv";
            this.openMKVdlg.Multiselect = true;
            this.openMKVdlg.Title = "Choose MKVs to Chapterize";
            // 
            // bwCheckUpdates
            // 
            this.bwCheckUpdates.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCheckUpdates_DoWork);
            this.bwCheckUpdates.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCheckUpdates_RunWorkerCompleted);
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tpSettings);
            this.tabControl.Controls.Add(this.tpQueue);
            this.tabControl.Controls.Add(this.tpAdvSettings);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(424, 209);
            this.tabControl.TabIndex = 1;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.pnlInterval);
            this.tpSettings.Controls.Add(this.pnlMain);
            this.tpSettings.Location = new System.Drawing.Point(4, 23);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(416, 182);
            this.tpSettings.TabIndex = 0;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // pnlInterval
            // 
            this.pnlInterval.Controls.Add(this.lblChapterInterval);
            this.pnlInterval.Controls.Add(this.lblChapterCount);
            this.pnlInterval.Controls.Add(this.lblNumOfChapters);
            this.pnlInterval.Controls.Add(this.trackBar1);
            this.pnlInterval.Controls.Add(this.lblTrackbarValue);
            this.pnlInterval.Controls.Add(this.lblMin);
            this.pnlInterval.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInterval.Location = new System.Drawing.Point(3, 3);
            this.pnlInterval.Name = "pnlInterval";
            this.pnlInterval.Size = new System.Drawing.Size(410, 74);
            this.pnlInterval.TabIndex = 32;
            // 
            // lblChapterInterval
            // 
            this.lblChapterInterval.AllowDrop = true;
            this.lblChapterInterval.AutoSize = true;
            this.lblChapterInterval.Enabled = false;
            this.lblChapterInterval.Location = new System.Drawing.Point(9, 14);
            this.lblChapterInterval.Name = "lblChapterInterval";
            this.lblChapterInterval.Size = new System.Drawing.Size(85, 13);
            this.lblChapterInterval.TabIndex = 12;
            this.lblChapterInterval.Text = "Chapter Interval:";
            // 
            // lblChapterCount
            // 
            this.lblChapterCount.AllowDrop = true;
            this.lblChapterCount.AutoSize = true;
            this.lblChapterCount.Location = new System.Drawing.Point(119, 55);
            this.lblChapterCount.Name = "lblChapterCount";
            this.lblChapterCount.Size = new System.Drawing.Size(0, 13);
            this.lblChapterCount.TabIndex = 17;
            // 
            // lblNumOfChapters
            // 
            this.lblNumOfChapters.AllowDrop = true;
            this.lblNumOfChapters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumOfChapters.AutoSize = true;
            this.lblNumOfChapters.Enabled = false;
            this.lblNumOfChapters.Location = new System.Drawing.Point(9, 55);
            this.lblNumOfChapters.Name = "lblNumOfChapters";
            this.lblNumOfChapters.Size = new System.Drawing.Size(104, 13);
            this.lblNumOfChapters.TabIndex = 13;
            this.lblNumOfChapters.Text = "Number of Chapters:";
            // 
            // trackBar1
            // 
            this.trackBar1.AllowDrop = true;
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.BackColor = System.Drawing.Color.White;
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(100, 14);
            this.trackBar1.Maximum = 60;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(235, 45);
            this.trackBar1.TabIndex = 19;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblTrackbarValue
            // 
            this.lblTrackbarValue.AllowDrop = true;
            this.lblTrackbarValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackbarValue.AutoSize = true;
            this.lblTrackbarValue.Enabled = false;
            this.lblTrackbarValue.Location = new System.Drawing.Point(340, 14);
            this.lblTrackbarValue.Name = "lblTrackbarValue";
            this.lblTrackbarValue.Size = new System.Drawing.Size(12, 13);
            this.lblTrackbarValue.TabIndex = 20;
            this.lblTrackbarValue.Text = "x";
            // 
            // lblMin
            // 
            this.lblMin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMin.AutoSize = true;
            this.lblMin.Enabled = false;
            this.lblMin.Location = new System.Drawing.Point(358, 14);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(43, 13);
            this.lblMin.TabIndex = 27;
            this.lblMin.Text = "minutes";
            // 
            // pnlMain
            // 
            this.pnlMain.AllowDrop = true;
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlChapterDB);
            this.pnlMain.Controls.Add(this.btnSwitch);
            this.pnlMain.Controls.Add(this.lblModeValue);
            this.pnlMain.Controls.Add(this.lblMode);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.lblSettings);
            this.pnlMain.Controls.Add(this.btnMerge);
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.cboxOverwrite);
            this.pnlMain.Controls.Add(this.lblTutorial);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(410, 176);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlChapterDB
            // 
            this.pnlChapterDB.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlChapterDB.Location = new System.Drawing.Point(0, 0);
            this.pnlChapterDB.Name = "pnlChapterDB";
            this.pnlChapterDB.Size = new System.Drawing.Size(410, 71);
            this.pnlChapterDB.TabIndex = 28;
            this.pnlChapterDB.Visible = false;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitch.Enabled = false;
            this.btnSwitch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSwitch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSwitch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitch.Image")));
            this.btnSwitch.Location = new System.Drawing.Point(246, 71);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(26, 24);
            this.btnSwitch.TabIndex = 31;
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // lblModeValue
            // 
            this.lblModeValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblModeValue.AutoSize = true;
            this.lblModeValue.Enabled = false;
            this.lblModeValue.Location = new System.Drawing.Point(188, 77);
            this.lblModeValue.Name = "lblModeValue";
            this.lblModeValue.Size = new System.Drawing.Size(42, 13);
            this.lblModeValue.TabIndex = 30;
            this.lblModeValue.Text = "Interval";
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.AutoSize = true;
            this.lblMode.Enabled = false;
            this.lblMode.Location = new System.Drawing.Point(145, 77);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(37, 13);
            this.lblMode.TabIndex = 29;
            this.lblMode.Text = "Mode:";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(194, 161);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(24, 13);
            this.lblStatus.TabIndex = 28;
            this.lblStatus.Text = "0/0";
            this.lblStatus.Visible = false;
            // 
            // lblSettings
            // 
            this.lblSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSettings.AutoSize = true;
            this.lblSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSettings.Location = new System.Drawing.Point(12, 158);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(45, 13);
            this.lblSettings.TabIndex = 26;
            this.lblSettings.Text = "Settings";
            this.lblSettings.Click += new System.EventHandler(this.lblSettings_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.AllowDrop = true;
            this.btnMerge.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMerge.Enabled = false;
            this.btnMerge.Location = new System.Drawing.Point(164, 137);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(85, 23);
            this.btnMerge.TabIndex = 18;
            this.btnMerge.Text = "Chapterize";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AllowDrop = true;
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblVersion.Location = new System.Drawing.Point(356, 163);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(11, 12);
            this.lblVersion.TabIndex = 19;
            this.lblVersion.Text = "v";
            this.lblVersion.Click += new System.EventHandler(this.label9_Click);
            // 
            // cboxOverwrite
            // 
            this.cboxOverwrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxOverwrite.AutoSize = true;
            this.cboxOverwrite.Checked = true;
            this.cboxOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxOverwrite.Enabled = false;
            this.cboxOverwrite.Location = new System.Drawing.Point(300, 141);
            this.cboxOverwrite.Name = "cboxOverwrite";
            this.cboxOverwrite.Size = new System.Drawing.Size(104, 17);
            this.cboxOverwrite.TabIndex = 22;
            this.cboxOverwrite.Text = "Overwrite old file";
            this.cboxOverwrite.UseVisualStyleBackColor = true;
            // 
            // lblTutorial
            // 
            this.lblTutorial.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTutorial.AutoSize = true;
            this.lblTutorial.Location = new System.Drawing.Point(54, 103);
            this.lblTutorial.Name = "lblTutorial";
            this.lblTutorial.Size = new System.Drawing.Size(290, 13);
            this.lblTutorial.TabIndex = 24;
            this.lblTutorial.Text = "Start by either dropping a MKV file on me or right-clicking me";
            // 
            // tpQueue
            // 
            this.tpQueue.Controls.Add(this.btnRemove);
            this.tpQueue.Controls.Add(this.btnAdd);
            this.tpQueue.Controls.Add(this.lboxFiles);
            this.tpQueue.Location = new System.Drawing.Point(4, 23);
            this.tpQueue.Name = "tpQueue";
            this.tpQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tpQueue.Size = new System.Drawing.Size(416, 182);
            this.tpQueue.TabIndex = 1;
            this.tpQueue.Text = "Queue";
            this.tpQueue.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(381, 47);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(27, 23);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "-";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(381, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(27, 23);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lboxFiles
            // 
            this.lboxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxFiles.FormattingEnabled = true;
            this.lboxFiles.Location = new System.Drawing.Point(3, 6);
            this.lboxFiles.Name = "lboxFiles";
            this.lboxFiles.Size = new System.Drawing.Size(367, 173);
            this.lboxFiles.TabIndex = 0;
            // 
            // tpAdvSettings
            // 
            this.tpAdvSettings.Controls.Add(this.grpboxMKVHasChapters);
            this.tpAdvSettings.Location = new System.Drawing.Point(4, 23);
            this.tpAdvSettings.Name = "tpAdvSettings";
            this.tpAdvSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvSettings.Size = new System.Drawing.Size(416, 182);
            this.tpAdvSettings.TabIndex = 2;
            this.tpAdvSettings.Text = "Advanced Settings";
            this.tpAdvSettings.UseVisualStyleBackColor = true;
            // 
            // grpboxMKVHasChapters
            // 
            this.grpboxMKVHasChapters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxMKVHasChapters.Controls.Add(this.rbtnDoNothing);
            this.grpboxMKVHasChapters.Controls.Add(this.rbtnRemoveThem);
            this.grpboxMKVHasChapters.Controls.Add(this.rbtnReplaceThem);
            this.grpboxMKVHasChapters.Location = new System.Drawing.Point(6, 6);
            this.grpboxMKVHasChapters.Name = "grpboxMKVHasChapters";
            this.grpboxMKVHasChapters.Size = new System.Drawing.Size(380, 64);
            this.grpboxMKVHasChapters.TabIndex = 1;
            this.grpboxMKVHasChapters.TabStop = false;
            this.grpboxMKVHasChapters.Text = "If the mkv has chapters...";
            // 
            // rbtnDoNothing
            // 
            this.rbtnDoNothing.AutoSize = true;
            this.rbtnDoNothing.Location = new System.Drawing.Point(261, 28);
            this.rbtnDoNothing.Name = "rbtnDoNothing";
            this.rbtnDoNothing.Size = new System.Drawing.Size(90, 17);
            this.rbtnDoNothing.TabIndex = 2;
            this.rbtnDoNothing.Text = "Skip the MKV";
            this.rbtnDoNothing.UseVisualStyleBackColor = true;
            this.rbtnDoNothing.CheckedChanged += new System.EventHandler(this.rbtnDoNothing_CheckedChanged);
            // 
            // rbtnRemoveThem
            // 
            this.rbtnRemoveThem.AutoSize = true;
            this.rbtnRemoveThem.Location = new System.Drawing.Point(128, 28);
            this.rbtnRemoveThem.Name = "rbtnRemoveThem";
            this.rbtnRemoveThem.Size = new System.Drawing.Size(95, 17);
            this.rbtnRemoveThem.TabIndex = 1;
            this.rbtnRemoveThem.Text = "Remove Them";
            this.rbtnRemoveThem.UseVisualStyleBackColor = true;
            this.rbtnRemoveThem.CheckedChanged += new System.EventHandler(this.rbtnRemoveThem_CheckedChanged);
            // 
            // rbtnReplaceThem
            // 
            this.rbtnReplaceThem.AutoSize = true;
            this.rbtnReplaceThem.Checked = true;
            this.rbtnReplaceThem.Location = new System.Drawing.Point(6, 28);
            this.rbtnReplaceThem.Name = "rbtnReplaceThem";
            this.rbtnReplaceThem.Size = new System.Drawing.Size(95, 17);
            this.rbtnReplaceThem.TabIndex = 0;
            this.rbtnReplaceThem.TabStop = true;
            this.rbtnReplaceThem.Text = "Replace Them";
            this.rbtnReplaceThem.UseVisualStyleBackColor = true;
            this.rbtnReplaceThem.CheckedChanged += new System.EventHandler(this.rbtnReplaceThem_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.progressBar.Location = new System.Drawing.Point(7, 172);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(365, 23);
            this.progressBar.TabIndex = 25;
            this.progressBar.Text = "0%";
            // 
            // MKVC
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(424, 209);
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MKVC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MKV Chapterizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenu.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.pnlInterval.ResumeLayout(false);
            this.pnlInterval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.tpQueue.ResumeLayout(false);
            this.tpAdvSettings.ResumeLayout(false);
            this.grpboxMKVHasChapters.ResumeLayout(false);
            this.grpboxMKVHasChapters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private WinForms.Controls.ProgressBarWithPercentage progressBar;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox cboxOverwrite;
        private System.Windows.Forms.Label lblTutorial;
        private Dotnetrix.Samples.CSharp.TabControl tabControl;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.TabPage tpQueue;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ListBox lboxFiles;
        private System.Windows.Forms.OpenFileDialog openMKVdlg;
        private System.Windows.Forms.TabPage tpAdvSettings;
        private System.Windows.Forms.GroupBox grpboxMKVHasChapters;
        private System.Windows.Forms.RadioButton rbtnDoNothing;
        private System.Windows.Forms.RadioButton rbtnRemoveThem;
        private System.Windows.Forms.RadioButton rbtnReplaceThem;
        private System.Windows.Forms.ToolStripMenuItem queueToolStripMenuItem;
        private System.Windows.Forms.Label lblStatus;
        private System.ComponentModel.BackgroundWorker bwCheckUpdates;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Label lblModeValue;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.Panel pnlChapterDB;
        private System.Windows.Forms.Panel pnlInterval;
        private System.Windows.Forms.Label lblChapterInterval;
        private System.Windows.Forms.Label lblChapterCount;
        private System.Windows.Forms.Label lblNumOfChapters;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label lblTrackbarValue;
        private System.Windows.Forms.Label lblMin;
    }
}

