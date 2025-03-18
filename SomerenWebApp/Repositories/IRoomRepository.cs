using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetAll();
        Room? GetByNum(int room_number);
        void Add(Room rm);
        void Edit(Room rm);
        void Delete(int room_number);
    }
}
