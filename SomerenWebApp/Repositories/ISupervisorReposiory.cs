using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface ISupervisorReposiory
    {
        List<Supervisor> GetAll();
        Supervisor? GetById(int id);
        bool IsUniqueSupervisor(Supervisor spv);
        void Add(Supervisor spv);
        void Edit(Supervisor spv);
        void Delete(int id);
    }
}
