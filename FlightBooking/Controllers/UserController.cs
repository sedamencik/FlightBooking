using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Text;

namespace FlightBooking.Controllers
{
    [Authorize(Roles = ("admin"))]
    public class UserController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:44359/api");
        private readonly HttpClient _client;

        public UserController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<User> users = new List<User>();
            List<UserViewModel> usersVM = new List<UserViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/user/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<User>>(data);
                usersVM = users.Select(x => new UserViewModel { Email = x.Email, FullName = x.FullName, Id = x.Id, Role = x.Role }).ToList();
            }
            return View(usersVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/user/Create", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["created"] = "ok";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            User user = new User();
            EditUserViewModel userVM = new EditUserViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/user/Get/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);
                userVM = new EditUserViewModel
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    Id = user.Id,
                    Role = user.Role
                };
            }
            return View(userVM);
        }

        [HttpPost]
        public IActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "/user/Edit", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["edited"] = "ok";
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            User user = new User();
            UserViewModel userVM = new UserViewModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"/user/Get/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<User>(data);
                if (user != null)
                {
                    userVM = new UserViewModel
                    {
                        Email = user.Email,
                        FullName = user.FullName,
                        Id = user.Id,
                        Role = user.Role,
                    };
                }
            }
            return View(userVM);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(Guid id)
        {

            HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + $"/user/Delete/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["deleted"] = "ok";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "ok";
            return RedirectToAction(nameof(Index));
        }
    }
}
