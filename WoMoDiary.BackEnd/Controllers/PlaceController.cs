using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        // GET api/place
        [HttpGet]
        public ActionResult<IEnumerable<Place>> Get()
        {
            return new List<Place>();
        }

        // GET api/place/5
        [HttpGet("{id}")]
        public ActionResult<Place> Get(Guid id)
        {
            return new Place { Id = id };
        }

        // POST api/place
        [HttpPost]
        public void Post([FromBody] Place value)
        {
        }

        // PUT api/place/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Place value)
        {
        }

        // DELETE api/place/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }
    }
}
