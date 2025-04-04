using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBDrinksRepository : IDrinksRepository
    {
        private readonly string _connectionString;

        public DBDrinksRepository(DefaultConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public List<Drink> GetAllDrinks()
        {
            List<Drink> drinks = new List<Drink>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, name, alcoholic, price FROM Drinks ORDER BY name";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    drinks.Add(ReadDrink(reader));
                }
                reader.Close();
            }
            return drinks;
        }

        private Drink ReadDrink(SqlDataReader reader)
        {
            return new Drink()
            {
                Id = (int)reader["id"],
                Name = (string)reader["name"],
                Alcoholic = (bool)reader["alcoholic"],
                Price = (decimal)reader["price"]
            };
        }


        public Drink? GetDrinkById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT id, name, alcoholic, price FROM Drinks WHERE id = @Id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close(); return null;
                }
                else
                {
                    reader.Read();
                    Drink s = ReadDrink(reader);

                    reader.Close();

                    return s;
                }
            }
        }

        public void Add(Drink drink)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Drinks (name, alcoholic, price) VALUES (@Name, @Alcoholic, @Price); SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Name", drink.Name);
                command.Parameters.AddWithValue("@Alcoholic", drink.Alcoholic);
                command.Parameters.AddWithValue("@Price", drink.Price);

                connection.Open();

                drink.Id = Convert.ToInt32(command.ExecuteScalar());
            }
        }      

        public void Edit(Drink drink)
        {
            Console.WriteLine($"Alc: {drink.Alcoholic}");

            if (GetDrinkById(drink.Id) == null) throw new Exception($"Drink with id({drink.Id}) was not found; Failed to edit Drink");

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Drinks SET name = @name, alcoholic = @alcoholic, price = @price WHERE id = @id";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@id", drink.Id);
                command.Parameters.AddWithValue("@name", drink.Name);
                command.Parameters.AddWithValue("@alcoholic", drink.Alcoholic);
                command.Parameters.AddWithValue("@price", drink.Price);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
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
        }
    }
}
