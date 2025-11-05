using System;
using System.Windows;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class LoginWindow : UserControl
    {
        public event EventHandler<string> LoginCorrecto; // "admin" o "user"

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string password = txtPassword.Password;

            // Por ahora, sin base de datos — lo simulamos
            if (usuario == "admin" && password == "1234")
            {
                LoginCorrecto?.Invoke(this, "admin");
            }
            else if (usuario == "user" && password == "1234")
            {
                LoginCorrecto?.Invoke(this, "user");
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }
    }
}
