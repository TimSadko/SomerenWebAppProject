using Microsoft.Data.SqlClient;
using SomerenWebApp.Controllers;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBParticipantRepository : IParticipantsRepository
    {
        private readonly string _connection_string;

        public DBParticipantRepository(DefaultConfiguration config)
        {
            _connection_string = config.GetConnectionString();
        }

        public List<Participant> GetAll()
        {
            List<Participant> participant = new List<Participant>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, student_number, activity_id From Participants ORDER BY student_number";
                SqlCommand com = new SqlCommand(query, conn);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                Participant std;

                while (reader.Read())
                {
                    std = ReadParticipant(reader);
                    participant.Add(std);
                }
                reader.Close();
            }

            return participant;
        }

        private Participant ReadParticipant(SqlDataReader reader)
        {
            return new Participant((int)reader["id"], (int)reader["student_number"], (int)reader["activity_id"]);
        }

        public Participant? GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, student_number, activity_id From Participants WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);
                com.Parameters.AddWithValue("@id", id);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                if (!reader.HasRows)
                {
                    reader.Close(); return null;
                }
                else
                {
                    reader.Read();
                    Participant s = ReadParticipant(reader);

                    reader.Close();

                    return s;
                }
            }
        }

        public bool IsUniqueParticipant(Participant std)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id FROM Participants WHERE student_number = @student_number AND activity_id = @activity_id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@student_number", std.StudentId);
                com.Parameters.AddWithValue("@activity_id", std.ActivityId);

                com.Connection.Open();

                SqlDataReader reader = com.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Close(); return false;
                }
                else
                {
                    reader.Close(); return true;
                }
            }
        }

        public void Add(Participant part)
        {
            if (CommonController._student_rep.GetByNum(part.StudentId) == null)
            {
                throw new Exception($"Failed to add Participant: Student with id: {part.StudentId} was not found!");
            }

            if (CommonController._activity_rep.GetById(part.ActivityId) == null)
            {
                throw new Exception($"Failed to add Participant: Activity with id: {part.ActivityId} was not found!");
            }

            if (!IsUniqueParticipant(part))
            {
                throw new Exception($"Failed to add Participant: Participant with student_number: {part.StudentId} and activity_id: {part.ActivityId} already exists");
            }

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "INSERT INTO Participants (student_number, activity_id) VALUES (@student_number, @activity_id); SELECT SCOPE_IDENTITY();";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@student_number", part.StudentId);
                com.Parameters.AddWithValue("@activity_id", part.ActivityId);

                com.Connection.Open();
                part.Id = Convert.ToInt32(com.ExecuteScalar());
            }
        }

        public void Edit(Participant spv)
        {
            if (CommonController._student_rep.GetByNum(spv.StudentId) == null)
            {
                throw new Exception($"Failed to edit Participant: Student with id: {spv.StudentId} was not found!");
            }

            if (CommonController._activity_rep.GetById(spv.ActivityId) == null)
            {
                throw new Exception($"Failed to edit Participant: Activity with id: {spv.ActivityId} was not found!");
            }

            if (!IsUniqueParticipant(spv))
            {
                throw new Exception($"Failed to edit Participant: Participant with student_number: {spv.StudentId} and activity_id: {spv.ActivityId} already exists");
            }

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "UPDATE Participants SET student_number=@student_number, activity_id=@activity_id WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", spv.Id);
                com.Parameters.AddWithValue("@student_number", spv.StudentId);
                com.Parameters.AddWithValue("@activity_id", spv.ActivityId);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "DELETE FROM Participants WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", id);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }
    }
}
