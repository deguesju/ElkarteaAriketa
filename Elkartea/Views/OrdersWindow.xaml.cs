using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Elkartea.Data;
using Elkartea.Models;
using Elkartea.Utils;

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
            try
            {
                using var db = new AppDbContext();
                dgOrders.ItemsSource = db.Orders?.ToList();
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Aginduak kargatzean errorea");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mesa = Interaction.InputBox("Mahaia:", "Agindua gehitu", "1");
                var usuario = Interaction.InputBox("Erabiltzailea:", "Agindua gehitu", "");
                var producto = Interaction.InputBox("Produktua:", "Agindua gehitu", "");
                if (!int.TryParse(Interaction.InputBox("Kantitatea:", "Agindua gehitu", "1"), out var cantidad)) cantidad = 1;
                if (!double.TryParse(Interaction.InputBox("Guztira:", "Agindua gehitu", "0"), out var total)) total = 0;

                using var db = new AppDbContext();
                db.Orders!.Add(new Order { Mesa = mesa, Usuario = usuario, Producto = producto, Cantidad = cantidad, Total = total });
                db.SaveChanges();
                LoadOrders();
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Agindua gehitzean errorea");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgOrders.SelectedItem is Order o)
                {
                    if (!int.TryParse(Interaction.InputBox("Kantitate berria:", "Agindua editatu", o.Cantidad.ToString()), out var nuevaCant))
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
                    MessageBox.Show("Aukeratu editatu nahi duzun agindua.");
                }
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Agindua editatzean errorea");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgOrders.SelectedItem is Order o)
                {
                    if (MessageBox.Show($"Agindua {o.Id} ezabatu?", "Berretsi", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
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
                    MessageBox.Show("Aukeratu ezabatu nahi duzun agindua.");
                }
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Agindua ezabatzean errorea");
            }
        }
    }
}
