using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Elkartea.Data;
using Elkartea.Models;

namespace TPV_Gastronomico.Views
{
    public partial class UsersWindow : UserControl
    {
        public UsersWindow()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using var db = new AppDbContext();
            dgUsers.ItemsSource = db.Users?.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var username = Interaction.InputBox("Username:", "Añadir usuario");
            if (string.IsNullOrWhiteSpace(username)) return;

            var password = Interaction.InputBox("Password:", "Añadir usuario", "1234");
            var role = Interaction.InputBox("Role:", "Añadir usuario", "user");

            using var db = new AppDbContext();
            db.Users!.Add(new User { Username = username, Password = password, Role = role });
            db.SaveChanges();
            LoadUsers();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User u)
            {
                var password = Interaction.InputBox("Nueva password:", "Editar usuario", u.Password);
                var role = Interaction.InputBox("Nuevo role:", "Editar usuario", u.Role);

                using var db = new AppDbContext();
                var user = db.Users!.FirstOrDefault(x => x.Id == u.Id);
                if (user != null)
                {
                    user.Password = password;
                    user.Role = role;
                    db.SaveChanges();
                }
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para editar.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is User u)
            {
                if (MessageBox.Show($"Eliminar usuario {u.Username}?", "Confirmar", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
                using var db = new AppDbContext();
                var user = db.Users!.FirstOrDefault(x => x.Id == u.Id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
                LoadUsers();
            }
            else
            {
                MessageBox.Show("Selecciona un usuario para eliminar.");
            }
        }
    }
}
