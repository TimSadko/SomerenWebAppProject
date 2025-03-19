using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;
using SomerenWebApp.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBRoomsRepository : IRoomRepository
    {
        private readonly string? _connectionString;

        public DBRoomsRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SommerenDB");
        }

        public void Add(Room rm)
        {
            throw new NotImplementedException();
        }

        public void Delete(int room_number)
        {
            throw new NotImplementedException();
        }

        public void Edit(Room rm)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAll()
        {
            throw new NotImplementedException();
        }

        public Room? GetByNum(int room_number)
        {
            throw new NotImplementedException();
        }

        //public List<Room> GetAll()
        //{
        //    List<Room> rooms = new List<Room>();

        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        string query = "SELECT room_id, room_type, room_number, room_size FROM Room ORDER BY room_number";
        //        SqlCommand cmd = new SqlCommand(query, conn);

        //        try
        //        {
        //            conn.Open();
        //            using (SqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    Room room = MapRoom(reader);
        //                    rooms.Add(room);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Something went wrong with the database.", ex);
        //        }
        //    }

        //    return rooms;
        //}

        //private Room MapRoom(SqlDataReader reader)
        //{
        //    int id = (int)reader["room_id"];
        //    string roomType = (string)reader["room_type"];
        //    string roomNumber = (string)reader["room_number"];
        //    string roomSize = (string)reader["room_size"];

        //    return new Room(id, roomType, roomNumber, roomSize);
        //}

        //public void Add(Room room)
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        string query = "INSERT INTO Room (room_type, room_number, room_size) VALUES (@RoomType, @RoomNumber, @RoomSize)";

        //        using (SqlCommand cmd = new SqlCommand(query, conn))
        //        {
        //            cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
        //            cmd.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
        //            cmd.Parameters.AddWithValue("@RoomSize", room.RoomSize);

        //            try
        //            {
        //                conn.Open();
        //                cmd.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Something went wrong.", ex);
        //            }
        //        }

        //    }
        //}

        //public Room? GetByNum(int roomId)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        string query = "SELECT room_id, room_type, room_number, room_size FROM Room WHERE room_id = @RoomId";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@RoomId", roomId);

        //        try
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    return MapRoom(reader);
        //                }
        //            }
        //        }
        //        catch (SqlException ex)
        //        {
        //            throw new Exception("Database error while fetching user by ID", ex);
        //        }
        //    }
        //    return null; // Return null if no room is found
        //}

        //public void Edit(Room room)
        //{
        //    using (SqlConnection conn = new SqlConnection(_connectionString))
        //    {
        //        string query = "UPDATE Room SET room_type = @RoomType, room_number = @RoomNumber, room_size = @RoomSize WHERE room_id = @Id";
        //        SqlCommand command = new SqlCommand(query, conn);

        //        command.Parameters.AddWithValue("@Id", room.RoomId);
        //        command.Parameters.AddWithValue("@RoomType", room.RoomType);
        //        command.Parameters.AddWithValue("@RoomNumber", room.RoomNumber);
        //        command.Parameters.AddWithValue("@RoomSize", room.RoomSize);

        //        try
        //        {
        //            conn.Open();
        //            command.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Opening database did not succeed.", ex);
        //        }

        //    }

        //}

        //public void Delete(int roomId)
        //{
        //    using (SqlConnection connection = new SqlConnection(_connectionString))
        //    {
        //        string query = "DELETE FROM Room WHERE room_id = @Id";
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.Parameters.AddWithValue("@Id", roomId);

        //        try
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Opening database did not succeed.", ex);
        //        }
        //    }
        //}
    }
}
