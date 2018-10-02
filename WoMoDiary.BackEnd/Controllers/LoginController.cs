using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly WoMoContext _context;
        private ILogger<LoginController> _logger;

        public LoginController(WoMoContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
            => new OkResult();

        // GET api/user/1436DD2A-3AE6-44AE-B369-8145E5AD69AD
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(Guid id)
        {
            _logger.LogWarning($"Looking for User with GUID '{id.ToString()}'");
            var user = await _context.Users.SingleOrDefaultAsync(i => i.Id == id);
            if (user == null) return new NotFoundObjectResult("Error");
            _logger.LogWarning($"Returns with User '{user}'");
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
        public async Task<ActionResult<User>> Put(Guid id, [FromBody] User value)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
            _context.Users.Remove(user);
            value.LastEdit = DateTimeOffset.Now;
            var newUser = await _context.Users.AddAsync(value);
            var result = await _context.SaveChangesAsync();
            return value;
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
