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
        SolutionHandler solHandler;
        private  int count = 0;
        private DataTable DT;
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
            DT = fileReader.RetrieveData();
            pnlFileSelect.Visible = false;
            pnlDataDisplay.Visible = true;
            dgvListDisplay.DataSource = DT;


        }

        private void btnTableOK_Click(object sender, EventArgs e)
        {
            solHandler.ScanDevicesAndBoxes(DT);
            pnlDataDisplay.Visible = false;
            pnlSolutionsettings.Visible = true;
        }

        private void btnOpenSolution_Click(object sender, EventArgs e)
        {
            
            solHandler.SetNetId();
            
            solHandler.ActivateConfiguration();
            pnlSolutionsettings.Visible = false;
            pnlFileSelect.Visible = true;

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            solHandler = new SolutionHandler();
            pnlWelcome.Visible = false;
            pnlSolutionsettings.Visible = true;
        }

        private void btnLibBrowse_Click(object sender, EventArgs e)
        {
            ofd = new OpenFileDialog();
            ofd.Title = "Select MDR control library ";
            ofd.Filter = "All Files (*.*)|*.*| Library file(*.library, *.compiled-library)|*.library;*.compiled-library";
            ofd.FilterIndex = 1;
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                tbLibFile.Text = ofd.FileName;
            }
        }

        private void btnLibNext_Click(object sender, EventArgs e)
        {
            solHandler.AddLibrary()
        }
    }
}
