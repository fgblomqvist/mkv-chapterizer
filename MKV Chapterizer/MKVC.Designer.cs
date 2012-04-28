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
            this.tsmtWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMKVdlg = new System.Windows.Forms.OpenFileDialog();
            this.bwCheckUpdates = new System.ComponentModel.BackgroundWorker();
            this.ttInfo = new System.Windows.Forms.ToolTip(this.components);
            this.odlgChooseChapterFile = new System.Windows.Forms.OpenFileDialog();
            this.tabControl = new Dotnetrix.Samples.CSharp.TabControl();
            this.tpSettings = new System.Windows.Forms.TabPage();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.lblSettings = new System.Windows.Forms.Label();
            this.pnlUIMode = new System.Windows.Forms.Panel();
            this.lblUIMode = new System.Windows.Forms.Label();
            this.pnlModeChange = new System.Windows.Forms.Panel();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblModeValue = new System.Windows.Forms.Label();
            this.pnlInterval = new System.Windows.Forms.Panel();
            this.cboxUnit = new System.Windows.Forms.ComboBox();
            this.lblChapterInterval = new System.Windows.Forms.Label();
            this.lblChapterCount = new System.Windows.Forms.Label();
            this.lblNumOfChapters = new System.Windows.Forms.Label();
            this.tbarInterval = new System.Windows.Forms.TrackBar();
            this.lblTrackbarValue = new System.Windows.Forms.Label();
            this.pnlChapterDB = new System.Windows.Forms.Panel();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnMerge = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.chkboxOverwrite = new System.Windows.Forms.CheckBox();
            this.lblTutorial = new System.Windows.Forms.Label();
            this.pnlChapterFile = new System.Windows.Forms.Panel();
            this.lblFileLocation = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtChapterFileLocation = new System.Windows.Forms.TextBox();
            this.tpQueue = new System.Windows.Forms.TabPage();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnAddFolder = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lboxFiles = new System.Windows.Forms.ListBox();
            this.tpAdvSettings = new System.Windows.Forms.TabPage();
            this.grpboxChapterFile = new System.Windows.Forms.GroupBox();
            this.btnChapterFormat = new System.Windows.Forms.Button();
            this.rbtnExtractChapters = new System.Windows.Forms.RadioButton();
            this.rbtnGenerateNew = new System.Windows.Forms.RadioButton();
            this.chkboxOutputChapterfile = new System.Windows.Forms.CheckBox();
            this.grpboxMKVHasChapters = new System.Windows.Forms.GroupBox();
            this.rbtnDoNothing = new System.Windows.Forms.RadioButton();
            this.rbtnRemoveThem = new System.Windows.Forms.RadioButton();
            this.rbtnReplaceThem = new System.Windows.Forms.RadioButton();
            this.progressBar = new WinForms.Controls.ProgressBarWithPercentage();
            this.contextMenu.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tpSettings.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlSettings.SuspendLayout();
            this.pnlUIMode.SuspendLayout();
            this.pnlModeChange.SuspendLayout();
            this.pnlInterval.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarInterval)).BeginInit();
            this.pnlChapterDB.SuspendLayout();
            this.pnlChapterFile.SuspendLayout();
            this.tpQueue.SuspendLayout();
            this.tpAdvSettings.SuspendLayout();
            this.grpboxChapterFile.SuspendLayout();
            this.grpboxMKVHasChapters.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsmtWebsite
            // 
            this.tsmtWebsite.Name = "tsmtWebsite";
            this.tsmtWebsite.Size = new System.Drawing.Size(116, 22);
            this.tsmtWebsite.Text = "Website";
            this.tsmtWebsite.Click += new System.EventHandler(this.websiteToolStripMenuItem_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmtWebsite});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(117, 26);
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
            // odlgChooseChapterFile
            // 
            this.odlgChooseChapterFile.Title = "Please choose a chapter file";
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tpSettings);
            this.tabControl.Controls.Add(this.tpQueue);
            this.tabControl.Controls.Add(this.tpAdvSettings);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(445, 197);
            this.tabControl.TabIndex = 1;
            // 
            // tpSettings
            // 
            this.tpSettings.Controls.Add(this.pnlMain);
            this.tpSettings.Location = new System.Drawing.Point(4, 23);
            this.tpSettings.Name = "tpSettings";
            this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpSettings.Size = new System.Drawing.Size(437, 170);
            this.tpSettings.TabIndex = 0;
            this.tpSettings.Text = "Settings";
            this.tpSettings.UseVisualStyleBackColor = true;
            // 
            // pnlMain
            // 
            this.pnlMain.AllowDrop = true;
            this.pnlMain.BackColor = System.Drawing.Color.Transparent;
            this.pnlMain.Controls.Add(this.pnlSettings);
            this.pnlMain.Controls.Add(this.pnlUIMode);
            this.pnlMain.Controls.Add(this.pnlModeChange);
            this.pnlMain.Controls.Add(this.pnlInterval);
            this.pnlMain.Controls.Add(this.pnlChapterDB);
            this.pnlMain.Controls.Add(this.lblStatus);
            this.pnlMain.Controls.Add(this.btnMerge);
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.chkboxOverwrite);
            this.pnlMain.Controls.Add(this.lblTutorial);
            this.pnlMain.Controls.Add(this.pnlChapterFile);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(431, 164);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlSettings
            // 
            this.pnlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlSettings.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSettings.Controls.Add(this.lblSettings);
            this.pnlSettings.Location = new System.Drawing.Point(2, 144);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(54, 18);
            this.pnlSettings.TabIndex = 36;
            // 
            // lblSettings
            // 
            this.lblSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSettings.AutoSize = true;
            this.lblSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSettings.Location = new System.Drawing.Point(3, 1);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(47, 13);
            this.lblSettings.TabIndex = 26;
            this.lblSettings.Text = "Settings";
            this.lblSettings.Click += new System.EventHandler(this.pnlSettings_Click);
            this.lblSettings.MouseEnter += new System.EventHandler(this.MouseEnter_ToHand);
            this.lblSettings.MouseLeave += new System.EventHandler(this.MouseLeave_ToDefault);
            // 
            // pnlUIMode
            // 
            this.pnlUIMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlUIMode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUIMode.Controls.Add(this.lblUIMode);
            this.pnlUIMode.Location = new System.Drawing.Point(58, 144);
            this.pnlUIMode.Name = "pnlUIMode";
            this.pnlUIMode.Size = new System.Drawing.Size(93, 18);
            this.pnlUIMode.TabIndex = 35;
            this.ttInfo.SetToolTip(this.pnlUIMode, "Switch");
            // 
            // lblUIMode
            // 
            this.lblUIMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblUIMode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUIMode.Location = new System.Drawing.Point(3, 1);
            this.lblUIMode.Name = "lblUIMode";
            this.lblUIMode.Size = new System.Drawing.Size(86, 13);
            this.lblUIMode.TabIndex = 34;
            this.lblUIMode.Text = "Advanced Mode";
            this.lblUIMode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ttInfo.SetToolTip(this.lblUIMode, "Switch mode");
            this.lblUIMode.Click += new System.EventHandler(this.lblUIMode_Click);
            this.lblUIMode.MouseEnter += new System.EventHandler(this.MouseEnter_ToHand);
            this.lblUIMode.MouseLeave += new System.EventHandler(this.MouseLeave_ToDefault);
            // 
            // pnlModeChange
            // 
            this.pnlModeChange.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pnlModeChange.Controls.Add(this.btnSwitch);
            this.pnlModeChange.Controls.Add(this.lblMode);
            this.pnlModeChange.Controls.Add(this.lblModeValue);
            this.pnlModeChange.Location = new System.Drawing.Point(150, 80);
            this.pnlModeChange.Name = "pnlModeChange";
            this.pnlModeChange.Size = new System.Drawing.Size(130, 23);
            this.pnlModeChange.TabIndex = 33;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSwitch.BackColor = System.Drawing.Color.Transparent;
            this.btnSwitch.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.btnSwitch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.btnSwitch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.btnSwitch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSwitch.Image = ((System.Drawing.Image)(resources.GetObject("btnSwitch.Image")));
            this.btnSwitch.Location = new System.Drawing.Point(110, 0);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(17, 24);
            this.btnSwitch.TabIndex = 31;
            this.btnSwitch.UseVisualStyleBackColor = false;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // lblMode
            // 
            this.lblMode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(9, 6);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(37, 13);
            this.lblMode.TabIndex = 29;
            this.lblMode.Text = "Mode:";
            // 
            // lblModeValue
            // 
            this.lblModeValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblModeValue.AutoSize = true;
            this.lblModeValue.Location = new System.Drawing.Point(46, 6);
            this.lblModeValue.Name = "lblModeValue";
            this.lblModeValue.Size = new System.Drawing.Size(44, 13);
            this.lblModeValue.TabIndex = 30;
            this.lblModeValue.Text = "Interval";
            // 
            // pnlInterval
            // 
            this.pnlInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlInterval.Controls.Add(this.cboxUnit);
            this.pnlInterval.Controls.Add(this.lblChapterInterval);
            this.pnlInterval.Controls.Add(this.lblChapterCount);
            this.pnlInterval.Controls.Add(this.lblNumOfChapters);
            this.pnlInterval.Controls.Add(this.tbarInterval);
            this.pnlInterval.Controls.Add(this.lblTrackbarValue);
            this.pnlInterval.Location = new System.Drawing.Point(0, 0);
            this.pnlInterval.Name = "pnlInterval";
            this.pnlInterval.Size = new System.Drawing.Size(431, 74);
            this.pnlInterval.TabIndex = 32;
            // 
            // cboxUnit
            // 
            this.cboxUnit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxUnit.FormattingEnabled = true;
            this.cboxUnit.Items.AddRange(new object[] {
            "seconds",
            "minutes",
            "hours"});
            this.cboxUnit.Location = new System.Drawing.Point(354, 10);
            this.cboxUnit.Name = "cboxUnit";
            this.cboxUnit.Size = new System.Drawing.Size(74, 21);
            this.cboxUnit.TabIndex = 28;
            this.cboxUnit.SelectedIndexChanged += new System.EventHandler(this.cboxUnit_SelectedIndexChanged);
            // 
            // lblChapterInterval
            // 
            this.lblChapterInterval.AllowDrop = true;
            this.lblChapterInterval.AutoSize = true;
            this.lblChapterInterval.Enabled = false;
            this.lblChapterInterval.Location = new System.Drawing.Point(9, 14);
            this.lblChapterInterval.Name = "lblChapterInterval";
            this.lblChapterInterval.Size = new System.Drawing.Size(89, 13);
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
            this.lblNumOfChapters.Size = new System.Drawing.Size(109, 13);
            this.lblNumOfChapters.TabIndex = 13;
            this.lblNumOfChapters.Text = "Number of Chapters:";
            // 
            // tbarInterval
            // 
            this.tbarInterval.AllowDrop = true;
            this.tbarInterval.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbarInterval.BackColor = System.Drawing.Color.White;
            this.tbarInterval.Enabled = false;
            this.tbarInterval.Location = new System.Drawing.Point(100, 14);
            this.tbarInterval.Maximum = 60;
            this.tbarInterval.Minimum = 1;
            this.tbarInterval.Name = "tbarInterval";
            this.tbarInterval.Size = new System.Drawing.Size(228, 45);
            this.tbarInterval.TabIndex = 19;
            this.tbarInterval.Value = 5;
            this.tbarInterval.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lblTrackbarValue
            // 
            this.lblTrackbarValue.AllowDrop = true;
            this.lblTrackbarValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrackbarValue.AutoSize = true;
            this.lblTrackbarValue.Enabled = false;
            this.lblTrackbarValue.Location = new System.Drawing.Point(334, 14);
            this.lblTrackbarValue.Name = "lblTrackbarValue";
            this.lblTrackbarValue.Size = new System.Drawing.Size(13, 13);
            this.lblTrackbarValue.TabIndex = 20;
            this.lblTrackbarValue.Text = "x";
            // 
            // pnlChapterDB
            // 
            this.pnlChapterDB.Controls.Add(this.lblSearch);
            this.pnlChapterDB.Controls.Add(this.btnSearch);
            this.pnlChapterDB.Controls.Add(this.txtSearch);
            this.pnlChapterDB.Location = new System.Drawing.Point(0, 0);
            this.pnlChapterDB.Name = "pnlChapterDB";
            this.pnlChapterDB.Size = new System.Drawing.Size(410, 71);
            this.pnlChapterDB.TabIndex = 28;
            this.pnlChapterDB.Visible = false;
            // 
            // lblSearch
            // 
            this.lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(180, 3);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(72, 13);
            this.lblSearch.TabIndex = 2;
            this.lblSearch.Text = "Movie Name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(175, 45);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(85, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search...";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.Location = new System.Drawing.Point(122, 19);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(191, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(163, 146);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(94, 13);
            this.lblStatus.TabIndex = 28;
            this.lblStatus.Text = "0/0 MyMovie.mkv";
            this.lblStatus.Visible = false;
            // 
            // btnMerge
            // 
            this.btnMerge.AllowDrop = true;
            this.btnMerge.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMerge.Enabled = false;
            this.btnMerge.Location = new System.Drawing.Point(166, 115);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(90, 23);
            this.btnMerge.TabIndex = 18;
            this.btnMerge.Text = "Chapterize";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AllowDrop = true;
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lblVersion.Location = new System.Drawing.Point(400, 150);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(10, 12);
            this.lblVersion.TabIndex = 19;
            this.lblVersion.Text = "v";
            this.lblVersion.Click += new System.EventHandler(this.label9_Click);
            this.lblVersion.MouseEnter += new System.EventHandler(this.MouseEnter_ToHand);
            this.lblVersion.MouseLeave += new System.EventHandler(this.MouseLeave_ToDefault);
            // 
            // chkboxOverwrite
            // 
            this.chkboxOverwrite.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkboxOverwrite.AutoSize = true;
            this.chkboxOverwrite.Checked = true;
            this.chkboxOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkboxOverwrite.Enabled = false;
            this.chkboxOverwrite.Location = new System.Drawing.Point(315, 121);
            this.chkboxOverwrite.Name = "chkboxOverwrite";
            this.chkboxOverwrite.Size = new System.Drawing.Size(111, 17);
            this.chkboxOverwrite.TabIndex = 22;
            this.chkboxOverwrite.Text = "Overwrite old file";
            this.chkboxOverwrite.UseVisualStyleBackColor = true;
            this.chkboxOverwrite.CheckedChanged += new System.EventHandler(this.cboxOverwrite_CheckedChanged);
            // 
            // lblTutorial
            // 
            this.lblTutorial.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTutorial.AutoSize = true;
            this.lblTutorial.Location = new System.Drawing.Point(32, 86);
            this.lblTutorial.Name = "lblTutorial";
            this.lblTutorial.Size = new System.Drawing.Size(364, 13);
            this.lblTutorial.TabIndex = 24;
            this.lblTutorial.Text = "Start by either dropping a MKV file on me or switching to advanced mode";
            // 
            // pnlChapterFile
            // 
            this.pnlChapterFile.Controls.Add(this.lblFileLocation);
            this.pnlChapterFile.Controls.Add(this.btnBrowse);
            this.pnlChapterFile.Controls.Add(this.txtChapterFileLocation);
            this.pnlChapterFile.Location = new System.Drawing.Point(0, 0);
            this.pnlChapterFile.Name = "pnlChapterFile";
            this.pnlChapterFile.Size = new System.Drawing.Size(431, 71);
            this.pnlChapterFile.TabIndex = 29;
            this.pnlChapterFile.Visible = false;
            // 
            // lblFileLocation
            // 
            this.lblFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFileLocation.AutoSize = true;
            this.lblFileLocation.Location = new System.Drawing.Point(12, 17);
            this.lblFileLocation.Name = "lblFileLocation";
            this.lblFileLocation.Size = new System.Drawing.Size(196, 13);
            this.lblFileLocation.TabIndex = 2;
            this.lblFileLocation.Text = "Choose the location of the chapter file:";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowse.Location = new System.Drawing.Point(396, 31);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(30, 23);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtChapterFileLocation
            // 
            this.txtChapterFileLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtChapterFileLocation.Location = new System.Drawing.Point(15, 33);
            this.txtChapterFileLocation.Name = "txtChapterFileLocation";
            this.txtChapterFileLocation.Size = new System.Drawing.Size(375, 20);
            this.txtChapterFileLocation.TabIndex = 0;
            // 
            // tpQueue
            // 
            this.tpQueue.Controls.Add(this.btnExport);
            this.tpQueue.Controls.Add(this.btnImport);
            this.tpQueue.Controls.Add(this.btnAddFolder);
            this.tpQueue.Controls.Add(this.btnRemove);
            this.tpQueue.Controls.Add(this.btnAdd);
            this.tpQueue.Controls.Add(this.lboxFiles);
            this.tpQueue.Location = new System.Drawing.Point(4, 23);
            this.tpQueue.Name = "tpQueue";
            this.tpQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tpQueue.Size = new System.Drawing.Size(437, 170);
            this.tpQueue.TabIndex = 1;
            this.tpQueue.Text = "Advanced";
            this.tpQueue.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExport.BackgroundImage")));
            this.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.Location = new System.Drawing.Point(403, 138);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(28, 28);
            this.btnExport.TabIndex = 5;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnImport
            // 
            this.btnImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImport.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImport.BackgroundImage")));
            this.btnImport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImport.Location = new System.Drawing.Point(403, 105);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(28, 28);
            this.btnImport.TabIndex = 4;
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnAddFolder
            // 
            this.btnAddFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddFolder.BackgroundImage")));
            this.btnAddFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddFolder.Location = new System.Drawing.Point(403, 39);
            this.btnAddFolder.Name = "btnAddFolder";
            this.btnAddFolder.Size = new System.Drawing.Size(29, 28);
            this.btnAddFolder.TabIndex = 3;
            this.btnAddFolder.UseVisualStyleBackColor = true;
            this.btnAddFolder.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnRemove.BackgroundImage")));
            this.btnRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRemove.Location = new System.Drawing.Point(403, 72);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(28, 28);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAdd.BackgroundImage")));
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(403, 6);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(28, 28);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lboxFiles
            // 
            this.lboxFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lboxFiles.FormattingEnabled = true;
            this.lboxFiles.HorizontalScrollbar = true;
            this.lboxFiles.Location = new System.Drawing.Point(3, 6);
            this.lboxFiles.Name = "lboxFiles";
            this.lboxFiles.Size = new System.Drawing.Size(394, 160);
            this.lboxFiles.TabIndex = 0;
            // 
            // tpAdvSettings
            // 
            this.tpAdvSettings.Controls.Add(this.grpboxChapterFile);
            this.tpAdvSettings.Controls.Add(this.grpboxMKVHasChapters);
            this.tpAdvSettings.Location = new System.Drawing.Point(4, 23);
            this.tpAdvSettings.Name = "tpAdvSettings";
            this.tpAdvSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvSettings.Size = new System.Drawing.Size(437, 170);
            this.tpAdvSettings.TabIndex = 2;
            this.tpAdvSettings.Text = "Advanced Settings";
            this.tpAdvSettings.UseVisualStyleBackColor = true;
            // 
            // grpboxChapterFile
            // 
            this.grpboxChapterFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxChapterFile.Controls.Add(this.btnChapterFormat);
            this.grpboxChapterFile.Controls.Add(this.rbtnExtractChapters);
            this.grpboxChapterFile.Controls.Add(this.rbtnGenerateNew);
            this.grpboxChapterFile.Controls.Add(this.chkboxOutputChapterfile);
            this.grpboxChapterFile.Location = new System.Drawing.Point(6, 76);
            this.grpboxChapterFile.Name = "grpboxChapterFile";
            this.grpboxChapterFile.Size = new System.Drawing.Size(423, 88);
            this.grpboxChapterFile.TabIndex = 2;
            this.grpboxChapterFile.TabStop = false;
            this.grpboxChapterFile.Text = "Chapter file creation";
            // 
            // btnChapterFormat
            // 
            this.btnChapterFormat.Location = new System.Drawing.Point(322, 43);
            this.btnChapterFormat.Name = "btnChapterFormat";
            this.btnChapterFormat.Size = new System.Drawing.Size(56, 34);
            this.btnChapterFormat.TabIndex = 3;
            this.btnChapterFormat.Text = "Format";
            this.btnChapterFormat.UseVisualStyleBackColor = true;
            this.btnChapterFormat.Click += new System.EventHandler(this.btnChapterFormat_Click);
            // 
            // rbtnExtractChapters
            // 
            this.rbtnExtractChapters.AutoSize = true;
            this.rbtnExtractChapters.Enabled = false;
            this.rbtnExtractChapters.Location = new System.Drawing.Point(36, 60);
            this.rbtnExtractChapters.Name = "rbtnExtractChapters";
            this.rbtnExtractChapters.Size = new System.Drawing.Size(181, 17);
            this.rbtnExtractChapters.TabIndex = 2;
            this.rbtnExtractChapters.Text = "Extract chapters from the mkv\'s";
            this.ttInfo.SetToolTip(this.rbtnExtractChapters, "If the mkv has chapters, they will get extracted.\r\nOtherwise, nothing will be don" +
        "e.");
            this.rbtnExtractChapters.UseVisualStyleBackColor = true;
            // 
            // rbtnGenerateNew
            // 
            this.rbtnGenerateNew.AutoSize = true;
            this.rbtnGenerateNew.Checked = true;
            this.rbtnGenerateNew.Enabled = false;
            this.rbtnGenerateNew.Location = new System.Drawing.Point(36, 43);
            this.rbtnGenerateNew.Name = "rbtnGenerateNew";
            this.rbtnGenerateNew.Size = new System.Drawing.Size(153, 17);
            this.rbtnGenerateNew.TabIndex = 1;
            this.rbtnGenerateNew.TabStop = true;
            this.rbtnGenerateNew.Text = "Generate the chapter files";
            this.rbtnGenerateNew.UseVisualStyleBackColor = true;
            // 
            // chkboxOutputChapterfile
            // 
            this.chkboxOutputChapterfile.AutoSize = true;
            this.chkboxOutputChapterfile.Location = new System.Drawing.Point(17, 23);
            this.chkboxOutputChapterfile.Name = "chkboxOutputChapterfile";
            this.chkboxOutputChapterfile.Size = new System.Drawing.Size(265, 17);
            this.chkboxOutputChapterfile.TabIndex = 0;
            this.chkboxOutputChapterfile.Text = "Only output chapter files in the mkvs\' directories";
            this.ttInfo.SetToolTip(this.chkboxOutputChapterfile, "If this is checked, MKV Chapterizer will output a\r\nfile called \"chapters\" in the " +
        "directory of each mkv.\r\nThis file can later be used to merge manually with\r\nmkvm" +
        "erge.");
            this.chkboxOutputChapterfile.UseVisualStyleBackColor = true;
            this.chkboxOutputChapterfile.CheckedChanged += new System.EventHandler(this.chkboxOutputChapterfile_CheckedChanged);
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
            this.grpboxMKVHasChapters.Size = new System.Drawing.Size(423, 64);
            this.grpboxMKVHasChapters.TabIndex = 1;
            this.grpboxMKVHasChapters.TabStop = false;
            this.grpboxMKVHasChapters.Text = "If the mkv has chapters...";
            // 
            // rbtnDoNothing
            // 
            this.rbtnDoNothing.AutoSize = true;
            this.rbtnDoNothing.Location = new System.Drawing.Point(272, 31);
            this.rbtnDoNothing.Name = "rbtnDoNothing";
            this.rbtnDoNothing.Size = new System.Drawing.Size(91, 17);
            this.rbtnDoNothing.TabIndex = 2;
            this.rbtnDoNothing.Text = "Skip the MKV";
            this.rbtnDoNothing.UseVisualStyleBackColor = true;
            this.rbtnDoNothing.CheckedChanged += new System.EventHandler(this.rbtnDoNothing_CheckedChanged);
            // 
            // rbtnRemoveThem
            // 
            this.rbtnRemoveThem.AutoSize = true;
            this.rbtnRemoveThem.Location = new System.Drawing.Point(139, 31);
            this.rbtnRemoveThem.Name = "rbtnRemoveThem";
            this.rbtnRemoveThem.Size = new System.Drawing.Size(97, 17);
            this.rbtnRemoveThem.TabIndex = 1;
            this.rbtnRemoveThem.Text = "Remove Them";
            this.rbtnRemoveThem.UseVisualStyleBackColor = true;
            this.rbtnRemoveThem.CheckedChanged += new System.EventHandler(this.rbtnRemoveThem_CheckedChanged);
            // 
            // rbtnReplaceThem
            // 
            this.rbtnReplaceThem.AutoSize = true;
            this.rbtnReplaceThem.Checked = true;
            this.rbtnReplaceThem.Location = new System.Drawing.Point(17, 31);
            this.rbtnReplaceThem.Name = "rbtnReplaceThem";
            this.rbtnReplaceThem.Size = new System.Drawing.Size(97, 17);
            this.rbtnReplaceThem.TabIndex = 0;
            this.rbtnReplaceThem.TabStop = true;
            this.rbtnReplaceThem.Text = "Replace Them";
            this.rbtnReplaceThem.UseVisualStyleBackColor = true;
            this.rbtnReplaceThem.CheckedChanged += new System.EventHandler(this.rbtnReplaceThem_CheckedChanged);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.progressBar.Location = new System.Drawing.Point(7, 199);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(431, 23);
            this.progressBar.TabIndex = 25;
            this.progressBar.Text = "0%";
            // 
            // MKVC
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(445, 226);
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
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MKVC_Closing);
            this.Load += new System.EventHandler(this.MKVC_Load);
            this.contextMenu.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tpSettings.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlUIMode.ResumeLayout(false);
            this.pnlModeChange.ResumeLayout(false);
            this.pnlModeChange.PerformLayout();
            this.pnlInterval.ResumeLayout(false);
            this.pnlInterval.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbarInterval)).EndInit();
            this.pnlChapterDB.ResumeLayout(false);
            this.pnlChapterDB.PerformLayout();
            this.pnlChapterFile.ResumeLayout(false);
            this.pnlChapterFile.PerformLayout();
            this.tpQueue.ResumeLayout(false);
            this.tpAdvSettings.ResumeLayout(false);
            this.grpboxChapterFile.ResumeLayout(false);
            this.grpboxChapterFile.PerformLayout();
            this.grpboxMKVHasChapters.ResumeLayout(false);
            this.grpboxMKVHasChapters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private WinForms.Controls.ProgressBarWithPercentage progressBar;
        private System.Windows.Forms.ToolStripMenuItem tsmtWebsite;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.CheckBox chkboxOverwrite;
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
        private System.Windows.Forms.TrackBar tbarInterval;
        private System.Windows.Forms.Label lblTrackbarValue;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlModeChange;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ToolTip ttInfo;
        private System.Windows.Forms.GroupBox grpboxChapterFile;
        private System.Windows.Forms.CheckBox chkboxOutputChapterfile;
        private System.Windows.Forms.ComboBox cboxUnit;
        private System.Windows.Forms.Button btnAddFolder;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Label lblUIMode;
        private System.Windows.Forms.Panel pnlUIMode;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.RadioButton rbtnExtractChapters;
        private System.Windows.Forms.RadioButton rbtnGenerateNew;
        private System.Windows.Forms.Button btnChapterFormat;
        private System.Windows.Forms.Panel pnlChapterFile;
        private System.Windows.Forms.Label lblFileLocation;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtChapterFileLocation;
        private System.Windows.Forms.OpenFileDialog odlgChooseChapterFile;
    }
}

