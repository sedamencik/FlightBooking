using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FlightBooking.Controllers
{
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;

        public AccountController(DatabaseContext context)
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
                        new Claim(ClaimTypes.Name, user.FullName),
                        new Claim(ClaimTypes.Role,user.Role)
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
                    FullName = model.FullName,
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

        [Authorize]
        public IActionResult Profile()
        {
            LoadProfileData();
            return View();
        }

        private void LoadProfileData()
        {
            Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
            User user = _context.Users.SingleOrDefault(x => x.Id == userId);
            ViewData["FullName"] = user.FullName;
        }

        [Authorize]
        public IActionResult ProfileChangeFullName([Required][StringLength(50)] string fullname)
        {
            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _context.Users.SingleOrDefault(x => x.Id == userId);

                user.FullName = fullname;

                _context.SaveChanges();
                return RedirectToAction(nameof(Profile));
            }
            LoadProfileData();
            return View(nameof(Profile));
        }

        [Authorize]
        public IActionResult ProfileChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid userId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier));
                User user = _context.Users.SingleOrDefault(x => x.Id == userId);
                if (user != null)
                {
                    if (model.OldPassword != user.Password)
                    {
                        ModelState.AddModelError("", "Your old password is incorrect.");
                        return View(nameof(Profile));
                    }
                    else
                    {
                        user.Password = model.NewPassword;

                        _context.SaveChanges();
                        ViewData["result"] = "PasswordChanged";
                        return RedirectToAction(nameof(Profile));
                    }

                }
            }
            LoadProfileData();
            return View(nameof(Profile));
        }

        [Authorize]
        public IActionResult Logout()
        {
            //Sign out the user.
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

    }
}
