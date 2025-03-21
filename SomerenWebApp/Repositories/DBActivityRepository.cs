using Microsoft.Data.SqlClient;
using SomerenWebApp.Models;
using System.Reflection.Metadata.Ecma335;

namespace SomerenWebApp.Repositories
{
    public class DBActivityRepository: IActivityRepository
    {
        private readonly string? _connection_string;

        public DBActivityRepository(IConfiguration config)
        {
            _connection_string = config.GetConnectionString("MessengerDatabase");
        }
        public List<Activity> GetAll()
        {
            List<Activity> Activities = new List<Activity>();

            using (SqlConnection conn = new SqlConnection(_connection_string))
            {
                string query = "SELECT id,[name], [date] FROM Activities ORDER BY [date]";
                SqlCommand com = new SqlCommand(query, conn);

                com.Connection.Open();
                SqlDataReader reader = com.ExecuteReader();

                Activity act;

                while (reader.Read())
                {
                    act = ReadUser(reader);
                    Activities.Add(act);
                }
                reader.Close();
            }

            return Activities;
        }

       

        private Activity ReadUser(SqlDataReader reader)
        {
            return new Activity((int)reader["id"], (string)reader["name"], (DateTime)reader["date"]);
        }

        public Activity? GetById(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "SELECT id,[name], [date] FROM Activities WHERE id = @id";

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
                    Activity act = ReadUser(reader);

                    reader.Close();

                    return act;
                }
            }
        }
        public void Add(Activity act)
        {
            
            if (GetById(act.ActivityId) == null) 
            {
                using (SqlConnection con = new SqlConnection(_connection_string))
                {
                    string query = "INSERT INTO Activities (name, date) VALUES (@name, @date); SELECT SCOPE_IDENTITY();";

                    SqlCommand com = new SqlCommand(query, con);

                    com.Parameters.AddWithValue("@name", act.ActivityName);
                    com.Parameters.AddWithValue("@date", act.Date);
                    

                    com.Connection.Open();
                    //com.ExecuteNonQuery();

                    act.ActivityId = Convert.ToInt32(com.ExecuteScalar());
                }
            }
            else throw new Exception($"Activity with id ({act.ActivityId}) already exists; Failed to add activity");
        }

        public void Edit(Activity act)
        {
            

            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "UPDATE Activities SET name=@name, date=@date WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", act.ActivityId);
                com.Parameters.AddWithValue("@name", act.ActivityName);
                com.Parameters.AddWithValue("@date", act.Date);
                

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(_connection_string))
            {
                string query = "DELETE FROM Activities WHERE id = @id";

                SqlCommand com = new SqlCommand(query, con);

                com.Parameters.AddWithValue("@id", id);

                com.Connection.Open();
                int affect = com.ExecuteNonQuery();

                if (affect == 0) throw new Exception("No record found!");
            }
        }

    }







    
}
