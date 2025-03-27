using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace SomerenWebApp.Models
{
    public class Drink 
    {
        public string Name { get; set; }
        public bool Alcoholic { get; set; }
        public decimal Price { get; set; }

        public Drink()
        {
        }
        public Drink(string name, bool alcoholic, int price)
        {
            Name = name;
            Alcoholic = alcoholic;
            Price = price;
        }
    }
}
