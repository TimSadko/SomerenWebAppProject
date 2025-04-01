using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace SomerenWebApp.Models
{
    public class Drink 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Alcoholic { get; set; }
        public decimal Price { get; set; }

        public Drink()
        {
        }
        public Drink(int id, string name, bool alcoholic, int price )
        {
            Name = name;
            Alcoholic = alcoholic;
            Price = price;
            Id = id;
        }
    }
}
