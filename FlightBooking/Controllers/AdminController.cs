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
            return View();
        }
    }
}
