using System;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
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

        // GET api/trip/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(Guid id)
        {
            return new User { Id = id };
        }

        // POST api/trip
        [HttpPost]
        public void Post([FromBody] User value)
        {
        }

        // PUT api/trip/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] User value)
        {
        }

        // DELETE api/trip/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
