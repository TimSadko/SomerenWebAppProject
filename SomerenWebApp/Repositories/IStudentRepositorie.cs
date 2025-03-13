using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IStudentRepositorie
    {
        List<Student> GetAll();
        Student? GetById(int id);
        void Add(Student std);
        void Edit(Student std);
        void Delete(int id);
    }
}
