using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Data.SqlClient;
using SomerenWebApp.Controllers;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBStudentRepositorie : IStudentRepositorie
    {
        private readonly string? _connection_string;

        public DBStudentRepositorie(DefaultConfiguration config)
        {
            _connection_string = config.GetConnectionString();
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT student_number, first_name, last_name, telephone_num, class, voucher_count, room_number From Students ORDER BY last_name";
                SqlCommand com = new SqlCommand(query, conn);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                Student std;

                while (reader.Read())
                {
                    std = ReadUser(reader);
                    students.Add(std);
                }
                reader.Close();
            }

            return students;
        }
        public List<Student> GetStudentsNotStayingInRoom(int room_number)
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT student_number, first_name, last_name, telephone_num, class, voucher_count, room_number From Students WHERE NOT room_number = @room_number OR room_number IS NULL ORDER BY last_name";

                SqlCommand com = new SqlCommand(query, conn);
                com.Parameters.AddWithValue("@room_number", room_number);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                Student std;

                while (reader.Read())
                {
                    std = ReadUser(reader);
                    students.Add(std);
                }
                reader.Close();
            }

            return students;
        }


        public List<Student>? GetStudentsStayingInRoom(int room_number)
        {          
            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT student_number, first_name, last_name, telephone_num, class, voucher_count, room_number From Students WHERE room_number = @room_number ORDER BY last_name";
                SqlCommand com = new SqlCommand(query, conn);

				com.Parameters.AddWithValue("@room_number", room_number);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

				if (!reader.HasRows) 
				{
					reader.Close();
					return null;
				}

                List<Student> students = new List<Student>();
                Student std;

                while (reader.Read())
                {
                    std = ReadUser(reader);
                    students.Add(std);
                }
                reader.Close();

                return students;
            }           
        }

        private Student ReadUser(SqlDataReader reader)
        {
            return new Student()
            {
                StudentNum = (int)reader["student_number"],
                FirstName = (string)reader["first_name"],
                LastName = (string)reader["last_name"],
                PhoneNum = (string)reader["telephone_num"],
                Class = (string)reader["class"],
                VoucherCount = (int)reader["voucher_count"],
                RoomNum = reader["room_number"] == DBNull.Value ? null : (int)reader["room_number"]
			};
        }

        public Student? GetByNum(int student_number)
        {
			using (SqlConnection con = new SqlConnection(_connection_string))
			{
				string query = "SELECT student_number, first_name, last_name, telephone_num, class, voucher_count, room_number From Students WHERE student_number = @StudentNumber";

				SqlCommand com = new SqlCommand(query, con);
				com.Parameters.AddWithValue("@StudentNumber", student_number);

				com.Connection.Open();
				SqlDataReader reader = com.ExecuteReader();

				if (!reader.HasRows)
				{
					reader.Close(); return null;
				}
				else
				{
					reader.Read();
					Student s = ReadUser(reader);

					reader.Close();

					return s;
				}
			}
		}

        public void Add(Student std)
        {
            if(std.RoomNum == 0) std.RoomNum = null;
			else if (CommonController._room_rep.GetByNum((int)std.RoomNum) == null) {
				throw new Exception($"Failed to add Student: room with number: {std.RoomNum} was not found!");
			}
			//Console.WriteLine(std);

			if (GetByNum(std.StudentNum) == null) // Check if student alredy exsists in database
            {			
				using (SqlConnection con = new SqlConnection(_connection_string))
				{
					string query = "INSERT INTO Students (student_number, first_name, last_name, telephone_num, class, voucher_count, room_number) VALUES (@StudentNumber, @FirstName, @LastName, @TelephoneNum, @Class, @VoucherCount, @RoomNumber)";

					SqlCommand com = new SqlCommand(query, con);

					com.Parameters.AddWithValue("@StudentNumber", std.StudentNum);
					com.Parameters.AddWithValue("@FirstName", std.FirstName);
					com.Parameters.AddWithValue("@LastName", std.LastName);
					com.Parameters.AddWithValue("@TelephoneNum", std.PhoneNum);
					com.Parameters.AddWithValue("@Class", std.Class);
					com.Parameters.AddWithValue("@VoucherCount", std.VoucherCount);
					com.Parameters.AddWithValue("@RoomNumber", (object)std.RoomNum ?? DBNull.Value);

					com.Connection.Open();
					com.ExecuteNonQuery();
				}
			}
            else throw new Exception($"Student with student_number({std.StudentNum}) already exists; Failed to add student");         
        }

        public void Edit(Student std)
        {
			if (std.RoomNum == 0) std.RoomNum = null;
			else if (CommonController._room_rep.GetByNum((int)std.RoomNum) == null)
			{
				throw new Exception($"Failed to edit Student: room with number: {std.RoomNum} was not found!");
			}

			using (SqlConnection con = new SqlConnection(_connection_string))
			{
				string query = "UPDATE Students SET first_name=@first_name, last_name=@last_name, telephone_num=@telephone_num, class=@class, voucher_count=@voucher_count, room_number=@room_number WHERE student_number = @student_number";

				SqlCommand com = new SqlCommand(query, con);

				com.Parameters.AddWithValue("@student_number", std.StudentNum);
				com.Parameters.AddWithValue("@first_name", std.FirstName);
				com.Parameters.AddWithValue("@last_name", std.LastName);
				com.Parameters.AddWithValue("@telephone_num", std.PhoneNum);
				com.Parameters.AddWithValue("@class", std.Class);
				com.Parameters.AddWithValue("@voucher_count", std.VoucherCount);
				com.Parameters.AddWithValue("@room_number", (object)std.RoomNum ?? DBNull.Value);

				com.Connection.Open();
				int affect = com.ExecuteNonQuery();

				if (affect == 0) throw new Exception("No record found!");
			}
		}

		public void Delete(int student_number)
		{
			using (SqlConnection con = new SqlConnection(_connection_string))
			{
				string query = "DELETE FROM Students WHERE student_number = @student_number";

				SqlCommand com = new SqlCommand(query, con);

				com.Parameters.AddWithValue("@student_number", student_number);

				com.Connection.Open();
				int affect = com.ExecuteNonQuery();

				if (affect == 0) throw new Exception("No record found!");
			}
		}

        public void UpdateRoomNumber(AddGuestModel add_model)
        {
            Student? std = GetByNum(add_model.GuestId);

            if (std == null)
            {
                throw new Exception($"Failed to edit room, Student: room with number: {std.RoomNum} was not found!");
            }

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "UPDATE Students SET room_number=@room_number WHERE student_number = @student_number";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@student_number", std.StudentNum);
                com.Parameters.AddWithValue("@room_number", add_model.RoomNumber == 0 ? DBNull.Value : add_model.RoomNumber);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }

        public void ClearStudentsRoom(int room_number)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "UPDATE Students SET room_number=NULL WHERE room_number = @room_number";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@room_number", room_number);

                com.Connection.Open();

                com.ExecuteNonQuery();
            }
        }
    }
}
