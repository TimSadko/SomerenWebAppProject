using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;

namespace SomerenWebApp.Controllers
{
    public class StudentsController : Controller
    {
        private IStudentRepositorie _rep;

        public StudentsController(IStudentRepositorie rep)
        {
            _rep = rep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = _rep.GetAll();

            return View(students);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Student std)
        {
            try
            {
                _rep.Add(std);

                return RedirectToAction("Index");
            }
			catch (Exception ex)
			{
				Console.WriteLine($"!!! {ex.Message}");
				return View(std);
			}
		}

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			return View(_rep.GetByNum((int)id));
		}

		[HttpPost]
		public IActionResult Edit(Student std)
		{
			try
			{
				_rep.Edit(std);

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"!!! {ex.Message}");
				return View(std);
			}
		}

		[HttpGet]
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			return View(_rep.GetByNum((int)id));
		}

		[HttpPost]
		public IActionResult Delete(Student std)
		{
			try
			{
				_rep.Delete(std.StudentNum);

				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"!!! {ex.Message}");
				return View(std);
			}
		}
	}
}
