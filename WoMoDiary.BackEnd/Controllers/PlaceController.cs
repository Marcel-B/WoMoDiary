using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;
using Microsoft.EntityFrameworkCore;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly WoMoContext _context;
        public PlaceController(WoMoContext context)
        {
            _context = context;
        }

        // GET api/place
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> Get()
        {
            var places = await _context.Places.ToListAsync();
            return new OkObjectResult(places);
        }

        // GET api/place/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Place>> Get(Guid id)
        {
            var place = await _context.Places.SingleOrDefaultAsync(p => p.Id == id);
            if (place == null) return new UnprocessableEntityObjectResult(id);
            return new OkObjectResult(place);
        }

        // POST api/place
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Place value)
        {
            var place = await _context.Places.AddAsync(value);
            var result = await _context.SaveChangesAsync();
            return new OkObjectResult(place);
        }

        // PUT api/place/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Place value)
        {
            var place = await _context.Places.SingleOrDefaultAsync(p => p.Id == id);
            if (place == null) return new UnprocessableEntityObjectResult(id);
            _context.Remove(place);
            value.LastEdit = DateTimeOffset.Now;
            var re = await _context.Places.AddAsync(value);
            var i = await _context.SaveChangesAsync();
            return new OkResult();
        }

        // DELETE api/place/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var place = await _context.Places.SingleOrDefaultAsync(p => p.Id == id);
            if (place == null) return new NotFoundResult();
            _context.Places.Remove(place);
            var result = await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
