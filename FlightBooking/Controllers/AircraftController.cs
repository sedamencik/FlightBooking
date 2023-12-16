using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FlightBooking.Controllers
{
    [Authorize(Roles = ("admin"))]
    public class AircraftController : Controller
    {
        private readonly DatabaseContext _context;

        public AircraftController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<AircraftViewModel> aircrafts = _context.Aircrafts.Select(x => new AircraftViewModel { Id = x.Id, Name = x.Name, SeatCount = x.SeatCount }).ToList();
            return View(aircrafts);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AircraftViewModel model)
        {
            if (ModelState.IsValid)
            {
                Aircraft aircraft = new Aircraft() { Id = model.Id, Name = model.Name, SeatCount = model.SeatCount };
                _context.Aircrafts.Add(aircraft);
                await _context.SaveChangesAsync();
                TempData["created"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Aircraft aircraft = await _context.Aircrafts.FindAsync(id);
            if (aircraft != null)
            {
                AircraftViewModel model = new AircraftViewModel
                {
                    Id = aircraft.Id,
                    Name = aircraft.Name,
                    SeatCount = aircraft.SeatCount
                };
                return View(model);
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AircraftViewModel model)
        {
            if (ModelState.IsValid)
            {
                Aircraft aircraft = _context.Aircrafts.Find(model.Id);
                if (aircraft != null)
                {
                    aircraft.Name = model.Name;
                    aircraft.SeatCount = model.SeatCount;
                    await _context.SaveChangesAsync();
                    TempData["edited"] = "ok";
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            Aircraft aircraft = _context.Aircrafts.Find(id);

            if (aircraft != null)
            {
                AircraftViewModel model = new AircraftViewModel
                {
                    Id = aircraft.Id,
                    Name = aircraft.Name,
                    SeatCount = aircraft.SeatCount
                };
                return View(model);
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            Aircraft aircraft = _context.Aircrafts.Find(id);
            if (aircraft != null)
            {
                _context.Aircrafts.Remove(aircraft);
                _context.SaveChanges();
                TempData["deleted"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }
    }
}
