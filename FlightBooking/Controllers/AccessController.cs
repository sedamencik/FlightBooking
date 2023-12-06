using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FlightBooking.Controllers
{
    public class AccessController : Controller
    {
        private readonly DatabaseContext _context;

        public AccessController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.SingleOrDefault(x => x.Email == model.Email && x.Password == model.Password);
                if (user != null)
                {
                    //Cookie configurations
                    List<Claim> claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim("Name", user.Name.ToString()),
                        new Claim("Surname", user.Surname.ToString())
                    };
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Email or password is incorrect.");
                    return View(model);
                }
            }
            //Model is not valid.
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            //Model is valid.
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(x => x.Email.ToLower() == model.Email.ToLower()))
                {
                    ModelState.AddModelError(nameof(model.Email), "Email has already been taken.");
                    return View(model);
                }

                //Creating user with props.
                User user = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Surname = model.Surname,
                    Password = model.Password
                };
                _context.Users.Add(user);
                int affectedRow = _context.SaveChanges();

                //Row is affected. User created successfully.
                if (affectedRow > 0)
                {
                    return RedirectToAction(nameof(Login));
                }
                //Row is not affected. User couldn't created.
                else
                {
                    ModelState.AddModelError("", "User can not be registered.");
                }
            }
            //Model is not valid.
            return View();
        }

    }
}
