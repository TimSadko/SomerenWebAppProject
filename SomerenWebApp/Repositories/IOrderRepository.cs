using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrder();
        Drink? GetOrderById(int id);
        void Add(Order order);
        void Edit(Order order);
        void Delete(int id);
    }
}

