using MySql.Data.MySqlClient;
using System.Windows;
using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using pizzamaker.Models.Singletons;
using Caliburn.Micro;

namespace pizzamaker.Models.Utilities
{
    public abstract class SqlHelper
    {

   
        /// <summary>
        /// Gives back a valid mysqlconnection you can change the connection data in App.Conf
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Opens a valid connection throw exceptions but we logged that
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Close a valid connection throw exceptions but we logged that
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Gives back the type of foods what we add to input.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public abstract BindableCollection<Food> GetFoodsByType(string type);
        /// <summary>
        /// Gets back the specific food picture.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract Byte[] GetRawPicture(int id);
        /// <summary>
        /// Log into the database. First param: Which class caused the error. Second param: Which Command caused the error. Third param: The message that exception has created.
        /// </summary>
        /// <param name="what"></param>
        /// <param name="command"></param>
        /// <param name="message"></param>
        public abstract void AddLog(string what, string icommand, string message);
    }
}