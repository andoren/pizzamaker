using Caliburn.Micro;
using MySql.Data.MySqlClient;
using pizzamaker.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using pizzamaker.Models.Utilities;

namespace pizzamaker.Models.Singletons
{
    public class DatabaseHelper:SqlHelper
    {
        private DatabaseHelper()
        {

        }
        private volatile static DatabaseHelper databaseHelper;
        public static DatabaseHelper getInstance() {
            if (databaseHelper == null) {
                lock (typeof(DatabaseHelper)) {
                    if (databaseHelper == null) {
                        databaseHelper = new DatabaseHelper();
                    }
                }
            }
            return databaseHelper;
        }
        public override BindableCollection<Food> GetFoodsByType(string t) {
            BindableCollection<Food> doughs = new BindableCollection<Food>();
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select id,name,description,price from foods where type = @t";

            MySqlParameter typeparameter = new MySqlParameter()
            {
                Value = t,
                ParameterName = "t",
                MySqlDbType = MySqlDbType.String


            };
            command.Parameters.Add(typeparameter);
           
            lock (this) {
                MySqlConnection connection = getConnection();


                try
            {
                FoodFactory foodFactory = new FoodFactory();
                command.Connection = connection;
                OpenConnection(connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string name = reader["name"].ToString();
                    string description = reader["description"].ToString();
                    double price = double.Parse(reader["price"].ToString());
                    Food food = foodFactory.GetFood(t);
                    food.Id = id;
                    food.Name = name;
                    food.Description = description;
                    food.Price = price;
                    doughs.Add(food);

                }
            }
                 
            catch (Exception e)
            {
                    var logger = LogHelper.getInstance();
                    logger.Log(Logging.LogType.DbLog, this.GetType().ToString(), "GetFoodsByType", e.Message);
                }
            finally
            {
                CloseConnection(connection);
            }
            }
            return doughs;
        }
        public override void AddLog(string what, string icommand , string message) {
            MySqlCommand command = new MySqlCommand();
            MySqlParameter whatparameter = new MySqlParameter()
            {
                Value = what,
                DbType = System.Data.DbType.String,
                ParameterName = "iwhat"

            };
            MySqlParameter commandparameter = new MySqlParameter()
            {
                Value = icommand,
                DbType = System.Data.DbType.String,
                ParameterName = "icommand"

            };
            MySqlParameter commandresult = new MySqlParameter()
            {
                Value = message,
                DbType = System.Data.DbType.String,
                ParameterName = "message"

            };
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "insert into logs(class,what,result)values(@iwhat,@icommand,@message)";
            command.Parameters.Add(whatparameter);
            command.Parameters.Add(commandparameter);
            command.Parameters.Add(commandresult);
            MySqlConnection connection = null;
            try
            {
                lock (this)
                {
                    connection = getConnection();
                    command.Connection = connection;
                    OpenConnection(connection);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                var logger = LogHelper.getInstance();
                logger.Log(Logging.LogType.FileLog, this.GetType().ToString(), "AddLog", e.Message);
            }
            finally {
                if(connection != null)CloseConnection(connection);
            }


        }
        public override Byte[] GetRawPicture(int id)
        {
            byte[] picture = null;
            
            
            MySqlCommand command = new MySqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.CommandText = "select rawpicture from foods where id = @id";
            MySqlParameter idparameter = new MySqlParameter()
            {
                Value = id,
                ParameterName = "id",
                MySqlDbType = MySqlDbType.Int32
                

            };
            command.Parameters.Add(idparameter);
            lock (this)
            {
                MySqlConnection connection = getConnection();
            try
            {

                command.Connection = connection;
                OpenConnection(connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    picture = (byte[])reader["rawpicture"];

                }
            }
            catch (Exception e)
            {
                    var logger = LogHelper.getInstance();
                    logger.Log(Logging.LogType.DbLog, this.GetType().ToString(), "GetRawPicture", e.Message);
                }
            finally
            {
                CloseConnection(connection);
            }
            }
            return picture;
        }

    }
}
