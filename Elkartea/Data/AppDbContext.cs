using Microsoft.EntityFrameworkCore;
using Elkartea.Models;

namespace Elkartea.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product>? Products { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<Reservation>? Reservations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=elkartea.db");
        }
    }
}
