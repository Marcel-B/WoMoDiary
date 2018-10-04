using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WoMoDiary.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WoMoDiary.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly WoMoContext _context;
        private readonly ILogger<UserController> _logger;

        public UserController(WoMoContext context, ILogger<UserController> logger)
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
            var user = await _context.Users.SingleOrDefaultAsync(i => i.UserId == id);
            if (user == null) return new NotFoundObjectResult("Error");
            _logger.LogWarning($"Returns with User '{user}'");
            return user;
        }

        // POST api/user
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] string value)
        {
            var usr = JsonConvert.DeserializeObject<User>(value);
            var user = await _context.Users.AddAsync(usr);
            var result = await _context.SaveChangesAsync();
            return new OkObjectResult(user);
        }

        // PUT api/user/1436DD2A-3AE6-44AE-B369-8145E5AD69AD
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> Put(Guid id, [FromBody] User value)
        {
            try
            {
                value.LastEdit = DateTimeOffset.Now;
                _context.Entry(value).State = EntityState.Modified;
                _context.Users.Update(value);
                var currentTrips =
                    await _context.Trips.Where(t => t.UserId == value.UserId).ToListAsync();

                _logger.LogInformation($"I have '{currentTrips.Count}' Trips for this User");
                var result = await _context.SaveChangesAsync();
                return value;
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(42), ex, $"Exception occured while put user '{value.UserId}'.{Environment.NewLine}");
                return StatusCode(500);
            }
        }

        // DELETE api/user/1436DD2A-3AE6-44AE-B369-8145E5AD69AD
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var toDelete = await _context.Users.SingleOrDefaultAsync(u => u.UserId == id);
            if (toDelete == null) return new NotFoundResult();
            _context.Remove(toDelete);
            var result = await _context.SaveChangesAsync();
            return new OkObjectResult(result);
        }
    }
}
