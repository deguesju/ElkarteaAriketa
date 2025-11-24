using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elkartea.Models
{
    public class Product
    {
        public int Id { get; set; }

        // Properties mapped to the database (match the migration / snapshot)
        public string Nombre { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public double Precio { get; set; }

        // Convenience properties used by the UI code. Marked NotMapped so EF Core
        // doesn't try to create separate columns for them.
        [NotMapped]
        public string Name
        {
            get => Nombre;
            set => Nombre = value;
        }

        [NotMapped]
        public int Stock
        {
            get => Cantidad;
            set => Cantidad = value;
        }

        [NotMapped]
        public decimal Price
        {
            get => Convert.ToDecimal(Precio);
            set => Precio = Convert.ToDouble(value);
        }

        public override string ToString()
        {
            // Useful for list bindings
            return Nombre;
        }
    }
}
