namespace SomerenWebApp.Models
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string Building { get; set; }
        public bool SingleRoom { get; set; }
        public string SingleRoomText { get => SingleRoom ? "Single Room" : "Student Dormitory"; }

        public Room()
        {

        }
        public Room(int room_number, string building, bool single_room)
        {
            Building = building;
            RoomNumber = room_number;  
            SingleRoom = single_room;
        }
    }
}
