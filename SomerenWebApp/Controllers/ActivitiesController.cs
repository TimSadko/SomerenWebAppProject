using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;

namespace SomerenWebApp.Controllers
{
    public class ActivitiesController : Controller
    {
        private IActivityRepository _rep;

        public ActivitiesController(IActivityRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Activity = _rep.GetAll();

            return View(Activity);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Activity act)
        {
            try
            {
                _rep.Add(act);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(act);
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_rep.GetById((int)id));
        }

        [HttpPost]
        public IActionResult Edit(Activity act)
        {
            try
            {
                _rep.Edit(act);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(act);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View(_rep.GetById((int)id));
        }

        [HttpPost]
        public IActionResult Delete(Activity act)
        {
            try
            {
                _rep.Delete(act.ActivityId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(act);
            }
        }

        
    }
}
