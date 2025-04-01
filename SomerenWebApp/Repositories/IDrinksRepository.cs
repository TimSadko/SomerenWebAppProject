using System.Collections.Generic;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IDrinksRepository
    {
        List<Drink> GetAllDrinks();
        Drink? GetDrinkById(int id);
        void Add(Drink drink);
        void Edit(Drink drink);
        void Delete(int id);
    }
}