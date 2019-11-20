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
        List<string> names = new List<string>();
        List<string> IPList = new List<string>();

        List<string> filesToChange = new List<string>();
        List<string> pathToFiles = new List<string>();
        FileInfo originalFile;
        FilesToReplace filesToReplaceForm = new FilesToReplace();

        public bool wasChacked;


        public DllValidator()
        {
            InitializeComponent();
            stationsListView.View = View.Details;

        }
        private void toggleRunning(bool condition, string label = null)
        {
            this.Invoke((MethodInvoker)delegate
            {
                SitesCombo.Enabled = condition;
                folderTextBox.Enabled = condition;
                runButton.Enabled = condition;
                folderDialogButton.Enabled = condition;
                if (label != null)
                {
                    label2.Text = label;
                }
            });
        }


        public void initSiteList()
        {
            backends = new NameValueCollection();
            folders = new NameValueCollection();
            names = new List<string>();
            IPList = new List<string>();

            var stringToReplace = SitesCombo.Text;
            var charToRemove = new String[] { " " };
            foreach (var c in charToRemove)
            {
                stringToReplace = stringToReplace.Replace(c, "");
            }
            var siteName = stringToReplace;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(ConfigurationManager.AppSettings["validator.xml"]);

                XmlNodeList nodeList = doc.DocumentElement.SelectSingleNode(siteName).ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    names.Add(node.Attributes["name"]?.InnerText);
                    IPList.Add(node.Attributes["IP"]?.InnerText);
                }

                nodeList = doc.DocumentElement.SelectSingleNode("backends").ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    backends.Add(node.Attributes["name"]?.InnerText, node.Attributes["IP"]?.InnerText);
                }

                nodeList = doc.DocumentElement.SelectSingleNode("folders").ChildNodes;
                foreach (XmlNode node in nodeList)
                {
                    folders.Add(node.Attributes["key"]?.InnerText, node.Attributes["name"]?.InnerText);
                }

            }catch(Exception e)
            {
                MessageBox.Show("Validator.xml is place in the wrong place, check app.config to see the right path.");
            }
            

        }
        private void runButton_Click(object sender, EventArgs e)
        {

            if (!(String.IsNullOrEmpty(folderTextBox.Text)))
            {
                string folderText = folderTextBox.Text;
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
                        string file = new FileInfo(folderText).Name;
                        string folder = Path.GetDirectoryName(folderText);
                        originalFile = new FileInfo(folderText);

                        originalDll = FileVersionInfo.GetVersionInfo(folderText);
                        originalVersion = originalDll.FileVersion;
                        label4.Text = $"Now Checking {file}, ver: {originalVersion}";

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

            for (int i = 0; i < backends.Count; i++)
            {
                validateDll(backends[i], backends.GetKey(i), fileName);
            }
            for (int i = 0; i < IPList.Count; i++)
            {
                validateDll(IPList[i], names[i], fileName);

            }
            if (filesToChange.Count > 0)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    filesToReplaceForm.Show();
                    filesToReplaceForm.initForm(filesToChange, pathToFiles, originalFile);
                });
            }
            cancellationToken.Cancel();
            toggleRunning(true, "Done.");

        }
        private void checkFiles(string stationName, string path, string fileName)
        {
            string[] files = Directory.GetFiles(path, "*.dll");
            string[] dirs = Directory.GetDirectories(path);
            foreach (string file in files)
            {
                string tempFile = new FileInfo(file).Name;
                if (tempFile.Equals(fileName))
                {
                    FileVersionInfo newDll = FileVersionInfo.GetVersionInfo(file);
                    string newVersion = newDll.FileVersion;

                    if (originalVersion == newVersion)
                    {
                        VersionGood(stationName + ":  " + path);
                        wasChacked = true;
                    }
                    else
                    {
                        VersionBad(stationName + ":  " + path, newVersion);

                        filesToChange.Add(fileName);
                        pathToFiles.Add(path);

                        wasChacked = true;
                    }
                    if (!wasChacked)
                    {
                        VersionBad(stationName + ":  " + path, "No files were found");
                    }
                }

            }

            if (dirs.Length >= 1)
            {
                foreach (string directory in dirs)
                {
                    checkFiles(stationName, directory, fileName);
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

                        //Connencting to computer with credentials
                        for (int i = 0; i < folders.Count; i++)
                        {
                            try
                            {

                                var command = "NET USE " + @"\\" + IP + @"\c" + " /user:" + username + " " + password;
                                if (Utils.ExecuteCommand(command, 5000) != 0)
                                {

                                    command = "NET USE " + @"\\" + IP + @"\c" + " /user:" + "user" + " " + password;
                                    Utils.ExecuteCommand(command, 5000);
                                }


                                string path = @"\\" + IP + @"\" + folders[i];

                                checkFiles(stationName, path, fileName);

                            }
                            catch (Exception e)
                            {
                                if (e is DirectoryNotFoundException)
                                {
                                    continue;
                                }

                            }


                        }

                    }
                    catch (Exception e)
                    {
                        if (e is FileNotFoundException)
                        {
                            VersionBad(stationName, "Files Not Found");
                        }

                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        progressBar1.PerformStep();
                        progressBar1.Refresh();

                    });
                }
                else
                {
                    VersionBad(stationName, "No Connection To: " + IP);
                    this.Invoke((MethodInvoker)delegate
                    {
                        progressBar1.PerformStep();
                        progressBar1.Refresh();

                    });
                }

                
            }
            catch(Exception e)
            {
                if(e is NullReferenceException)
                {
                   // VersionBad(stationName, e.Message);
                }
                else
                {
                    VersionBad(stationName, "No Connection To: " + IP);
                    this.Invoke((MethodInvoker)delegate
                    {
                        progressBar1.PerformStep();
                        progressBar1.Refresh();

                    });
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

                stationsListView.Items.Add(li);
                stationsListView.Refresh();
            });
        }








    }
}
