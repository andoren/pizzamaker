using Caliburn.Micro;
using MySql.Data.MySqlClient;
using pizzamaker.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pizzamaker.Models.Utilities
{
    public class DatabaseHelper:SqlHelper
    {
        private DatabaseHelper()
        {

        }
        private static DatabaseHelper databaseHelper;
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
        public BindableCollection<Food> GetFoodsByType(string t) {
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
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseConnection(connection);
            }
            return doughs;
        }

        public Byte[] GetRawPicture(int id)
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
                MessageBox.Show(e.Message);
            }
            finally
            {
                CloseConnection(connection);
            }
            return picture;
        }

    }
}
