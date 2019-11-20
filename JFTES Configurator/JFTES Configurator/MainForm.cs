using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Collections.ObjectModel;
using static System.Windows.Forms.ListView;
using System.Xml;
using System.Configuration;

namespace JFTES_Configurator
{
    public partial class MainForm : Form
    {
        /**
         * stationListView.Items[].Text= ID 
         * stationListView.Items[].Subitem[1]= Name
         * stationListView.Items[].Subitem[2]= IP
         * stationListView.Items[].Subitem[3]= StationID
         * stationListView.Items[].Subitem[4]= isPilot
         * stationListView.Items[].Subitem[5]= hasSME
         **/
        public string stationTable = "dbo.SiteStations";
        public ListView backupList = new ListView();
        public static string dbIP="";
        public static string dbUserName="";
        public static string dbPassword="";
        public static string dbName = "";
        public static string dbPort = "";
        public static string autoRefresh = "";
        List<Station> stationToUpdate = new List<Station>();

  



        public XmlDocument doc = new XmlDocument();


        public static bool isFirstRun=true;
        SqlConnection cnn;
        string connectionString;
        SqlCommand command;
        string sql;
        SqlDataReader dataReader;


        public MainForm()
        {
            InitializeComponent();
            if (isFirstRun)
            {
                defaultConnectionString();
                isFirstRun = false;
            }
            InitializeInfoLabel();
            progressBar1.Hide();
            if (sitesComboBox.Items.Count == 0)
            {
                switchButtons(false);
                sitesComboBox.Enabled = false;
                Thread getSitesThread = new Thread(() => fillComboBox(sitesComboBox));
                getSitesThread.Start();
            }
            //initListView();
        }
        public void fillComboBox(ComboBox combo)
        {
            switchButtons(false);
            string conStr = getConnectionString("JFTAppSiteSetup");
            string sql = "SELECT * FROM dbo.SitesLayout";
            string update = "Defiend connection and statement, trying to connect...";
            this.Invoke((MethodInvoker)delegate {
                progressBar1.PerformStep();
                progressBarLabel.ForeColor = System.Drawing.Color.Black;
                progressBarLabel.Text = update;
            });
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                try
                {
                    SqlCommand oCmd = new SqlCommand(sql, connection);
                    connection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        update = "Oh I found some values";
                        this.Invoke((MethodInvoker)delegate
                        {
                            progressBar1.PerformStep();
                            progressBarLabel.Text = update;
                        });
                        for (int i = 0; oReader.Read(); i++)
                        {
                            string name = oReader["Name"].ToString();
                            this.Invoke((MethodInvoker)delegate
                            {
                                combo.Items.Add(name);
                            });
                        }

                        connection.Close();
                        update = "Done";
                        this.Invoke((MethodInvoker)delegate {
                            progressBarLabel.ForeColor = System.Drawing.Color.Green;
                            progressBarLabel.Text = update;
                            progressBar1.Value = 0;
                            sitesComboBox.Enabled = true;
                            switchButtons(true);
                        });
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    update = "Can't connect to database, verify the data and try again";
                    this.Invoke((MethodInvoker)delegate {
                        progressBarLabel.ForeColor = System.Drawing.Color.Red;
                        progressBarLabel.Text = update;
                        progressBar1.Value = 0;
                        switchButtons(true);
                    });
                }
            }
        }

        private void initListView()
        {
            for (int i = 0; i <5; i++)
            {
                string[] row = { i+"", "00", "00", "000", "V" ,"V"};
                var listViewItem = new ListViewItem(row);
                stationsListView.Items.Add(listViewItem);
            }
        }

        private void connectToDB(string sql)
        {
            string update;

            string conStr = getConnectionString("");//TODO Enter name of db
            update = "Defiend connection string";
            this.Invoke((MethodInvoker)delegate {
                progressBar1.PerformStep();
                progressBarLabel.Text = update; 
            });

            

            cnn = new SqlConnection(conStr);

            update = "Defiend connection and statement, trying to connect...";
            this.Invoke((MethodInvoker)delegate {
                progressBar1.PerformStep();
                progressBarLabel.Text = update; 
            });
            try
            {
                cnn.Open();

                update = "Connection opened!";
                this.Invoke((MethodInvoker)delegate {
                    progressBar1.PerformStep();
                    progressBarLabel.Text = update; 
                });

                command = new SqlCommand(sql, cnn);
                dataReader = command.ExecuteReader();

                update = "Executing statement, almost there...";
                this.Invoke((MethodInvoker)delegate {
                    progressBar1.PerformStep();
                    progressBarLabel.Text = update; 
                });

                while (dataReader.Read())
                {
                    //TODO all you need todo with database 
                    /* 
                     *take all the number of station ID 
                     * see what is trainee
                     * save the numbers for trainee 
                     * etc
                    
                     */
                }

                dataReader.Close();
                command.Dispose();
                cnn.Close();

                update = "Connection closed, work is done.";
                this.Invoke((MethodInvoker)delegate {
                    progressBar1.PerformStep();
                    progressBarLabel.Text = update;
                    switchButtons(true);
                });

            }
            catch(Exception ex)
            {
                update = "Can't connect to database, verify the data and try again";
                this.Invoke((MethodInvoker)delegate {
                    progressBarLabel.Text = update; 
                    progressBar1.Value = 0;
                    switchButtons(true);
                });
            }
        }

        private string[] stationToRow(int id, Station station)
        {
            string isPilot = station.getIsPilot() ? "V" : "X";
            string hasSME =station.getHasSME() ? "V" : "X";
            string[] row = {id+"", station.getName(), station.getIP(), station.getID() + "", isPilot, hasSME };
            return row;
        }
        private int maxNum(ListView list)
        {
            int max = 0;
            foreach(ListViewItem  item in stationsListView.Items)
            {
                int id;
                try
                {
                    id = Int32.Parse(item.Text);
                }
                catch
                {
                    return -1;
                }
                if (id >= max)
                {
                    max = id;
                }
            }
            return max;

        }

        public void updateListWithItem(int id, Station station)
        {
            if (id == -1)
            {
                id = maxNum(stationsListView) + 1;
                stationsListView.Items.Add(new ListViewItem(stationToRow(id, station), id));
            }
            else
            {
                for(int i=0; i < stationsListView.Items.Count; i++)
                {
                    try
                    {
                        int itemId = Int32.Parse(stationsListView.Items[i].Text);
                        if (itemId == id)
                        {
                            stationsListView.Items[i] = new ListViewItem(stationToRow(id, station));
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Youre Id enterd is not true");
                        return;
                    }
                    
                }
            }
        }

        public string getConnectionString(string nameOfDB)
        {
            connectionString = $"Data Source={dbIP}; " +
                   $"Initial Catalog={nameOfDB};" +
                   $"User ID={dbUserName};" +
                   $"Password={dbPassword};"; 


            return connectionString;
        }
        public void InitializeInfoLabel()
        {
            string info = $" IP: {dbIP}." +
                $"\n Port: {dbPort}." +
                $"\n\n User Name: {dbUserName}." +
                $"\n Password: {dbPassword}." +
                $"\n\n AutoRefresh Path: {autoRefresh}.";


            currentInfoLabel.Text = info;
        }

        public static void defaultConnectionString()
        {
            dbIP = @"SHMULIK-PC\JOBSS";
            dbPort = "1433";
            autoRefresh ="net.tcp://SIMSERVER:809/AutoRefreshService";
            dbUserName = "BagiraUser";
            dbPassword = "12345";

        }

        private void optionButton_Click(object sender, EventArgs e)
        {
            OptionForm optionForm = new OptionForm(this);
            optionForm.Show();
            Hide();
        }

        private void runButton_Click(object sender, EventArgs e)
        {

            updateXmlButton_Click(sender, e);
            updateDBButton_Click(sender, e);


        }
        public bool textToBool(string text)
        {
            bool result ;
            switch (text)
            {
                case "V":
                    result = true;
                    break;
                case "X":
                    result = false;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }

        private void editStationButton_Click(object sender, EventArgs e)
        {
            if (stationsListView.SelectedItems.Count > 0)
            {
                var item = stationsListView.SelectedItems[0];
                Station editStation = new Station();
                int id = Int32.Parse(item.Text);
                editStation.setName(item.SubItems[1].Text);
                editStation.setIP(item.SubItems[2].Text);
                editStation.setID(Int32.Parse(item.SubItems[3].Text));
                editStation.setIsPilot(textToBool(item.SubItems[4].Text));
                editStation.setHasSME(textToBool(item.SubItems[5].Text));
                EditStation edit = new EditStation(editStation, this, id);
                edit.Show();
            }
            else
            {
                MessageBox.Show("Select a station first");
            }
        }

        private void addToListViewButton_Click(object sender, EventArgs e)
        {
            EditStation edit = new EditStation(this);
            edit.Show();
        }



        private void addBackendstoStationList()
        {
            stationToUpdate.Add(new Station("CGFXBE","192.168.0.120",0,false,false));
            stationToUpdate.Add(new Station("SIMSERVER", "192.168.0.110", 0, false, false));
            stationToUpdate.Add(new Station("SimHost", "192.168.0.100", 0, false, false));
        }
        private void updateXmlButton_Click(object sender, EventArgs e)
        {
            try
            {
                stationToUpdate.Clear();
                addBackendstoStationList();

                progressBar1.Maximum = 5;
                progressBar1.Step = 1;
                progressBar1.Show();
                progressBarLabel.Text = "Working on it...";
                progressBarLabel.Show();
                foreach (ListViewItem item in stationsListView.Items)
                {
                    bool pilot = item.SubItems[4].Text.Equals("V");
                    bool sme = item.SubItems[5].Text.Equals("V");
                    int stationNum = Int32.Parse(item.SubItems[3].Text);
                    stationToUpdate.Add(new Station(item.SubItems[1].Text, item.SubItems[2].Text, stationNum, pilot, sme));
                }
                progressBar1.PerformStep();

                XmlEditor.UiConfigChange(stationToUpdate);
                progressBarLabel.Text = "UI Config- Done";

                progressBar1.PerformStep();


                /**
                 *Get XML from IP of Station 
                 * Change the values according to other values in the table (has sme/ is pilot)
                 * Save XML
                 **/
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            //TODO
            /*
             * upload all XML files
             * change them according to the table
             **/ 
          
        }



        private void updateDBButton_Click(object sender, EventArgs e)
        {

            backupList = stationsListView;
            progressBar1.Maximum = 5;
            progressBar1.Step = 1;
            progressBar1.Show();
            progressBarLabel.Text = "Working on it...";
            progressBarLabel.Show();
            string sql = "";//TODO write a query
            Thread connectionthread = new Thread(()=>connectToDB(sql));
            connectionthread.Start();
            switchButtons(false);


            //TODO
            /*
             * connect to DB
             * update values according to listview
             **/

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (stationsListView.SelectedItems.Count > 0)
            {
                var item = stationsListView.SelectedItems[0];
                item.Remove();
            }
            else
            {
                MessageBox.Show("Select a station first");
            }
        }

        private void fillListViewButton_Click(object sender, EventArgs e)
        {
            while (stationsListView.Items.Count > 0)
            {
                foreach(ListViewItem item in stationsListView.Items)
                {
                    item.Remove();
                }
            }
            int min=0;
            int max=0;
            if (sitesComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Select a site layout first.");
                return;
            }
            switch (sitesComboBox.SelectedIndex)
            {

                case 0:
                    min = 100;
                    max = 199;
                    break;
                case 1:
                    min = 200;
                    max = 299;
                    break;
                case 2:
                    min = 300;
                    max = 399;
                    break;
                case 3:
                    min = 400;
                    max = 499;
                    break;
                case 4:
                    min = 500;
                    max = 599;
                    break;
                case 5:
                    min = 600;
                    max = 699;
                    break;
                case 6:
                    min = 700;
                    max = 799;
                    break;
                case 7:
                    min = 800;
                    max = 899;
                    break;
                case 8:
                    min = 900;
                    max = 999;
                    break;
                default:
                    min = 0;
                    max = 99;
                    break;
            }
            progressBar1.Maximum = 5;
            progressBar1.Step = 1;
            progressBar1.Show();
            progressBarLabel.Text = "Working on it...";
            progressBarLabel.Show();
            string sql = $"SELECT * FROM {stationTable} WHERE StationNumber BETWEEN {min} AND {max}";//TODO write a query
            Thread thread = new Thread(() => getStationFromDB(sql));
            thread.Start();
            switchButtons(false);

        }

        private void getStationFromDB(string sql)
        {
            string update;

            string conStr=getConnectionString("JFTAppSiteSetup");

            update = "Defiend connection and statement, trying to connect...";
            this.Invoke((MethodInvoker)delegate {
                progressBar1.PerformStep();
                progressBarLabel.Text = update;
            });
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                try
                {
                    SqlCommand oCmd = new SqlCommand(sql, connection);
                    connection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        update = "Oh I found some values";
                        this.Invoke((MethodInvoker)delegate
                        {
                            progressBar1.PerformStep();
                            progressBarLabel.Text = update;
                        });
                        for (int i = 0; oReader.Read(); i++)
                        {
                            string name = oReader["StationName"].ToString();
                            string IP = "Fill";
                            string stationID = oReader["StationNumber"].ToString();
                            string[] row = { i + "", name, IP, stationID ,"",""};
                            this.Invoke((MethodInvoker)delegate
                            {
                                stationsListView.Items.Add(new ListViewItem(row));
                            });
                        }

                        connection.Close();
                        update = "Done";
                        this.Invoke((MethodInvoker)delegate {
                            progressBarLabel.Text = update;
                            progressBar1.Value = 0;
                            switchButtons(true);
                        });
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    update = "Can't connect to database, verify the data and try again";
                    this.Invoke((MethodInvoker)delegate {
                        progressBarLabel.Text = update;
                        progressBar1.Value = 0;
                        switchButtons(true);
                    });
                }
            }


        }
        private void switchButtons(bool condition)
        {
            fillListViewButton.Enabled = condition;
            updateXMLnDBButton.Enabled = condition;
            updateXmlButton.Enabled = condition;
            updateDBButton.Enabled = condition;
        }
    }
}
