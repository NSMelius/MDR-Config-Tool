using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MDRConfigTool
{
    public partial class FileSelector : Form
    {
        OpenFileDialog ofd;
        ExcelHandler fileReader;
        SolutionHandler solHandler;
        Timer timer = new Timer();
        private static int count = 0;
        private DataTable DT;
        public FileSelector()
        {
            InitializeComponent();

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ofd = new OpenFileDialog();
            this.ofd.Title = "Select I/O Spreadsheet File";
            this.ofd.InitialDirectory = @"C:\";
            this.ofd.Filter = "All Files(*.*)|*.*|Excel Spreadsheets (*.xls,*.xlsm, *.xlsx)|*.xls;*.xlsm;*.xlsx| Text File (*.txt, *.csv)|*.txt;*.csv";
            this.ofd.FilterIndex = 2;
            this.ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = ofd.FileName;
            }
        }

        private void btnOPenFile_Click(object sender, EventArgs e)
        {
            this.fileReader = new ExcelHandler(tbFilePath.Text);
            this.DT = fileReader.RetrieveData();
            this.pnlFileSelect.Visible = false;
            this.pnlDataDisplay.Visible = true;
            this.dgvListDisplay.DataSource = DT;


        }

        private void btnTableOK_Click(object sender, EventArgs e)
        {
            this.pnlDataDisplay.Visible = false;
            this.pnlSolutionsettings.Visible = true;
        }

        private void btnOpenSolution_Click(object sender, EventArgs e)
        {
            this.solHandler = new SolutionHandler();
            this.solHandler.SetNetId();
            this.solHandler.ScanDevicesAndBoxes(DT);
            this.solHandler.ActivateConfiguration();
        }

 

    }
}
