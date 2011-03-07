﻿namespace MKV_Chapterizer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblSettings = new System.Windows.Forms.Label();
            this.progressBar = new WinForms.Controls.ProgressBarWithPercentage();
            this.lblTrackbarValue = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.btnMerge = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cboxOverwrite = new System.Windows.Forms.CheckBox();
            this.lblTutorial = new System.Windows.Forms.Label();
            this.lblChapterCount = new System.Windows.Forms.Label();
            this.bwAddChapters = new System.ComponentModel.BackgroundWorker();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.bwRemoveChapters = new System.ComponentModel.BackgroundWorker();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.contextMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.Controls.Add(this.lblMin);
            this.panel1.Controls.Add(this.lblSettings);
            this.panel1.Controls.Add(this.progressBar);
            this.panel1.Controls.Add(this.lblTrackbarValue);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.btnMerge);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.cboxOverwrite);
            this.panel1.Controls.Add(this.lblTutorial);
            this.panel1.Controls.Add(this.lblChapterCount);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 139);
            this.panel1.TabIndex = 0;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Enabled = false;
            this.lblMin.Location = new System.Drawing.Point(310, 9);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(43, 13);
            this.lblMin.TabIndex = 27;
            this.lblMin.Text = "minutes";
            // 
            // lblSettings
            // 
            this.lblSettings.AutoSize = true;
            this.lblSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSettings.Location = new System.Drawing.Point(12, 117);
            this.lblSettings.Name = "lblSettings";
            this.lblSettings.Size = new System.Drawing.Size(45, 13);
            this.lblSettings.TabIndex = 26;
            this.lblSettings.Text = "Settings";
            this.lblSettings.Click += new System.EventHandler(this.lblSettings_Click);
            // 
            // progressBar
            // 
            this.progressBar.ForeColor = System.Drawing.SystemColors.Desktop;
            this.progressBar.Location = new System.Drawing.Point(3, 143);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(360, 23);
            this.progressBar.TabIndex = 25;
            this.progressBar.Text = "0%";
            // 
            // lblTrackbarValue
            // 
            this.lblTrackbarValue.AllowDrop = true;
            this.lblTrackbarValue.AutoSize = true;
            this.lblTrackbarValue.Enabled = false;
            this.lblTrackbarValue.Location = new System.Drawing.Point(292, 9);
            this.lblTrackbarValue.Name = "lblTrackbarValue";
            this.lblTrackbarValue.Size = new System.Drawing.Size(12, 13);
            this.lblTrackbarValue.TabIndex = 20;
            this.lblTrackbarValue.Text = "x";
            // 
            // trackBar1
            // 
            this.trackBar1.AllowDrop = true;
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(103, 9);
            this.trackBar1.Maximum = 30;
            this.trackBar1.Minimum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(184, 45);
            this.trackBar1.TabIndex = 19;
            this.trackBar1.Value = 5;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // btnMerge
            // 
            this.btnMerge.AllowDrop = true;
            this.btnMerge.Enabled = false;
            this.btnMerge.Location = new System.Drawing.Point(142, 96);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(75, 23);
            this.btnMerge.TabIndex = 18;
            this.btnMerge.Text = "Chapterize";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AllowDrop = true;
            this.label3.AutoSize = true;
            this.label3.Enabled = false;
            this.label3.Location = new System.Drawing.Point(12, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Number of Chapters:";
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Chapter Interval:";
            // 
            // label8
            // 
            this.label8.AllowDrop = true;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(116, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "Copyright 2011 © Cyb3rH4Xter";
            // 
            // label9
            // 
            this.label9.AllowDrop = true;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(329, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(11, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "v";
            // 
            // cboxOverwrite
            // 
            this.cboxOverwrite.AutoSize = true;
            this.cboxOverwrite.Checked = true;
            this.cboxOverwrite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cboxOverwrite.Enabled = false;
            this.cboxOverwrite.Location = new System.Drawing.Point(255, 100);
            this.cboxOverwrite.Name = "cboxOverwrite";
            this.cboxOverwrite.Size = new System.Drawing.Size(104, 17);
            this.cboxOverwrite.TabIndex = 22;
            this.cboxOverwrite.Text = "Overwrite old file";
            this.cboxOverwrite.UseVisualStyleBackColor = true;
            // 
            // lblTutorial
            // 
            this.lblTutorial.AutoSize = true;
            this.lblTutorial.Location = new System.Drawing.Point(100, 80);
            this.lblTutorial.Name = "lblTutorial";
            this.lblTutorial.Size = new System.Drawing.Size(170, 13);
            this.lblTutorial.TabIndex = 24;
            this.lblTutorial.Text = "Start by dropping a MKV file on me";
            // 
            // lblChapterCount
            // 
            this.lblChapterCount.AllowDrop = true;
            this.lblChapterCount.AutoSize = true;
            this.lblChapterCount.Location = new System.Drawing.Point(122, 57);
            this.lblChapterCount.Name = "lblChapterCount";
            this.lblChapterCount.Size = new System.Drawing.Size(0, 13);
            this.lblChapterCount.TabIndex = 17;
            // 
            // bwAddChapters
            // 
            this.bwAddChapters.WorkerReportsProgress = true;
            this.bwAddChapters.WorkerSupportsCancellation = true;
            this.bwAddChapters.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwAddChapters_DoWork);
            this.bwAddChapters.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwAddChapters_ProgressChanged);
            this.bwAddChapters.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwAddChapters_RunWorkerCompleted);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 22);
            this.toolStripMenuItem1.Text = "Check for Updates";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.websiteToolStripMenuItem.Text = "Website";
            this.websiteToolStripMenuItem.Click += new System.EventHandler(this.websiteToolStripMenuItem_Click);
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.websiteToolStripMenuItem});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(172, 48);
            // 
            // bwRemoveChapters
            // 
            this.bwRemoveChapters.WorkerReportsProgress = true;
            this.bwRemoveChapters.WorkerSupportsCancellation = true;
            this.bwRemoveChapters.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwRemoveChapters_DoWork);
            this.bwRemoveChapters.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwRemoveChapters_ProgressChanged);
            this.bwRemoveChapters.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwRemoveChapters_RunWorkerCompleted);
            // 
            // MKVC
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 139);
            this.ContextMenuStrip = this.contextMenu;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MKVC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MKV Chapterizer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_Closing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.contextMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTrackbarValue;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.Label lblChapterCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.ComponentModel.BackgroundWorker bwAddChapters;
        private System.Windows.Forms.CheckBox cboxOverwrite;
        private System.Windows.Forms.Label lblTutorial;
        private WinForms.Controls.ProgressBarWithPercentage progressBar;
        private System.Windows.Forms.Label lblSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.Label lblMin;
        private System.ComponentModel.BackgroundWorker bwRemoveChapters;
    }
}

