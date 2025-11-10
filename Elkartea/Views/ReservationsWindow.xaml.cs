using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualBasic;
using Elkartea.Data;
using Elkartea.Models;

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
            using var db = new AppDbContext();
            dgReservations.ItemsSource = db.Reservations?.ToList();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var mesa = Interaction.InputBox("Mesa:", "Añadir reserva", "A1");
            if (string.IsNullOrWhiteSpace(mesa)) return;
            if (!DateTime.TryParse(Interaction.InputBox("Fecha (yyyy-MM-dd HH:mm):", "Añadir reserva", DateTime.Now.ToString("s")), out var fecha))
                fecha = DateTime.Now;
            var turno = Interaction.InputBox("Turno:", "Añadir reserva", "Comida");
            var cliente = Interaction.InputBox("Cliente:", "Añadir reserva", "");

            using var db = new AppDbContext();
            db.Reservations!.Add(new Reservation { Mesa = mesa, Fecha = fecha, Turno = turno, Cliente = cliente });
            db.SaveChanges();
            LoadReservations();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservations.SelectedItem is Reservation r)
            {
                var cliente = Interaction.InputBox("Cliente:", "Editar reserva", r.Cliente);
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
                MessageBox.Show("Selecciona una reserva para editar.");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservations.SelectedItem is Reservation r)
            {
                if (MessageBox.Show($"Eliminar reserva mesa {r.Mesa}?", "Confirmar", MessageBoxButton.YesNo) != MessageBoxResult.Yes) return;
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
                MessageBox.Show("Selecciona una reserva para eliminar.");
            }
        }
    }
}
