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
        public IActionResult Confirm()
        {
            var orders = _orderRepository.GetAllOrder();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
          

            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                _orderRepository.Add(order);

                return RedirectToAction("Confirm");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(order);
            }
        }
    }
}
