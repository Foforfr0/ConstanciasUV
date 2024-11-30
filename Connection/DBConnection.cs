using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Constancias.Connection
{
    public class DBConnection
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"]
            .ConnectionString;

        public static string getStringConnection()
        {
            return connectionString;    
        }
        public void openConnection()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    MessageBox.Show("Conexion exitosa");
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
