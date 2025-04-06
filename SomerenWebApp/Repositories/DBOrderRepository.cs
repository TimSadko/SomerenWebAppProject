using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
	public class DBOrderRepository : IOrderRepository
	{
		private readonly string _connectionString;

		public DBOrderRepository(DefaultConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString();
		}

		public List<Order> GetAllDrinks()
		{
			List<Order> order = new List<Order>();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT id,drink_id, student_number, price FROM Orders ORDER BY drink_id";
				SqlCommand command = new SqlCommand(query, connection);

				connection.Open();

				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					order.Add(ReadOrder(reader));
				}
				reader.Close();
			}
			return order;
		}

		private Order ReadOrder(SqlDataReader reader)
		{
			return new Order()
			{
				Id = (int)reader["id"],
				DrinkID = (int)reader["drink_id"],
				StudentNumber = (int)reader["student_number"],
				Price = (int)reader["price"],

			};
		}


		public Order? GetOrderById(int id)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "SELECT id, drink_id, student_number, price FROM Orders WHERE id = @Id";

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
					Order o = ReadOrder(reader);

					reader.Close();

					return o;
				}
			}
		}

		public void Add(Order order)
		{
			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "INSERT INTO Orders (id, drink_id, student_number, price) VALUES (@Id,@DrinkID, @StudentNumber, @Price);";

				SqlCommand command = new SqlCommand(query, connection);

				command.Parameters.AddWithValue("@Id", order.Id);
				command.Parameters.AddWithValue("@drink_id", order.DrinkID);
                command.Parameters.AddWithValue("@student_number", order.StudentNumber);
                command.Parameters.AddWithValue("@Price", order.Price);

				connection.Open();

				order.Id = Convert.ToInt32(command.ExecuteScalar());
			}
		}

		public IActionResult Create(Order order)
		{
			throw new NotImplementedException();
		}
	}
}
