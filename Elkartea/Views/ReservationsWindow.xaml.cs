using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TPV_Gastronomico.Views
{
    public partial class ReservationsWindow : UserControl
    {
        public class Reserva
        {
            public int Id { get; set; }
            public string Mesa { get; set; }
            public DateTime Fecha { get; set; }
            public string Turno { get; set; } // "Comida" o "Cena"
            public string Cliente { get; set; }
        }

        private List<Reserva> reservas = new List<Reserva>();

        public ReservationsWindow()
        {
            InitializeComponent();
            CargarReservasDummy();
        }

        private void CargarReservasDummy()
        {
            reservas = new List<Reserva>
            {
                new Reserva { Id = 1, Mesa = "1", Fecha = DateTime.Today, Turno = "Comida", Cliente = "Iñaki" },
                new Reserva { Id = 2, Mesa = "3", Fecha = DateTime.Today, Turno = "Cena", Cliente = "Ane" },
                new Reserva { Id = 3, Mesa = "5", Fecha = DateTime.Today.AddDays(1), Turno = "Cena", Cliente = "Mikel" },
            };

            dgReservations.ItemsSource = reservas;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aquí se abriría un formulario para crear una nueva reserva.");
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservations.SelectedItem is Reserva r)
                MessageBox.Show($"Editar reserva de {r.Cliente} en mesa {r.Mesa} ({r.Turno})");
            else
                MessageBox.Show("Selecciona una reserva para editar.");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dgReservations.SelectedItem is Reserva r)
                MessageBox.Show($"Eliminar reserva de {r.Cliente}");
            else
                MessageBox.Show("Selecciona una reserva para eliminar.");
        }
    }
}
