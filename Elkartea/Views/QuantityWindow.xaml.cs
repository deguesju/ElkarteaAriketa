using System.Windows;

namespace TPV_Gastronomico.Views
{
    public partial class QuantityWindow : Window
    {
        public int Quantity { get; private set; }

        public QuantityWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtQty.Text, out int qty) && qty > 0)
            {
                Quantity = qty;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Introduce una cantidad válida.");
            }
        }
    }
}
