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
    public class LoginController : ControllerBase
    {
        private readonly WoMoContext _context;

        public LoginController(WoMoContext context)
        {
            _context = context;
        }

        // GET api/trip
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
            => new OkResult();

        // GET api/user/1436DD2A-3AE6-44AE-B369-8145E5AD69AD
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
            return new OkObjectResult(user);
        }

        // PUT api/user/1436DD2A-3AE6-44AE-B369-8145E5AD69AD
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] User value)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(user);
            value.LastEdit = DateTimeOffset.Now;
            var newUser = await _context.Users.AddAsync(value);
            var result = await _context.SaveChangesAsync();
            return new OkObjectResult(newUser);
        }

        // DELETE api/user/1436DD2A-3AE6-44AE-B369-8145E5AD69AD
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var toDelete = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            if (toDelete == null) return new NotFoundResult();
            _context.Remove(toDelete);
            var result = await _context.SaveChangesAsync();
            return new OkObjectResult(result);
        }
    }
}
