using System.Windows;
using TPV_Gastronomico.Views;

namespace Elkartea
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MostrarLogin(); // Cargar la vista de login al inicio
        }

        public void MostrarLogin()
        {
            var loginView = new LoginWindow();
            loginView.LoginCorrecto += Login_Exitoso;
            MainContent.Content = loginView;
        }

        private void Login_Exitoso(object sender, string tipoUsuario)
        {
            if (tipoUsuario == "admin")
            {
                MainContent.Content = new AdminPanel();
            }
            else
            {
                MainContent.Content = new UserPanel();
            }
        }
    }
}
