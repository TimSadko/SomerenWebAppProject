using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
	public class DBRoomRepository : IRoomRepository
	{
		private readonly string? _connection_string;

		public DBRoomRepository(IConfiguration config)
		{
			_connection_string = config.GetConnectionString("MessengerDatabase");
		}

		public List<Room> GetAll()
		{
			throw new NotImplementedException();
		}

		private Room ReadRoom(SqlDataReader reader)
		{
			return new Room((int)reader["room_number"], (string)reader["building"]);
		}

		public Room? GetByNum(int room_number)
		{
			using (SqlConnection con = new SqlConnection(_connection_string))
			{
				string query = "SELECT room_number, building From Rooms WHERE room_number = @room_number";

				SqlCommand com = new SqlCommand(query, con);
				com.Parameters.AddWithValue("@room_number", room_number);

				com.Connection.Open();
				SqlDataReader reader = com.ExecuteReader();

				if (!reader.HasRows)
				{
					reader.Close(); return null;
				}
				else
				{
					reader.Read();
					Room r = ReadRoom(reader);

					reader.Close();

					return r;
				}
			}
		}

		public void Add(Room rm)
		{
			throw new NotImplementedException();
		}

		public void Edit(Room rm)
		{
			throw new NotImplementedException();
		}

		public void Delete(int room_number)
		{
			throw new NotImplementedException();
		}	
	}
}
