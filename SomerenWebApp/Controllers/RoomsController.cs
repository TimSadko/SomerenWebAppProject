﻿using Microsoft.AspNetCore.Mvc;
using SomerenWebApp.Models;
using SomerenWebApp.Repositories;

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

    }
}
