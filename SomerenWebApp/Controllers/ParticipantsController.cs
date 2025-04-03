using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;

namespace SomerenWebApp.Controllers
{
    public class ParticipantsController : Controller
    {
        private IParticipantsRepository _rep;


        public ParticipantsController(IParticipantsRepository rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var parti = _rep.GetAll();

            return View(parti);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Participant parti)
        {
            try
            {
                _rep.Add(parti);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(parti);
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
        public IActionResult Edit(Participant parti)
        {
            try
            {
                _rep.Edit(parti);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(parti);
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
        public IActionResult Delete(Participant parti)
        {
            try
            {
                _rep.Delete(parti.Id);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(parti);
            }
        }
        
    }
}
