using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;
using System.Collections.Generic;

namespace SomerenWebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var orders = _orderRepository.GetAllOrders();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Add()
        {   
            return View();
        }

        [HttpPost]
        public IActionResult Add(Order order)
        {
            try
            {
                _orderRepository.Add(order);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(order);
            }
        }
    }
}
