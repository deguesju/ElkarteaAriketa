using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Elkartea.Data;
using Elkartea.Models;

namespace TPV_Gastronomico.Views
{
    public partial class StockWindow : UserControl
    {
        public StockWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            using var db = new AppDbContext();
            dgStock.ItemsSource = db.Products?.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var nombre = Interaction.InputBox("Nombre del producto:", "Añadir producto");
            if (string.IsNullOrWhiteSpace(nombre)) return;

            if (!int.TryParse(Interaction.InputBox("Cantidad:", "Añadir producto", "0"), out var cantidad))
                cantidad = 0;
            if (!double.TryParse(Interaction.InputBox("Precio:", "Añadir producto", "0"), out var precio))
                precio = 0;

            using var db = new AppDbContext();
            var p = new Product { Nombre = nombre, Cantidad = cantidad, Precio = precio };
            db.Products!.Add(p);
            db.SaveChanges();
            LoadProducts();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgStock.SelectedItem is Product p)
            {
                if (!int.TryParse(Interaction.InputBox("Nueva cantidad:", "Editar producto", p.Cantidad.ToString()), out var nuevaCantidad))
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
                MessageBox.Show("Selecciona un producto para editar.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgStock.SelectedItem is Product p)
            {
                var res = MessageBox.Show($"Eliminar producto {p.Nombre}?", "Confirmar", MessageBoxButton.YesNo);
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
                MessageBox.Show("Selecciona un producto para eliminar.");
            }
        }
    }
}
