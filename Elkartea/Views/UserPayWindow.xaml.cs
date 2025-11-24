using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Elkartea.Models;

namespace TPV_Gastronomico.Views
{
    public partial class UserPayWindow : UserControl
    {
        private List<Product> AllProducts;

        public UserPayWindow()
        {
            InitializeComponent();

            // Adibide produktuak (Basque)
            AllProducts = new List<Product>
            {
                new Product { Name = "Olioa", Price = 4.50m },
                new Product { Name = "Ardo Gorria", Price = 8.90m },
                new Product { Name = "Koka-Kola", Price = 2.00m },
                new Product { Name = "Ogia", Price = 1.20m },
                new Product { Name = "Gazta", Price = 3.50m },
                new Product { Name = "Kafea", Price = 1.50m },
                new Product { Name = "Ur Mineral", Price = 1.00m },
                new Product { Name = "Perretxikoak", Price = 6.00m }
            };

            dgConsumed.ItemsSource = new List<ConsumedItem>();

            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(query))
            {
                lbResults.Visibility = Visibility.Collapsed;
                return;
            }

            var found = AllProducts
                .Where(p => p.Name.ToLower().Contains(query))
                .ToList();

            lbResults.ItemsSource = found;
            lbResults.Visibility = found.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void lbResults_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lbResults.SelectedItem is not Product product)
                return;

            QuantityWindow qtyWin = new QuantityWindow();

            if (qtyWin.ShowDialog() == true)
            {
                int qty = qtyWin.Quantity;

                var list = dgConsumed.ItemsSource as List<ConsumedItem>;

                list.Add(new ConsumedItem
                {
                    Product = product.Name,
                    Quantity = qty,
                    UnitPrice = product.Price
                });

                dgConsumed.Items.Refresh();
                UpdateTotal();
            }

            lbResults.Visibility = Visibility.Collapsed;
        }

        private void UpdateTotal()
        {
            var list = dgConsumed.ItemsSource as List<ConsumedItem>;
            decimal total = list.Sum(x => x.Subtotal);
            txtTotal.Text = total.ToString("C");
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (dgConsumed.SelectedItem is ConsumedItem item)
            {
                var list = dgConsumed.ItemsSource as List<ConsumedItem>;
                list.Remove(item);
                dgConsumed.Items.Refresh();
                UpdateTotal();
            }
            else
            {
                MessageBox.Show("Aukeratu ezabatu nahi duzun produktu bat.", "Informazioa", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            var list = dgConsumed.ItemsSource as List<ConsumedItem>;
            if (list == null || !list.Any())
            {
                MessageBox.Show("Ordaindu beharreko produkturik ez dago.", "Informazioa", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            decimal subtotal = list.Sum(x => x.Subtotal);
            const decimal taxRate = 0.21m; // 21% BEZ
            decimal iva = decimal.Round(subtotal * taxRate, 2);
            decimal total = subtotal + iva;

            var sb = new StringBuilder();
            sb.AppendLine("--- TIKETA ---");
            sb.AppendLine($"Data: {System.DateTime.Now:G}");
            sb.AppendLine("------------------------------");
            sb.AppendLine("Kop  Deskripzioa        Zenbatekoa");

            foreach (var it in list)
            {
                string line = string.Format("{0,3}  {1,-18}  {2,8}", it.Quantity, Truncate(it.Product, 18), it.Subtotal.ToString("C"));
                sb.AppendLine(line);
            }

            sb.AppendLine("------------------------------");
            sb.AppendLine(string.Format("{0,-22}{1,10}", "Azpisuma:", subtotal.ToString("C")));
            sb.AppendLine(string.Format("{0,-22}{1,10}", $"BEZ ({taxRate:P0}):", iva.ToString("C")));
            sb.AppendLine(string.Format("{0,-22}{1,10}", "Guztira:", total.ToString("C")));
            sb.AppendLine("------------------------------");
            sb.AppendLine("Eskerrik asko zure erosketagatik");

            // Show receipt window
            var receiptWin = new ReceiptWindow(sb.ToString());
            receiptWin.Owner = Window.GetWindow(this);
            receiptWin.ShowDialog();

            // After paying, clear the list
            list.Clear();
            dgConsumed.Items.Refresh();
            UpdateTotal();
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength - 3) + "...";
        }
    }
}
