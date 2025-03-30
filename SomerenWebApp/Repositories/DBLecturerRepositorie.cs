using Microsoft.Data.SqlClient;
using SomerenWebApp.Controllers;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBLecturerRepositorie : ILecturerRepositorie
    {
        private readonly string? _connection_string;

        public DBLecturerRepositorie(DefaultConfiguration config)
        {
            _connection_string = config.GetConnectionString();
        }

        public List<Lecturer> GetAll()
        {
            List<Lecturer> lecturers = new List<Lecturer>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, first_name, last_name, telephone_num, age, room_number From Lecturers ORDER BY last_name";
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
                RoomNumber = reader["room_number"] == DBNull.Value ? null : (int)reader["room_number"]
            };
        }

        public Lecturer? GetById(int lecturerId)
        {               
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, first_name, last_name, telephone_num, age, room_number From Lecturers WHERE id = @LecturerId";

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

        public Lecturer? GetLecturerStayingInRoom(int room_number)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, first_name, last_name, telephone_num, age, room_number From Lecturers WHERE room_number = @room_number";

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
                    Lecturer l = ReadLecturer(reader);

                    reader.Close();

                    return l;
                }
            }
        }


        public void Add(Lecturer lec)
        {
			if (lec.RoomNumber == 0) lec.RoomNumber = null;
			else if (CommonController._room_rep.GetByNum((int)lec.RoomNumber) == null) throw new Exception($"Failed to add Lecturer: room with number: {lec.RoomNumber} was not found!");

			using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "INSERT INTO Lecturers (first_name, last_name, telephone_num, age, room_number) VALUES (@FirstName, @LastName, @PhoneNumber, @Age, @RoomNumber); SELECT SCOPE_IDENTITY();";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@FirstName", lec.FirstName);
                com.Parameters.AddWithValue("@LastName", lec.LastName);
                com.Parameters.AddWithValue("@PhoneNumber", lec.PhoneNumber);
                com.Parameters.AddWithValue("@Age", lec.Age);
                com.Parameters.AddWithValue("@RoomNumber", (object)lec.RoomNumber ?? DBNull.Value);

                com.Connection.Open();

				lec.LecturerId = Convert.ToInt32(com.ExecuteScalar());
			}         
        }

		public void Edit(Lecturer lec)
		{
			if (lec.RoomNumber == 0) lec.RoomNumber = null;
			else if (CommonController._room_rep.GetByNum((int)lec.RoomNumber) == null) throw new Exception($"Failed to edit Student: room with number: {lec.RoomNumber} was not found!");

			using (SqlConnection con = new SqlConnection(_connection_string))
			{
                string query = "UPDATE Lecturers SET first_name = @FirstName, last_name = @LastName, telephone_num = @PhoneNumber, age = @Age, room_number = @RoomNumber WHERE id = @LecturerId";

				SqlCommand com = new SqlCommand(query, con);

				com.Parameters.AddWithValue("@LecturerId", lec.LecturerId);
				com.Parameters.AddWithValue("@FirstName", lec.FirstName);
				com.Parameters.AddWithValue("@LastName", lec.LastName);
				com.Parameters.AddWithValue("@PhoneNumber", lec.PhoneNumber);
				com.Parameters.AddWithValue("@Age", lec.Age);
				com.Parameters.AddWithValue("@RoomNumber", (object)lec.RoomNumber ?? DBNull.Value);

				com.Connection.Open();
				int affect = com.ExecuteNonQuery();

				if (affect == 0) throw new Exception("No record found!");
			}
		}

		public void Delete(int lectureeId)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "DELETE FROM Lecturers WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", lectureeId);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }

        public void UpdateRoomNumber(AddGuestModel add_model)
        {
            Lecturer? lec = GetById(add_model.GuestId);

            if (lec == null)
            {
                throw new Exception($"Failed to edit room, Lecturer: room with number: {lec.RoomNumber} was not found!");
            }

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "UPDATE Lecturers SET room_number = @RoomNumber WHERE id = @LecturerId";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@LecturerId", lec.LecturerId);
                com.Parameters.AddWithValue("@RoomNumber", add_model.RoomNumber == 0 ? DBNull.Value : add_model.RoomNumber);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }
    }
}
