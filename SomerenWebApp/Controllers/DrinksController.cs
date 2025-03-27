using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;
using System.Collections.Generic;

namespace SomerenWebApp.Controllers
{
    public class DrinksController : Controller
    {
        private IDrinksRepository _drinksRepository;
        public DrinksController(IDrinksRepository drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public IActionResult Index()
        {
            var drinks = _drinksRepository.GetAllDrinks();
            return View(drinks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Drink drink)
        {
            try
            {
                _drinksRepository.Add(drink);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(drink);
            }
        }

        [HttpGet]
        [Route("/Drinks/Edit/{name}")]
        public IActionResult Edit(string name)
        {
            var drink = _drinksRepository.GetDrinkByName(name);

            if (drink == null)
            {
                Console.WriteLine($"Error: No drink found with ID: {name}");
                return NotFound("Drink not found.");
            }
            return View(drink);
        }

        [HttpPost]
        public IActionResult Edit(Drink drink)
        {
            try
            {
                _drinksRepository.Edit(drink);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(drink);
            }
        }

        [HttpGet]
        [Route("/Drinks/Delete/{name}")]
        public IActionResult Delete(string name)
        {
            //Console.WriteLine($"~~{name}~~");
            Drink drink = _drinksRepository.GetDrinkByName(name);        

            return View(drink);
        }

        [HttpPost]
        public IActionResult Delete(Drink drink)
        {
            try
            {
                _drinksRepository.Delete(drink);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(drink.Name);
            }
        }
    }
}