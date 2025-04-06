using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;

namespace SomerenWebApp.Repositories
{
    public interface IOrderRepository
    {
        List<Order> GetAllOrders();
        Order? GetOrderById(int id);
        void Add(Order order);
        void Edit(Order order);
        void Delete(int id);
    }
}

