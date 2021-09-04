using Microsoft.EntityFrameworkCore;

namespace CurrencyWorker.Data.Model
{
    public class CurrencyContext : DbContext
    {
        public string ConnectionString { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite(ConnectionString);
            optionsBuilder.UseSqlite("Filename=/home/umut/src/Currencies.db");
        }

    }
}