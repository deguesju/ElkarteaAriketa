using System.Windows;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class AdminPanel : UserControl
    {
        public AdminPanel()
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
            // Raise an event or communicate with MainWindow via parent
            var mw = Window.GetWindow(this) as Elkartea.MainWindow;
            if (mw != null)
            {
                mw.MostrarLogin();
            }
        }
    }
}
