namespace MKV_Chapterizer.bin
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
            this.lboxResults = new System.Windows.Forms.ListBox();
            this.grpboxChapters = new System.Windows.Forms.GroupBox();
            this.lviewChapters = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpboxSearch = new System.Windows.Forms.GroupBox();
            this.btnUse = new System.Windows.Forms.Button();
            this.grpboxResults.SuspendLayout();
            this.grpboxChapters.SuspendLayout();
            this.grpboxSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(6, 24);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(38, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(329, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // txtboxSearchName
            // 
            this.txtboxSearchName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtboxSearchName.Location = new System.Drawing.Point(50, 21);
            this.txtboxSearchName.Name = "txtboxSearchName";
            this.txtboxSearchName.Size = new System.Drawing.Size(273, 20);
            this.txtboxSearchName.TabIndex = 2;
            // 
            // grpboxResults
            // 
            this.grpboxResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxResults.Controls.Add(this.lboxResults);
            this.grpboxResults.Location = new System.Drawing.Point(12, 77);
            this.grpboxResults.Name = "grpboxResults";
            this.grpboxResults.Size = new System.Drawing.Size(203, 355);
            this.grpboxResults.TabIndex = 3;
            this.grpboxResults.TabStop = false;
            this.grpboxResults.Text = "Results";
            // 
            // lboxResults
            // 
            this.lboxResults.FormattingEnabled = true;
            this.lboxResults.Location = new System.Drawing.Point(7, 19);
            this.lboxResults.Name = "lboxResults";
            this.lboxResults.Size = new System.Drawing.Size(190, 329);
            this.lboxResults.TabIndex = 0;
            // 
            // grpboxChapters
            // 
            this.grpboxChapters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxChapters.Controls.Add(this.lviewChapters);
            this.grpboxChapters.Location = new System.Drawing.Point(221, 77);
            this.grpboxChapters.Name = "grpboxChapters";
            this.grpboxChapters.Size = new System.Drawing.Size(203, 355);
            this.grpboxChapters.TabIndex = 4;
            this.grpboxChapters.TabStop = false;
            this.grpboxChapters.Text = "Chapters";
            // 
            // lviewChapters
            // 
            this.lviewChapters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderTime});
            this.lviewChapters.Location = new System.Drawing.Point(7, 19);
            this.lviewChapters.Name = "lviewChapters";
            this.lviewChapters.Size = new System.Drawing.Size(188, 329);
            this.lviewChapters.TabIndex = 0;
            this.lviewChapters.UseCompatibleStateImageBehavior = false;
            this.lviewChapters.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 123;
            // 
            // columnHeaderTime
            // 
            this.columnHeaderTime.Text = "Time";
            // 
            // grpboxSearch
            // 
            this.grpboxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpboxSearch.Controls.Add(this.lblName);
            this.grpboxSearch.Controls.Add(this.btnSearch);
            this.grpboxSearch.Controls.Add(this.txtboxSearchName);
            this.grpboxSearch.Location = new System.Drawing.Point(12, 12);
            this.grpboxSearch.Name = "grpboxSearch";
            this.grpboxSearch.Size = new System.Drawing.Size(412, 59);
            this.grpboxSearch.TabIndex = 5;
            this.grpboxSearch.TabStop = false;
            this.grpboxSearch.Text = "Search";
            // 
            // btnUse
            // 
            this.btnUse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUse.Location = new System.Drawing.Point(12, 438);
            this.btnUse.Name = "btnUse";
            this.btnUse.Size = new System.Drawing.Size(412, 27);
            this.btnUse.TabIndex = 6;
            this.btnUse.Text = "Use";
            this.btnUse.UseVisualStyleBackColor = true;
            // 
            // ChapterDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 477);
            this.Controls.Add(this.btnUse);
            this.Controls.Add(this.grpboxSearch);
            this.Controls.Add(this.grpboxChapters);
            this.Controls.Add(this.grpboxResults);
            this.Name = "ChapterDB";
            this.Text = "ChapterDB";
            this.Load += new System.EventHandler(this.ChapterDB_Load);
            this.grpboxResults.ResumeLayout(false);
            this.grpboxChapters.ResumeLayout(false);
            this.grpboxSearch.ResumeLayout(false);
            this.grpboxSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtboxSearchName;
        private System.Windows.Forms.GroupBox grpboxResults;
        private System.Windows.Forms.ListBox lboxResults;
        private System.Windows.Forms.GroupBox grpboxChapters;
        private System.Windows.Forms.ListView lviewChapters;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderTime;
        private System.Windows.Forms.GroupBox grpboxSearch;
        private System.Windows.Forms.Button btnUse;
    }
}