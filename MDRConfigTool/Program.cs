using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using TCatSysManagerLib;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using TwinCAT.Ads;

namespace MDRConfigTool
{
    public class Program
    {
        public static TcAdsClient adsClient;
        public static ITcSysManager11 sysMan;
        public static EnvDTE.Project pro;
        public static EnvDTE.Solution sol;
        public static EnvDTE80.DTE2 dte;
        public static EnvDTE80.ErrorItems errors;
        public static EnvDTE80.ErrorItem item;
        public static EnvDTE80.ErrorList el;
        public static EnvDTE80.ToolWindows tw;
        public static string error;
        public static bool s;
        public static bool f;
        public static int result;
        public static Excel.Application oXL;
        public static Excel._Workbook oWB;
        public static Excel._Worksheet oSheet;
        public static Excel.Range oRng;
        



        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Form1());

            MessageFilter.Register();

            EnvDTE80.DTE2 dte = attachToExistingDte(@"C:\Users\CharlesZ\Desktop\CZ_Support\AI Project\MDR-Config-Tool\MDRConfigTool\bin\Debug\TwinCATProject\TwinCATProject.sln", "VisualStudio.DTE.12.0");
            dte.SuppressUI = false;
            dte.UserControl = true;
            dte.MainWindow.Visible = true;

            sol = dte.Solution;
            pro = sol.Projects.Item(1);
            sysMan = (ITcSysManager11)pro.Object;

            ITcSmTreeItem someTask = sysMan.LookupTreeItem("TIRT^PlcTask");


            dte.Solution.SolutionBuild.Build(true);

            System.Threading.Thread.Sleep(5000);

            ErrorItems errors = dte.ToolWindows.ErrorList.ErrorItems;

            for (int i = 1; i <= dte.ToolWindows.ErrorList.ErrorItems.Count; i++)
            {
                ErrorItem ei = errors.Item(i);

                string desc = "N/A";
                if (ei.Description != null)
                    desc = ei.Description.ToString();

                error = string.Format("Error Description: {0}", desc + Environment.NewLine);

                Console.WriteLine(error);

            }
            /*
            Console.WriteLine(Environment.ExitCode);
            StreamWriter exitcode = System.IO.File.AppendText(@"C:\Users\CharlesZ\Desktop\CZ_Support\Support Resources\ADS\ErrorOutput\SetTargetExample\bin\Debug\record.txt");
            exitcode.WriteLine(Environment.ExitCode);

            System.IO.File.WriteAllText(@"C:\Users\CharlesZ\Desktop\CZ_Support\Support Resources\ADS\ErrorOutput\SetTargetExample\bin\Debug\record.txt", "Error List" + Environment.NewLine);

            MessageFilter.Revoke();
            System.Threading.Thread.Sleep(5000);
            */

            /*
            try
            {
                    dte.ActiveWindow.Close();
            }
            catch (InvalidOperationException ex)
            {
                dte.Quit();
            }        
             */
            TcAdsClient adsclient = new TcAdsClient();

            Application.Run(new FileSelector());
        }



        public static EnvDTE80.DTE2 attachToExistingDte(string solutionPath, string progId)
        {
            EnvDTE80.DTE2 dte = null;
            try
            {

                Hashtable dteInstances = Helper.GetIDEInstances(false, progId);
                IDictionaryEnumerator hashtableEnumerator = dteInstances.GetEnumerator();

                while (hashtableEnumerator.MoveNext())
                {
                    EnvDTE80.DTE2 dteTemp = (EnvDTE80.DTE2)hashtableEnumerator.Value;
                    if (dteTemp.Solution.FullName == solutionPath)
                    {
                        Console.WriteLine("Found solution in list of all open DTE objects. " + dteTemp.Name);
                        dte = dteTemp;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return dte;
        }

        public void ScanDevicesAndBoxes()
        {
            //Start Excel and get Application object.
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;

            //Get a new workbook.
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add());
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;



            // sysMan.SetTargetNetId("1.2.3.4.5.6"); // Setting of the target NetId.
            ITcSmTreeItem ioDevicesItem = sysMan.LookupTreeItem("TIID"); // Get The IO Devices Node

            // Scan Devices (Be sure that the target system is in Config mode!)
            string scannedXml = ioDevicesItem.ProduceXml(false); // Produce Xml implicitly starts the ScanDevices on this node.

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(scannedXml); // Loads the Xml data into an XML Document
            XmlNodeList xmlDeviceList = xmlDoc.SelectNodes("TreeItem/DeviceGrpDef/FoundDevices/Device");
            List<ITcSmTreeItem> devices = new List<ITcSmTreeItem>();

            //local variables for our spreadsheet writing statements
            int deviceCount = 0;
            string term = "Term";
            bool bTerm;
            int i = 2;

            // Add all found devices to configuration
            foreach (XmlNode node in xmlDeviceList)
            {
                // Add a selection or subrange of devices
                int itemSubType = int.Parse(node.SelectSingleNode("ItemSubType").InnerText);
                string typeName = node.SelectSingleNode("ItemSubTypeName").InnerText;
                XmlNode xmlAddress = node.SelectSingleNode("AddressInfo");

                ITcSmTreeItem device = ioDevicesItem.CreateChild(string.Format("Device_{0}", ++deviceCount), itemSubType, string.Empty, null);

                string xml = string.Format("<TreeItem><DeviceDef>{0}</DeviceDef></TreeItem>", xmlAddress.OuterXml);
                device.ConsumeXml(xml); // Consume Xml Parameters (here the Address of the Device)
                devices.Add(device);
            }


            // Scan all added devices for attached boxes
            foreach (ITcSmTreeItem device in devices)
            {
                string xml = "<TreeItem><DeviceDef><ScanBoxes>1</ScanBoxes></DeviceDef></TreeItem>"; // Using the "ScanBoxes XML-Method"
                try
                {
                    device.ConsumeXml(xml); // Consume starts the ScanBoxes and inserts every found box/terminal into the configuration
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Warning: {0}", ex.Message);
                }

                foreach (ITcSmTreeItem Box in device)
                {
                    oSheet.Cells[1, 1] = Box.Name; //assigns EK1100 to first table cell

                    foreach (ITcSmTreeItem box in Box)
                    {
                        bTerm = box.Name.Contains(term);
                        //checking to see if the box name contains the string Term, so we don't write extra EtherCAT data and only write the terminals
                        if (bTerm)
                        {
                            oSheet.Cells[i, 1] = box.Name; //writes each sub item into spreadsheet column rows
                            i++;
                        }
                    }
                }
            } //end of foreach loops

            oXL.Visible = false;
            oXL.UserControl = false;
            oWB.SaveAs(@"C:\Users\CharlesZ\Desktop\CZ_Support\Support Resources\ADS\ErrorOutput\IO_List.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            oWB.Close();
            oXL.Quit();
        }

        public void SetNetId()
        {
            //finding the open form so we can get the textbox value
            TextBox t = Application.OpenForms["FileSelector"].Controls["textBox1"] as TextBox;
            //sets the target to the textbox value.
            sysMan.SetTargetNetId(t.Text);
        }

        public void ActivateConfiguration()
        {
            //    adsClient = new TcAdsClient();
            //    adsClient.Connect(10000);
            //   adsClient.WriteControl(new StateInfo(AdsState.Start, adsClient.ReadState().DeviceState));
            sysMan.ActivateConfiguration();
            sysMan.StartRestartTwinCAT();
        }
    }
}
