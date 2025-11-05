using Microsoft.EntityFrameworkCore;
using slotgame.Models;

namespace slotgame.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<GameList> GameList { get; set; }
    }
}