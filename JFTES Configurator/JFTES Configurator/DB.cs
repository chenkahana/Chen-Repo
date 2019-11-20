using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JFTES_Configurator
{
     public static class DB
    {
        public static string stationTable = "dbo.SiteStations";
        public static string dbIP = "";
        public static string dbUserName = "";
        public static string dbPassword = "";
        public static string dbName = "";
        public static string dbPort = "";
        public static string autoRefresh = "";
        public static string connectionString;

        public static string getConnectionString(string nameOfDB)
        {
            connectionString = $"Data Source={dbIP}; " +
                   $"Initial Catalog={nameOfDB};" +
                   $"User ID={dbUserName};" +
                   $"Password={dbPassword};";


            return connectionString;
        }

        public static ComboBox fillComboBox()
        {
            string conStr = getConnectionString("JFTAppSiteSetup");
            string sql = "SELECT * FROM dbo.SitesLayout";
            string update = "Defiend connection and statement, trying to connect...";
           
            using (SqlConnection connection = new SqlConnection(conStr))
            {
                ComboBox sites = new ComboBox();
                try
                {
                    SqlCommand oCmd = new SqlCommand(sql, connection);
                    connection.Open();
                    using (SqlDataReader oReader = oCmd.ExecuteReader())
                    {
                        
                        for (int i = 0; oReader.Read(); i++)
                        {
                            string name = oReader["Name"].ToString();
                            sites.Items.Add(name);
                        }

                        connection.Close();
                        
                        return sites;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }



    }
}
