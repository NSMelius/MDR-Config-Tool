using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCatSysManagerLib;
using EnvDTE;
using EnvDTE80;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Linq;

using TwinCAT.Ads;
using System.Windows.Forms;
using System.Data;
using System.Text.RegularExpressions;

namespace MDRConfigTool
{
    public class SolutionHandler
    {
        public static ITcSysManager11 sysMan;
        public static EnvDTE.DTE dte;
        public static EnvDTE.Project pro;
        public static EnvDTE.Solution sol;
        public static EnvDTE80.DTE2 errDte;
        public static TcAdsClient adsClient;
        public static string declarations;
        public string error;
        public bool s;
        public bool f;
        public int result;






        public SolutionHandler()
        {

            // Application.Run(new Form1());

            MessageFilter.Register();

            dte = attachToExistingDte(@"C:\Users\CharlesZ\Desktop\CZ_Support\Support Resources\ADS\TwinCATProject\TwinCATProject.sln", "VisualStudio.DTE.12.0");
            dte.SuppressUI = false;
            dte.UserControl = true;
            dte.MainWindow.Visible = true;

            sol = dte.Solution;
            pro = sol.Projects.Item(1);
            sysMan = (ITcSysManager11)pro.Object;


            //---Navigate to References node and cast to Library Manager interface
            ITcSmTreeItem references = sysMan.LookupTreeItem("TIPC^Untitled1^Untitled1 Project^References");
            ITcPlcLibraryManager libraryManager = (ITcPlcLibraryManager)references;

            /* ------------------------------------------------------
             * Checks to make sure library doesn't already exist, otherwise the insert and repository creation will fail.
             * This is the reason for removing and deleting first. If the library doesn't exist already, the code gets ignored.
             * Library must be saved to the C:\\TwinCAT folder!!!
             * ------------------------------------------------------ */

            foreach (ITcPlcLibRef libraryReference in libraryManager.References)
            {
                if (libraryReference is ITcPlcLibrary)
                {
                    ITcPlcLibrary library = (ITcPlcLibrary)libraryReference;
                    if (library.Name == "MDR_Control")
                    {
                        DeleteLibrary(ref libraryManager);
                    }
                }
            }
            /* ------------------------------------------------------
             * Add our MDR library to the project References.
             * ------------------------------------------------------ */
            AddLibrary(ref libraryManager);


            /* ------------------------------------------------------
             * Adding our PLC code
             * ------------------------------------------------------ */

            //PLCdeclarations();

            //  linkVariables();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FileSelector());

            MessageFilter.Revoke();
        }

        public void PLCdeclarations(DataTable dt)
        {

            string FB_MDR = "FB_MDR_Control;";
            int i = 1;
            string mainVar = @"PROGRAM MAIN
VAR";
            string endMain = @"          
END_VAR";
            ITcSmTreeItem io = sysMan.LookupTreeItem("TIID");
            //     io.ExportChild("Device 1 (EtherCAT)", @"C:\TwinCAT\Device 1 (EtherCAT).xti");
            XDocument doc = XDocument.Load(@"C:\TwinCAT\Device 1 (EtherCAT).xti");

            List<ITcSmTreeItem> devices = new List<ITcSmTreeItem>();
            foreach (ITcSmTreeItem Box in devices)
            {
                Console.WriteLine(Box.Name);
                if (Box.ItemSubTypeName.Contains("EP7402-0057"))
                {

                    //Add function blocks to MAIN declaration
                    ITcSmTreeItem main = sysMan.LookupTreeItem("TIPC^Untitled1^Untitled1 Project^POUs^MAIN");

                    //Cast to specific interface for declaration and implementation area
                    ITcPlcDeclaration mainDecl = (ITcPlcDeclaration)main;
                    ITcPlcImplementation mainImpl = (ITcPlcImplementation)main;


                    //Get current declaration and implementation area content
                    string strMainDecl = mainDecl.DeclarationText;
                    string strMainImpl = mainImpl.ImplementationText;

                    string[] boxName = new string[Box.ChildCount];
                    //    for (int i = 0; i < Box.ChildCount; i++)
                    //  {

                    boxName[i] = Box.Name;
                    i++;
                    declarations = string.Format("{0}", boxName[i] + ":" + FB_MDR + Environment.NewLine);
                    System.IO.File.WriteAllText(@"C:\TwinCAT\record.txt", declarations + Environment.NewLine);
                    // mainDecl.DeclarationText = mainVar + declarations + endMain;
                    //  }


                }
                /*
                foreach (ITcSmTreeItem EtherCAT in Box)
                {

                }
                 */

            }//end of foreach loops
            var settings = dte.GetObject("TcAutomationSettings");
            settings.SilentMode = true;
            dte.Solution.SolutionBuild.Build(true); // compile the plc project
            settings.SilentMode = false;

        }

        public static void AddLibrary(ref ITcPlcLibraryManager libraryManager)
        {
            //---Create new repo path
            string newRepoPath = @"C:\MDR_Library_Repo";

            //---Create new repository and insert into repo path---
            Directory.CreateDirectory(newRepoPath);
            libraryManager.InsertRepository("MDR_Repo", newRepoPath, 0);

            //---Install library into new repository
            libraryManager.InstallLibrary("MDR_Repo", @"C:\TwinCAT\MDR_Control.library", true); //--library must exists in TwinCAT folder.

            //---Add library from repository
            libraryManager.AddLibrary("MDR_Control", "0.4", "BAUS");
        }

        public static void DeleteLibrary(ref ITcPlcLibraryManager libraryManager)
        {
            //---Create new repo path
            string newRepoPath = @"C:\MDR_Library_Repo";

            //---Remove Library from references 
            libraryManager.RemoveReference("MDR_Control");

            //---Uninstall library from repository
            libraryManager.UninstallLibrary("MDR_Repo", "MDR_Control", "0.4", "BAUS");

            //---Remove repository from system    
            libraryManager.RemoveRepository("MDR_Repo");
            Directory.Delete(newRepoPath, true);
        }

        public static void linkVariables()
        {
            /* ------------------------------------------------------
            * Create variable mapping between I/Os and PLC inputs/outputs
             * 
             * MAKE a template of the inputs and ouputs for one given device.
             * Then you need to reuse that template string path whenever a new EP7402 is found.
             * it will be one of your objects basically that you insert into the string + obj.
             * 
             * Then you somehow need to increment based off the ethercat address of name box.
            * ------------------------------------------------------ */
            string plcInputsPath = "TIPC^Untitled1^Untitled1 Instance^PlcTask Inputs";
            string plcOutputsPath = "TIPC^Untitled1^Untitled1 Instance^PlcTask Outputs";

            sysMan.LinkVariables(plcInputsPath + "^MAIN.fbMDR.stInput^Control_3", plcOutputsPath + "^MAIN.fbMDR_2.stOutput^Control_3");
            sysMan.LinkVariables(plcInputsPath + "^MAIN.fbMDR.stInput^Control_4", plcOutputsPath + "^MAIN.fbMDR_2.stOutput^Control_4");
            sysMan.LinkVariables(plcInputsPath + "^MAIN.fbMDR_2.stInput^Control_1", plcOutputsPath + "^MAIN.fbMDR.stOutput^Control_1");
            sysMan.LinkVariables(plcInputsPath + "^MAIN.fbMDR_2.stInput^Control_2", plcOutputsPath + "^MAIN.fbMDR.stOutput^Control_2");
            //Control_3 . MAIN.fbMDR_2.stOutput . PlcTask Outputs . Untitled1 Instance . Untitled1
        }

        public static void activateAndRunPLC()
        {
            adsClient = new TcAdsClient();
            adsClient.Connect("192.168.56.1.1.1", 10000); //AMS net id of target system. TwinCAT system service port = 10000
            System.Threading.Thread.Sleep(10000); //waiting for TwinCAT to go into Run mode.
            sysMan.ActivateConfiguration();
            sysMan.StartRestartTwinCAT();
            try
            {
                bool rundo = true;
                do
                {
                    if (adsClient.ReadState().AdsState == AdsState.Run) //reads TwinCAT's system state, if it is in run mode then log into plc.
                    {
                        string xml = "<TreeItem><IECProjectDef><OnlineSettings><Commands><LoginCmd>true</LoginCmd><StartCmd>true</StartCmd></Commands></OnlineSettings></IECProjectDef></TreeItem>";
                        ITcSmTreeItem plcProject = sysMan.LookupTreeItem("TIPC^Untitled1^Untitled1 Project");
                        plcProject.ConsumeXml(xml);
                        rundo = false;
                    }
                } while (rundo);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        public static EnvDTE.DTE attachToExistingDte(string solutionPath, string progId)
        {
            EnvDTE.DTE dte = null;
            try
            {

                Hashtable dteInstances = Helper.GetIDEInstances(false, progId);
                IDictionaryEnumerator hashtableEnumerator = dteInstances.GetEnumerator();

                while (hashtableEnumerator.MoveNext())
                {
                    EnvDTE.DTE dteTemp = (EnvDTE.DTE)hashtableEnumerator.Value;
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


        public void ScanDevicesAndBoxes(DataTable dt)
        {
            //Start Excel and get Application object.
            /*oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = true;

            //Get a new workbook.
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add());
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;
            */


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
                    int j = 0;
                    //oSheet.Cells[1, 1] = Box.Name; //assigns EK1100 to first table cell
                    if (Box.ItemSubTypeName.Contains("EP7402-0057"))
                    {
                        Box.Name = dt.Rows[j].ItemArray[0].ToString()+j.ToString();
                        j++;
                        this.EditParams(Box, dt.Rows[j].ItemArray[2].ToString());
                    }
                    foreach (ITcSmTreeItem box in Box)
                    {
                        bTerm = box.ItemType == 6;
                        //checking to see if the box name contains the string Term, so we don't write extra EtherCAT data and only write the terminals
                        if (bTerm)
                        {
                            //oSheet.Cells[i, 1] = box.Name; //writes each sub item into spreadsheet column rows
                            i++;
                        }

                    }
                }
            } //end of foreach loops

            /*oXL.Visible = false;
            oXL.UserControl = false;
            oWB.SaveAs(@"C:\Users\CharlesZ\Desktop\CZ_Support\Support Resources\ADS\ErrorOutput\IO_List.xlsx", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            oWB.Close();
            oXL.Quit();*/
        }//ScanDevicesandBoxes()

        public void SetNetId()
        {
            //finding the open form so we can get the textbox value
            TextBox t = Application.OpenForms["FileSelector"].Controls["tbAmsNetId"] as TextBox;
            //sets the target to the textbox value.
            sysMan.SetTargetNetId(t.Text);
        }//SetNetId()

        public void ActivateConfiguration()
        {
            //    adsClient = new TcAdsClient();
            //    adsClient.Connect(10000);
            //   adsClient.WriteControl(new StateInfo(AdsState.Start, adsClient.ReadState().DeviceState));
            sysMan.ActivateConfiguration();
            sysMan.StartRestartTwinCAT();
        }   //ActivateConfiguration()

        public void EditParams(ITcSmTreeItem drive, string filename)
        {
            //Create the filepath to the motor file using the path provided to load the excel spreadsheet.
            string file = filename, sFileRegex = @"(^[\w,\s].{1}[A-za-z]{3}";
            string path = Application.OpenForms["FileSelector"].Controls["tvAmsNetId"].Text;
            Regex.Replace(path, sFileRegex, file);

            //open the Motor file as an xml document,read its tags with InitCmds
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(path);
            XmlNode xNode = xDoc.SelectSingleNode("InitCmds");

            string driveParams = drive.ProduceXml();
            string newStartListParam = "<InitCmd><Transition>PS</Transition><Comment><![CDATA[Motor temperature sensor type]]></Comment><Timeout>0</Timeout><OpCode>3</OpCode><DriveNo>0</DriveNo><IDN>32829</IDN><Elements>64</Elements><Attribute>0</Attribute><Data>0400</Data></InitCmd>";
            int idx = driveParams.IndexOf("</InitCmd></InitCmds></SoE></Mailbox>");
            idx = idx + 10;
            driveParams = driveParams.Insert(idx, newStartListParam);
            drive.ConsumeXml(driveParams);

        }//EditParams
    }//class

  

}//namespace
