using Microsoft.Data.SqlClient;
using SomerenWebApp.Controllers;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public class DBSupervisorReposiory : ISupervisorReposiory
    {
        private readonly string? _connection_string;

        public DBSupervisorReposiory(DefaultConfiguration config)
        {
            _connection_string = config.GetConnectionString();
        }

        public List<Supervisor> GetAll()
        {
            List<Supervisor> supervisors = new List<Supervisor>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, lecturer_id, activity_id From Supervisors ORDER BY lecturer_id";
                SqlCommand com = new SqlCommand(query, conn);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                Supervisor spv;

                while (reader.Read())
                {
                    spv = ReadSupervisor(reader);
                    supervisors.Add(spv);
                }
                reader.Close();
            }

            return supervisors;
        }

        private Supervisor ReadSupervisor(SqlDataReader reader)
        {
            return new Supervisor((int)reader["id"], (int)reader["lecturer_id"], (int)reader["activity_id"]);
        }

        public Supervisor? GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id, lecturer_id, activity_id From Supervisors WHERE id = @id";

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
                    Supervisor s = ReadSupervisor(reader);

                    reader.Close();

                    return s;
                }
            }
        }

        public bool IsUniqueSupervisor(Supervisor spv)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id FROM Supervisors WHERE lecturer_id = @lecturer_id AND activity_id = @activity_id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@lecturer_id", spv.LecturerId);
                com.Parameters.AddWithValue("@activity_id", spv.ActivityId);

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

        public void Add(Supervisor spv)
        {
            if (CommonController._lecturer_rep.GetById(spv.LecturerId) == null)
            {
                throw new Exception($"Failed to add Supervisor: Lecturer with id: {spv.LecturerId} was not found!");
            }

            if (CommonController._activity_rep.GetById(spv.ActivityId) == null)
            {
                throw new Exception($"Failed to add Supervisor: Activity with id: {spv.ActivityId} was not found!");
            }

            if (!IsUniqueSupervisor(spv))
            {
                throw new Exception($"Failed to add Supervisor: Supervisor with lecturer_id: {spv.LecturerId} and activity_id: {spv.ActivityId} already exists");
            }

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "INSERT INTO Supervisors (lecturer_id, activity_id) VALUES (@lecturer_id, @activity_id); SELECT SCOPE_IDENTITY();";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@lecturer_id", spv.LecturerId);
                com.Parameters.AddWithValue("@activity_id", spv.ActivityId);

                com.Connection.Open();
                spv.Id = Convert.ToInt32(com.ExecuteScalar());
            }
        }       

        public void Edit(Supervisor spv)
        {
            if (CommonController._lecturer_rep.GetById(spv.LecturerId) == null)
            {
                throw new Exception($"Failed to edit Supervisor: Lecturer with id: {spv.LecturerId} was not found!");
            }

            if (CommonController._activity_rep.GetById(spv.ActivityId) == null)
            {
                throw new Exception($"Failed to edit Supervisor: Activity with id: {spv.ActivityId} was not found!");
            }

            if (!IsUniqueSupervisor(spv))
            {
                throw new Exception($"Failed to edit Supervisor: Supervisor with lecturer_id: {spv.LecturerId} and activity_id: {spv.ActivityId} already exists");
            }

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "UPDATE Supervisors SET lecturer_id=@lecturer_id, activity_id=@activity_id WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id",spv.Id);
                com.Parameters.AddWithValue("@lecturer_id",spv.LecturerId);
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
                string query = "DELETE FROM Supervisors WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", id);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }        
    }
}
