namespace MDRConfigTool
{
    partial class FileSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileSelector));
            this.tbFilePath = new System.Windows.Forms.TextBox();
            this.btnFileBrowse = new System.Windows.Forms.Button();
            this.btnOPenFile = new System.Windows.Forms.Button();
            this.pnlFileSelect = new System.Windows.Forms.Panel();
            this.pnlDataDisplay = new System.Windows.Forms.Panel();
            this.btnTableOK = new System.Windows.Forms.Button();
            this.dgvListDisplay = new System.Windows.Forms.DataGridView();
            this.pnlSolutionsettings = new System.Windows.Forms.Panel();
            this.btnOpenSolution = new System.Windows.Forms.Button();
            this.gbAmsSettings = new System.Windows.Forms.GroupBox();
            this.lbAmsNetId = new System.Windows.Forms.Label();
            this.tbAmsNetId = new System.Windows.Forms.TextBox();
            this.pnlWelcome = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.pnlLibSelect = new System.Windows.Forms.Panel();
            this.btnLibNext = new System.Windows.Forms.Button();
            this.btnLibBrowse = new System.Windows.Forms.Button();
            this.tbLibFile = new System.Windows.Forms.TextBox();
            this.pnlFileSelect.SuspendLayout();
            this.pnlDataDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDisplay)).BeginInit();
            this.pnlSolutionsettings.SuspendLayout();
            this.gbAmsSettings.SuspendLayout();
            this.pnlWelcome.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.pnlLibSelect.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(117, 129);
            this.tbFilePath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(515, 22);
            this.tbFilePath.TabIndex = 0;
            this.tbFilePath.Text = "C:\\";
            // 
            // btnFileBrowse
            // 
            this.btnFileBrowse.Location = new System.Drawing.Point(533, 161);
            this.btnFileBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFileBrowse.Name = "btnFileBrowse";
            this.btnFileBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnFileBrowse.TabIndex = 1;
            this.btnFileBrowse.Text = "Browse...";
            this.btnFileBrowse.UseVisualStyleBackColor = true;
            this.btnFileBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOPenFile
            // 
            this.btnOPenFile.Location = new System.Drawing.Point(699, 377);
            this.btnOPenFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOPenFile.Name = "btnOPenFile";
            this.btnOPenFile.Size = new System.Drawing.Size(100, 28);
            this.btnOPenFile.TabIndex = 2;
            this.btnOPenFile.Text = "Open";
            this.btnOPenFile.UseVisualStyleBackColor = true;
            this.btnOPenFile.Click += new System.EventHandler(this.btnOPenFile_Click);
            // 
            // pnlFileSelect
            // 
            this.pnlFileSelect.Controls.Add(this.tbFilePath);
            this.pnlFileSelect.Controls.Add(this.btnOPenFile);
            this.pnlFileSelect.Controls.Add(this.btnFileBrowse);
            this.pnlFileSelect.Location = new System.Drawing.Point(355, 210);
            this.pnlFileSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlFileSelect.Name = "pnlFileSelect";
            this.pnlFileSelect.Size = new System.Drawing.Size(465, 278);
            this.pnlFileSelect.TabIndex = 3;
            // 
            // pnlDataDisplay
            // 
            this.pnlDataDisplay.Controls.Add(this.btnTableOK);
            this.pnlDataDisplay.Controls.Add(this.dgvListDisplay);
            this.pnlDataDisplay.Location = new System.Drawing.Point(149, 121);
            this.pnlDataDisplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlDataDisplay.Name = "pnlDataDisplay";
            this.pnlDataDisplay.Size = new System.Drawing.Size(671, 368);
            this.pnlDataDisplay.TabIndex = 4;
            this.pnlDataDisplay.Visible = false;
            // 
            // btnTableOK
            // 
            this.btnTableOK.Location = new System.Drawing.Point(699, 433);
            this.btnTableOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTableOK.Name = "btnTableOK";
            this.btnTableOK.Size = new System.Drawing.Size(100, 28);
            this.btnTableOK.TabIndex = 1;
            this.btnTableOK.Text = "OK";
            this.btnTableOK.UseVisualStyleBackColor = true;
            this.btnTableOK.Click += new System.EventHandler(this.btnTableOK_Click);
            // 
            // dgvListDisplay
            // 
            this.dgvListDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListDisplay.Location = new System.Drawing.Point(3, 0);
            this.dgvListDisplay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvListDisplay.Name = "dgvListDisplay";
            this.dgvListDisplay.ReadOnly = true;
            this.dgvListDisplay.Size = new System.Drawing.Size(796, 402);
            this.dgvListDisplay.TabIndex = 0;
            // 
            // pnlSolutionsettings
            // 
            this.pnlSolutionsettings.Controls.Add(this.btnOpenSolution);
            this.pnlSolutionsettings.Controls.Add(this.gbAmsSettings);
            this.pnlSolutionsettings.Location = new System.Drawing.Point(96, 89);
            this.pnlSolutionsettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlSolutionsettings.Name = "pnlSolutionsettings";
            this.pnlSolutionsettings.Size = new System.Drawing.Size(724, 400);
            this.pnlSolutionsettings.TabIndex = 5;
            // 
            // btnOpenSolution
            // 
            this.btnOpenSolution.Location = new System.Drawing.Point(667, 434);
            this.btnOpenSolution.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenSolution.Name = "btnOpenSolution";
            this.btnOpenSolution.Size = new System.Drawing.Size(132, 28);
            this.btnOpenSolution.TabIndex = 3;
            this.btnOpenSolution.Text = "Create Solution";
            this.btnOpenSolution.UseVisualStyleBackColor = true;
            this.btnOpenSolution.Click += new System.EventHandler(this.btnOpenSolution_Click);
            // 
            // gbAmsSettings
            // 
            this.gbAmsSettings.Controls.Add(this.lbAmsNetId);
            this.gbAmsSettings.Controls.Add(this.tbAmsNetId);
            this.gbAmsSettings.Location = new System.Drawing.Point(216, 130);
            this.gbAmsSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbAmsSettings.Name = "gbAmsSettings";
            this.gbAmsSettings.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbAmsSettings.Size = new System.Drawing.Size(309, 123);
            this.gbAmsSettings.TabIndex = 2;
            this.gbAmsSettings.TabStop = false;
            this.gbAmsSettings.Text = "Ams Settings";
            // 
            // lbAmsNetId
            // 
            this.lbAmsNetId.AutoSize = true;
            this.lbAmsNetId.Location = new System.Drawing.Point(-4, 50);
            this.lbAmsNetId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAmsNetId.Name = "lbAmsNetId";
            this.lbAmsNetId.Size = new System.Drawing.Size(126, 17);
            this.lbAmsNetId.TabIndex = 1;
            this.lbAmsNetId.Text = "Target Ams Net Id:";
            // 
            // tbAmsNetId
            // 
            this.tbAmsNetId.Location = new System.Drawing.Point(132, 47);
            this.tbAmsNetId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbAmsNetId.Name = "tbAmsNetId";
            this.tbAmsNetId.Size = new System.Drawing.Size(132, 22);
            this.tbAmsNetId.TabIndex = 0;
            this.tbAmsNetId.Text = "127.0.0.1";
            // 
            // pnlWelcome
            // 
            this.pnlWelcome.Controls.Add(this.pictureBox1);
            this.pnlWelcome.Controls.Add(this.btnStart);
            this.pnlWelcome.Location = new System.Drawing.Point(120, 106);
            this.pnlWelcome.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlWelcome.Name = "pnlWelcome";
            this.pnlWelcome.Size = new System.Drawing.Size(700, 383);
            this.pnlWelcome.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(88, 4);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(527, 398);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(623, 405);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 28);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Next";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // pnlLibSelect
            // 
            this.pnlLibSelect.Controls.Add(this.btnLibNext);
            this.pnlLibSelect.Controls.Add(this.btnLibBrowse);
            this.pnlLibSelect.Controls.Add(this.tbLibFile);
            this.pnlLibSelect.Location = new System.Drawing.Point(17, 16);
            this.pnlLibSelect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pnlLibSelect.Name = "pnlLibSelect";
            this.pnlLibSelect.Size = new System.Drawing.Size(803, 473);
            this.pnlLibSelect.TabIndex = 7;
            // 
            // btnLibNext
            // 
            this.btnLibNext.Location = new System.Drawing.Point(699, 441);
            this.btnLibNext.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLibNext.Name = "btnLibNext";
            this.btnLibNext.Size = new System.Drawing.Size(100, 28);
            this.btnLibNext.TabIndex = 2;
            this.btnLibNext.Text = "Next";
            this.btnLibNext.UseVisualStyleBackColor = true;
            this.btnLibNext.Click += new System.EventHandler(this.btnLibNext_Click);
            // 
            // btnLibBrowse
            // 
            this.btnLibBrowse.Location = new System.Drawing.Point(497, 230);
            this.btnLibBrowse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnLibBrowse.Name = "btnLibBrowse";
            this.btnLibBrowse.Size = new System.Drawing.Size(100, 28);
            this.btnLibBrowse.TabIndex = 1;
            this.btnLibBrowse.Text = "Browse...";
            this.btnLibBrowse.UseVisualStyleBackColor = true;
            this.btnLibBrowse.Click += new System.EventHandler(this.btnLibBrowse_Click);
            // 
            // tbLibFile
            // 
            this.tbLibFile.Location = new System.Drawing.Point(191, 194);
            this.tbLibFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tbLibFile.Name = "tbLibFile";
            this.tbLibFile.Size = new System.Drawing.Size(405, 22);
            this.tbLibFile.TabIndex = 0;
            this.tbLibFile.Text = "C:\\";
            // 
            // FileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 503);
            this.Controls.Add(this.pnlLibSelect);
            this.Controls.Add(this.pnlFileSelect);
            this.Controls.Add(this.pnlDataDisplay);
            this.Controls.Add(this.pnlWelcome);
            this.Controls.Add(this.pnlSolutionsettings);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FileSelector";
            this.Text = "MDR Drive Configuration Tool";
            this.pnlFileSelect.ResumeLayout(false);
            this.pnlFileSelect.PerformLayout();
            this.pnlDataDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDisplay)).EndInit();
            this.pnlSolutionsettings.ResumeLayout(false);
            this.gbAmsSettings.ResumeLayout(false);
            this.gbAmsSettings.PerformLayout();
            this.pnlWelcome.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.pnlLibSelect.ResumeLayout(false);
            this.pnlLibSelect.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Button btnFileBrowse;
        private System.Windows.Forms.Button btnOPenFile;
        private System.Windows.Forms.Panel pnlFileSelect;
        private System.Windows.Forms.Panel pnlDataDisplay;
        private System.Windows.Forms.DataGridView dgvListDisplay;
        private System.Windows.Forms.Panel pnlSolutionsettings;
        private System.Windows.Forms.TextBox tbAmsNetId;
        private System.Windows.Forms.Button btnTableOK;
        private System.Windows.Forms.Button btnOpenSolution;
        private System.Windows.Forms.GroupBox gbAmsSettings;
        private System.Windows.Forms.Label lbAmsNetId;
        private System.Windows.Forms.Panel pnlWelcome;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnlLibSelect;
        private System.Windows.Forms.Button btnLibNext;
        private System.Windows.Forms.Button btnLibBrowse;
        private System.Windows.Forms.TextBox tbLibFile;
    }
}

