using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using System.IO;
using System.Threading;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Reflection;
using System.Xml.Linq;

namespace Dll_Validator
{
    public partial class DllValidator : Form
    {
        NameValueCollection backends;
        List<string> names = new List<string>();
        List<string> IPList = new List<string>();
        string[] filesChosen;
        string[] filesChosenNames;
        int site;

        private CancellationTokenSource cancellationToken = new CancellationTokenSource();

        //CR- Exlude to configuration-DONE
        string username = ConfigurationManager.AppSettings["student"];
        string password = ConfigurationManager.AppSettings["password"];


        FileVersionInfo originalDll;
        string originalVersion;

        public DllValidator()
        {
            InitializeComponent();


            backends = new NameValueCollection();

            //CR- Import from configuration 
            backends.Add("SimServer", "192.168.0.110");
            backends.Add("CGFx", "192.168.0.120");
            backends.Add("SimHost", "192.168.0.100");

            stationsListView.View = View.Details;

        }
        private void toggleRunning(bool condition, string label = null)
        {
            this.Invoke((MethodInvoker)delegate
            {
                SitesCombo.Enabled = condition;
                folderTextBox.Enabled = condition;
                folderDialogButton.Enabled = condition;
                runButton.Enabled = condition;
                if (label != null)
                {
                    label2.Text = label;
                }
            });
        }



        public void initSiteList()
        {
            var stringToReplace = SitesCombo.Text;
            var charToRemove = new String[] { " " };
            foreach (var c in charToRemove)
            {
                stringToReplace = stringToReplace.Replace(c, "");
            }
            var siteName = stringToReplace;

            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigurationManager.AppSettings["validator.xml"]);
            XmlNodeList nodeList = doc.DocumentElement.SelectSingleNode("backends").ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                names.Add(node.Attributes["name"]?.InnerText);
                IPList.Add(node.Attributes["IP"]?.InnerText);
            }
            nodeList = doc.DocumentElement.SelectSingleNode(siteName).ChildNodes;
            foreach (XmlNode node in nodeList)
            {
                names.Add(node.Attributes["name"]?.InnerText);
                IPList.Add(node.Attributes["IP"]?.InnerText);
            }

        }
        private void runButton_Click(object sender, EventArgs e)
        {


            if (!(String.IsNullOrEmpty(folderTextBox.Text)))
            {
                site = SitesCombo.SelectedIndex;
                initSiteList();

                try
                {

                    if (site >= 0)
                    {
                        stationsListView.Items.Clear();

                        progressBar1.Value = 0;
                        progressBar1.Step = 1;
                        progressBar1.Maximum = names.Count;

                        toggleRunning(false, " ");

                        cancellationToken = new CancellationTokenSource();
                        Task.Factory.StartNew(() => startValidation(filesChosen));
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
            else
            {
                MessageBox.Show("Please Fill All Required Fields");
            }
        }


        public static int ExecuteCommand(string command, int timeout)
        {
            var processInfo = new ProcessStartInfo("cmd.exe", "/C " + command)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = "C:\\",
            };

            var process = Process.Start(processInfo);
            process.WaitForExit(timeout);
            var exitCode = process.ExitCode;
            process.Close();
            return exitCode;
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
                        try
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                label2.Text += ".";
                            });
                        }
                        catch
                        {

                        }
                    }
                    Thread.Sleep(500);
                }
            }
            this.Invoke((MethodInvoker)delegate
            {
                label2.Text = "Done.";
            });
        }
        private void startValidation(string[] dllPaths)
        {


            for (int j = 0; j < IPList.Count; j++)
            {
                if (isConnected(IPList[j]))
                {
                    for (int i = 0; i < dllPaths.Length; i++)
                    {
                        originalDll = FileVersionInfo.GetVersionInfo(dllPaths[i]);
                        originalVersion = originalDll.FileVersion;

                        validateDll(IPList[j], names[j], dllPaths[i], i);
                    }
                }
                else
                {
                    VersionBad(names[j], $"Cannot connect to {IPList[j]}");
                }

            }
            cancellationToken.Cancel();
            toggleRunning(true);

        }
        public bool isConnected(string IP)
        {
            //making sure computer is connected 
            Ping ping = new Ping();
            PingReply reply = ping.Send(IP);

            return (reply.Status == IPStatus.Success);

        }

        private void validateDll(string IP, string stationName, string dllPath, int fileInArrayIndex)
        {

            try
            {
                //Making the path without :
                string temp = "";
                var charToRemove = new String[] { "C:" };
                foreach (var c in charToRemove)
                {
                    temp = dllPath.Replace(c, "C");
                }
                string fullPath = @"\\" + IP + @"\" + temp;


                //Connencting to computer with credentials
                var directory = Path.GetDirectoryName(fullPath).Trim();
                var command = "NET USE " + directory + " /user:" + username + " " + password;
                ExecuteCommand(command, 5000);




                FileVersionInfo newDll = FileVersionInfo.GetVersionInfo(fullPath);
                string newVersion = newDll.FileVersion;

                if (originalVersion == newVersion)
                {
                    VersionGood(stationName, $"{filesChosenNames[fileInArrayIndex]} Version: {newVersion}");
                }
                else
                {
                    VersionBad(stationName, $"{filesChosenNames[fileInArrayIndex]} Version: {newVersion}");
                }
            }
            catch (Exception e)
            {
                if (e is FileNotFoundException)
                {
                    VersionBad(stationName, $"{filesChosenNames[fileInArrayIndex]} Not Found");
                }
                else
                {
                    VersionBad(stationName, e.Message);
                }
            }
        }

        private void folderDialogButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;
            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                filesChosen = choofdlog.FileNames;
                filesChosenNames = choofdlog.SafeFileNames;
                for (int i = 0; i < filesChosen.Length; i++)
                {
                    if (i == 0)
                    {
                        folderTextBox.Text = $"{filesChosen[i]} ";
                    }
                    else
                    {
                        folderTextBox.Text += $",{filesChosen[i]} ";
                    }
                }
            }
            else
                folderTextBox.Text = string.Empty;
        }


        private void VersionGood(string stationName, string comment = "Version Good")
        {

            ListViewItem li = new ListViewItem();
            li.ForeColor = Color.Green;
            li.Text = stationName;
            li.SubItems.Add(comment);
            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.PerformStep();
                progressBar1.Refresh();

                stationsListView.Items.Add(li);
                stationsListView.Refresh();
            });
        }

        private void VersionBad(string stationName, string comment = "Version Bad")
        {

            ListViewItem li = new ListViewItem();
            li.ForeColor = Color.Red;
            li.Text = stationName;
            li.SubItems.Add(comment);
            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.PerformStep();
                progressBar1.Refresh();

                stationsListView.Items.Add(li);
                stationsListView.Refresh();
            });
        }








    }
}
