using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class UserPayWindow : UserControl
    {
        private List<ProductItem> AllProducts;

        public UserPayWindow()
        {
            InitializeComponent();

            AllProducts = new List<ProductItem>
            {
                new ProductItem { Name="Aceite", Price=4.50m, ImagePath="Images/aceite.png" },
                new ProductItem { Name="Vino", Price=8.90m, ImagePath="Images/vino.png" },
                new ProductItem { Name="Coca-Cola", Price=2.00m, ImagePath="Images/cocacola.png" },
                new ProductItem { Name="Pan", Price=1.20m, ImagePath="Images/pan.png" }
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
            if (lbResults.SelectedItem is not ProductItem product)
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
    }
}
