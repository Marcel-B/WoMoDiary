using com.b_velop.WoMoDiary.Domain;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.WoMoDiary.BackEnd
{
    public class WoMoContext : DbContext
    {
        public WoMoContext(DbContextOptions<WoMoContext> builder) : base(builder)
        {

        }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Place> Places { get; set; }
    }
}
