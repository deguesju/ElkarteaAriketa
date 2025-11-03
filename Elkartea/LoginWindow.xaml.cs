using Elkartea;
using System.Data.SQLite;
using System.Windows;

namespace TPV_Gastronomico
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            using (var conn = new SQLiteConnection("Data Source=tpv.db"))
            {
                conn.Open();
                var cmd = new SQLiteCommand("SELECT role FROM users WHERE username=@u AND password=@p", conn);
                cmd.Parameters.AddWithValue("@u", username);
                cmd.Parameters.AddWithValue("@p", password); // Para demo; luego usaremos hash
                var role = cmd.ExecuteScalar() as string;

                if (role == "admin")
                {
                    new AdminWindow().Show();
                    this.Close();
                }
                else if (role == "user")
                {
                    new UserWindow().Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos");
                }
            }
        }
    }
}
