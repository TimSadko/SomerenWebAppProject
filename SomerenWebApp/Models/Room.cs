namespace SomerenWebApp.Models
{
    public class Room
    {
        public int RoomNumber { get; set; }
        public string Building { get; set; }

        public Room()
        {

        }
        public Room(int room_number, string building)
        {
            Building = building;
            RoomNumber = room_number;  
        }
    }
}
