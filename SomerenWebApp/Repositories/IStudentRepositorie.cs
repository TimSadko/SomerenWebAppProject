using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IStudentRepositorie
    {
        List<Student> GetAll();
        List<Student> GetStudentsNotStayingInRoom(int room_number);
        List<Student>? GetStudentsStayingInRoom(int room_number);
        Student? GetByNum(int student_number);
        void Add(Student std);
        void Edit(Student std);
        void Delete(int student_number);
        void UpdateRoomNumber(AddGuestModel add_model);
        void ClearStudentsRoom(int room_number);
    }
}
