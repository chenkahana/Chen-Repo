using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desributer
{
    public partial class Destributer : Form
    {

        private FolderBrowserDialog folderDialog;
        private string source;
        private string destination;
        private bool isChecked;

        public Destributer()
        {
            InitializeComponent();
            isChecked = networkComputerCheckBox.Checked;
            ChangeDestination();
        }

        private void Distributer_Load(object sender, EventArgs e)
        {

        }

        private void sourceFolderButton_Click(object sender, EventArgs e)
        {
            folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                sourceFolderText.Text = folderDialog.SelectedPath;
                source = sourceFolderText.Text;
            }
        }

        private void destinationButton_Click(object sender, EventArgs e)
        {
            folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                localDestinationText.Text = folderDialog.SelectedPath;
            }
        }

        private void networkComputerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isChecked = networkComputerCheckBox.Checked;
            ChangeDestination();
        }
        private void ChangeDestination()
        {
            destinationButton.Enabled = !isChecked;
            localDestinationText.Enabled = !isChecked;
            distanceComputerDestination.Enabled = isChecked;
        }
        
        private void runButton_Click(object sender, EventArgs e)
        {
            destination = isChecked ? distanceComputerDestination.Text : localDestinationText.Text;

            if (!string.IsNullOrEmpty(source)&& !string.IsNullOrEmpty(destination))
            {
                DirectoryInfo dir = new DirectoryInfo(source);
                
                Directory.Move(source, destination+"\\" + dir.Name);
                MessageBox.Show($"The folder located in: {source}, was moved to: {destination}");
            }
            else
            {
                MessageBox.Show("No source and/ or destination");
            }

        }
    }
}
