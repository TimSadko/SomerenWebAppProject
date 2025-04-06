using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SomerenWebApp.Controllers;
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

		public List<Order> GetAllOrders()
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
				Price = (decimal)reader["price"],

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
			if (CommonController._student_rep.GetByNum(order.StudentNumber) == null) throw new Exception();

			var drink = CommonController._drink_rep.GetDrinkById(order.DrinkID);

            if (drink == null) throw new Exception();

			using (SqlConnection connection = new SqlConnection(_connectionString))
			{
				string query = "INSERT INTO Orders (drink_id, student_number, price) VALUES (@drink_id, @student_number, @price); SELECT SCOPE_IDENTITY();";

				SqlCommand command = new SqlCommand(query, connection);
				
				command.Parameters.AddWithValue("@drink_id", order.DrinkID);
                command.Parameters.AddWithValue("@student_number", order.StudentNumber);
                command.Parameters.AddWithValue("@price", drink.Alcoholic ? drink.Price * (decimal)1.21 : (decimal)1.09);

				connection.Open();

				order.Id = Convert.ToInt32(command.ExecuteScalar());
			}
		}

        public void Edit(Order order)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
