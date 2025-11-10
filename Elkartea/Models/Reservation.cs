using System;

namespace Elkartea.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Mesa { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Turno { get; set; } = string.Empty; // "Comida" o "Cena"
        public string Cliente { get; set; } = string.Empty;
    }
}
