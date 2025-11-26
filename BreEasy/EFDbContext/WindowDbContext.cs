using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BreEasy.EFDbContext
{
    public class WindowDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public WindowDbContext(DbContextOptions<WindowDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // First try environment variable, fall back to appsettings.json
                var connectionString =
                    Environment.GetEnvironmentVariable("CONNECTION_STRING")
                    ?? _configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        public DbSet<Window> Windows { get; set; }
        public DbSet<Location> Locations { get; set; }
    }
}
