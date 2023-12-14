using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Controllers
{
    public class FlightController : Controller
    {
        private readonly DatabaseContext _context;

        public FlightController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<FlightViewModel> flights = _context.Flights.Select(x => new FlightViewModel { Id = x.Id, Route = x.Route, Aircraft = x.Aircraft, FlightTime = x.FlightTime }).ToList();
            return View(flights);
            //return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadSubClasses();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,RouteId,AircraftId,FlightTime")] CreateFlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                Aircraft aircraft = _context.Aircrafts.Find(model.AircraftId);
                Entities.Route route = _context.Routes.Find(model.RouteId);
                if (aircraft != null && route != null)
                {
                    Flight flight = new Flight() { Id = model.Id, Route = route, Aircraft = aircraft, FlightTime = model.FlightTime };
                    _context.Flights.Add(flight);
                    _context.SaveChanges();
                    TempData["created"] = "ok";
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            LoadSubClasses();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            LoadSubClasses();
            Flight flight = await _context.Flights.Include(f => f.Aircraft).Include(f => f.Route).FirstOrDefaultAsync(f => f.Id == id);
            if (flight != null)
            {

                CreateFlightViewModel model = new CreateFlightViewModel
                {
                    Id = flight.Id,
                    AircraftId = flight.Aircraft.Id,
                    RouteId = flight.Route.Id,
                    FlightTime = flight.FlightTime
                };
                return View(model);
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateFlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                Flight flight = await _context.Flights.Include(f => f.Aircraft).Include(f => f.Route).FirstOrDefaultAsync(f => f.Id == model.Id);
                if (flight != null)
                {
                    Aircraft aircraft = _context.Aircrafts.Find(model.AircraftId);
                    Entities.Route route = _context.Routes.Find(model.RouteId);
                    flight.Aircraft = aircraft;
                    flight.Route = route;
                    flight.FlightTime = model.FlightTime;
                    await _context.SaveChangesAsync();
                    TempData["edited"] = "ok";
                    return RedirectToAction(nameof(Index));
                }
            }
            LoadSubClasses();
            return View(model);
        }
        //[HttpGet]
        //public IActionResult Delete(Guid id)
        //{
        //    Aircraft aircraft = _context.Aircrafts.Find(id);

        //    if (aircraft != null)
        //    {
        //        AircraftViewModel model = new AircraftViewModel
        //        {
        //            Id = aircraft.Id,
        //            Name = aircraft.Name,
        //            SeatCount = aircraft.SeatCount
        //        };
        //        return View(model);
        //    }
        //    TempData["error"] = "ok";
        //    return RedirectToAction(nameof(Index));
        //}
        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirm(Guid id)
        //{
        //    Aircraft aircraft = _context.Aircrafts.Find(id);
        //    if (aircraft != null)
        //    {
        //        _context.Aircrafts.Remove(aircraft);
        //        _context.SaveChanges();
        //        TempData["deleted"] = "ok";
        //        return RedirectToAction(nameof(Index));
        //    }
        //    TempData["error"] = "ok";
        //    return RedirectToAction(nameof(Index));
        //}
        //}
        public void LoadSubClasses()
        {
            ViewBag.Routes = _context.Routes.Select(x => new RouteViewModel { Id = x.Id, StartPoint = x.StartPoint, EndPoint = x.EndPoint }).ToList();
            ViewBag.Aircrafts = _context.Aircrafts.Select(x => new AircraftViewModel { Id = x.Id, Name = x.Name, SeatCount = x.SeatCount }).ToList();
        }
    }
}