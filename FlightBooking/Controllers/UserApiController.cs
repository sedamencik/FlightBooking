using FlightBooking.Entities;
using FlightBooking.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Controllers
{
    [Route("api/user/[action]")]
    [ApiController]
    public class UserApiController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UserApiController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetAll()
        {
            var users = await _context.Users.Select(x => new UserViewModel { Email = x.Email, FullName = x.FullName, Id = x.Id, Role = x.Role }).ToListAsync();
            if (users is null)
            {
                return NoContent();
            }
            return users;

        }

        // GET api/user/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> Get(Guid id)
        {
            User? u = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (u is null)
            {
                return NoContent();
            }
            UserViewModel userVM = new UserViewModel
            {
                Email = u.Email,
                FullName = u.FullName,
                Id = u.Id,
            };
            return userVM;
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UserViewModel user)
        {
            User? u = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (u is null)
            {
                return NotFound();
            }
            u.FullName = user.FullName;
            u.Email = user.Email;
            u.Role = user.Role;
            await _context.SaveChangesAsync();
            return Ok();
        }

        // DELETE api/user/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            User? u = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (u is null)
            {
                return NotFound();
            }
            //if (k.Kitaplar.Any(x => x.YazarID == id))
            //{
            //    return NotFound("Yazara ait Kitaplar var");
            //}
            _context.Remove(u);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
