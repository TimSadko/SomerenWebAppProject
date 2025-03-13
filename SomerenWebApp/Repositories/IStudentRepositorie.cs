using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IStudentRepositorie
    {
        List<Student> GetAll();
        Student? GetByNum(int student_number);
        void Add(Student std);
        void Edit(Student std);
        void Delete(int student_number);
    }
}
