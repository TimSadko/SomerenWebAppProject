using Microsoft.AspNetCore.Mvc;
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
    }
}
