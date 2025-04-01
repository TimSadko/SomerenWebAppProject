using System.Security.Cryptography;

namespace SomerenWebApp.Models
{
    public class Supervisor
    {
        private int _id;
        private int _lecturer_id;
        private int _activity_id;

        public Supervisor() { }

        public Supervisor(int id, int lecturer_id, int activity_id)
        {
			_id = id;
			_lecturer_id = lecturer_id;
            _activity_id = activity_id;
        }

        public int Id { get => _id; set => _id = value; }
        public int LecturerId { get => _lecturer_id; set => _lecturer_id = value; }
        public int ActivityId { get => _activity_id; set => _activity_id = value; }
    }
}
