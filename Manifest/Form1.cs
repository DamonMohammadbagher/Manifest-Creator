using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Management.Instrumentation;
using System.Management;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using MSXML2;
using System.Collections.Specialized;
using System.Net.NetworkInformation;
using System.Net;

namespace Manifest
{
    public partial class Form1 : Form
    {
        public string _result = "";
       
        public Form1()
        {
            InitializeComponent();
           
        }
       
  
        string[] mytags;
        bool ISstop = false;
        public void Prerequset()
        {
            try
            {
                if (treeView2.SelectedNode.Tag != null)
                {
                    try
                    {
                        mytags = treeView2.SelectedNode.Tag.ToString().Split(';');
                    }
                    catch (Exception err)
                    {

                        //
                    }
                }
                else
                {
                    
                    try
                    {
                        mytags[0] = "NoDATA;";
                        mytags[1] = "NoDATA;";
                        mytags[2] = "NoDATA;";
                        mytags[3] = "NoDATA;";
                        mytags[4] = "NoDATA;";
                        mytags[5] = "NoDATA;";
                        mytags[6] = "NoDATA;";
                        mytags[7] = "NoDATA;";
                    }
                    catch (Exception err)
                    {

                        //
                    }

                }
            }
            catch (Exception er)
            {
                
                
            }
            
        }
        ArrayList __MyXml_RawText = new ArrayList();
        PingReply reply;
        public void getdata(BackgroundWorker bgw, DoWorkEventArgs e ,string Listbox_Selection,string tag1)
        {
            try
            {
                if (!ISstop)
                {

                    Ping pingSender = new Ping();
                    IPAddress address = null;
                    if (IPAddress.TryParse(Listbox_Selection, out address))
                    {

                        for (int cu = 0; cu < 2; cu++)
                        {
                            reply = pingSender.Send(address);
                        }
                        if (reply.Status == IPStatus.Success)
                        {
                            string PCName_str = System.Net.Dns.GetHostByAddress(Listbox_Selection).HostName;

                            string s = "";
                            try
                            {
                                s = "\\\\" + Listbox_Selection + "\\root\\cimv2";

                            }
                            catch (Exception listerr)
                            {
                                //;
                            }

                            try
                            {
                                //DAMON :BAYAD DAR IN GHESMA NODE ROOT ADD SHAVAD BAR ROYEH METHOD GETDATE()
                                __MyXml_RawText.Add("<" + PCName_str + ">");
                                TreeNode Root_node = new TreeNode(Listbox_Selection + " " + PCName_str);

                                try
                                {
                                    ManagementObjectSearcher searcher_Computer = new ManagementObjectSearcher(s, "SELECT * FROM Win32_ComputerSystem");

                                    //                    ManagementObjectSearcher searcher_Computer = new ManagementObjectSearcher(s, "SELECT * FROM Win32_ComputerSystem");

                                    //                    __MyXml_RawText.Add("<" + PCName_str + ">");

                                    if (tag1 == null)
                                    {
                                        __MyXml_RawText.Add("<Special_Properties>");
                                        __MyXml_RawText.Add("<UID>" + "NoDATA;" + "</UID>");
                                        __MyXml_RawText.Add("<Owner>" + "NoDATA;" + "</Owner>");
                                        __MyXml_RawText.Add("<TypeUse>" + "NoDATA;" + "</TypeUse>");
                                        __MyXml_RawText.Add("<RealaseDate>" + "NoDATA;" + "</RealaseDate>");
                                        __MyXml_RawText.Add("<ExpireDate>" + "NoDATA;" + "</ExpireDate>");
                                        __MyXml_RawText.Add("<Organization>" + "NoDATA;" + "</Organization>");
                                        __MyXml_RawText.Add("<Description>" + "NoDATA;" + "</Description>");
                                        __MyXml_RawText.Add("</Special_Properties>");
                                    }
                                    else
                                    {
                                        string[] mytags = tag1.Split(';');


                                        if (mytags != null)
                                        {
                                            //-----------------------------
                                            __MyXml_RawText.Add("<Special_Properties>");
                                            __MyXml_RawText.Add("<UID>" + mytags[1].ToString() + "</UID>");
                                            __MyXml_RawText.Add("<Owner>" + mytags[2].ToString() + "</Owner>");
                                            __MyXml_RawText.Add("<TypeUse>" + mytags[3].ToString() + "</TypeUse>");
                                            __MyXml_RawText.Add("<RealaseDate>" + mytags[4].ToString() + "</RealaseDate>");
                                            __MyXml_RawText.Add("<ExpireDate>" + mytags[5].ToString() + "</ExpireDate>");
                                            __MyXml_RawText.Add("<Organization>" + mytags[6].ToString() + "</Organization>");
                                            __MyXml_RawText.Add("<Description>" + mytags[7].ToString() + "</Description>");
                                            __MyXml_RawText.Add("</Special_Properties>");
                                        }
                                        else
                                        {
                                            __MyXml_RawText.Add("<Special_Properties>");
                                            __MyXml_RawText.Add("<UID>" + "NoDATA;" + "</UID>");
                                            __MyXml_RawText.Add("<Owner>" + "NoDATA;" + "</Owner>");
                                            __MyXml_RawText.Add("<TypeUse>" + "NoDATA;" + "</TypeUse>");
                                            __MyXml_RawText.Add("<RealaseDate>" + "NoDATA;" + "</RealaseDate>");
                                            __MyXml_RawText.Add("<ExpireDate>" + "NoDATA;" + "</ExpireDate>");
                                            __MyXml_RawText.Add("<Organization>" + "NoDATA;" + "</Organization>");
                                            __MyXml_RawText.Add("<Description>" + "NoDATA;" + "</Description>");
                                            __MyXml_RawText.Add("</Special_Properties>");
                                        }
                                        //-----------------------------
                                    }


                                    TreeNode a_Computer_Name = new TreeNode("Computer Name ");
                                    foreach (ManagementObject queryObj in searcher_Computer.Get())
                                    {
                                        __MyXml_RawText.Add("<Computer_Name>" + queryObj["Name"].ToString() + "</Computer_Name>");

                                        a_Computer_Name.Nodes.Add("Computer_Name: " + queryObj["Name"].ToString());
                                    }
                                    Root_node.Nodes.Add(a_Computer_Name);
                                }
                                catch (Exception pointerror)
                                {
                                    __MyXml_RawText.Add("<Computer_Name>" + PCName_str + "</Computer_Name>");
                                }
                                try
                                {



                                    ManagementObjectSearcher searcher_Os = new ManagementObjectSearcher(s, "SELECT * FROM Win32_OperatingSystem");

                                    __MyXml_RawText.Add("<" + "Operation_System" + ">");
                                    TreeNode a_OS_VER = new TreeNode("Operation System ");
                                    foreach (ManagementObject queryObj in searcher_Os.Get())
                                    {
                                        __MyXml_RawText.Add("<BootDevice>" + queryObj["BootDevice"].ToString() + "</BootDevice>");
                                        __MyXml_RawText.Add("<BuildNumber>" + queryObj["BuildNumber"].ToString() + "</BuildNumber>");
                                        __MyXml_RawText.Add("<BuildType>" + queryObj["BuildType"].ToString() + "</BuildType>");
                                        __MyXml_RawText.Add("<WindowsVersion>" + queryObj["Caption"].ToString() + "</WindowsVersion>");
                                        __MyXml_RawText.Add("<WindowsDrive>" + queryObj["WindowsDirectory"].ToString() + "</WindowsDrive>");

                                        a_OS_VER.Nodes.Add("BootDevice: " + queryObj["BootDevice"]);
                                        a_OS_VER.Nodes.Add("BuildNumber: " + queryObj["BuildNumber"]);
                                        a_OS_VER.Nodes.Add("BuildType: " + queryObj["BuildType"]);
                                        a_OS_VER.Nodes.Add("Caption: " + queryObj["Caption"]);
                                        a_OS_VER.Nodes.Add("Caption: " + queryObj["WindowsDirectory"]);
                                    }
                                    Root_node.Nodes.Add(a_OS_VER);

                                    __MyXml_RawText.Add("</" + "Operation_System" + ">");
                                }
                                catch (Exception)
                                {
                                    __MyXml_RawText.Add("</" + "Operation_System" + ">");

                                }
                                if (checkBox3.Checked)
                                {
                                    try
                                    {
                                        ManagementObjectSearcher searcher_Motherboard = new ManagementObjectSearcher(s, "SELECT * FROM Win32_BaseBoard");

                                        __MyXml_RawText.Add("<" + "MOTHERBOARD" + ">");
                                        TreeNode a_Motherboard = new TreeNode("MOTHERBOARD ");
                                        foreach (ManagementObject queryObj in searcher_Motherboard.Get())
                                        {

                                            __MyXml_RawText.Add("<Motherboard_Product>" + queryObj["Product"].ToString() + "</Motherboard_Product>");
                                            __MyXml_RawText.Add("<Motherboard_SerialNumber>" + queryObj["SerialNumber"].ToString() + "</Motherboard_SerialNumber>");

                                            a_Motherboard.Nodes.Add("Motherboard Product: " + queryObj["Product"].ToString());
                                            a_Motherboard.Nodes.Add("Motherboard SerialNumber: " + queryObj["SerialNumber"].ToString());
                                        }
                                        Root_node.Nodes.Add(a_Motherboard);

                                        __MyXml_RawText.Add("</" + "MOTHERBOARD" + ">");
                                    }
                                    catch (Exception pointerror)
                                    {
                                        __MyXml_RawText.Add("</" + "MOTHERBOARD" + ">");
                                    }
                                }

                                if (checkBox2.Checked)
                                {
                                    try
                                    {
                                        ManagementObjectSearcher searcher_CPU = new ManagementObjectSearcher(s, "SELECT * FROM Win32_Processor");

                                        __MyXml_RawText.Add("<" + "CPU" + ">");
                                        TreeNode a_cpu = new TreeNode("CPU ");
                                        foreach (ManagementObject queryObj in searcher_CPU.Get())
                                        {

                                            __MyXml_RawText.Add("<CPU_Name>" + queryObj["Name"].ToString() + "</CPU_Name>");
                                            __MyXml_RawText.Add("<CPU_Manufacturer>" + queryObj["Manufacturer"].ToString() + "</CPU_Manufacturer>");

                                            a_cpu.Nodes.Add("CPU Name: " + queryObj["Name"].ToString());
                                            a_cpu.Nodes.Add("CPU Manufacturer: " + queryObj["Manufacturer"].ToString());
                                        }
                                        Root_node.Nodes.Add(a_cpu);

                                        __MyXml_RawText.Add("</" + "CPU" + ">");
                                    }
                                    catch (Exception pointerror)
                                    {
                                        __MyXml_RawText.Add("</" + "CPU" + ">");
                                    }
                                }
                                if (checkBox4.Checked)
                                {
                                    try
                                    {
                                        ManagementObjectSearcher searcher_BIOS = new ManagementObjectSearcher(s, "SELECT * FROM Win32_BIOS");

                                        __MyXml_RawText.Add("<" + "BIOS" + ">");
                                        TreeNode a_bios = new TreeNode("BIOS ");
                                        foreach (ManagementObject queryObj in searcher_BIOS.Get())
                                        {

                                            __MyXml_RawText.Add("<BIOS_Name>" + queryObj["Name"].ToString() + "</BIOS_Name>");
                                            __MyXml_RawText.Add("<BIOS_Manufacturer>" + queryObj["Name"].ToString() + "</BIOS_Manufacturer>");

                                            a_bios.Nodes.Add("BIOS Name: " + queryObj["Name"].ToString());
                                            a_bios.Nodes.Add("BIOS Manufacturer: " + queryObj["Manufacturer"].ToString());
                                        }
                                        Root_node.Nodes.Add(a_bios);

                                        __MyXml_RawText.Add("</" + "BIOS" + ">");
                                    }
                                    catch (Exception pointerror)
                                    {
                                        __MyXml_RawText.Add("</" + "BIOS" + ">");
                                    }
                                }
                                if (checkBox1.Checked)
                                {
                                    try
                                    {
                                        ManagementObjectSearcher searcher_MEM = new ManagementObjectSearcher(s, "SELECT * FROM  Win32_LogicalMemoryConfiguration");

                                        __MyXml_RawText.Add("<" + "MEMORY" + ">");
                                        TreeNode a_Memory = new TreeNode("MEMORY ");
                                        foreach (ManagementObject queryObj in searcher_MEM.Get())
                                        {

                                            __MyXml_RawText.Add("<MEMORY_AvailableVirtualMemory>" + queryObj["AvailableVirtualMemory"].ToString() + "</MEMORY_AvailableVirtualMemory>");
                                            __MyXml_RawText.Add("<MEMORY_TotalPageFileSpace>" + queryObj["TotalPageFileSpace"].ToString() + "</MEMORY_TotalPageFileSpace>");
                                            __MyXml_RawText.Add("<MEMORY_TotalPhysicalMemory>" + queryObj["TotalPhysicalMemory"].ToString() + "</MEMORY_TotalPhysicalMemory>");
                                            __MyXml_RawText.Add("<MEMORY_TotalVirtualMemory>" + queryObj["TotalVirtualMemory"].ToString() + "</MEMORY_TotalVirtualMemory>");

                                            a_Memory.Nodes.Add("MEMORY AvailableVirtualMemory: " + queryObj["AvailableVirtualMemory"].ToString());
                                            a_Memory.Nodes.Add("MEMORY TotalPageFileSpace: " + queryObj["TotalPageFileSpace"]);
                                            a_Memory.Nodes.Add("MEMORY TotalPhysicalMemory: " + queryObj["TotalPhysicalMemory"]);
                                            a_Memory.Nodes.Add("MEMORY TotalVirtualMemory: " + queryObj["TotalVirtualMemory"]);
                                        }
                                        Root_node.Nodes.Add(a_Memory);

                                        __MyXml_RawText.Add("</" + "MEMORY" + ">");
                                    }
                                    catch (Exception pointerror)
                                    {
                                        __MyXml_RawText.Add("</" + "MEMORY" + ">");
                                    }
                                }
                                if (checkBox6.Checked)
                                {
                                    try
                                    {
                                        ManagementObjectSearcher searcher_ONbourdSound = new ManagementObjectSearcher(s, "SELECT * FROM   Win32_SoundDevice");

                                        __MyXml_RawText.Add("<" + "ONSOUND" + ">");
                                        TreeNode a_Sound = new TreeNode("ONSOUND ");
                                        foreach (ManagementObject queryObj in searcher_ONbourdSound.Get())
                                        {

                                            __MyXml_RawText.Add("<Onboard_SoundType>" + queryObj["Description"].ToString() + "</Onboard_SoundType>");
                                            __MyXml_RawText.Add("<Onboard_SoundName>" + queryObj["Name"].ToString() + "</Onboard_SoundName>");

                                            a_Sound.Nodes.Add("Onboard Sound Type: " + queryObj["Description"].ToString());
                                            a_Sound.Nodes.Add("Onboard Sound Name: " + queryObj["Name"]);
                                        }
                                        Root_node.Nodes.Add(a_Sound);

                                        __MyXml_RawText.Add("</" + "ONSOUND" + ">");
                                    }
                                    catch (Exception pointerror)
                                    {
                                        __MyXml_RawText.Add("</" + "ONSOUND" + ">");
                                    }
                                }
                                if (checkBox8.Checked)
                                {
                                    try
                                    {
                                        ManagementObjectSearcher searcher_MON = new ManagementObjectSearcher(s, "SELECT * FROM   Win32_DesktopMonitor");

                                        __MyXml_RawText.Add("<" + "MONITOR" + ">");
                                        TreeNode a_Monitor = new TreeNode("MONITOR ");
                                        foreach (ManagementObject queryObj in searcher_MON.Get())
                                        {

                                            __MyXml_RawText.Add("<MONITOR_Type>" + queryObj["MonitorType"].ToString() + "</MONITOR_Type>");
                                            __MyXml_RawText.Add("<MONITOR_Name>" + queryObj["Name"].ToString() + "</MONITOR_Name>");

                                            a_Monitor.Nodes.Add("MONITOR Type: " + queryObj["MonitorType"].ToString());
                                            a_Monitor.Nodes.Add("MONITOR Name: " + queryObj["Name"]);
                                        }
                                        Root_node.Nodes.Add(a_Monitor);

                                        __MyXml_RawText.Add("</" + "MONITOR" + ">");
                                    }
                                    catch (Exception pointerror)
                                    {
                                        __MyXml_RawText.Add("</" + "MONITOR" + ">");
                                    }
                                }
                                try
                                {
                                    ManagementObjectSearcher searcher_USER = new ManagementObjectSearcher(s, "SELECT * FROM   Win32_NetworkLoginProfile");

                                    __MyXml_RawText.Add("<" + "LOGON_USERS" + ">");
                                    TreeNode a_Logonusr = new TreeNode("LOGON USERS ");
                                    foreach (ManagementObject queryObj in searcher_USER.Get())
                                    {


                                        __MyXml_RawText.Add("<LOGON_UsersName>" + queryObj["Name"].ToString() + "</LOGON_UsersName>");

                                        a_Logonusr.Nodes.Add("LOGON Users Name: " + queryObj["Name"]);
                                    }
                                    Root_node.Nodes.Add(a_Logonusr);

                                    __MyXml_RawText.Add("</" + "LOGON_USERS" + ">");
                                }
                                catch (Exception pointerror)
                                {
                                    __MyXml_RawText.Add("</" + "LOGON_USERS" + ">");
                                }
                                if (checkBox7.Checked)
                                {
                                    try
                                    {
                                        ManagementObjectSearcher searcher_IPADDRESS = new ManagementObjectSearcher(s, "SELECT * FROM   Win32_NetworkAdapterConfiguration");

                                        TreeNode a_IPAddress = new TreeNode("IPADDRESS ");
                                        foreach (ManagementObject queryObj in searcher_IPADDRESS.Get())
                                        {
                                            if (queryObj["IPAddress"] == null) { }
                                            else
                                            {
                                                String[] arrIPAddress = (String[])(queryObj["IPAddress"]);
                                                foreach (String arrValue in arrIPAddress)
                                                {

                                                    a_IPAddress.Nodes.Add("IP ADDRESS: " + arrValue);
                                                }

                                            }

                                        }
                                        Root_node.Nodes.Add(a_IPAddress);
                                    }
                                    catch (Exception pointerror) { }
                                }
                                if (checkBox5.Checked)
                                {
                                    try
                                    {
                                        TreeNode a_Logicaldisk = new TreeNode("LOGICAL DISK ");
                                        __MyXml_RawText.Add("<" + "LOGICAL_DISK" + ">");
                                        try
                                        {
                                            ManagementObjectSearcher searcher_DISKs_ID = new ManagementObjectSearcher(s, "SELECT * FROM Win32_DiskDrive");
                                            foreach (ManagementObject queryObj in searcher_DISKs_ID.Get())
                                            {
                                                __MyXml_RawText.Add("<DISKID>" + queryObj["Caption"] + "  " + queryObj["Size"] + "  " + queryObj["InterfaceType"] + "</DISKID>");
                                                //__MyXml_RawText.Add("<DISKID>" + queryObj["Size"] + "</DISKID>");                                                        

                                                a_Logicaldisk.Nodes.Add("DISKID: " + queryObj["Caption"] + "  " + queryObj["Size"] + "  " + queryObj["InterfaceType"]);
                                                // a_Logicaldisk.Nodes.Add("Size: " + queryObj["Size"]);
                                            }
                                        }
                                        catch (Exception err)
                                        {


                                        }

                                        ManagementObjectSearcher searcher_DISKs = new ManagementObjectSearcher(s, "SELECT * FROM Win32_LogicalDisk");


                                        foreach (ManagementObject queryObj in searcher_DISKs.Get())
                                        {

                                            __MyXml_RawText.Add("<DISK>" + queryObj["Description"] + "  " + queryObj["Caption"].ToString().Remove(1, 1) + "   Size: " + queryObj["Size"] + "</DISK>");
                                            a_Logicaldisk.Nodes.Add("Name: " + queryObj["Description"] + "  " + queryObj["Name"]);
                                            a_Logicaldisk.Nodes.Add("Size: " + queryObj["Size"]);
                                        }
                                        Root_node.Nodes.Add(a_Logicaldisk);
                                        __MyXml_RawText.Add("</" + "LOGICAL_DISK" + ">");
                                    }
                                    catch (Exception pointerror)
                                    {
                                        __MyXml_RawText.Add("</" + "LOGICAL_DISK" + ">");
                                    }
                                }
                                if (checkBox10.Checked)
                                {
                                    try
                                    {
                                        TreeNode a_VGA = new TreeNode("VGA");
                                        __MyXml_RawText.Add("<" + "VGA" + ">");
                                        ManagementObjectSearcher searcher_VGA = new ManagementObjectSearcher(s, "SELECT * FROM Win32_DisplayConfiguration");
                                        foreach (ManagementObject queryObj in searcher_VGA.Get())
                                        {
                                            __MyXml_RawText.Add("<VGAID>" + queryObj["DeviceName"] + "   Ver: " + queryObj["DriverVersion"] + "  " + "</VGAID>");
                                            a_VGA.Nodes.Add(queryObj["DeviceName"] + "   Ver: " + queryObj["DriverVersion"]);
                                        }
                                        __MyXml_RawText.Add("</" + "VGA" + ">");
                                        Root_node.Nodes.Add(a_VGA);
                                    }
                                    catch (Exception errore)
                                    {
                                        __MyXml_RawText.Add("</" + "VGA" + ">");
                                    }

                                }

                                bgw.ReportProgress(100, Root_node);



                                _result = Listbox_Selection + " ==> GetData  Ok ";
                            }
                            catch (Exception ee)
                            {
                                _result = ee.Message;

                                __MyXml_RawText.Add("</" + PCName_str + ">");

                            }


                            __MyXml_RawText.Add("</" + PCName_str + ">");

                        }
                    }
                    else
                    {
                        _result = "IPAddress Not Valid";
                        TreeNode t_error = new TreeNode(address + "  " + _result);
                        bgw.ReportProgress(100, t_error);
                    }
                }
            }
            catch (Exception allerror)
            {

                _result = Listbox_Selection + "  " + allerror.Message;
                TreeNode t_error = new TreeNode(_result);
                bgw.ReportProgress(100, t_error);

            }  
    
            }             
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            treeView2.CheckBoxes = true;
            try
            {
                if (toolStripTextBox1.Text != null)
                {
                    TreeNode tn = new TreeNode(toolStripTextBox1.Text);

                    string Raw = toolStripTextBox1.Text + ";" + textBoxuid.Text + ";" + textBoxOwner.Text + ";" + textBoxtype.Text + ";" + textBoxReleaseDate.Text + ";" + textBoxExpireDate.Text + ";" + textBoxDescription.Text + ";";
                    tn.Tag = Raw;
                    treeView2.Nodes.Add(tn);
                }
            }
            catch (Exception)
            {
                
              
            }

            

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {                    
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            foreach (TreeNode t in treeView2.Nodes)
            {
                treeView2.Nodes.Remove(t);
               
            }
           treeView2.Nodes.Clear();
           treeView2.ResetText();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                
                openFileDialog1.Filter = "txt files (*.txt)|*.txt";
                openFileDialog1.DefaultExt = "txt";
                openFileDialog1.FilterIndex = 0;
                openFileDialog1.FileName = "IPList";
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.ShowDialog();
                FileInfo FileExtension_Valid = new FileInfo(openFileDialog1.FileName);
                if (FileExtension_Valid.Extension == ".txt")
                {                                        
                    treeView2.CheckBoxes = true;
                    using (StreamReader sr = FileExtension_Valid.OpenText())
                    {
                        string s = "";
                        while ((s = sr.ReadLine()) != null)
                        {                            
                            treeView2.Nodes.Add(s);
                        }

                    }
                }
                
            }
            catch (Exception err2)
            {
            
            }           
        }


        ArrayList l1 = new ArrayList();
        string errorArray;

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            try
            {
                ISstop = false;
                Prerequset();

                l1.Clear();


                foreach (TreeNode s in treeView2.Nodes)
                {
                    string str = "\\\\" + s.Text + "\\root\\cimv2";
                    try
                    {
                       
                        l1.Add(s.Text);
                        treeView1.CheckBoxes = true;
                     

                    }
                    catch (Exception r)
                    {
                        string hh = r.Message;
                        errorArray = s.Text + " ==> " + hh;
                        listBox2.Items.Add(errorArray);

                    }



                }


                backgroundWorker2.RunWorkerAsync();
            }
            catch (Exception errors)
            {
                
               // throw;
            }
            
           

            
        }
        string tags1;
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            
            try
            {
                ISstop = false;
               
                if (treeView2.SelectedNode.Tag != null)
                {
                    tags1 = treeView2.SelectedNode.Tag.ToString();
                }
                else
                {
                    tags1 = treeView2.SelectedNode.Text + ";" + textBoxuid.Text + ";" + textBoxOwner.Text + ";" + textBoxtype.Text + ";" + textBoxReleaseDate.Text + ";" + textBoxExpireDate.Text + ";" + TextboxOrganization.Text + ";" + textBoxDescription.Text + ";";
                }
            }
            catch (Exception errors)
            {
                
               
            }
           
            try
            {
                
                Prerequset();
                if (treeView2.SelectedNode.Text != null | treeView2.SelectedNode.Text != " ")
                {
                    //selectedAddress = listBox1.SelectedItem.ToString();
                    selectedAddress = treeView2.SelectedNode.Text;
                    backgroundWorker1.RunWorkerAsync();
                }
                else { MessageBox.Show("Select a Host", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1); }

            }
            catch (Exception er)
            {
                
              
            }
           

           
        }

     

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            __MyXml_RawText.Clear();
            foreach (TreeNode t in treeView1.Nodes)
            {
                treeView1.Nodes.Remove(t);
            }
            treeView1.Nodes.Clear();
            treeView1.ResetText();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //listBox1.SelectedItems.Clear();
            treeView2.Nodes.Clear();
            //listBox1.Refresh();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;          
            checkBox6.Checked = true;
            checkBox7.Checked = true;
            checkBox8.Checked = true;
            checkBox10.Checked = true;
            if (!checkBox9.Checked)
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox5.Checked = false;          
                checkBox6.Checked = false;
                checkBox7.Checked = false;
                checkBox8.Checked = false;
                checkBox10.Checked = false;
            }


        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

          

                try
                {
                    getdata(backgroundWorker1, e, selectedAddress,tags1);
                }
                catch (Exception esr)
                {
                    
                }
            
                    
        }

        

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {           
            try
            {
                TreeNode tn = (TreeNode)e.UserState;               
                treeView1.Nodes.Add(tn);
                listBox2.Items.Add(_result);
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                
                
               
                
            }
            catch (Exception error)
            {

                listBox2.Items.Add(error.Message);
            }            
          
        }



       
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {                        
            listBox2.Items.Add("----Process--Completed----");
            
        }


        string selectedAddress = "";
        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
           
           
           
                try
                {
                    for (int i = 0; i < l1.Count; i++)
                    {
                        
                        selectedAddress = l1[i].ToString(); 

                     //   listBox2.SelectedIndex = i;                 
                        string tags;
                        if (treeView2.Nodes[i].Tag != null)
                        {
                            tags = treeView2.Nodes[i].Tag.ToString();
                        }
                        else
                        {
                            // tags = treeView2.SelectedNode.Text + ";" + textBoxuid.Text + ";" + textBoxOwner.Text + ";" + textBoxtype.Text + ";" + textBoxReleaseDate.Text + ";" + textBoxExpireDate.Text + ";" + TextboxOrganization.Text + ";" + textBoxDescription.Text + ";";
                            tags = null;
                        }
                        getdata(backgroundWorker2, e, selectedAddress,tags);
                    }
                }
                catch (Exception esr2)
                {

                    //  throw;
                }
           
           
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                TreeNode tn = (TreeNode)e.UserState;
                treeView1.Nodes.Add(tn);
                listBox2.Items.Add(_result);
                treeView1.SelectedNode = tn;
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
                
                
            }
            catch (Exception error)
            {                
            
            }
            listBox2.SelectedIndex = listBox2.Items.Count - 1;
            
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          
            listBox2.Items.Add("----Process--Completed----");
            
        }

        private XmlDocument dom = new XmlDocument();
        private XMLClass myxmlclass = new XMLClass();
        private TreeNode tNode = new TreeNode();
        private Thread XMLLoadT, MYXMLClass_T;

        private delegate void _load(string _s);
        public string _SPath;        
        private void _LoadXMLMethod()
        {
            BeginInvoke(new _load(dom.Load),_SPath);
        }

        private delegate void _AddNode(XmlNode _inXmlNode, TreeNode _inTreeNode);
        private void _AddNodeMethod()
        {
            BeginInvoke(new _AddNode(myxmlclass.AddNode), dom.DocumentElement, tNode);
        }
        
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                treeView1.Nodes.Clear();
                openFileDialog1.Filter = "xml files (*.xml)|*.xml";
                openFileDialog1.DefaultExt = "xml";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.ShowDialog();
                _SPath = openFileDialog1.FileName;
                // SECTION 1. Create a DOM Document and load the XML data into it.
               
                try
                {
                    dom.Load(_SPath);
                    XMLLoadT = new Thread(_LoadXMLMethod);
                    XMLLoadT.Priority = ThreadPriority.AboveNormal;
                    XMLLoadT.Start();
                }
                catch (Exception er)
                {

                    XMLLoadT.Abort();
                    MessageBox.Show(er.Message);
                }

                treeView1.Nodes.Add(new TreeNode(dom.DocumentElement.Name));
                tNode = treeView1.Nodes[0];

                MYXMLClass_T = new Thread(_AddNodeMethod);
                MYXMLClass_T.Priority = ThreadPriority.Highest;
                MYXMLClass_T.Start();

                // SECTION 3. Populate the TreeView with the DOM nodes.                                
               
                // myxmlclass.AddNode(dom.DocumentElement, tNode);               
            
            }
            catch (XmlException xmlEx)
            {
                MessageBox.Show(xmlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            XMLLoadT.Abort();
            MYXMLClass_T.Abort();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            

        }
        public void SetXml_to_File(string PATH)
        {
            string XMLstr = "<root>";
           // listBox3.Items.Add("<root>");
            foreach (string SR in __MyXml_RawText)
            {           
                XMLstr += "\r\n";
                XMLstr += SR.ToString();
            }
            XMLstr += "\r\n";
            XMLstr += "</root>";
           
            DOMDocument dom = new DOMDocument();
            dom.loadXML(XMLstr);
            try
            {
                dom.save(PATH);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
           
        }
       
        

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog FBD = new SaveFileDialog();
                FBD.Filter = "XML files (*.xml)|*.xml";
                FBD.FilterIndex = 2;
                FBD.RestoreDirectory = true;
                FBD.ShowDialog();
                string PATH = FBD.FileName;
                SetXml_to_File(PATH);      
            }
            catch (Exception)
            {
                
            }
                
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            try
            {
                SaveFileDialog FBD = new SaveFileDialog();
                FBD.Filter = "XML files (*.xml)|*.xml";
                FBD.FilterIndex = 2;
                FBD.RestoreDirectory = true;
                FBD.ShowDialog();
                string PATH = FBD.FileName;
                SetXml_to_File(PATH);
            }
            catch (Exception)
            {

            }
        }
        ArrayList myarray = new ArrayList();
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            string Raw = " ";
            try
            {
                Raw = treeView2.SelectedNode.Text + ";" + textBoxuid.Text + ";" + textBoxOwner.Text + ";" + textBoxtype.Text + ";" + textBoxReleaseDate.Text + ";" + textBoxExpireDate.Text + ";" + TextboxOrganization.Text + ";" + textBoxDescription.Text + ";";
                string[] ts = Raw.Split(';');
            }
            catch (Exception errr)
            {
                
                
            }
          
            //===================================
                            //listBox1.SelectedItem += Raw;
                            //listBox1.Items[listBox1.SelectedIndices[0]] += Raw;
            //===================================
            try
            {
                treeView2.SelectedNode.Tag = Raw.ToString();
                if (treeView2.SelectedNode.Tag != null | treeView2.SelectedNode.Tag != " ")
                {
                    treeView2.SelectedNode.Checked = true;
                }
            }
            catch (Exception eerr)
            {
                textBoxuid.Text = " ";
                textBoxOwner.Text = " ";
                textBoxtype.Text = " ";
                textBoxReleaseDate.Text = " ";
                textBoxExpireDate.Text = " ";
                TextboxOrganization.Text = " ";
                textBoxDescription.Text = " ";
                
            }
           
            textBoxuid.Text  = " "; 
            textBoxOwner.Text  = " "; 
            textBoxtype.Text = " ";
            textBoxReleaseDate.Text= " ";
            textBoxExpireDate.Text = " ";
            TextboxOrganization.Text = " ";
            textBoxDescription.Text = " ";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxIPAddress.Text = "";
            textBoxuid.Text = "";
            textBoxOwner.Text = "";
            textBoxtype.Text = "";
            textBoxReleaseDate.Text = "";
            textBoxExpireDate.Text = "";
            textBoxDescription.Text = "";
          
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.ShowDialog();
        }

      


        private void treeView2_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
            textBoxIPAddress.Text = " ";
            textBoxuid.Text = " ";
            textBoxOwner.Text = " ";
            textBoxtype.Text = " ";
            textBoxReleaseDate.Text = " ";
            textBoxExpireDate.Text = " ";
            TextboxOrganization.Text = " ";
            textBoxDescription.Text = " ";
            if (e.Node.Tag != null)
            {
                
                string[] ts = e.Node.Tag.ToString().Split(';');
                try
                {
                    textBoxIPAddress.Text = ts[0];
                    textBoxuid.Text = ts[1];
                    textBoxOwner.Text = ts[2];
                    textBoxtype.Text = ts[3];
                    textBoxReleaseDate.Text = ts[4];
                    textBoxExpireDate.Text = ts[5];
                    TextboxOrganization.Text = ts[6];
                    textBoxDescription.Text = ts[7];
                }
                catch (Exception eee) { }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           // _result = "";
        }

        private void toolStripButton4_Click_1(object sender, EventArgs e)
        {
            ISstop = true;
        }

       

       
       
    }
}
