using Microsoft.EntityFrameworkCore;
using PlayersDotnet6.Models;

namespace PlayersDotnet6.Data
{
    public class PlayerContext : DbContext
    {
        public PlayerContext(DbContextOptions<PlayerContext> options) : base(options)
        {
        }

        public DbSet<Player> Players{ get; set; }
    }
}
