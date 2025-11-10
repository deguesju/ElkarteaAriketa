using System.Windows;
using Elkartea.Data;

namespace TPV_Gastronomico
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ensure DB and seeds BEFORE WPF processes StartupUri / creates windows
            DatabaseSeeder.EnsureSeedData();

            base.OnStartup(e);
        }
    }
}
