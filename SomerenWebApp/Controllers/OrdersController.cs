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
            List<Order> orders = _orderRepository.GetAllDrinks();
            return View(orders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Drinks = CommonController._drink_rep.GetAllDrinks();
            ViewBag.Students = CommonController._student_rep.GetAll(); 

            // Debugging output to check if students are loaded
            System.Diagnostics.Debug.WriteLine($"Students count: {((List<Student>)ViewBag.Students)?.Count}");

            return View();
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderRepository.Add(order);
                return RedirectToAction("Index");
            }

            ViewBag.Drinks = CommonController._drink_rep.GetAllDrinks();
            ViewBag.Students = CommonController._student_rep.GetAll(); // Correct method call again

            return View(order);
        }
    }
}
