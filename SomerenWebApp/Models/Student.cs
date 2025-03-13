namespace SomerenWebApp.Models
{
    public class Student
    {
        private int _student_num;
        private string _first_name;
        private string _last_name;
        private string _phone_num;
        private string _class;
        private int _voucher_count;
        private int _room_num;

        public Student() { }

        public Student(int student_num, string first_name, string last_name, string phone_num, string @class, int voucher_count, int room_num)
        {
            _student_num = student_num;
            _first_name = first_name;
            _last_name = last_name;
            _phone_num = phone_num;
            _class = @class;
            _voucher_count = voucher_count;
            _room_num = room_num;
        }

        public int StudentNum { get => _student_num; }
        public string FirstName { get => _first_name; }
        public string LastName { get => _last_name; }
        public string PhoneNum { get => _phone_num; }
        public string Class { get => _class; }
        public int VoucherCount { get => _voucher_count; }
        public int RoomNum { get => _room_num; }
    }
}
