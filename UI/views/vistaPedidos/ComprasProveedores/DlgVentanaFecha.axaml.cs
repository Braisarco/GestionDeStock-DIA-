
using System;
using Avalonia.Controls;

namespace UI.views.vistaPedidos.ComprasProveedores;

    public partial class DlgVentanaFecha : Window
    {
        public DlgVentanaFecha()
        {
            InitializeComponent();
            MostrarFechaAleatoria();

            var btAceptar = this.FindControl<Button>("btAceptar");
            btAceptar.Click += (_, _) => onAceptar();


        }

        private void MostrarFechaAleatoria()
        {
            // Calcular y mostrar la fecha aleatoria
            DateTime fechaHoy = DateTime.Now.Date;
            DateOnly fechaEntrega = DateOnly.FromDateTime(fechaHoy.AddDays(3));
            var fechaTextBlock = this.FindControl<TextBlock>("FechaTextBlock");
            if (fechaTextBlock != null)
            {
                fechaTextBlock.Text = $"Fecha de Entrega: {fechaEntrega}";
            }
        }

        private void onAceptar()
        {
            // Cerrar la ventana al hacer clic en "Aceptar"
            Close();
        }
    }

