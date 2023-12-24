using FlightBooking.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Controllers
{
    [Authorize(Roles = ("admin"))]
    public class AdminController : Controller
    {
        DatabaseContext _context;

        public AdminController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["userCount"] = _context.Users.Count().ToString();
            ViewData["aircraftCount"] = _context.Aircrafts.Count().ToString();
            ViewData["routeCount"] = _context.Routes.Count().ToString();
            ViewData["flightCount"] = _context.Flights.Count().ToString();
            ViewData["ticketCount"] = _context.Tickets.Count().ToString();
            return View();
        }
    }
}
