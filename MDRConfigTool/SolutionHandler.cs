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
        private  ITcSysManager11 sysMan;
        private  EnvDTE.DTE dte;
        private  EnvDTE.Project pro;
        private  EnvDTE.Solution sol;
        private  EnvDTE80.DTE2 errDte;
        private  TcAdsClient adsClient;
        private  string declarations;
        private string error;
        private bool s;
        private bool f;
        private int result;
        private  string BASEFOLDER;
        private  string TcTemplatePath = @"C:\TwinCAT\3.1\Components\Base\PrjTemplate\TwinCAT Project.tsproj";
        private string _solutionName;
        private string _tcProjectName;
        
        private  string ProgID = "TcXaeShell.DTE.15.0";

        public SolutionHandler(string filePath, string FileName)
        {
            this.BASEFOLDER = filePath;
            this._solutionName = _tcProjectName = FileName;

            MessageFilter.Register();

            /* -----------------------------------------------------------------
             * Creates new solution based off of TwinCAT's XAE and PLC templates.
             * -----------------------------------------------------------------*/ 
            if (dte == null) CreateNewProject();

            pro = sol.Projects.Item(1);
            sysMan = (ITcSysManager11)pro.Object;
           
            //System.Threading.Thread.Sleep(5000);
            ITcSmTreeItem plc = sysMan.LookupTreeItem("TIPC");
            plc.CreateChild("Untitled1", 0, "", "Standard PLC Template");
            //---Navigate to References node and cast to Library Manager interface
            ITcSmTreeItem references = sysMan.LookupTreeItem("TIPC^Untitled1^Untitled1 Project^References");
            ITcPlcLibraryManager libraryManager = (ITcPlcLibraryManager)this.RetrieveLibMan();

            /* ------------------------------------------------------
             * Check to see if library already exists, if it does it will delete before adding to avoid any exceptions.
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

            MessageFilter.Revoke();
        }//Constructor()
  

        public void CreateNewProject()
        {


            /* ------------------------------------------------------
             * Create Visual Studio instance and make VS window visible
             * ------------------------------------------------------ */
            Type t = System.Type.GetTypeFromProgID(ProgID);
            dte = (EnvDTE.DTE)System.Activator.CreateInstance(t);
            dte.SuppressUI = false;
            dte.UserControl = true; // true = leaves VS window open after code execution
            dte.MainWindow.Visible = true;

            /* ------------------------------------------------------
             * Create directories for new Visual Studio solution
             * ------------------------------------------------------ */
            //Helper.DeleteDirectory(BASEFOLDER);
            Directory.CreateDirectory(BASEFOLDER);
            Directory.CreateDirectory(BASEFOLDER);

            /* ------------------------------------------------------
             * Create and save new solution
             * ------------------------------------------------------ */
            sol = dte.Solution;
            sol.Create(BASEFOLDER, _solutionName + ".sln");
            sol.SaveAs(BASEFOLDER + "\\" + _solutionName);

            /* ------------------------------------------------------
             * Create new TwinCAT project, based on TwinCAT Project file (delivered with TwinCAT XAE)
             * ------------------------------------------------------ */
            pro = sol.AddFromTemplate(TcTemplatePath, BASEFOLDER + "\\" + _tcProjectName, _tcProjectName);
           

        }



        public void PLCdeclarations(DataTable dt)
        {

            string mainVar = @"VAR_GLOBAL" + Environment.NewLine;
            string endMain = Environment.NewLine + @"          
END_VAR";


            //Add function blocks to MAIN declaration
            ITcSmTreeItem main = sysMan.LookupTreeItem("TIPC^Untitled1^Untitled1 Project^GVLs");
            ITcSmTreeItem GVL = main.CreateChild("MDR_Control_FBs", 615, "", 0);
            //Cast to specific interface for declaration and implementation area
            ITcPlcDeclaration mainDecl = (ITcPlcDeclaration)GVL;


            //Get current declaration and implementation area content
            string strMainDecl = mainDecl.DeclarationText;
           //string strMainImpl = mainImpl.ImplementationText;

            string[] boxName = new string[dt.Rows.Count];
            int j = 0;
            int i = 0;


            for (i = 0; i < dt.Rows.Count - 1; i++)
            {
                boxName[i] = dt.Rows[j].ItemArray[0].ToString() + j.ToString();
                Console.WriteLine(boxName[i]);
                j++;
            }

            declarations = string.Join(": FB_MDR_Control;" + Environment.NewLine, boxName);
            mainDecl.DeclarationText = mainVar + declarations + endMain;

            System.Threading.Thread.Sleep(2000);
            var settings = dte.GetObject("TcAutomationSettings");
            settings.SilentMode = true;
            dte.Solution.SolutionBuild.Build(true); // compile the plc project
            settings.SilentMode = false;
        }

    public  void AddLibrary(ref ITcPlcLibraryManager libraryManager)
        {
            //---Create new repo path
            string newRepoPath = @"C:\TwinCAT\3.1\Components\Plc\Managed Libraries";
            string oldRepoPath = @"C:\TwinCAT\3.1\Components\Plc\Managed Libraries\BAUS";
            //---Create new repository and insert into repo path---
            //if (Directory.Exists(oldRepoPath))
           // {
           //     libraryManager.RemoveRepository("MDR_Repo");
           // }
           // Directory.CreateDirectory(newRepoPath);
            
          //  libraryManager.InsertRepository("BAUS", newRepoPath, 0);

            string libPath = buildPathString("MDR_Control.library");

            //---Install library into new repository
            libraryManager.InstallLibrary("System", libPath, true); //--library must exists in TwinCAT folder.
            //--- libpath needs to be added to basefolder in this install command (needs full path to folder + MDR_Control.library

            //---Add library from repository
            libraryManager.AddLibrary("MDR_Control", "1.0", "BAUS");
        }

        public void DeleteLibrary(ref ITcPlcLibraryManager libraryManager)
        {
            //---Create new repo path
            //string newRepoPath = @"C:\TwinCAT\3.1\Components\Plc\Managed Libraries\BAUS";

            //---Remove Library from references 
            libraryManager.RemoveReference("MDR_Control");

            //---Uninstall library from repository
            libraryManager.UninstallLibrary("System", "MDR_Control", "1.0", "BAUS");

            //---Remove repository from system    
          
        }

        public void linkVariables(DataTable dt)
        {
            string[] boxName = new string[dt.Rows.Count];
            int j = 0;
            int i = 0;
            string plcInputsPath = "TIPC^Untitled1^Untitled1 Instance^PlcTask Inputs";
            string plcOutputsPath = "TIPC^Untitled1^Untitled1 Instance^PlcTask Outputs";

            for (i = 0; i < dt.Rows.Count - 1; i++)
            {
                boxName[i] = dt.Rows[j].ItemArray[0].ToString() + j.ToString();
                if (i >= 1)
                {
                    sysMan.LinkVariables(plcInputsPath + "^MDR_Control_FBs." + boxName[i - 1] + ".stInput^SlugMode_From_Upstream_Zone", plcOutputsPath + "^MDR_Control_FBs." + boxName[i] + ".stOutput^SlugMode_To_Downstream_Zone");
                    sysMan.LinkVariables(plcInputsPath + "^MDR_Control_FBs." + boxName[i - 1] + ".stInput^Wakeup_From_Upstream_Zone", plcOutputsPath + "^MDR_Control_FBs." + boxName[i] + ".stOutput^Wakeup_To_Downstream_Zone");
                    sysMan.LinkVariables(plcInputsPath + "^MDR_Control_FBs." + boxName[i] + ".stInput^RTR_From_Downstream_Zone", plcOutputsPath + "^MDR_Control_FBs." + boxName[i - 1] + ".stOutput^RTR_To_Upstream_Zone");
                    sysMan.LinkVariables(plcInputsPath + "^MDR_Control_FBs." + boxName[i] + ".stInput^Occupied_From_Downstream_Zone", plcOutputsPath + "^MDR_Control_FBs." + boxName[i - 1] + ".stOutput^Occupied_To_Upstream_Zone");
                }


                Console.WriteLine(boxName[i]);
                j++;
            }

            



        }

        public void activateAndRunPLC()
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
        public  EnvDTE.DTE attachToExistingDte(string solutionPath, string progId)
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
        
            ITcSmTreeItem ioDevicesItem = sysMan.LookupTreeItem("TIID"); // Get The IO Devices Node

            // Scan Devices (Be sure that the target system is in Config mode!)
            string scannedXml = ioDevicesItem.ProduceXml(false); // Produce Xml implicitly starts the ScanDevices on this node.

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(scannedXml); // Loads the Xml data into an XML Document
            XmlNodeList xmlDeviceList = xmlDoc.SelectNodes("TreeItem/DeviceGrpDef/FoundDevices/Device");
            List<ITcSmTreeItem> devices = new List<ITcSmTreeItem>();
            
            //local variables for our spreadsheet writing statements
            int deviceCount = 0;
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
                int j = 0;
                foreach (ITcSmTreeItem Box in device)
                {
                    
                    //oSheet.Cells[1, 1] = Box.Name; //assigns EK1100 to first table cell
                    if (Box.ItemSubTypeName.Contains("EP7402-0057"))
                    {
                        bool match = false;
                        do
                        {
                            if (dt.Rows[j].ItemArray[1].ToString().Contains("EP7402"))
                            {
                                Box.Name = dt.Rows[j].ItemArray[0].ToString() + j.ToString();
                                match = true;
                                this.EditParams(Box, dt.Rows[j].ItemArray[2].ToString());
                                j++;
                            }
                            else { j++; };
                        } while (j < dt.Rows.Count || match);
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

        }//ScanDevicesandBoxes()

        public void SetNetId(string NetId)
        {
            //finding the open form so we can get the textbox value
            if (NetId.Equals("127.0.0.1.1.1"))
            {
                NetId = AmsNetId.Local.ToString();
            }
            //sets the target to the textbox value.
            sysMan.SetTargetNetId(NetId);
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
            string file = filename;
            string path = buildPathString(file);

            //open the Motor file as an xml document,read its tags with InitCmds
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(path);
            XmlNode xNode = xDoc.SelectSingleNode("/Mailbox/CoE/InitCmds");

            string driveParams = drive.ProduceXml();
            XmlNodeList xmlInitCmds = xNode.ChildNodes;
            foreach (XmlNode Cmd in xmlInitCmds)
            {
                string newStartListParam = "<" + Cmd.Name + ">";
                foreach (XmlNode node in Cmd)
                {
                    newStartListParam += "<" + node.Name + ">";
                    newStartListParam += node.InnerText;
                    newStartListParam += "</" + node.Name + ">";
                }
                newStartListParam += "</" + Cmd.Name+ ">";
                int idx = driveParams.IndexOf("</InitCmd></InitCmds><CanOpenType>55</CanOpenType></CoE></Mailbox>");
                idx = idx + 10;
                driveParams = driveParams.Insert(idx, newStartListParam);
            }
            drive.ConsumeXml(driveParams);

        }//EditParams

    public string buildPathString(string file)
        {
            
            string path = BASEFOLDER;
            path += "\\" + file;

            return path;
        }


    public ITcSmTreeItem RetrieveLibMan()
        {
            ITcSmTreeItem references = sysMan.LookupTreeItem("TIPC^Untitled1^Untitled1 Project^References");
            return references;
        }

        private bool isLinkingCompatible(ITcSmTreeItem drive)
        {
            /*--------------------------------
             * Check the that the passed drive has a hardware version newer than the beta version
             * make sure that we can read the revision version of the drive through its xml file
             * find the index of the xml node for the CoE parameter for Hardwareversion 16#1009
             * Check if the value of that node is of Hardware version 01(?) or higher.
             * THe last beta revision number is 1114169
             * -------------------------------*/

            ulong lastBeta = 1114169;
            string driveParams = drive.ProduceXml();
            int idxStart = driveParams.LastIndexOf("<RevisionNo>"), idxEnd = driveParams.IndexOf("</RevisionNo>");
            ulong revisionNo; ulong.TryParse(driveParams.Substring(idxStart, (idxEnd - idxStart)),out revisionNo);

            if(revisionNo > lastBeta)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }//isLinkingCompatible()

       /* public string[] getRoutes()
       // {
            

        }//getRoutes()*/

        //private void CreatePLCVariable()
       // {
            /*-------------------------------------------
             * This Method should be used to create thhesary PLC variable within the PLC project.
             * The process should simulate the act of opening the drive's node in TwinCAt, openning the PLC tab,
             * and then checking the create PLC DataTyp box.
             * For now, we should see if we can get the necessary xml nodes from the drive to use the create Datatype method
             * of the ITcTypeSystem interface.
             * ------------------------------------------*/
         //   ITcTypeSystem TS = sysMan.GetTypeSystem();
          //      TS.CreateType();
       // }//CreatePLCVariable()

        private void LinkIoToPlc()
        {
            /*------------------------------------------
             * This is a placehiolder method for the code needed to link our drives to the PLC.
             * When we say PLC we mean explicitly and exclusively the PLC, no NC task/configuration
             * can be used.
             * -----------------------------------------*/
        }//LinkIoToPlc()
    






    }//class

   

  

}//namespace
