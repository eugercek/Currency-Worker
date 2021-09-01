using Microsoft.EntityFrameworkCore;

namespace CurrencyWorker.Data.Model
{
    public class CurrenyContext : DbContext
    {
        public string ConnectionString { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

    }
}