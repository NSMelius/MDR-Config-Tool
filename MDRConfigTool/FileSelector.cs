/*-------------------------------------------------------------------------------------
 * This is the main class of the project once it is launched my program.cs 
 * This Form will walk the user through the process of creating a TwinCAt solution, scanning in their I/O,
 * ,creating Function block declarations for each MDR drive added to the project, 
 * and linking the function blocks and I/O Variables.
 * 
 * The flow should be: [Welcomes Screen] -> [Folder selection] -> Read Xcel Spreadsheet->
 * Save cells to Data table -> [Confirm Data is correct] -> Create Solution -> [Get Target NetId] -> 
 * Connect to Target -> Scan I/O -> Rename Drive Instances in I/O tree -> Edit Drive Startup List ->
 * Create PLC Declarations -> Link Functions Blocks togather -> Link I/O to structures -> DONE
 * 
--------------------------------------------------------------------------------------*/ 

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
        
        
        

        ExcelHandler fileReader;
        Timer timer = new Timer();
        SolutionHandler solHandler;
        private static int count = 0;
        private DataTable DT;

        public FileSelector()
        {
            InitializeComponent();
            pnlWelcome.BringToFront();
            

        }
        private void btnStart_Click(object sender, EventArgs e)
        {

            pnlWelcome.Visible = false;
            pnlFileSelect.Visible = true;

        }

        private void FolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if(fbd.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = fbd.SelectedPath;
            }
            /*
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select I/O Spreadsheet File";
            ofd.InitialDirectory = @"C:\";
            ofd.Filter = "All Files(*.*)|*.*|Excel Spreadsheets (*.xls,*.xlsm, *.xlsx)|*.xls;*.xlsm;*.xlsx| Text File (*.txt, *.csv)|*.txt;*.csv";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = ofd.FileName;
            }
            */

        }

        private void btnOPenFile_Click(object sender, EventArgs e)
        {
            string sProjectPath = tbFilePath.Text + @"\" + tbProjectName.Text;
            string sFilePath = tbFilePath.Text + @"\" + tbFileName.Text;
            if (!String.IsNullOrEmpty(tbProjectName.Text) && !String.IsNullOrEmpty(tbFileName.Text))
            {

                fileReader = new ExcelHandler(sFilePath);
                DT = fileReader.RetrieveData();

                if (!sProjectPath.Contains(".sln"))
                {
                    sProjectPath += ".sln";
                }
                solHandler = new SolutionHandler(sProjectPath);
                pnlFileSelect.Visible = false;
                pnlDataDisplay.Visible = true;
                dgvListDisplay.DataSource = DT;
            }
            else { MessageBox.Show("Please Enter a Project Name and File Name"); }
            

        }

        private void btnTableOK_Click(object sender, EventArgs e)
        {
           
            pnlDataDisplay.Visible = false;
            pnlSolutionsettings.Visible = true;
        }

        private void btnOpenSolution_Click(object sender, EventArgs e)
        {
            
            solHandler.SetNetId();
            solHandler.ScanDevicesAndBoxes(DT);
            solHandler.PLCdeclarations(DT);
            solHandler.linkVariables();
            solHandler.ActivateConfiguration();
            pnlSolutionsettings.Visible = false;
           

        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            string file;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select I/O Spreadsheet File";
            ofd.InitialDirectory = tbFilePath.Text;
            ofd.Filter = "All Files(*.*)|*.*|Excel Spreadsheets (*.xls,*.xlsm, *.xlsx)|*.xls;*.xlsm;*.xlsx| Text File (*.txt, *.csv)|*.txt;*.csv";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;

            if(ofd.ShowDialog() == DialogResult.OK)
            {
                file = ofd.FileName;
                int idx = file.LastIndexOf('\\');
                file = file.Substring(idx+1);
                tbFileName.Text = file;
            }

             
        }
    }
}
