using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;

namespace JFTES_Configuration
{
    public partial class configurator : Form
    {
        NameValueCollection IPs;
        NameValueCollection SimA;
        NameValueCollection SimB;
        NameValueCollection SimC1;
        NameValueCollection SimC2;
        NameValueCollection SimD171;
        NameValueCollection SimD175;
        XmlDocument doc = new XmlDocument();
        XmlNodeList nodeList;
        List<NameValueCollection> sites;
        private int site;
        private CancellationTokenSource cancellationToken = new CancellationTokenSource();
        private bool isIOS;
        private int maxIndex;
        private int minIndex;


        /*
         * 0=Main
         * 1=secondery
         * 2=RolePlayer
         * 3=t1
         * 4=t2
         * 5=t3
         * 6=t4
         * 7=t5
         * 8=t6
         * 9=t7
         * 10=t8
         * */


        /*
         * Names:
         * rootConfig
         * 
         * */



        public configurator()
        {
            InitializeComponent();
            IPs = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/IPs");
            SimA = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/SimA");
            SimB = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/SimB");
            SimC1 = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/SimC1");
            SimC2 = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/SimC2");
            SimD171 = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/SimD171");
            SimD175 = (NameValueCollection)ConfigurationManager.GetSection("customAppSettingsGroup/SimD175");
            sites = new List<NameValueCollection>();
            sites.Add(SimA);
            sites.Add(SimB);
            sites.Add(SimC1);
            sites.Add(SimC2);
            sites.Add(SimD171);
            sites.Add(SimD175);
            label2.Text = " ";
        }

        public void editConfigFiles(string fullPath, string newValue, bool sme = false)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = fullPath;
            Configuration otherConfig = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);


            otherConfig.AppSettings.Settings["StationID"].Value = newValue;
            if (sme)
                otherConfig.AppSettings.Settings["SmeStationId"].Value = newValue;


            otherConfig.Save();
        }

        public void changeXMLs(string IP, string stationName, string stationNum, bool isIOS)
        {


            try
            {
                string path = @"\\" + IP + @"\c\jftes\jftapp\release";
                //Change computer name
                string[] rootConfig = null;
                string[] jftAppConfigurations = null;


                Ping ping = new Ping();
                PingReply reply = ping.Send(IP);

                if (reply.Status == IPStatus.Success)
                {
                    rootConfig = Directory.GetFiles(path, "RootConfig.xml", SearchOption.AllDirectories);


                    //Change station num
                    jftAppConfigurations = Directory.GetFiles(path, "JFTAppConfiguration.xml", SearchOption.AllDirectories);



                    //Changing the nodes in the .config files (stationId)
                    editConfigFiles(path + @"\JFTApp\JFTApp.exe.config", stationNum);
                    if (!isIOS)
                        editConfigFiles(@"\\" + IP + @"\C\TraineeData\MediaCenter\MediaCenter.exe.config", stationNum, true);


                    changeName(rootConfig, stationName);
                    changeNum(jftAppConfigurations, stationNum);



                    Task.Factory.StartNew(() => FilesChanged(stationName));
                }
                else
                {
                    Task.Factory.StartNew(() => FilesNotFound(stationName));
                }


            }
            catch (Exception ex)
            {
                if (!(ex is IOException))
                {
                    MessageBox.Show(ex.Message);
                }
                Task.Factory.StartNew(() => FilesNotFound(stationName));
            }

        }

        private void changeNum(string[] filesToChange, string stationNum)
        {
            foreach (string file in filesToChange)
            {
                try
                {
                    StreamReader oReader = new StreamReader(file);
                    doc.Load(oReader);
                    oReader.Close();
                    nodeList = doc.GetElementsByTagName("StationNumber");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "");
                }

                //doc.Load(file);
                if (nodeList.Count == 0)
                {
                    continue;
                }

                foreach (XmlNode node in nodeList)
                {
                    node.InnerText = stationNum;
                }
                doc.Save(file);
            }
        }

        private void changeName(string[] filesToChange, string stationName)
        {
            foreach (string file in filesToChange)
            {
                try
                {
                    StreamReader oReader = new StreamReader(file);
                    doc.Load(oReader);
                    oReader.Close();
                    nodeList = doc.GetElementsByTagName("ComputerName");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message + "");
                }

                if (nodeList.Count == 0)
                {
                    continue;
                }

                foreach (XmlNode node in nodeList)
                {
                    node.InnerText = stationName;
                }
                doc.Save(file);
            }

        }

        private void FilesChanged(string stationName)
        {

                ListViewItem li = new ListViewItem();
                li.ForeColor = Color.Green;
                li.Text = stationName;
            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.PerformStep();
                progressBar1.Refresh();

                StatusListView.Items.Add(li);
                StatusListView.Refresh();
            });
        }

        private void FilesNotFound(string stationName)
        {

                ListViewItem li = new ListViewItem();
                li.ForeColor = Color.Red;
                li.Text = stationName;
            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.PerformStep();
                progressBar1.Refresh();

                StatusListView.Items.Add(li);
                StatusListView.Refresh();
            });
        }


        private void changeToSite(NameValueCollection selectedSite)
        {
            for (int i = 0, j = minIndex; i < selectedSite.Count && j < maxIndex; i++, j++)
            {

                isIOS = i < 2 ? true : false;

                changeXMLs(IPs[j], selectedSite.GetKey(i), selectedSite.GetValues(i)[0], isIOS);

            }
            cancellationToken.Cancel();
            toggleRunning(true, "Done.");

        }

        private void RunButton_Click(object sender, EventArgs e)
        {
            site = SitesCombo.SelectedIndex;
            try
            {

                if (site >= 0)
                {
                    calcMaxAndMin(site);

                    StatusListView.Items.Clear();


                    progressBar1.Value = 0;
                    progressBar1.Step = 1;
                    progressBar1.Maximum = sites[site].Count-1;

                    toggleRunning(false," ");

                    NameValueCollection selectedSite = sites[site];

                    isIOS = false;
                    cancellationToken = new CancellationTokenSource();
                    Task.Factory.StartNew(() => changeToSite(selectedSite));
                    Task.Factory.StartNew(() => Loading(), cancellationToken.Token);

                }
                else
                {
                    MessageBox.Show("Please Select Site");
                }
            }
            catch (Exception ex)
            {
                toggleRunning(true, ex.Message);
            }
        }
        private void toggleRunning(bool condition, string label = null)
        {
            this.Invoke((MethodInvoker)delegate
            {
                SitesCombo.Enabled = condition;
                RunButton.Enabled = condition;
                if (label != null)
                {
                    label2.Text = label;
                }
            });
        }

        private void calcMaxAndMin(int selectedIndex)
        {
            int numOfSitesA = SimA.Count - 1;
            int numOfSitesB = SimB.Count - 1;
            int numOfSitesC1 = SimC1.Count - 1;
            int numOfSitesC2 = SimC2.Count - 1;
            int numOfSitesD171 = SimD171.Count - 1;
            int numOfSitesD175 = SimD175.Count - 1;
            int[] sitesNums = { numOfSitesA, numOfSitesB, numOfSitesC1, numOfSitesC2, numOfSitesD171, numOfSitesD175 };

            if (selectedIndex == 0)
            {
                minIndex = 0;
                maxIndex = sitesNums[0];
                return;
            }
            for (int i = 0; i <= selectedIndex; i++)
            {
                maxIndex += sitesNums[i];
                if (i != selectedIndex)
                {
                    minIndex += sitesNums[i];
                }
            }

            //switch (selectedIndex)
            //{
            //    case 0:
            //        minIndex = 0;
            //        maxIndex = numOfSitesA;
            //        break;
            //    case 1:
            //        minIndex = numOfSitesA;
            //        maxIndex = minIndex + numOfSitesB;
            //        break;
            //    case 2:
            //        minIndex = numOfSitesA + numOfSitesB;
            //        maxIndex = minIndex + numOfSitesC1;
            //        break;
            //    case 3:
            //        minIndex = numOfSitesA + numOfSitesB + numOfSitesC1;
            //        maxIndex = minIndex + numOfSitesC2;
            //        break;
            //    case 4:
            //        minIndex = numOfSitesA + numOfSitesB + numOfSitesC1 + numOfSitesC2;
            //        maxIndex = minIndex + numOfSitesD171;
            //        break;
            //    case 5:
            //        minIndex = numOfSitesA + numOfSitesB + numOfSitesC1 + numOfSitesC2 + numOfSitesD171;
            //        maxIndex = minIndex + numOfSitesD175;
            //        break;
            //    default:
            //        minIndex = 0;
            //        maxIndex = numOfSitesA;
            //        break;
            //}


        }

        private void Loading()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            label2.Text = "Loading.";
                        });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            label2.Text += ".";
                        });
                    }
                    Thread.Sleep(500);
                }
            }
        }

    }
}
