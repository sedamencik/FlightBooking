using FlightBooking.Entities;
using FlightBooking.Models;
using FlightBooking.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Host;
using System.Diagnostics;
using System.Security.Claims;

namespace FlightBooking.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private LanguageService _localization;
        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context,LanguageService localization)
        {
            _logger = logger;
            _context = context;
            _localization = localization;
        }

        public IActionResult Index()
        {
            ViewBag.Welcome = _localization.Getkey("Welcome").Value;
            var currentCulture = Thread.CurrentThread.CurrentCulture.Name;

            ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
            List<FlightViewModel> flights = _context.Flights.Select(x => new FlightViewModel { Id = x.Id, Route = x.Route, Aircraft = x.Aircraft, FlightTime = x.FlightTime }).ToList();
            return View(flights);
        }
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.UtcNow.AddYears(1)
                });
            
            return Redirect("/");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}