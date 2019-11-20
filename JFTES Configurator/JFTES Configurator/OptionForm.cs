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
    public partial class OptionForm : Form
    {
        private MainForm form;
        public OptionForm(MainForm mainForm)
        {
            InitializeComponent();
            InitializeTextBox();
            form = mainForm;
            label1.Hide();

        }
        private void InitializeTextBox()
        {
            IPTextBox.Text = MainForm.dbIP;
            DBUserNameTextBox.Text = MainForm.dbUserName;
            DBPasswordTextBox.Text = MainForm.dbPassword;
            DBNameTextBox.Text = MainForm.dbName;
            DBPortTextBox.Text = MainForm.dbPort;
            autoRefreshTextBox.Text = MainForm.autoRefresh;


        }
        private void doneButton_Click(object sender, EventArgs e)
        {
            MainForm.dbName = DBNameTextBox.Text;
            MainForm.dbPort = DBPortTextBox.Text;
            MainForm.dbIP = IPTextBox.Text;
            MainForm.dbUserName = DBUserNameTextBox.Text;
            MainForm.dbPassword =  DBPasswordTextBox.Text;
            MainForm.autoRefresh = autoRefreshTextBox.Text;

            form.InitializeInfoLabel();
            form.Show();
            Close();
        }

        private void IPTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == '.'))
            {
                e.Handled = true;
                Thread thread = new Thread(showError);
                try
                {
                    thread.Start();
                }
                catch { label1.Hide(); }
            }
        }

        private void DBPortTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && !(e.KeyChar == '.'))
            {
                e.Handled = true;
                Thread thread = new Thread(showError);
                try
                {
                    thread.Start();
                }
                catch { label1.Hide(); }
            }
        }
        public void showError()
        {
            this.Invoke((MethodInvoker)delegate {
                label1.Text = "IP and Port must be numbers only";
                label1.Show();
            });

            Thread.Sleep(1000);

            this.Invoke((MethodInvoker)delegate {
                label1.Hide();
            });
        }
    }
}
