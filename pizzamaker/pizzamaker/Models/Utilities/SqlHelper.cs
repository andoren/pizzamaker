using MySql.Data.MySqlClient;
using System.Windows;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace pizzamaker.Models.Utilities
{
    public abstract class SqlHelper
    {

   
        protected MySqlConnection getConnection()
        {
            string server = ConfigurationManager.AppSettings["DATABASEPATH"];
            string database = ConfigurationManager.AppSettings["DATABASENAME"];
            string uid = ConfigurationManager.AppSettings["DATABASEUSER"];
            string password = ConfigurationManager.AppSettings["DATABASEPASSWORD"];
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "CharSet = utf8;" + "UID=" + uid + ";" + "PASSWORD=" + password + "; Connect Timeout=5";

            return new MySqlConnection(connectionString);
        }

        protected bool OpenConnection(MySqlConnection connection)
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        MessageBox.Show("Nem tudok a mysql szerverhez kapcsolódni.");
                        MessageBox.Show(e.Message);
                        break;

                    case 1045:
                        MessageBox.Show("Hibás felhasználónév vagy jelszó");
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("OpenConnection Exception ág.");
                return false;
            }
        }
        protected bool CloseConnection(MySqlConnection connection)
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                MessageBox.Show("CloseConnection MysqlExceptioág");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("CloseConnection Exception ág");
                return false;
            }
        }
    }
}