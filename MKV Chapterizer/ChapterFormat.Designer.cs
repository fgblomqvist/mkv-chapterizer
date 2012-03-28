namespace MKV_Chapterizer
{
    partial class ChapterFormat
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
            this.lblInstructions = new System.Windows.Forms.Label();
            this.cboxPatterns = new System.Windows.Forms.ComboBox();
            this.lblPattern = new System.Windows.Forms.Label();
            this.txtExample = new System.Windows.Forms.TextBox();
            this.txtPattern = new System.Windows.Forms.TextBox();
            this.lblExample = new System.Windows.Forms.Label();
            this.scBoxes = new System.Windows.Forms.SplitContainer();
            this.txtSeparator = new System.Windows.Forms.TextBox();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblVariables = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scBoxes)).BeginInit();
            this.scBoxes.Panel1.SuspendLayout();
            this.scBoxes.Panel2.SuspendLayout();
            this.scBoxes.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblInstructions
            // 
            this.lblInstructions.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(113, 15);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(316, 13);
            this.lblInstructions.TabIndex = 0;
            this.lblInstructions.Text = "Please choose a format to use when creating raw chapter files:";
            // 
            // cboxPatterns
            // 
            this.cboxPatterns.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cboxPatterns.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxPatterns.FormattingEnabled = true;
            this.cboxPatterns.Location = new System.Drawing.Point(186, 41);
            this.cboxPatterns.Name = "cboxPatterns";
            this.cboxPatterns.Size = new System.Drawing.Size(165, 21);
            this.cboxPatterns.TabIndex = 1;
            this.cboxPatterns.SelectedIndexChanged += new System.EventHandler(this.cboxPatterns_SelectedIndexChanged);
            // 
            // lblPattern
            // 
            this.lblPattern.AutoSize = true;
            this.lblPattern.Location = new System.Drawing.Point(9, 77);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(45, 13);
            this.lblPattern.TabIndex = 2;
            this.lblPattern.Text = "Pattern:";
            // 
            // txtExample
            // 
            this.txtExample.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExample.Location = new System.Drawing.Point(0, 0);
            this.txtExample.Multiline = true;
            this.txtExample.Name = "txtExample";
            this.txtExample.ReadOnly = true;
            this.txtExample.Size = new System.Drawing.Size(255, 208);
            this.txtExample.TabIndex = 3;
            // 
            // txtPattern
            // 
            this.txtPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPattern.Location = new System.Drawing.Point(0, 0);
            this.txtPattern.Multiline = true;
            this.txtPattern.Name = "txtPattern";
            this.txtPattern.Size = new System.Drawing.Size(253, 140);
            this.txtPattern.TabIndex = 4;
            this.txtPattern.TextChanged += new System.EventHandler(this.txtPattern_TextChanged);
            // 
            // lblExample
            // 
            this.lblExample.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblExample.AutoSize = true;
            this.lblExample.Location = new System.Drawing.Point(273, 77);
            this.lblExample.Name = "lblExample";
            this.lblExample.Size = new System.Drawing.Size(53, 13);
            this.lblExample.TabIndex = 6;
            this.lblExample.Text = "Example:";
            // 
            // scBoxes
            // 
            this.scBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scBoxes.IsSplitterFixed = true;
            this.scBoxes.Location = new System.Drawing.Point(12, 93);
            this.scBoxes.Name = "scBoxes";
            // 
            // scBoxes.Panel1
            // 
            this.scBoxes.Panel1.Controls.Add(this.txtSeparator);
            this.scBoxes.Panel1.Controls.Add(this.lblSeparator);
            this.scBoxes.Panel1.Controls.Add(this.lblVariables);
            this.scBoxes.Panel1.Controls.Add(this.txtPattern);
            // 
            // scBoxes.Panel2
            // 
            this.scBoxes.Panel2.Controls.Add(this.txtExample);
            this.scBoxes.Size = new System.Drawing.Size(519, 208);
            this.scBoxes.SplitterDistance = 254;
            this.scBoxes.SplitterWidth = 10;
            this.scBoxes.TabIndex = 7;
            // 
            // txtSeparator
            // 
            this.txtSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSeparator.Location = new System.Drawing.Point(0, 172);
            this.txtSeparator.Name = "txtSeparator";
            this.txtSeparator.Size = new System.Drawing.Size(100, 20);
            this.txtSeparator.TabIndex = 8;
            this.txtSeparator.TextChanged += new System.EventHandler(this.txtSeparator_TextChanged);
            // 
            // lblSeparator
            // 
            this.lblSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSeparator.AutoSize = true;
            this.lblSeparator.Location = new System.Drawing.Point(-3, 156);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(58, 13);
            this.lblSeparator.TabIndex = 7;
            this.lblSeparator.Text = "Separator:";
            // 
            // lblVariables
            // 
            this.lblVariables.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblVariables.AutoSize = true;
            this.lblVariables.Location = new System.Drawing.Point(142, 143);
            this.lblVariables.Name = "lblVariables";
            this.lblVariables.Size = new System.Drawing.Size(109, 65);
            this.lblVariables.TabIndex = 6;
            this.lblVariables.Text = "Variables:\r\n%N - Chapter name\r\n%I - Chapter number\r\n%T - Chapter time\r\n%L - New l" +
    "ine";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(190, 321);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(276, 321);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ChapterFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 356);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.scBoxes);
            this.Controls.Add(this.lblExample);
            this.Controls.Add(this.lblPattern);
            this.Controls.Add(this.cboxPatterns);
            this.Controls.Add(this.lblInstructions);
            this.Name = "ChapterFormat";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chapter Format";
            this.Load += new System.EventHandler(this.ChapterFormat_Load);
            this.scBoxes.Panel1.ResumeLayout(false);
            this.scBoxes.Panel1.PerformLayout();
            this.scBoxes.Panel2.ResumeLayout(false);
            this.scBoxes.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scBoxes)).EndInit();
            this.scBoxes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.ComboBox cboxPatterns;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.TextBox txtExample;
        private System.Windows.Forms.TextBox txtPattern;
        private System.Windows.Forms.Label lblExample;
        private System.Windows.Forms.SplitContainer scBoxes;
        private System.Windows.Forms.Label lblVariables;
        private System.Windows.Forms.Label lblSeparator;
        private System.Windows.Forms.TextBox txtSeparator;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}