using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elkartea.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Mesa { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public double Total { get; set; }
    }
}
