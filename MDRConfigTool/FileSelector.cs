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
        Timer timer = new Timer();
        private static int count = 0;
        public FileSelector()
        {
            InitializeComponent();

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Title = "Select I/O Spreadsheet File";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "All Files(*.*)|*.*|Excel Spreadsheets (*.xls,*.xlsm, *.xlsx)|*.xls;*.xlsm;*.xlsx| Text File (*.txt, *.csv)|*.txt;*.csv";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = ofd.FileName;
            }
        }

        private void btnOPenFile_Click(object sender, EventArgs e)
        {
            fileReader = new ExcelHandler(tbFilePath.Text);
            DataTable DT = fileReader.RetrieveData();
            pnlFileSelect.Visible = false;
            pnlDataDisplay.Visible = true;
            dgvListDisplay.DataSource = DT;


        }

        private void btnTableOK_Click(object sender, EventArgs e)
        {
            pnlDataDisplay.Visible = false;
            pnlSolutionsettings.Visible = true;
        }

        private void btnOpenSolution_Click(object sender, EventArgs e)
        {
            SolutionHandler solHandler = new SolutionHandler();
            solHandler.SetNetId();
            solHandler.ScanDevicesAndBoxes();
            solHandler.ActivateConfiguration();
        }

 

    }
}
