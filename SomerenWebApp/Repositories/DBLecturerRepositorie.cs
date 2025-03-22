using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBLecturerRepositorie : ILecturerRepositorie
    {
        private readonly string? _connection_string;

        public DBLecturerRepositorie(IConfiguration config)
        {
            _connection_string = config.GetConnectionString("MessengerDatabase");
        }

        public List<Lecturer> GetAll()
        {
            List<Lecturer> lecturers = new List<Lecturer>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, first_name, last_name, telephone_num, age, room_number From Lecturers";
                SqlCommand com = new SqlCommand(query, conn);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                Lecturer lec;

                while (reader.Read())
                {
                    lec = ReadLecturer(reader);
                    lecturers.Add(lec);
                }
                reader.Close();
            }

            return lecturers;
        }

        private Lecturer ReadLecturer(SqlDataReader reader)
        {
            return new Lecturer()
            {
                LecturerId = (int)reader["id"],
                FirstName = (string)reader["first_name"],
                LastName = (string)reader["last_name"],
                PhoneNumber = (string)reader["telephone_num"],
                Age = (int)reader["age"],
                RoomNumber = (int)reader["room_number"]
            };
        }

        public Lecturer? GetById(int lecturerId)
        { 
          
        
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id," +
                    " first_name," +
                    " last_name, telephone_num, age, room_number From Lecturers WHERE id = @LecturerId";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@LecturerId", lecturerId);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close(); return null;
                }
                else
                {
                    reader.Read();
                    Lecturer l = ReadLecturer(reader);

                    reader.Close();

                    return l;
                }
            }
        }
        

        public void Add(Lecturer lec)
        {
            if (GetRoomByNum(lec.RoomNumber) == null)
            {
                throw new Exception($"Failed to add Lecturer: room with number: {lec.RoomNumber} was not found!");
            }
            if (GetById(lec.LecturerId) == null) // Check if lecturer alredy exsists in database
            {
                using (SqlConnection con = new SqlConnection(_connection_string))
                {
                    string query = "INSERT INTO Lecturer (first_name, first_name, last_name, telephone_num, room_number)" +
                    " VALUES (@LecturerId, @FirstName, @LastName, @TelephoneNum, @RoomNumber)";

                    SqlCommand com = new SqlCommand(query, con);

                    com.Parameters.AddWithValue("@LecturerId", lec.LecturerId);
                    com.Parameters.AddWithValue("@FirstName", lec.FirstName);
                    com.Parameters.AddWithValue("@LastName", lec.LastName);
                    com.Parameters.AddWithValue("@TelephoneNum", lec.PhoneNumber);
                    com.Parameters.AddWithValue("@RoomNumber", lec.RoomNumber);

                    com.Connection.Open();
                    com.ExecuteNonQuery();
                }
            }
            else throw new Exception($"Student with student_number({lec.LecturerId}) already exists; Failed to add student");

        }



        public void Delete(int lectureeId)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "DELETE FROM Lecturers WHERE id = @student_number";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", lectureeId);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }
            
        

        public void Edit(Lecturer lec)
        {

            if (GetRoomByNum(lec.RoomNumber) == null)
            {
                throw new Exception($"Failed to edit Lecturer: room with number: {lec.RoomNumber} was not found!");
            }

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "INSERT INTO Lecturer (first_name, first_name, last_name, telephone_num, room_number)" +
                                    " VALUES (@LecturerId, @FirstName, @LastName, @TelephoneNum, @RoomNumber)";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@LecturerId", lec.LecturerId);
                com.Parameters.AddWithValue("@FirstName", lec.FirstName);
                com.Parameters.AddWithValue("@LastName", lec.LastName);
                com.Parameters.AddWithValue("@TelephoneNum", lec.PhoneNumber);
                com.Parameters.AddWithValue("@RoomNumber", lec.RoomNumber);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }
        private Room? GetRoomByNum(int room_number)
        {
            Room ReadRoom(SqlDataReader reader)
            {
                return new Room((int)reader["room_number"], (string)reader["building"]);
            }

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
    }
}
