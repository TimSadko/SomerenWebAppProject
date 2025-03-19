using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;

namespace SomerenWebApp.Repositories
{
    public class DBRoomRepository : IRoomRepository
    {
        private readonly string? _connectionString;

        public DBRoomRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MessengerDatabase");
        }

        public List<Room> GetAll()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT room_number, building FROM Rooms ORDER BY room_number";
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

            return new Room(room_number, building);
        }

        public void Add(Room room)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Rooms (room_number,building) VALUES ( @room_number, @building)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@room_number", room.RoomNumber);
                    cmd.Parameters.AddWithValue("@building", room.Building);

                    conn.Open();
                    cmd.ExecuteNonQuery();                  
                }
            }
        }

        public Room? GetByNum(int room_number)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT room_number, building FROM Rooms WHERE room_number = @RoomNumber";
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
                string query = "UPDATE Rooms SET building = @building WHERE room_number = @room_number";
                SqlCommand command = new SqlCommand(query, conn);

                command.Parameters.AddWithValue("@room_number", room.RoomNumber);
                command.Parameters.AddWithValue("@building", room.Building);

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
    }
}
