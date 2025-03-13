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
                Age = (int)reader["age"]
            };
        }

        public Lecturer? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(Lecturer lec)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(Lecturer lec)
        {
            throw new NotImplementedException();
        }
    }
}
