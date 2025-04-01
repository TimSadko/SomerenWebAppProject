using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface ILecturerRepositorie
    {
        List<Lecturer> GetAll();
        Lecturer? GetLecturerStayingInRoom(int room_number);
        Lecturer? GetById(int id);
        void Add(Lecturer std);
        void Edit(Lecturer std);
        void Delete(int id);
        void UpdateRoomNumber(AddGuestModel add_model);
    }
}
