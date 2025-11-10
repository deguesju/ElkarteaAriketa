using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Elkartea.Data;
using Elkartea.Models;

namespace TPV_Gastronomico.Views
{
    public partial class OrdersWindow : UserControl
    {
        public OrdersWindow()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            using var db = new AppDbContext();
            dgOrders.ItemsSource = db.Orders?.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var mesa = Interaction.InputBox("Mesa:", "Añadir pedido", "1");
            var usuario = Interaction.InputBox("Usuario:", "Añadir pedido", "");
            var producto = Interaction.InputBox("Producto:", "Añadir pedido", "");
            if (!int.TryParse(Interaction.InputBox("Cantidad:", "Añadir pedido", "1"), out var cantidad)) cantidad = 1;
            if (!double.TryParse(Interaction.InputBox("Total:", "Añadir pedido", "0"), out var total)) total = 0;

            using var db = new AppDbContext();
            db.Orders!.Add(new Order { Mesa = mesa, Usuario = usuario, Producto = producto, Cantidad = cantidad, Total = total });
            db.SaveChanges();
            LoadOrders();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem is Order o)
            {
                if (!int.TryParse(Interaction.InputBox("Nueva cantidad:", "Editar pedido", o.Cantidad.ToString()), out var nuevaCant))
                    return;
                using var db = new AppDbContext();
                var ord = db.Orders!.FirstOrDefault(x => x.Id == o.Id);
                if (ord != null)
                {
                    ord.Cantidad = nuevaCant;
                    db.SaveChanges();
                }
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Selecciona un pedido para editar.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgOrders.SelectedItem is Order o)
            {
                if (MessageBox.Show($"Eliminar pedido {o.Id}?", "Confirmar", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
                using var db = new AppDbContext();
                var ord = db.Orders!.FirstOrDefault(x => x.Id == o.Id);
                if (ord != null)
                {
                    db.Orders.Remove(ord);
                    db.SaveChanges();
                }
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Selecciona un pedido para eliminar.");
            }
        }
    }
}
