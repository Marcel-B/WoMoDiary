using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        WoMoContext _context;
        public LoginController(WoMoContext context)
        {
            _context = context;
        }
        //// GET api/trip
        //[HttpGet]
        //public ActionResult<IEnumerable<User>> Get()
        //{
        //    return new List<TripOtd>
        //    {
        //        new TripOtd { Id = Guid.NewGuid(), Name = "Italien", Description="This is a nice description", Places = new List<Place>
        //            {
        //                new CampingPlace{Name = "Futzi und Emma", Description ="No fresh water", Location  = new Location{

        //                        Longitude = 4,
        //                        Latitude = 55,
        //                    }
        //                },
        //                new Restaurant{Name = "Denn's in", Description = "Funny little room", Location = new Location{
        //                        Longitude = 42,
        //                        Latitude = 12,
        //                    }
        //                },
        //                new NicePlace{ Name = "Waterfall", Description ="Awesome Waterfall", Location = new Location{
        //                        Longitude = 11,
        //                        Latitude = 22,
        //                    }
        //                }
        //            }
        //        }
        //    };
        //}

        // GET api/user/
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null) return new NotFoundResult();
            return user;
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User value)
        {
            var user = await _context.Users.AddAsync(value);
            var result = await _context.SaveChangesAsync();
            return new CreatedResult("/api/user", user);
        }

        // PUT api/user/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] User value)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(user);
            value.LastEdit = DateTimeOffset.Now;
            var newUser = await _context.Users.AddAsync(value);
            var result = await _context.SaveChangesAsync();
            return new OkResult();
        }

        // DELETE api/user/
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var toDelete = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (toDelete == null) return new NotFoundResult();
            _context.Remove(toDelete);
            var result = await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
