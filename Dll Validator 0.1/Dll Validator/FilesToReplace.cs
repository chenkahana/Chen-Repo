using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace Dll_Validator
{
    public partial class FilesToReplace : Form
    {
        private CancellationTokenSource cancellationToken = new CancellationTokenSource();
        List<string> filesAndPaths = new List<string>();
        List<string> filesToChange = new List<string>();
        FileInfo originalFile;
        int numOfFilesChanged = 0;
        int numOfFilesToChange = 0;
        public FilesToReplace()
        {
            InitializeComponent();
            label2.Text = "";

        }
        public void initForm(List<string> filesToChange, List<string> pathToFiles, FileInfo originalFile)
        {
            checkedFilesBox.Items.Clear();
            for (int i = 0; i < filesToChange.Count; i++)
            {
                string fileWithPath = pathToFiles[i] + @"\" + filesToChange[i];

                checkedFilesBox.Items.Add(fileWithPath);
                filesAndPaths.Add(fileWithPath);
            }

            this.originalFile = originalFile;
        }



        private void changeButton_Click(object sender, EventArgs e)
        {
            CheckedItemCollection col = checkedFilesBox.CheckedItems;
            numOfFilesToChange = col.Count;

            progressBar1.Value = 0;
            progressBar1.Step = 1;
            progressBar1.Maximum = numOfFilesToChange;

            toggleRunning(false);

            Task.Factory.StartNew(() => changeFiles(col));
            Task.Factory.StartNew(() => Loading(), cancellationToken.Token);
            checkedFilesBox.Refresh();
        }
        private void toggleRunning(bool condition)
        {
            this.Invoke((MethodInvoker)delegate
            {
                changeButton.Enabled = condition;
                closeButton.Enabled = condition;
            });
        }
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        List<string> filesThatWereChanged = new List<string>();
        public void changeFiles(CheckedItemCollection col)
        {
            try
            {
                foreach (string fileFromList in col)
                {
                    for (int i = 0; i < filesAndPaths.Count; i++)
                    {
                        if (fileFromList.Equals(filesAndPaths[i]))
                        {
                            numOfFilesChanged++;
                            System.IO.File.Copy(originalFile.FullName, filesAndPaths[i], true);
                            filesThatWereChanged.Add(fileFromList);
                        }
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        progressBar1.PerformStep();
                    });
                }
                cancellationToken.Cancel();

                MessageBox.Show($"{numOfFilesChanged} were changed");
                this.Invoke((MethodInvoker)delegate
                {
                    foreach(string fileFromList in filesThatWereChanged)
                    {
                        checkedFilesBox.Items.Remove(fileFromList);
                    }
                    label2.Text = "Done";
                    toggleRunning(true);
                    checkedFilesBox.Refresh();
                });
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                toggleRunning(true);
            }
            
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
    }
}
