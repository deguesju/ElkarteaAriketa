using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Elkartea.Data;

namespace TPV_Gastronomico.Views
{
    public class LoginEventArgs : EventArgs
    {
        public string Role { get; }
        public string Username { get; }

        public LoginEventArgs(string role, string username)
        {
            Role = role;
            Username = username;
        }
    }

    public partial class LoginWindow : UserControl
    {
        public event EventHandler<LoginEventArgs> LoginCorrecto; // now provides role and username

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
                if (role.Contains("admin")) LoginCorrecto?.Invoke(this, new LoginEventArgs("admin", user.Username));
                else LoginCorrecto?.Invoke(this, new LoginEventArgs("user", user.Username));
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }
    }
}
