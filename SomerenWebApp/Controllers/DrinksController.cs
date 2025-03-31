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
        [Route("/Drinks/Edit/{Id}")]
        public IActionResult Edit(int id)
        {
            var drink = _drinksRepository.GetDrinkById( id);

            if (drink == null)
            {
                Console.WriteLine($"Error: No drink found with ID: {id}");
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
        [Route("/Drinks/Delete/{id:int}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Drink? drink = _drinksRepository.GetDrinkById((int)id);
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