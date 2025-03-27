using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBDrinksRepository : IDrinksRepository
    {
        private readonly string _connectionString;

        public DBDrinksRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MessengerDatabase");
        }

        public void Add(Drink drink)
        {
            if (drink != null)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "INSERT INTO Drinks (name, alcoholic, price) VALUES (@Name, @Alcoholic, @Price)";
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", drink.Name);
                        command.Parameters.AddWithValue("@Alcoholic", drink.Alcoholic);
                        command.Parameters.AddWithValue("@Price", drink.Price);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(Drink drink)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Drinks WHERE name = @Name";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", drink.Name); 
                    int affected = command.ExecuteNonQuery();
                    if (affected == 0)
                    {
                        throw new Exception("No drink found to delete.");
                    }
                }
            }
        }


        public List<Drink> GetAllDrinks()
        {
            List<Drink> drinks = new List<Drink>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT name, alcoholic, price FROM Drinks";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Drink drink = new Drink
                            {
                                Name = reader.GetString(0),
                                Alcoholic = reader.GetBoolean(1),
                                Price = reader.GetDecimal(2) 
                            };
                            drinks.Add(drink);
                        }
                        reader.Close();
                    }
                }
            }
            return drinks;
        }

        public Drink? GetDrinkByName(string name)
        {
            return GetAllDrinks().FirstOrDefault(d => d.Name == name);
        }

        public void Edit(Drink drink)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Drinks SET alcoholic = @Alcoholic, price = @Price WHERE name = @Name";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", drink.Name);
                    command.Parameters.AddWithValue("@Alcoholic", drink.Alcoholic);
                    command.Parameters.AddWithValue("@Price", drink.Price);
                    int affected = command.ExecuteNonQuery();
                    if (affected == 0)
                    {
                        throw new Exception("No drink found to update.");
                    }
                }
            }
        }
    }
}
