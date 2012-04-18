namespace MKV_Chapterizer
{
    partial class ChapterDB
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
            this.lblName = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtboxSearchName = new System.Windows.Forms.TextBox();
            this.grpboxResults = new System.Windows.Forms.GroupBox();
            this.dgViewResults = new System.Windows.Forms.DataGridView();
            this.grpboxChapters = new System.Windows.Forms.GroupBox();
            this.lviewChapters = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpboxSearch = new System.Windows.Forms.GroupBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnUse = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblSelected = new System.Windows.Forms.Label();
            this.grpboxResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResults)).BeginInit();
            this.grpboxChapters.SuspendLayout();
            this.grpboxSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(39, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(363, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtboxSearchName
            // 
            this.txtboxSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtboxSearchName.Location = new System.Drawing.Point(50, 21);
            this.txtboxSearchName.Name = "txtboxSearchName";
            this.txtboxSearchName.Size = new System.Drawing.Size(307, 20);
            this.txtboxSearchName.TabIndex = 2;
            this.txtboxSearchName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtboxSearchName_KeyDown);
            // 
            // grpboxResults
            // 
            this.grpboxResults.Controls.Add(this.dgViewResults);
            this.grpboxResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpboxResults.Location = new System.Drawing.Point(0, 0);
            this.grpboxResults.Name = "grpboxResults";
            this.grpboxResults.Size = new System.Drawing.Size(218, 372);
            this.grpboxResults.TabIndex = 3;
            this.grpboxResults.TabStop = false;
            this.grpboxResults.Text = "Results";
            // 
            // dgViewResults
            // 
            this.dgViewResults.AllowUserToAddRows = false;
            this.dgViewResults.AllowUserToDeleteRows = false;
            this.dgViewResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgViewResults.BackgroundColor = System.Drawing.Color.White;
            this.dgViewResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgViewResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgViewResults.GridColor = System.Drawing.Color.White;
            this.dgViewResults.Location = new System.Drawing.Point(3, 16);
            this.dgViewResults.MultiSelect = false;
            this.dgViewResults.Name = "dgViewResults";
            this.dgViewResults.ReadOnly = true;
            this.dgViewResults.RowHeadersVisible = false;
            this.dgViewResults.RowTemplate.Height = 15;
            this.dgViewResults.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgViewResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgViewResults.Size = new System.Drawing.Size(212, 353);
            this.dgViewResults.TabIndex = 0;
            this.dgViewResults.SelectionChanged += new System.EventHandler(this.dgViewResults_SelectionChanged);
            this.dgViewResults.Sorted += new System.EventHandler(this.dgViewResults_Sorted);
            // 
            // grpboxChapters
            // 
            this.grpboxChapters.Controls.Add(this.lviewChapters);
            this.grpboxChapters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpboxChapters.Location = new System.Drawing.Point(0, 0);
            this.grpboxChapters.Name = "grpboxChapters";
            this.grpboxChapters.Size = new System.Drawing.Size(224, 372);
            this.grpboxChapters.TabIndex = 4;
            this.grpboxChapters.TabStop = false;
            this.grpboxChapters.Text = "Chapters";
            // 
            // lviewChapters
            // 
            this.lviewChapters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderTime});
            this.lviewChapters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lviewChapters.FullRowSelect = true;
            this.lviewChapters.Location = new System.Drawing.Point(3, 16);
            this.lviewChapters.Name = "lviewChapters";
            this.lviewChapters.Size = new System.Drawing.Size(218, 353);
            this.lviewChapters.TabIndex = 0;
            this.lviewChapters.UseCompatibleStateImageBehavior = false;
            this.lviewChapters.View = System.Windows.Forms.View.Details;
            this.lviewChapters.SelectedIndexChanged += new System.EventHandler(this.lviewChapters_SelectedIndexChanged);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 136;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Time";
            this.columnHeaderTime.Width = 78;
            // 
            // grpboxSearch
            // 
            this.grpboxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxSearch.Controls.Add(this.lblStatus);
            this.grpboxSearch.Controls.Add(this.lblName);
            this.grpboxSearch.Controls.Add(this.btnSearch);
            this.grpboxSearch.Controls.Add(this.txtboxSearchName);
            this.grpboxSearch.Location = new System.Drawing.Point(12, 12);
            this.grpboxSearch.Name = "grpboxSearch";
            this.grpboxSearch.Size = new System.Drawing.Size(446, 80);
            this.grpboxSearch.TabIndex = 5;
            this.grpboxSearch.TabStop = false;
            this.grpboxSearch.Text = "Search";
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(153, 53);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(138, 13);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Please enter a moviename";
            // 
            // btnUse
            // 
            this.btnUse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUse.Location = new System.Drawing.Point(12, 493);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(446, 27);
            this.btnUse.TabIndex = 6;
            this.btnUse.Text = "Use";
            this.btnUse.UseVisualStyleBackColor = true;
            this.btnUse.Click += new System.EventHandler(this.btnUse_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 98);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpboxResults);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpboxChapters);
            this.splitContainer1.Size = new System.Drawing.Size(446, 372);
            this.splitContainer1.SplitterDistance = 218;
            this.splitContainer1.TabIndex = 7;
            // 
            // lblSelected
            // 
            this.lblSelected.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSelected.AutoSize = true;
            this.lblSelected.Location = new System.Drawing.Point(150, 473);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(153, 13);
            this.lblSelected.TabIndex = 4;
            this.lblSelected.Text = "You have selected 0 chapters";
            // 
            // ChapterDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 532);
            this.Controls.Add(this.lblSelected);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnUse);
            this.Controls.Add(this.grpboxSearch);
            this.Name = "ChapterDB";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChapterDB";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChapterDB_FormClosing);
            this.Load += new System.EventHandler(this.ChapterDB_Load);
            this.grpboxResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgViewResults)).EndInit();
            this.grpboxChapters.ResumeLayout(false);
            this.grpboxSearch.ResumeLayout(false);
            this.grpboxSearch.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtboxSearchName;
        private System.Windows.Forms.GroupBox grpboxResults;
        private System.Windows.Forms.GroupBox grpboxChapters;
        private System.Windows.Forms.ListView lviewChapters;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.GroupBox grpboxSearch;
        private System.Windows.Forms.Button btnUse;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgViewResults;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblSelected;
    }
}