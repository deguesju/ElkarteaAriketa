/* using System;
using System.Data.SQLite;
using System.IO;

namespace Elkartea.Data
{
    public static class Database
    {
        private static string dbPath = "tpv.db";
        private static string connectionString = $"Data Source={dbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            if (!File.Exists(dbPath))
            {
                InitializeDatabase();
            }

            return new SQLiteConnection(connectionString);
        }

        private static void InitializeDatabase()
        {
            Console.WriteLine("Creando base de datos inicial...");
            SQLiteConnection.CreateFile(dbPath);

            string seedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "seed.sql");
            string script = File.ReadAllText(seedPath);

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand(script, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Base de datos creada e inicializada correctamente.");
        }
    }
}
*/