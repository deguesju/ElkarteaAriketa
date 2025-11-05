using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class UsersWindow : UserControl
    {
        public class Usuario
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
        }

        private List<Usuario> usuarios = new List<Usuario>();

        public UsersWindow()
        {
            InitializeComponent();
            CargarUsuariosDummy();
        }

        private void CargarUsuariosDummy()
        {
            usuarios = new List<Usuario>
            {
                new Usuario { Id = 1, Username = "admin", Role = "Administrador" },
                new Usuario { Id = 2, Username = "usuario1", Role = "Usuario" },
                new Usuario { Id = 3, Username = "usuario2", Role = "Usuario" }
            };

            dgUsers.ItemsSource = usuarios;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí se abriría un formulario para añadir un nuevo usuario.");
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is Usuario u)
                MessageBox.Show($"Editar usuario: {u.Username}");
            else
                MessageBox.Show("Selecciona un usuario para editar.");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is Usuario u)
                MessageBox.Show($"Eliminar usuario: {u.Username}");
            else
                MessageBox.Show("Selecciona un usuario para eliminar.");
        }
    }
}
