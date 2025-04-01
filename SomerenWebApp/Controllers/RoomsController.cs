using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;
using System.Reflection;

namespace SomerenWebApp.Controllers
{
	public class RoomsController : Controller
	{

        private readonly IRoomRepository _roomsRepository;

        public RoomsController(IRoomRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rooms = _roomsRepository.GetAll();
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Room room)
        {
            try
            {
                _roomsRepository.Add(room);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(room);
            }
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                Room? room = _roomsRepository.GetByNum((int)id);
                return View(room);
            }
        }

        [HttpPost]
        public IActionResult Edit(Room room)
        {
            try
            {
                _roomsRepository.Edit(room);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(room);
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room? room = _roomsRepository.GetByNum((int)id);
            return View(room);
        }

        [HttpPost]
        public IActionResult Delete(Room room)
        {
            try
            {
                _roomsRepository.Delete(room.RoomNumber);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(room);
            }
        }

        [HttpGet]
        [Route("/Rooms/AddGuest/{room_number}")]
        public IActionResult AddGuest(int room_number)
        {
            Room r = _roomsRepository.GetByNum(room_number);

            return View(new AddGuestModel(room_number, r.SingleRoom, 0));
        }

        [HttpPost]
        public IActionResult AddGuest(AddGuestModel add_model)
        {
            try
            {
                _roomsRepository.AddGuest(add_model);
                return RedirectToAction("Edit", new { id = add_model.RoomNumber });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return View(add_model);
            }
        }

        [HttpGet]
        [Route("/Rooms/RemoveGuest/{room_number}/{guest_id}/{single_room}")]
        public IActionResult RemoveGuest(int room_number, int guest_id, bool single_room)
        {
            try
            {
                if (single_room)
                {
                    CommonController._lecturer_rep.UpdateRoomNumber(new AddGuestModel(0, single_room, guest_id));
                }
                else
                {
                    CommonController._student_rep.UpdateRoomNumber(new AddGuestModel(0, single_room, guest_id));
                }
                
                return RedirectToAction("Edit", new { id = room_number });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return RedirectToAction("Edit", new { id = room_number });
            }
        }

        [HttpGet]
        [Route("/Rooms/ClearRoom/{room_number}")]
        public IActionResult ClearRoom(int room_number)
        {
            try
            {
                CommonController._student_rep.ClearStudentsRoom(room_number);

                return RedirectToAction("Edit", new { id = room_number });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"!!! {ex.Message}");
                return RedirectToAction("Edit", new { id = room_number });
            }
        }
    }
}
