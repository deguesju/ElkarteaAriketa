using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Elkartea.Data;
using Elkartea.Models;
using Elkartea.Utils;

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
            try
            {
                using var db = new AppDbContext();
                dgUsers.ItemsSource = db.Users?.ToList();
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erabiltzaileak kargatzean errorea");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var username = Interaction.InputBox("Erabiltzaile izena:", "Erabiltzailea gehitu");
                if (string.IsNullOrWhiteSpace(username)) return;

                var password = Interaction.InputBox("Pasahitza:", "Erabiltzailea gehitu", "1234");
                var role = Interaction.InputBox("Rol:", "Erabiltzailea gehitu", "user");

                using var db = new AppDbContext();
                db.Users!.Add(new User { Username = username, Password = password, Role = role });
                db.SaveChanges();
                LoadUsers();
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erabiltzailea gehitzean errorea");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgUsers.SelectedItem is User u)
                {
                    var password = Interaction.InputBox("Pasahitz berria:", "Erabiltzailea editatu", u.Password);
                    var role = Interaction.InputBox("Rol berria:", "Erabiltzailea editatu", u.Role);

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
                    MessageBox.Show("Aukeratu editatu nahi duzun erabiltzailea.");
                }
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erabiltzailea editatzean errorea");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgUsers.SelectedItem is User u)
                {
                    if (MessageBox.Show($"{u.Username} ezabatu?", "Berretsi", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
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
                    MessageBox.Show("Aukeratu ezabatu nahi duzun erabiltzailea.");
                }
            }
            catch (System.Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erabiltzailea ezabatzean errorea");
            }
        }
    }
}
