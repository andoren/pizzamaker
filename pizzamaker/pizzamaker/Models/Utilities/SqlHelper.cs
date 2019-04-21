using MySql.Data.MySqlClient;
using System.Windows;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using pizzamaker.Models.Singletons;

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
                var logger = LogHelper.getInstance();
                switch (e.Number)
                {
                    case 0:
                        logger.Log(Logging.LogType.FileLog, this.GetType().ToString(), "OpenConnection", e.Message);
                        break;

                    case 1045:
                        logger.Log(Logging.LogType.FileLog, this.GetType().ToString(), "OpenConnection", e.Message);
                        break;
                }
                return false;
            }
            catch (Exception ex)
            {
                var logger = LogHelper.getInstance();
                logger.Log(Logging.LogType.FileLog, this.GetType().ToString(), "OpenConnection", ex.Message);
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
                var logger = LogHelper.getInstance();
                logger.Log(Logging.LogType.FileLog, this.GetType().ToString(), "CloseConnection", e.Message);
                return false;
            }
            catch (Exception ex)
            {
                var logger = LogHelper.getInstance();
                logger.Log(Logging.LogType.FileLog, this.GetType().ToString(), "CloseConnection", ex.Message);
                return false;
            }
        }
    }
}