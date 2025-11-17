using System.Windows;
using System.Windows.Controls;
using TPV_Gastronomico.Views; // allow creating UserPayWindow

namespace TPV_Gastronomico.Views
{
    public partial class UserPanel : UserControl
    {
        public UserPanel()
        {
            InitializeComponent();
            var tb = FindName("txtWelcome") as TextBlock;
            if (tb != null)
                tb.Text = "Ongi etorri";
        }

        public UserPanel(string nombreUsuario) : this()
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                return;

            var tb = FindName("txtWelcome") as TextBlock;
            if (tb != null)
            {
                tb.Text = $"Ongi etorri, {nombreUsuario}!";
            }
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

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Children.Clear();
            MainContent.Children.Add(new TPV_Gastronomico.Views.UserPayWindow());
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            var mw = Window.GetWindow(this) as Elkartea.MainWindow;
            if (mw != null)
            {
                mw.MostrarLogin();
            }
        }

     }
}
