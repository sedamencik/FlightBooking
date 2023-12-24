using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace FlightBooking.Controllers
{
    public class FlightController : Controller
    {
        private readonly DatabaseContext _context;

        public FlightController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = ("admin"))]
        public IActionResult Index()
        {
            List<FlightViewModel> flights = _context.Flights.Select(x => new FlightViewModel { Id = x.Id, Route = x.Route, Aircraft = x.Aircraft, FlightTime = x.FlightTime }).ToList();
            return View(flights);
            //return View();
        }
        [Authorize(Roles = ("admin"))]
        [HttpGet]
        public IActionResult Create()
        {
            LoadSubClasses();
            return View();
        }
        [Authorize(Roles = ("admin"))]
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
                    flight.AvailableSeatCount = flight.Aircraft.SeatCount;
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
        [Authorize(Roles = ("admin"))]
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
        [Authorize(Roles = ("admin"))]
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
        [Authorize(Roles = ("admin"))]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
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
        [Authorize(Roles = ("admin"))]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            Flight flight = _context.Flights.Find(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
                TempData["deleted"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }
        public void LoadSubClasses()
        {
            ViewBag.Routes = _context.Routes.Select(x => new RouteViewModel { Id = x.Id, StartPoint = x.StartPoint, EndPoint = x.EndPoint }).ToList();
            ViewBag.Aircrafts = _context.Aircrafts.Select(x => new AircraftViewModel { Id = x.Id, Name = x.Name, SeatCount = x.SeatCount }).ToList();
        }

        [HttpGet]
        public IActionResult Details(Guid id)
        {
            LoadSubClasses();
            Flight flight = _context.Flights.Include(f => f.Aircraft).Include(f => f.Route).Include(f => f.Tickets).FirstOrDefault(f => f.Id == id);
            if (flight != null)
            {
                var seatLayout = new List<SeatViewModel>();
                for (int i = 1; i <= flight.Aircraft.SeatCount; i++)
                {
                    seatLayout.Add(new SeatViewModel { SeatNumber = i, IsAvailable = true });
                }
                if (flight.Tickets != null)
                {
                    foreach (var ticket in flight.Tickets)
                    {
                        var seat = seatLayout.FirstOrDefault(s => s.SeatNumber == ticket.TicketNumber);

                        if (ticket != null)
                        {
                            seat.IsAvailable = false;
                        }
                    }
                }

                return View(new BookingViewModel { Flight = flight, SeatLayout = seatLayout });
            }
            TempData["error"] = "ok";
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult BookSeat(Guid flightId, int seatNumber, Guid seatId)
        {
            Flight flight = _context.Flights.Include(f => f.Aircraft).Include(f => f.Route).Include(f => f.Tickets).FirstOrDefault(f => f.Id == flightId);
            Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _context.Users.SingleOrDefault(x => x.Id == userId);

            if (flight == null || user == null)
            {
                return NotFound();
            }

            // İlgili koltuğu satın al
            if (flight.AvailableSeatCount > 0 && !flight.Tickets.Any(t => t.TicketNumber == seatNumber))
            {
                flight.Tickets.Add(new Ticket { TicketNumber = seatNumber, Flight = flight, User = user });
                flight.AvailableSeatCount--;

                _context.SaveChanges();
                TempData["bought"] = "ok";
            }

            return RedirectToAction("Index", "Home");
        }
    }
}