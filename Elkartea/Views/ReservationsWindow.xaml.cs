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
    public partial class ReservationsWindow : UserControl
    {
        public ReservationsWindow()
        {
            InitializeComponent();
            LoadReservations();
        }

        private void LoadReservations()
        {
            try
            {
                using var db = new AppDbContext();
                dgReservations.ItemsSource = db.Reservations?.ToList();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erreserbak kargatzean errorea");
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mesa = Interaction.InputBox("Mahaia:", "Erreserba gehitu", "A1");
                if (string.IsNullOrWhiteSpace(mesa)) return;
                if (!DateTime.TryParse(Interaction.InputBox("Data (yyyy-MM-dd HH:mm):", "Erreserba gehitu", DateTime.Now.ToString("s")), out var fecha))
                    fecha = DateTime.Now;
                var turno = Interaction.InputBox("Txanda:", "Erreserba gehitu", "Jana");
                var cliente = Interaction.InputBox("Bezeroa:", "Erreserba gehitu", "");

                using var db = new AppDbContext();
                db.Reservations!.Add(new Reservation { Mesa = mesa, Fecha = fecha, Turno = turno, Cliente = cliente });
                db.SaveChanges();
                LoadReservations();
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erreserba gehitzean errorea");
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgReservations.SelectedItem is Reservation r)
                {
                    var cliente = Interaction.InputBox("Bezeroa:", "Erreserba editatu", r.Cliente);
                    using var db = new AppDbContext();
                    var res = db.Reservations!.FirstOrDefault(x => x.Id == r.Id);
                    if (res != null)
                    {
                        res.Cliente = cliente;
                        db.SaveChanges();
                    }
                    LoadReservations();
                }
                else
                {
                    MessageBox.Show("Aukeratu editatu nahi duzun erreserba.");
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erreserba editatzean errorea");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgReservations.SelectedItem is Reservation r)
                {
                    if (MessageBox.Show($"{r.Mesa} mahaiaren erreserba ezabatu?", "Berretsi", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
                    using var db = new AppDbContext();
                    var res = db.Reservations!.FirstOrDefault(x => x.Id == r.Id);
                    if (res != null)
                    {
                        db.Reservations.Remove(res);
                        db.SaveChanges();
                    }
                    LoadReservations();
                }
                else
                {
                    MessageBox.Show("Aukeratu ezabatu nahi duzun erreserba.");
                }
            }
            catch (Exception ex)
            {
                ErrorHelper.HandleException(ex, "Erreserba ezabatzean errorea");
            }
        }
    }
}
