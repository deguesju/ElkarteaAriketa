using System.Collections.Generic;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class UserPayWindow : UserControl
    {
        public UserPayWindow()
        {
            InitializeComponent();

            // optional: setup a sample empty source so designer doesn't complain
            dgConsumed.ItemsSource = new List<ConsumedItem>();
        }
    }

    public class ConsumedItem
    {
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Subtotal => Quantity * UnitPrice;
    }
}
