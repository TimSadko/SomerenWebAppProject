using System.Collections.Generic;
using SomerenWebApp.Models;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IDrinksRepository
    {
        List<Drink> GetAllDrinks();
        void Add(Drink drink);
        void Edit(Drink drink);
        void Delete(Drink drink);
        Drink? GetDrinkByName(string name);
    }
}