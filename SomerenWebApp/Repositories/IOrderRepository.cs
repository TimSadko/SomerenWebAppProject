using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
	public interface IOrderRepository
	{
        List<Order> GetAllDrinks();

        void Add(Order order);
        public IActionResult Create(Order order);

        Order? GetOrderById(int id);

    }
}
