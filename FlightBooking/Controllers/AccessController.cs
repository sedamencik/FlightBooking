using FlightBooking.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Controllers
{
    public class AccessController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Login Actions
            }
            //Can't logged in.
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Register Actions
            }
            //Can't registered.
            return View();
        }

    }
}
