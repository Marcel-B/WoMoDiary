using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        WoMoContext _context;
        public TripController(WoMoContext context)
        {
            _context = context;
        }

        // GET api/trip
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trip>>> Get()
        {
            //return new List<TripOtd>
            //{
            //    new TripOtd { Id = Guid.NewGuid(), Name = "Italien", Description="This is a nice description", Places = new List<Place>
            //        {
            //            new CampingPlace{Name = "Futzi und Emma", Description ="No fresh water", Location  = new Location{

            //                    Longitude = 4,
            //                    Latitude = 55,
            //                }
            //            },
            //            new Restaurant{Name = "Denn's in", Description = "Funny little room", Location = new Location{
            //                    Longitude = 42,
            //                    Latitude = 12,
            //                }
            //            },
            //            new NicePlace{ Name = "Waterfall", Description ="Awesome Waterfall", Location = new Location{
            //                    Longitude = 11,
            //                    Latitude = 22,
            //                }
            //            }
            //        }
            //    }
            //};
            //var trips = await _context.Trips.Where(t => t.User.Id == userId).ToListAsync();
            var trips = await _context.Trips.ToListAsync();
            return new OkObjectResult(trips);
        }

        // GET api/trip
        [HttpGet("byid/{userId}")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetById(Guid userId)
        {
            var trips = await _context.Trips.Where(i => i.UserFk == userId).ToListAsync();
            return new OkObjectResult(trips);
        }
        // GET api/trip/
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> Get(Guid id)
        {
            var foo = await _context.Trips.SingleOrDefaultAsync(i => i.Id == id);
            return foo;
        }

        // POST api/trip
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Trip value)
        {
            var result = await _context.Trips.AddAsync(value);
            var r = await _context.SaveChangesAsync();
            return new OkResult();
        }

        // PUT api/trip/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Trip value)
        {
            var s = await _context.Trips.SingleOrDefaultAsync(i => i.Id == id);
            if (s == null) return new NotFoundObjectResult(id);
            _context.Trips.Remove(s);
            await _context.AddAsync(value);
            var result = await _context.SaveChangesAsync();
            return new OkResult();
        }

        // DELETE api/trip/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var s = await _context.Trips.SingleOrDefaultAsync(i => i.Id == id);
            if (s == null) return new NotFoundObjectResult(id);
            _context.Trips.Remove(s);
            var result = await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
