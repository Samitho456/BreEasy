using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql;

namespace BreEasy.EFDbContext
{
    public class WindowDbContext : DbContext
    {
        public WindowDbContext(DbContextOptions<WindowDbContext> options)
            : base(options)
        {
        }

        public DbSet<Window> Windows { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
   
}
