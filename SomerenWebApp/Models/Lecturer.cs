﻿namespace SomerenWebApp.Models
{
    public class Lecturer
    {
        public int LecturerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int Age { get; set; }

        public Lecturer()
        {
        }

        public Lecturer(int LecturerId, string FirstName, string LastName, string PhoneNumber, int Age)
        {
            this.LecturerId = LecturerId;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.PhoneNumber = PhoneNumber;
            this.Age = Age;
        }
    }
}
