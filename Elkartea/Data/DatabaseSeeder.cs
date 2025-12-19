using System.Linq;
using System.Collections.Generic;
using Elkartea.Models;
using Elkartea.Utils;

namespace Elkartea.Data
{
    public static class DatabaseSeeder
    {
        /// <summary>
        /// Datu-basea sortu eta hasierako datuak txertatzen ditu.
        /// Try/catch gehitu da datu-basearekin lan egitean sor daitezkeen akatsak kudeatzeko.
        /// </summary>
        public static void EnsureSeedData()
        {
            try
            {
                using var db = new AppDbContext();

                // EnsureCreated erabilita DB fitxategia sortzen da, baina ez ditu migrations aplikatzen.
                db.Database.EnsureCreated();

                if (!db.Products!.Any())
                {
                    db.Products.AddRange(
                        new Product { Nombre = "Txuleta", Cantidad = 10, Precio = 25.50 },
                        new Product { Nombre = "Ardo Gorria", Cantidad = 30, Precio = 8.00 },
                        new Product { Nombre = "Olioa", Cantidad = 50, Precio = 4.50 },
                        new Product { Nombre = "Koka-Kola", Cantidad = 100, Precio = 2.00 },
                        new Product { Nombre = "Ogia", Cantidad = 40, Precio = 1.20 },
                        new Product { Nombre = "Gazta", Cantidad = 25, Precio = 3.50 },
                        new Product { Nombre = "Kafea", Cantidad = 80, Precio = 1.50 }
                    );
                    db.SaveChanges();
                }

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
            catch (System.Exception ex)
            {
                // Erroreak zentralizatuta kudeatzen dira
                ErrorHelper.HandleException(ex, "Datu-basea betetzean errorea");
            }
        }
    }
}
