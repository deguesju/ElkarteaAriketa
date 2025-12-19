using System;
using System.Windows;
using Elkartea.Utils;

namespace TPV_Gastronomico.Views
{
    public partial class ReceiptWindow : Window
    {
        public ReceiptWindow(string receiptText)
        {
            InitializeComponent();
            txtReceipt.Text = receiptText;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var pd = new System.Windows.Controls.PrintDialog();
                if (pd.ShowDialog() == true)
                {
                    // Print the TextBox content
                    pd.PrintVisual(txtReceipt, "Receipt");
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Tiketa inprimatzean errorea");
            }
        }
    }
}
