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
				Console.WriteLine($"!!! {ex.Message}");
				return View(drink);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Console.WriteLine($"{id} drinks");

            var drink = _drinksRepository.GetDrinkById(id);

            Console.WriteLine($" name: {drink.Name}");
            Console.WriteLine($" alc: {drink.Alcoholic}");
            Console.WriteLine($" price: {drink.Price}");


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
				Console.WriteLine($"!!! {ex.Message}");
				return View(drink);
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Drink? drink = _drinksRepository.GetDrinkById(id);
            return View(drink);
        }

        [HttpPost]
        public IActionResult Delete(Drink drink)
        {
            try
            {
                _drinksRepository.Delete(drink.Id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(drink);
            }
        }
    }
}