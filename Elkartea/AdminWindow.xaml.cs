using System.Windows;
using TPV_Gastronomico.Views;

namespace Elkartea
{
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
        }

        private void btnStock_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new StockWindow());
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new UsersWindow());
        }

        private void btnReservations_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new ReservationsWindow());
        }

        private void btnOrders_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new OrdersWindow());
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Volver al login dentro de MainWindow: podemos abrir MainWindow de nuevo o cerrar esta ventana
            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
