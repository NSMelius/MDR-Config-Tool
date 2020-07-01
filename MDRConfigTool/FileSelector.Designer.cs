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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.pnlFileSelect.SuspendLayout();
            this.pnlDataDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDisplay)).BeginInit();
            this.pnlSolutionsettings.SuspendLayout();
            this.gbAmsSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbFilePath
            // 
            this.tbFilePath.Location = new System.Drawing.Point(88, 105);
            this.tbFilePath.Name = "tbFilePath";
            this.tbFilePath.Size = new System.Drawing.Size(387, 20);
            this.tbFilePath.TabIndex = 0;
            this.tbFilePath.Text = "C:\\";
            // 
            // btnFileBrowse
            // 
            this.btnFileBrowse.Location = new System.Drawing.Point(400, 131);
            this.btnFileBrowse.Name = "btnFileBrowse";
            this.btnFileBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnFileBrowse.TabIndex = 1;
            this.btnFileBrowse.Text = "Browse...";
            this.btnFileBrowse.UseVisualStyleBackColor = true;
            this.btnFileBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnOPenFile
            // 
            this.btnOPenFile.Location = new System.Drawing.Point(524, 306);
            this.btnOPenFile.Name = "btnOPenFile";
            this.btnOPenFile.Size = new System.Drawing.Size(75, 23);
            this.btnOPenFile.TabIndex = 2;
            this.btnOPenFile.Text = "Open";
            this.btnOPenFile.UseVisualStyleBackColor = true;
            this.btnOPenFile.Click += new System.EventHandler(this.btnOPenFile_Click);
            // 
            // pnlFileSelect
            // 
            this.pnlFileSelect.Controls.Add(this.textBox1);
            this.pnlFileSelect.Controls.Add(this.tbFilePath);
            this.pnlFileSelect.Controls.Add(this.btnOPenFile);
            this.pnlFileSelect.Controls.Add(this.btnFileBrowse);
            this.pnlFileSelect.Location = new System.Drawing.Point(12, 12);
            this.pnlFileSelect.Name = "pnlFileSelect";
            this.pnlFileSelect.Size = new System.Drawing.Size(603, 385);
            this.pnlFileSelect.TabIndex = 3;
            // 
            // pnlDataDisplay
            // 
            this.pnlDataDisplay.Controls.Add(this.btnTableOK);
            this.pnlDataDisplay.Controls.Add(this.dgvListDisplay);
            this.pnlDataDisplay.Location = new System.Drawing.Point(12, 12);
            this.pnlDataDisplay.Name = "pnlDataDisplay";
            this.pnlDataDisplay.Size = new System.Drawing.Size(603, 385);
            this.pnlDataDisplay.TabIndex = 4;
            this.pnlDataDisplay.Visible = false;
            // 
            // btnTableOK
            // 
            this.btnTableOK.Location = new System.Drawing.Point(524, 352);
            this.btnTableOK.Name = "btnTableOK";
            this.btnTableOK.Size = new System.Drawing.Size(75, 23);
            this.btnTableOK.TabIndex = 1;
            this.btnTableOK.Text = "OK";
            this.btnTableOK.UseVisualStyleBackColor = true;
            this.btnTableOK.Click += new System.EventHandler(this.btnTableOK_Click);
            // 
            // dgvListDisplay
            // 
            this.dgvListDisplay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListDisplay.Location = new System.Drawing.Point(0, 5);
            this.dgvListDisplay.Name = "dgvListDisplay";
            this.dgvListDisplay.ReadOnly = true;
            this.dgvListDisplay.Size = new System.Drawing.Size(600, 325);
            this.dgvListDisplay.TabIndex = 0;
            // 
            // pnlSolutionsettings
            // 
            this.pnlSolutionsettings.Controls.Add(this.btnOpenSolution);
            this.pnlSolutionsettings.Controls.Add(this.gbAmsSettings);
            this.pnlSolutionsettings.Location = new System.Drawing.Point(12, 11);
            this.pnlSolutionsettings.Name = "pnlSolutionsettings";
            this.pnlSolutionsettings.Size = new System.Drawing.Size(603, 386);
            this.pnlSolutionsettings.TabIndex = 5;
            // 
            // btnOpenSolution
            // 
            this.btnOpenSolution.Location = new System.Drawing.Point(489, 307);
            this.btnOpenSolution.Name = "btnOpenSolution";
            this.btnOpenSolution.Size = new System.Drawing.Size(75, 23);
            this.btnOpenSolution.TabIndex = 3;
            this.btnOpenSolution.Text = "Create";
            this.btnOpenSolution.UseVisualStyleBackColor = true;
            this.btnOpenSolution.Click += new System.EventHandler(this.btnOpenSolution_Click);
            // 
            // gbAmsSettings
            // 
            this.gbAmsSettings.Controls.Add(this.lbAmsNetId);
            this.gbAmsSettings.Controls.Add(this.tbAmsNetId);
            this.gbAmsSettings.Location = new System.Drawing.Point(3, 6);
            this.gbAmsSettings.Name = "gbAmsSettings";
            this.gbAmsSettings.Size = new System.Drawing.Size(232, 100);
            this.gbAmsSettings.TabIndex = 2;
            this.gbAmsSettings.TabStop = false;
            this.gbAmsSettings.Text = "Ams Settings";
            // 
            // lbAmsNetId
            // 
            this.lbAmsNetId.AutoSize = true;
            this.lbAmsNetId.Location = new System.Drawing.Point(-3, 41);
            this.lbAmsNetId.Name = "lbAmsNetId";
            this.lbAmsNetId.Size = new System.Drawing.Size(96, 13);
            this.lbAmsNetId.TabIndex = 1;
            this.lbAmsNetId.Text = "Target Ams Net Id:";
            // 
            // tbAmsNetId
            // 
            this.tbAmsNetId.Location = new System.Drawing.Point(99, 38);
            this.tbAmsNetId.Name = "tbAmsNetId";
            this.tbAmsNetId.Size = new System.Drawing.Size(100, 20);
            this.tbAmsNetId.TabIndex = 0;
            this.tbAmsNetId.Text = "127.0.0.1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(88, 206);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 3;
            // 
            // FileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 409);
            this.Controls.Add(this.pnlFileSelect);
            this.Controls.Add(this.pnlSolutionsettings);
            this.Controls.Add(this.pnlDataDisplay);
            this.Name = "FileSelector";
            this.Text = "MDR Drive Configuration Tool";
            this.pnlFileSelect.ResumeLayout(false);
            this.pnlFileSelect.PerformLayout();
            this.pnlDataDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDisplay)).EndInit();
            this.pnlSolutionsettings.ResumeLayout(false);
            this.gbAmsSettings.ResumeLayout(false);
            this.gbAmsSettings.PerformLayout();
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
        private System.Windows.Forms.TextBox textBox1;
    }
}

