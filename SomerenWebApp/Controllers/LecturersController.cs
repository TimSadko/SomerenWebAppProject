using Microsoft.AspNetCore.Mvc;
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
    }
}
