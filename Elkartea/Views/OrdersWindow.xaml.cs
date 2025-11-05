using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class OrdersWindow : UserControl
    {
        public class Pedido
        {
            public int Id { get; set; }
            public string Mesa { get; set; }
            public string Usuario { get; set; }
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public double Total { get; set; }
        }

        private List<Pedido> pedidos = new List<Pedido>();

        public OrdersWindow()
        {
            InitializeComponent();
            CargarPedidosDummy();
        }

        private void CargarPedidosDummy()
        {
            pedidos = new List<Pedido>
            {
                new Pedido { Id = 1, Mesa = "2", Usuario = "usuario1", Producto = "Txuleta", Cantidad = 2, Total = 51.00 },
                new Pedido { Id = 2, Mesa = "5", Usuario = "usuario2", Producto = "Vino Tinto", Cantidad = 1, Total = 8.00 },
                new Pedido { Id = 3, Mesa = "3", Usuario = "usuario3", Producto = "Sidra", Cantidad = 4, Total = 22.00 }
            };
            dgOrders.ItemsSource = pedidos;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí se abriría un formulario para registrar un nuevo pedido.");
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem is Pedido p)
                MessageBox.Show($"Editar pedido #{p.Id} de {p.Usuario}");
            else
                MessageBox.Show("Selecciona un pedido para editar.");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem is Pedido p)
                MessageBox.Show($"Eliminar pedido #{p.Id}");
            else
                MessageBox.Show("Selecciona un pedido para eliminar.");
        }
    }
}
