using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CurrencyWorker.Data.Model
{
    public class CurrencyContext : DbContext
    {
        public string ConnectionString { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        private readonly IConfiguration _configuration;

        public CurrencyContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite(_configuration.GetConnectionString("SQLite"));
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Postgres"));
            // optionsBuilder.UseSqlite("Filename=/home/umut/src/Currencies.db");
        }

    }
}