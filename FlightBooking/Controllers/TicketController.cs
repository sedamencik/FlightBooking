using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FlightBooking.Controllers
{
    public class TicketController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketController(DatabaseContext context)
        {
            _context = context;
        }

        [Authorize(Roles = ("admin"))]
        public IActionResult Index()
        {
            List<TicketViewModel> tickets = _context.Tickets.Include(t => t.User).Include(t => t.Flight).Include(t => t.Flight.Route).Include(t => t.Flight.Aircraft).Select(x => new TicketViewModel { Flight = x.Flight, Id = x.Id, TicketNumber = x.TicketNumber, User = x.User }).ToList();
            return View(tickets);
        }
        [Authorize(Roles = ("admin"))]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            Ticket ticket = await _context.Tickets.Include(t => t.Flight).Include(t => t.User).FirstOrDefaultAsync(t => t.Id == id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                _context.SaveChanges();
                TempData["deleted"] = "ok";
                return RedirectToAction(nameof(Index));

            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }


        [Authorize]
        public IActionResult MyTickets()
        {
            Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            List<Ticket> tickets = _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Flight)
                .Include(t => t.Flight.Route)
                .Include(t => t.Flight.Aircraft)
                .Where(x => x.User.Id == userId).ToList();
            List<TicketViewModel> userTickets = tickets.Select(x => new TicketViewModel { Flight = x.Flight, Id = x.Id, TicketNumber = x.TicketNumber, User = x.User }).ToList();

            return View(userTickets);
        }
    }
}
