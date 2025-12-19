using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Elkartea.Data;
using Elkartea.Models;
using Elkartea.Utils;

namespace TPV_Gastronomico.Views
{
    public partial class StockWindow : UserControl
    {
        public StockWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        /// <summary>
        /// Produktuak kargatzen ditu datu-basetik. Saiakera eta errore kudeaketa eginda.
        /// </summary>
        private void LoadProducts()
        {
            try
            {
                using var db = new AppDbContext();
                dgStock.ItemsSource = db.Products?.ToList();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Produktuen kargatzean errorea");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var nombre = Interaction.InputBox("Produktuen izena:", "Produktua gehitu");
                if (string.IsNullOrWhiteSpace(nombre)) return;

                if (!int.TryParse(Interaction.InputBox("Kopurua:", "Produktua gehitu", "0"), out var cantidad))
                    cantidad = 0;
                if (!double.TryParse(Interaction.InputBox("Prezioa:", "Produktua gehitu", "0"), out var precio))
                    precio = 0;

                using var db = new AppDbContext();
                var p = new Product { Nombre = nombre, Cantidad = cantidad, Precio = precio };
                db.Products!.Add(p);
                db.SaveChanges();
                LoadProducts();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Produktua gehitzean errorea");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgStock.SelectedItem is Product p)
                {
                    if (!int.TryParse(Interaction.InputBox("Kopuru berria:", "Produktua editatu", p.Cantidad.ToString()), out var nuevaCantidad))
                        return;

                    using var db = new AppDbContext();
                    var prod = db.Products!.FirstOrDefault(x => x.Id == p.Id);
                    if (prod != null)
                    {
                        prod.Cantidad = nuevaCantidad;
                        db.SaveChanges();
                    }
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Aukeratu editatu nahi duzun produktua.");
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Produktua editatzean errorea");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgStock.SelectedItem is Product p)
                {
                    var res = MessageBox.Show($"{p.Nombre} ezabatu?", "Berretsi", MessageBoxButton.YesNo);
                    if (res != MessageBoxResult.Yes) return;

                    using var db = new AppDbContext();
                    var prod = db.Products!.FirstOrDefault(x => x.Id == p.Id);
                    if (prod != null)
                    {
                        db.Products.Remove(prod);
                        db.SaveChanges();
                    }
                    LoadProducts();
                }
                else
                {
                    MessageBox.Show("Aukeratu ezabatu nahi duzun produktua.");
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Produktua ezabitzean errorea");
            }
        }
    }
}
