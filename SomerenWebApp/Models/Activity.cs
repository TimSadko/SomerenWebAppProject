namespace SomerenWebApp.Models
{
    public class Activity
    {
        private int _id;
        private string _name;
        private DateTime _date;

        public Activity()
        {
            _id = 0;
            _name = " ";
            _date = DateTime.Now;
        }

        public Activity(int id, string name, DateTime date)
        {
            _id = id;
            _name = name;
            _date = date;
        }

        public int ActivityId { get => _id; set => _id = value; }
        public string ActivityName { get => _name; set => _name = value; }
        public DateTime Date { get => _date; set => _date = value; }


        public override string? ToString()
        {
            return $"_id: {_id}; _name: {_name};  _date: {_date};";
        }
    }
    

}
