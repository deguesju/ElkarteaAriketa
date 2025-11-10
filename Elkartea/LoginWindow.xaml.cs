using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Elkartea.Data;

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

            // Validar contra la base de datos
            using var db = new AppDbContext();
            var user = db.Users?.FirstOrDefault(u => u.Username == usuario && u.Password == password);

            if (user != null)
            {
                // Normaliza rol para la vista principal
                var role = (user.Role ?? string.Empty).ToLower();
                if (role.Contains("admin")) LoginCorrecto?.Invoke(this, "admin");
                else LoginCorrecto?.Invoke(this, "user");
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }
    }
}
