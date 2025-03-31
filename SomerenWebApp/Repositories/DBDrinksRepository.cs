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
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Drinks (id, name, alcoholic, price) VALUES (@Id,@Name, @Alcoholic, @Price)";
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", drink.Id);
                    command.Parameters.AddWithValue("@Name", drink.Name);
                    command.Parameters.AddWithValue("@Alcoholic", drink.Alcoholic);
                    command.Parameters.AddWithValue("@Price", drink.Price);
                    command.ExecuteNonQuery();
                }
            }           
        }



        void IDrinksRepository.Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Drinks WHERE id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                int affect = command.ExecuteNonQuery();
                if (affect == 0) throw new Exception("No record found!");
            }
            throw new NotImplementedException();
        }

        public List<Drink> GetAllDrinks()
        {
            List<Drink> drinks = new List<Drink>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id ,name, alcoholic, price FROM Drinks";
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Drink drink = new Drink
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Alcoholic = reader.GetBoolean(2),
                                Price = reader.GetDecimal(3) 
                            };
                            drinks.Add(drink);
                        }
                        reader.Close();
                    }
                }
            }
            return drinks;
        }


        public Drink? GetDrinkById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, name,alcoholic,price FROM Drinks WHERE id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Drink
                        {
                            Id = reader.GetInt32(0), // Assuming 'id' is the first column
                            Name = reader.GetString(1) // Assuming 'name' is the second column
                        };
                    }
                }
            }
            return null; // Return null if no drink is found
        }

        public void Edit(Drink drink)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Rooms SET building = @building WHERE room_number = @room_number";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@id", drink.Id);
                command.Parameters.AddWithValue("@name", drink.Name);
                command.Parameters.AddWithValue("@alcoholic", drink.Alcoholic);
                command.Parameters.AddWithValue("@price", drink.Price);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
