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

        int site;

        private CancellationTokenSource cancellationToken = new CancellationTokenSource();
        string username = "student";
        string password = "Zaq1Xsw2";
        FileVersionInfo originalDll;
        string originalVersion;
        NameValueCollection folders;


        public DllValidator()
        {
            InitializeComponent();
            folders = new NameValueCollection();
            folders.Add("jftapp", @"c\jftes\jftapp");
            folders.Add("traineeapp", @"c\traineedata\traineeapp");
            backends = new NameValueCollection();
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
                runButton.Enabled = condition;
                if (label != null)
                {
                    label2.Text = label;
                }
            });
        }


 
        List<string> names = new List<string>();
        List<string> IPList = new List<string>();

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
            XmlNodeList nodeList = doc.DocumentElement.SelectSingleNode(siteName).ChildNodes;
            foreach(XmlNode node in nodeList)
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
                        progressBar1.Maximum = names.Count + 2;

                        toggleRunning(false, " ");
                        string file = new FileInfo(folderTextBox.Text).Name;
                        string folder = Path.GetDirectoryName(folderTextBox.Text);

                        originalDll = FileVersionInfo.GetVersionInfo(folderTextBox.Text);
                        originalVersion = originalDll.FileVersion;


                        cancellationToken = new CancellationTokenSource();
                        Task.Factory.StartNew(() => startValidation(file));
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
        }
        private void startValidation(string fileName)
        {
            for (int i = 0; i < 3; i++)
            {
                validateDll(backends[i], backends.GetKey(i), fileName);
            }
            for (int i = 0; i < IPList.Count ; i++)
            {
                validateDll(IPList[i], names[i], fileName);

            }
            cancellationToken.Cancel();
            toggleRunning(true, "Done.");

        }

        private void checkFiles(string path, string fileName)
        {
            string[] files = Directory.GetFiles(path, "*.dll");
            string[] dirs = Directory.GetDirectories(path);
            foreach (string file in files)
            {
                FileVersionInfo newDll = FileVersionInfo.GetVersionInfo(path);
                string newVersion = newDll.FileVersion;
                if (file.Equals(fileName))
                {
                    if (originalVersion == newVersion)
                    {
                        VersionGood(path);
                    }
                    else
                    {
                        VersionBad(path);
                    }
                }

            }

            if (dirs.Length >= 1)
            {
                foreach (string directory in dirs)
                {
                    checkFiles(directory,fileName);
                }
            }

        }
        private void validateDll(string IP, string stationName, string fileName)
        {
            try
            {
                //making sure computer is connected 
                Ping ping = new Ping();
                PingReply reply = ping.Send(IP);

                if (reply.Status == IPStatus.Success)
                {
                    try
                    {
                        //Making the path without :
                       // string temp = "";
                       // var charToRemove = new String[] { "C:" };
                       // foreach (var c in charToRemove)
                        //{
                        //    temp = dllPath.Replace(c, "C");
                       // }
                        //string fullPath = @"\\" + IP + @"\" + temp;

                        
                        //Connencting to computer with credentials
                        for(int i = 0; i < folders.Count; i++)
                        {
                            var command = "NET USE " + @"\\" + IP + @"\c" + " /user:" + username + " " + password;
                            ExecuteCommand(command, 5000);
                            string path = @"\\"+IP + @"\" + folders[i];
                            checkFiles(path, fileName);
                            //var directory = Path.GetDirectoryName().Trim();

                        }

                    }
                    catch(Exception e)
                    {
                        if(e is FileNotFoundException){
                            VersionBad(stationName, "Files Not Found");
                        }
                        else
                        {
                            VersionBad(stationName, e.Message);
                        }
                    }
                }
                else
                {
                    VersionBad(stationName, "No Connection To: " + IP);
                }
            }
            catch
            {
                VersionBad(stationName, "No Connection To: " + IP);

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
                folderTextBox.Text = choofdlog.FileName;
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
