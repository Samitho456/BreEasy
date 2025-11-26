using Microsoft.EntityFrameworkCore;

namespace BreEasy.EFDbContext
{
    public class WindowDbContext : DbContext
    {
        // Add constructor that accepts options so AddDbContext can pass configured options
        public WindowDbContext(DbContextOptions<WindowDbContext> options)
            : base(options)
        {
        }

        // Override OnConfiguring to provide a fallback connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Data Source=localhost,1433;User ID=SA;Password=YourStrong!Passw0rd;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }   

        public DbSet<Window> Windows { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}

