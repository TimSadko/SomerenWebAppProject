namespace SomerenWebApp.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }
        public int? RoomNumber { get; set; }
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string TextRoomNumber { get => RoomNumber == null ? "*None*" : RoomNumber.ToString(); }

        public Lecturer()
        {
        }

        public Lecturer(int LecturerId, string FirstName, string LastName, string PhoneNumber, int Age, int RoomNumber)
        {
            this.LecturerId = LecturerId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Age = Age;
            this.RoomNumber = RoomNumber;
        }

		public override string? ToString()
		{
			return $"LecturerId: {LecturerId}; FirstName: {FirstName}; LastName: {LastName}; PhoneNumber: {PhoneNumber}; Age: {Age}; RoomNumber: {RoomNumber};";
		}
	}
}
