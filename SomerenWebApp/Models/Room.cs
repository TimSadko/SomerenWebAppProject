namespace SomerenWebApp.Models
{
	public class Room
	{
		private int _room_number;
		private string _building;

		public Room()
		{
			_room_number = 0;
			_building = "";
		}

		public Room(int room_number, string building)
		{
			_room_number = room_number;
			_building = building;
		}

		public int RoomNumber { get => _room_number; set => _room_number = value; }
		public string Building { get => _building; set => _building = value; }
	}
}
