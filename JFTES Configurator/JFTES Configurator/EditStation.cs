using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFTES_Configurator
{
    
    public partial class EditStation : Form
    {
        private Station station;
        private MainForm main;
        private int id=-1;

        public EditStation(MainForm mainform)
        {
            main = mainform;
            InitializeComponent();
            label4.Hide();
        }
        public EditStation(Station editStationon, MainForm mainform, int listID)
        {
            InitializeComponent();
            station = editStationon;
            initfileds(editStationon);
            main = mainform;
            id = listID;
            label4.Hide();

        }

        public void initfileds(Station station)
        {
            nameTextBox.Text = station.getName();
            IPTextBox.Text = station.getIP();
            StationIDTextBox.Text = station.getID()+"";
            isPilotCheckBox.Checked = station.getIsPilot();
            hasSMECheckBox.Checked = station.getHasSME();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string isPilot=isPilotCheckBox.Checked?"V":"X";
            string hasSME = hasSMECheckBox.Checked ? "V" : "X";
            int stationID;
            try
            {
                stationID = Int32.Parse(StationIDTextBox.Text);
            }catch(Exception ex)
            {
                stationID = 0;
            }
            Station newStation = new Station(nameTextBox.Text, IPTextBox.Text, stationID, isPilotCheckBox.Checked, hasSMECheckBox.Checked);
            main.updateListWithItem(id,newStation);
            this.Close();
        }

        private void IPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)&&!(e.KeyChar == '.'))
            {
                e.Handled = true;
                Thread thread = new Thread(showError);
                try
                {
                    thread.Start();
                }
                catch { label4.Hide(); }
            }

        }
        public void showError()
        {
            this.Invoke((MethodInvoker)delegate {
                label4.Text = "IP and Station ID must be numbers only";
                label4.Show();
            });

            Thread.Sleep(1000);

            this.Invoke((MethodInvoker)delegate {
                label4.Hide();
            });
        }

        private void StationIDTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == '.'))
            {
                e.Handled = true;
                Thread thread = new Thread(showError);
                try
                {
                    thread.Start();
                }
                catch { label4.Hide(); }
            }
        }
    }
}
