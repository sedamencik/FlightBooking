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


        // GET: api/user/getall
        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            if (users is null)
            {
                return NoContent();
            }
            return users;

        }

        // GET api/user/get/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            User? u = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (u is null)
            {
                return NoContent();
            }

            return u;
        }

        // POST api/user/create
        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT api/user/5
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditUserViewModel user)
        {
            User? u = await _context.Users.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
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
