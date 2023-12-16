using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace FlightBooking.Controllers
{
    [Authorize(Roles = ("admin"))]
    public class RouteController : Controller
    {
        private readonly DatabaseContext _context;

        public RouteController(DatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<RouteViewModel> routes = _context.Routes.Select(x => new RouteViewModel { Id = x.Id, StartPoint = x.StartPoint, EndPoint = x.EndPoint }).ToList();
            return View(routes);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RouteViewModel model)
        {
            if (ModelState.IsValid)
            {
                Entities.Route route = new Entities.Route { Id = model.Id, StartPoint = model.StartPoint, EndPoint = model.EndPoint };
                _context.Routes.Add(route);
                await _context.SaveChangesAsync();
                TempData["created"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Entities.Route route = await _context.Routes.FindAsync(id);
            if (route != null)
            {
                RouteViewModel model = new RouteViewModel
                {
                    Id = route.Id,
                    StartPoint = route.StartPoint,
                    EndPoint = route.EndPoint
                };
                return View(model);
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RouteViewModel model)
        {
            if (ModelState.IsValid)
            {
                Entities.Route route = _context.Routes.Find(model.Id);
                if (route != null)
                {
                    route.StartPoint = model.StartPoint;
                    route.EndPoint = model.EndPoint;
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
            Entities.Route route = _context.Routes.Find(id);

            if (route != null)
            {
                RouteViewModel model = new RouteViewModel
                {
                    Id = route.Id,
                    StartPoint = route.StartPoint,
                    EndPoint = route.EndPoint
                };
                return View(model);
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(Guid id)
        {
            Entities.Route route = _context.Routes.Find(id);
            if (route != null)
            {
                _context.Routes.Remove(route);
                _context.SaveChanges();
                TempData["deleted"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }
    }
}
