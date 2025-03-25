using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;

namespace SomerenWebApp.Controllers
{
    public class SupervisorsController : Controller
    {
        private ISupervisorReposiory _rep;

        public SupervisorsController(ISupervisorReposiory rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var super = _rep.GetAll();

            return View(super);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Supervisor spv)
        {
            try
            {
                _rep.Add(spv);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(spv);
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
        public IActionResult Edit(Supervisor spv)
        {
            try
            {
                _rep.Edit(spv);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(spv);
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
        public IActionResult Delete(Supervisor spv)
        {
            try
            {
                _rep.Delete(spv.Id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(spv);
            }
        }
    }
}
