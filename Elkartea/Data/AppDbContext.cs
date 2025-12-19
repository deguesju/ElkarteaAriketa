using Microsoft.EntityFrameworkCore;
using Elkartea.Models;
using Elkartea.Utils;

namespace Elkartea.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }

        /// <summary>
        /// DB konfiguratzea. Try/catch gehitu da konexio akatsen kudeaketarako.
        /// Garrantzitsua: ez duen eragina transakzioetan, baina mezu erabilgarriak erakusten ditu.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlite("Data Source=elkartea.db");
            }
            catch (System.Exception ex)
            {
                // Errorea zentralizatu eta erabiltzaileari jakinarazi
                ErrorHelper.HandleException(ex, "Datu-basearen konfigurazioan errorea");
                throw; // Errorea igorri berriro behar izanez gero (goiko mailan kontrolatzeko)
            }
        }
    }
}
