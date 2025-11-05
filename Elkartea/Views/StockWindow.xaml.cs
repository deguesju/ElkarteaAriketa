using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class StockWindow : UserControl
    {
        public class Producto
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Cantidad { get; set; }
            public double Precio { get; set; }
        }

        private List<Producto> productos = new List<Producto>();

        public StockWindow()
        {
            InitializeComponent();
            CargarDatosDummy();
        }

        private void CargarDatosDummy()
        {
            productos = new List<Producto>
            {
                new Producto { Id = 1, Nombre = "Txuleta", Cantidad = 10, Precio = 25.50 },
                new Producto { Id = 2, Nombre = "Vino Tinto", Cantidad = 30, Precio = 8.00 },
                new Producto { Id = 3, Nombre = "Sidra", Cantidad = 20, Precio = 5.50 },
            };
            dgStock.ItemsSource = productos;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí se abriría una ventana para añadir un producto nuevo.");
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgStock.SelectedItem is Producto p)
                MessageBox.Show($"Editar producto: {p.Nombre}");
            else
                MessageBox.Show("Selecciona un producto para editar.");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStock.SelectedItem is Producto p)
                MessageBox.Show($"Eliminar producto: {p.Nombre}");
            else
                MessageBox.Show("Selecciona un producto para eliminar.");
        }
    }
}
