namespace SomerenWebApp.Models
{
    public class AddGuestModel
    {
        private int _room_number;
        private bool _single_room;
        private int _guest_id;

        public AddGuestModel() { }

        public AddGuestModel(int room_number, bool single_room, int guest_id)
        {
            _room_number = room_number;
            _single_room = single_room;
            _guest_id = guest_id;
        }

        public int RoomNumber { get => _room_number; set => _room_number = value; }
        public bool SingleRoom { get => _single_room; set => _single_room = value; }
        public int GuestId { get => _guest_id; set => _guest_id = value; }
    }
}
