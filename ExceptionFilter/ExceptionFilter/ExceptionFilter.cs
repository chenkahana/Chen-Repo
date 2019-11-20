using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ExceptionFilter
{
    public partial class ExceptionFilter : Form

    {
        private FolderBrowserDialog folderDialog;
        private Dictionary<long, string> duplicates = new Dictionary<long, string>();

        public ExceptionFilter()
        {
            InitializeComponent();
        }

        private void folderDialogButton_Click(object sender, EventArgs e)
        {
            folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                folderDialogText.Text = folderDialog.SelectedPath;
            }
            progressBar1.Value = 0;
        }

        private void handleException(Exception e)
        {
            if (e is UnauthorizedAccessException)
            {
                MessageBox.Show("Access to selected folder is denied", "Unaothorized Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e is FileLoadException)
            {
                MessageBox.Show("Close the file and try again", "Trying To Delete an Open File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Somthing went wrong", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void deleteDup(string path)
        {
            var stopwatch = Stopwatch.StartNew();
            duplicates.Clear();
            progressBar1.Value = 0;
            progressBar1.Minimum = 0;
            progressBar1.Step = 1;
            int counterDeleted = 0;
            int counter = 0;

            //path defied bellow will be the path of the requested folder
            string sourcePath = path;
            if (string.IsNullOrEmpty(sourcePath))
            {
                MessageBox.Show("Folder Path wasn't defiend", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourcePath);
                FileInfo[] files = dir.GetFiles();
                progressBar1.Maximum = files.Length;

                for (int i = 0; i < files.Length; i++)
                {
                    counter++;
                    progressBar1.PerformStep();

                    string value = files[i].FullName;
                    long size = files[i].Length;

                    if(duplicates.TryGetValue(size,out value))              
                    {
                        File.Delete(files[i].FullName);
                        counterDeleted++;
                    }
                    else
                    {
                        duplicates.Add(size, files[i].FullName);
                    }
 
                }
            
                duplicates.Clear();
                
            }
            catch (Exception e)
            {
                handleException(e);
            }
            MessageBox.Show(counterDeleted + " files out of " + counter + " were deleted! in "+stopwatch.ElapsedMilliseconds+" milisecondes", "Great success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            stopwatch.Stop();

        }

        private void runButton_Click(object sender, EventArgs e)
        {
            deleteDup(folderDialogText.Text);
        }


    }
}
