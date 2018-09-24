using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripController : ControllerBase
    {
        // GET api/trip
        [HttpGet]
        public ActionResult<IEnumerable<Trip>> Get()
        {
           return new List<Trip>
            {
                new Trip { Id = Guid.NewGuid().ToString(), Name = "Italien", Description="This is a nice description", Places = new List<IPlace>
                    {
                        new CampingPlace{Name = "Futzi und Emma", Description ="No fresh water", Location  = new Location{

                                Longitude = 4,
                                Latitude = 55,
                            }
                        },
                        new Restaurant{Name = "Denn's in", Description = "Funny little room", Location = new Location{
                                Longitude = 42,
                                Latitude = 12,
                            }
                        },
                        new NicePlace{ Name = "Waterfall", Description ="Awesome Waterfall", Location = new Location{
                                Longitude = 11,
                                Latitude = 22,
                            }
                        }
                    }
                },
            };
        }

        // GET api/trip/5
        [HttpGet("{id}")]
        public ActionResult<Trip> Get(Guid id)
        {
            return new Trip
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Italien",
                Description = "This is a nice description",
                Places = new List<IPlace>
                    {
                        new CampingPlace{Id = id, Name = "Futzi und Emma", Description ="No fresh water", Location  = new Location{

                                Longitude = 4,
                                Latitude = 55,
                            }
                    }
                }
            };
        }

        // POST api/trip
        [HttpPost]
        public void Post([FromBody] Trip value)
        {
        }

        // PUT api/trip/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Trip value)
        {
        }

        // DELETE api/trip/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
