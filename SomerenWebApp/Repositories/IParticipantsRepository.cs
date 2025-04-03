using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IParticipantsRepository
    {
        List<Participant> GetAll();
        Participant? GetById(int id);
        bool IsUniqueParticipant(Participant parti);
        void Add(Participant parti);
        void Edit(Participant parti);
        void Delete(int id);
       
    }
}
