using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBStudentRepositorie : IStudentRepositorie
    {
        private readonly string? _connection_string;

        public DBStudentRepositorie(IConfiguration config)
        {
            _connection_string = config.GetConnectionString("MessengerDatabase");
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT student_number, first_name, last_name, telephone_num, class, voucher_count, room_number From Students";
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

        private Student ReadUser(SqlDataReader reader)
        {
            return new Student((int)reader["student_number"], (string)reader["first_name"], (string)reader["last_name"], (string)reader["telephone_num"], (string)reader["class"], (int)reader["voucher_count"], (int)reader["room_number"]);
        }

        public Student? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Student std)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Student std)
        {
            throw new NotImplementedException();
        }     
    }
}
