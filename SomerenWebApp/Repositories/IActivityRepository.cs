using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
   
    public interface IActivityRepository
    {
        List<Activity> GetAll();
        Activity? GetById(int id);
        void Add(Activity act);
        void Edit(Activity act);
        void Delete(int id);
    }
}
