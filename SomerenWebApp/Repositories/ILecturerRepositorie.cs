using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface ILecturerRepositorie
    {
        List<Lecturer> GetAll();
        Lecturer? GetById(int id);
        void Add(Lecturer std);
        void Edit(Lecturer std);
        void Delete(int id);
    }
}
