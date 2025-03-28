using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using SomerenWebApp.Controllers;

namespace SomerenWebApp.Repositories
{
    public class DBRoomRepository : IRoomRepository
    {
        private readonly string? _connectionString;

        public DBRoomRepository(DefaultConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString();
        }

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT room_number, building, single_room FROM Rooms ORDER BY room_number";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Room room = MapRoom(reader);
                        rooms.Add(room);
                    }
                }

            }

            return rooms;
        }

		public List<Room> GetAllAvalibleForStudents()
		{
			List<Room> rooms = new List<Room>();

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string query = "SELECT room_number, building, single_room FROM Rooms WHERE room_number NOT IN (SELECT room_number FROM Lecturers) AND room_number NOT IN (SELECT room_number FROM Students GROUP BY room_number HAVING COUNT(*) > 7) ORDER BY room_number";
				SqlCommand cmd = new SqlCommand(query, conn);
				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Room room = MapRoom(reader);
						rooms.Add(room);
					}
				}

			}

			return rooms;
		}

		public List<Room> GetAllAvalibleForLecturers()
		{
			List<Room> rooms = new List<Room>();

			using (SqlConnection conn = new SqlConnection(_connectionString))
			{
				string query = "SELECT room_number, building, single_room FROM Rooms WHERE room_number NOT IN (SELECT room_number FROM Lecturers) AND room_number NOT IN (SELECT room_number FROM Students) ORDER BY room_number";
				SqlCommand cmd = new SqlCommand(query, conn);
				conn.Open();
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Room room = MapRoom(reader);
						rooms.Add(room);
					}
				}

			}

			return rooms;
		}

		private Room MapRoom(SqlDataReader reader)
        {
            int room_number = (int)reader["room_number"];
            string building = (string)reader["building"];
            bool single_room = (bool)reader["single_room"];

            return new Room(room_number, building, single_room);
        }

        public void Add(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Rooms (room_number, building, single_room) VALUES (@room_number, @building, @single_room)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@room_number", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@building", room.Building);
                    cmd.Parameters.AddWithValue("@single_room", room.SingleRoom);

                    conn.Open();
                    cmd.ExecuteNonQuery();                  
                }
            }
        }

        public Room? GetByNum(int room_number)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT room_number, building, single_room FROM Rooms WHERE room_number = @RoomNumber";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@RoomNumber", room_number);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return MapRoom(reader);
                    }
                }

            }
            return null; // Return null if no room is found
        }

        public void Edit(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Rooms SET building = @building, single_room = @single_room WHERE room_number = @room_number";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@room_number", room.RoomNumber);
                command.Parameters.AddWithValue("@building", room.Building);
                command.Parameters.AddWithValue("@single_room", room.SingleRoom);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int room_number)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Rooms WHERE room_number = @room_number";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@room_number", room_number);

                connection.Open();
                int affect = command.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }

        public void AddGuest(AddGuestModel add_model)
        {
            if (add_model.SingleRoom)
            {
                CommonController._lecturer_rep.UpdateRoomNumber(add_model);
            }
            else
            {
                CommonController._student_rep.UpdateRoomNumber(add_model);
            }
        }
    }
}
