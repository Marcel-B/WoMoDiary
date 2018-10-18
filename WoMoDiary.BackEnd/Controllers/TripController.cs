using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using com.b_velop.WoMoDiary.Domain;

namespace com.b_velop.WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        private readonly WoMoContext _context;
        private readonly ILogger<TripController> _logger;

        public TripController(WoMoContext context, ILogger<TripController> logger)
        {
            _context = context;
            _logger = logger;
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

        // GET api/trip/byuser/1436DD2A-3AE6-44AE-B369-8145E5AD69AD
        [HttpGet("byuser/{userId}")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetById(Guid userId)
        {
            try
            {
                var trips = await _context.Trips.Where(t => t.User.UserId == userId).ToListAsync();
                return new OkObjectResult(trips);
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(42), ex, $"Error occured while loading trips by user '{userId}'");
                return StatusCode(500);
            }
        }

        // GET api/trip/
        [HttpGet("{id}")]
        public async Task<ActionResult<Trip>> Get(Guid id)
        {
            var foo = await _context.Trips.SingleOrDefaultAsync(i => i.TripId == id);
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
            var s = await _context.Trips.SingleOrDefaultAsync(i => i.TripId == id);
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
            var s = await _context.Trips.SingleOrDefaultAsync(i => i.TripId == id);
            if (s == null) return new NotFoundObjectResult(id);
            _context.Trips.Remove(s);
            var result = await _context.SaveChangesAsync();
            return new OkResult();
        }
    }
}
