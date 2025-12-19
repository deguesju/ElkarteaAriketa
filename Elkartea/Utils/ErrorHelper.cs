using System;
using System.Diagnostics;
using System.Windows;

namespace Elkartea.Utils
{
    /// <summary>
    /// Laguntzailea: Erroreak kudeatzeko zentralizatua.
    /// Testuinguru egokia eta mezu erabilgarria erakusten du erabiltzeari.
    /// </summary>
    public static class ErrorHelper
    {
        /// <summary>
        /// Errorea tratatu eta erabiltzaileari mezua erakutsi.
        /// comments: 'context' parametroa euskarazeko testuinguru laburra izan behar da.
        /// </summary>
        public static void HandleException(Exception ex, string context = "")
        {
            try
            {
                Debug.WriteLine($"[Error] {context}: {ex}");
                string caption = "Errorea";
                string message = string.IsNullOrWhiteSpace(context)
                    ? "Sisteman errore bat gertatu da. Mesedez, berriro probatu eta jarraitzen badu jakinarazi." 
                    : $"{context}: errore bat gertatu da. Mesedez, berriro probatu eta jarraitzen badu jakinarazi.";

                // Erabiltzaileari erakutsi
                MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                // Ez dugu gehiago egin nahi hemen — debug-era idatzi besterik ez.
                try { Debug.WriteLine("Errore kudeatzean exekuzio errorea."); } catch { }
            }
        }
    }
}
