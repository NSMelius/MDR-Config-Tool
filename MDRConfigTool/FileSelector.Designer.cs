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
            this.dgvListDisplay = new System.Windows.Forms.DataGridView();
            this.pnlFileSelect.SuspendLayout();
            this.pnlDataDisplay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDisplay)).BeginInit();
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
            this.pnlFileSelect.Controls.Add(this.tbFilePath);
            this.pnlFileSelect.Controls.Add(this.btnOPenFile);
            this.pnlFileSelect.Controls.Add(this.btnFileBrowse);
            this.pnlFileSelect.Location = new System.Drawing.Point(12, 12);
            this.pnlFileSelect.Name = "pnlFileSelect";
            this.pnlFileSelect.Size = new System.Drawing.Size(603, 346);
            this.pnlFileSelect.TabIndex = 3;
            // 
            // pnlDataDisplay
            // 
            this.pnlDataDisplay.Controls.Add(this.dgvListDisplay);
            this.pnlDataDisplay.Location = new System.Drawing.Point(12, 12);
            this.pnlDataDisplay.Name = "pnlDataDisplay";
            this.pnlDataDisplay.Size = new System.Drawing.Size(603, 334);
            this.pnlDataDisplay.TabIndex = 4;
            this.pnlDataDisplay.Visible = false;
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
            // FileSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 409);
            this.Controls.Add(this.pnlDataDisplay);
            this.Controls.Add(this.pnlFileSelect);
            this.Name = "FileSelector";
            this.Text = "MDR Drive Configuration Tool";
            this.pnlFileSelect.ResumeLayout(false);
            this.pnlFileSelect.PerformLayout();
            this.pnlDataDisplay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tbFilePath;
        private System.Windows.Forms.Button btnFileBrowse;
        private System.Windows.Forms.Button btnOPenFile;
        private System.Windows.Forms.Panel pnlFileSelect;
        private System.Windows.Forms.Panel pnlDataDisplay;
        private System.Windows.Forms.DataGridView dgvListDisplay;
    }
}

