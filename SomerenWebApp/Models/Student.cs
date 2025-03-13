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

        public Student() 
        { 
            _student_num = 0;
            _first_name = "";
			_last_name = "";
			_phone_num = "";
			_class = "";
			_voucher_count = 0;
            _room_num = 0;
		}

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

        public int StudentNum { get => _student_num; set => _student_num = value; }
        public string FirstName { get => _first_name; set => _first_name = value; }
        public string LastName { get => _last_name; set => _last_name = value; }
        public string PhoneNum { get => _phone_num; set => _phone_num = value; }
        public string Class { get => _class; set => _class = value; }
        public int VoucherCount { get => _voucher_count; set => _voucher_count = value; }
        public int RoomNum { get => _room_num; set => _room_num = value; }

        public string FullName { get => $"{_first_name} {_last_name}"; }

		public override string? ToString()
		{
            return $"_student_num: {_student_num}; _first_name: {_first_name}; _last_name: {_last_name}; _phone_num: {_phone_num}; _class: {_class}; _voucher_count: {_voucher_count}; _room_num: {_room_num}";
		}
	}
}
