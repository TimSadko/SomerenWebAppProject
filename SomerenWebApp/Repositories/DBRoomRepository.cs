using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;
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
                        Room room = ReadRoom(reader);
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
				string query = "SELECT room_number, building, single_room FROM Rooms WHERE single_room = 0 AND room_number NOT IN (SELECT room_number FROM Students WHERE room_number IS NOT NULL GROUP BY room_number HAVING COUNT(*) > 7) ORDER BY room_number";
				SqlCommand cmd = new SqlCommand(query, conn);

				conn.Open();

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Room room = ReadRoom(reader);
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
				string query = "SELECT room_number, building, single_room FROM Rooms WHERE single_room = 1 AND room_number NOT IN (SELECT room_number FROM Lecturers WHERE room_number IS NOT NULL)";
				SqlCommand cmd = new SqlCommand(query, conn);

				conn.Open();

				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Room room = ReadRoom(reader);
						rooms.Add(room);
					}
				}

			}

			return rooms;
		}

		private Room ReadRoom(SqlDataReader reader)
        {
            return new Room()
            {
                RoomNumber = (int)reader["room_number"],
                Building = (string)reader["building"],
                SingleRoom = (bool)reader["single_room"]
			};
        }

        public void Add(Room room)
        {
            if (room.RoomNumber == 0) throw new Exception("failed to add Room: 0 is not allowed as RoomNumber");

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
                        return ReadRoom(reader);
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
