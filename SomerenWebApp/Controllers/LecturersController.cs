using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;

namespace SomerenWebApp.Controllers
{
    public class LecturersController : Controller
    {
        private ILecturerRepositorie _rep;

        public LecturersController(ILecturerRepositorie rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var lecturers = _rep.GetAll();

            return View(lecturers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Lecturer lec)
        {
            try
            {
                _rep.Add(lec);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(lec);
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
        public IActionResult Edit(Lecturer lec)
        {
            try
            {
                _rep.Edit(lec);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(lec);
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
        public IActionResult Delete(Lecturer lec)
        {
            try
            {
                _rep.Delete(lec.LecturerId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(lec);
            }
        }
    }
}
