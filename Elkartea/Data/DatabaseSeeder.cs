using System.Linq;
using System.Collections.Generic;
using Elkartea.Models;

namespace Elkartea.Data
{
    public static class DatabaseSeeder
    {
        public static void EnsureSeedData()
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            if (!db.Products!.Any())
            {
                db.Products.AddRange(
                    new Product { Nombre = "Txuleta", Cantidad = 10, Precio = 25.50 },
                    new Product { Nombre = "Vino Tinto", Cantidad = 30, Precio = 8.00 }
                );
                db.SaveChanges();
            }

            // Usuarios "requeridos" — ajusta contraseñas/roles según necesites
            var requiredUsers = new List<User>
            {
                new User { Username = "admin", Password = "admin123", Role = "admin" },
                new User { Username = "user", Password = "user123", Role = "user" },
                new User { Username = "jaime", Password = "1234", Role = "Administrador" },
                new User { Username = "danel", Password = "danel", Role = "Administrador" }

            };

            foreach (var u in requiredUsers)
            {
                if (!db.Users!.Any(x => x.Username == u.Username))
                {
                    db.Users.Add(u);
                }
            }

            db.SaveChanges();
        }
    }
}
