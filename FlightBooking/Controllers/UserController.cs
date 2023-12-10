using FlightBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FlightBooking.Controllers
{
    public class UserController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:7257/api");
        private readonly HttpClient _client;

        public UserController(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = baseUrl;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/user/GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(data);
            }
            return View(users);
        }
    }
}
