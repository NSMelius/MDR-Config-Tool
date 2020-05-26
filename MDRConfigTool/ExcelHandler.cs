using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;

namespace MDRConfigTool
{
    class ExcelHandler
    {
        private Excel.Application _app;
        private Excel.Workbooks _workbooks;
        private Excel.Workbook _workbook;
        public Excel.Worksheet _worksheet { get; }

        //base contructor for the Excel file handler, in this scenario no file was selected, so a new worksheet is opened
        //As of MAy 25th, this contructor should never be called. This is here in case we want to give the option to a customer to fill out the worksheet
        //after opening the Config tool
        public ExcelHandler()
        {
            this._app = new Excel.Application();
            this._workbooks = this._app.Workbooks;
            this._workbook = this._workbooks.Add();
            this._app.Visible = true;
        }//Constructor()

        //This is the constructor that our form calls after the user selects a file.
        //It simply calls this instances version of the OpenFile method, passing the String agrument along.
        public ExcelHandler(string File)
        {
            this.Openfile(File);

        }//Constructor(string)

        public DataTable RetrieveData() {

            
            //get the current worksheet from the workbook, and get its active range.
            //The template provided by Garth Gaddy only has 3 columns, but we will assume the customer does not folow the
            //Template exactly and get the number of columns
            worksheet = _workbook.ActiveSheet;
            int nColumns = worksheet.UsedRange.Columns.Count;
            int nRows = worksheet.UsedRange.Rows.Count;

            //create a new DataTable object. The DT holds data in a similar map as the Spreadsheet, and is easy to translate for the user.
            DataTable DT = new DataTable();
            Excel.Range headerRange = worksheet.get_Range((Excel.Range)worksheet.Cells[1, 1], (Excel.Range)worksheet.Cells[1, nColumns]);
            Excel.Range dataRange = worksheet.get_Range((Excel.Range)worksheet.Cells[2,1], (Excel.Range)worksheet.Cells[nRows,nColumns]);
            
            for (int i = 1; i <= nColumns; i++)
            {
                DT.Columns.Add(headerRange.Columns[i].Value);

            }
            AddData(nColumns, nRows, dataRange, DT);



            return DT;
           
        }//RetriveData();

        public void Openfile(string file)
        {
            this._app = new Excel.Application();
            this._workbooks = this._app.Workbooks;
            if (!string.IsNullOrEmpty(file))
            {
                if (file.Contains(".csv") || file.Contains(".txt"))
                { this._workbooks.OpenText(file, 0, 1, 1, Excel.XlTextQualifier.xlTextQualifierNone, false, false, false, true); }
                else if (file.Contains(".xls") || file.Contains(".xlms") || file.Contains(".xlsx"))
                {
                    this._workbook = this._workbooks.Open(file);
                }
            }
        }

        private void AddData(int cCount, int rCount, Excel.Range range, DataTable DT)
        {
            DataRow row;
            object[,] data = range.Value2;
            for(int j = 1; j <= cCount;j++)
            for (int i = 1; i <= rCount; i++)
            {
                string CellVal = String.Empty;
                try
                {
                    CellVal = (string)data[i, j];
                }catch(Exception ex)
                {
                   
                }
                    if (j == 1) {
                        row = DT.NewRow();
                        row[j-1] = CellVal;
                        DT.Rows.Add(row);
                    }
                    else
                    {
                        row = DT.Rows[i-1];
                        row[j-1] = CellVal;
                    }
            }
        }

        public void WritetoExcel(DataTable DT)
        {

        }
        
    }//Class
}//namespace
