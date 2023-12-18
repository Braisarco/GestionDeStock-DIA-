using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using Avalonia.Controls;
using UI.core;
using UI.core.gestionPedidos;

namespace UI.views.vistaCalendario;

public partial class vistaCalendario : UserControl
{
    private Storage _storage;
    public vistaCalendario(Storage storageInstance)
    {
        _storage = storageInstance;
        
        InitializeComponent();
        
        Calendar calendario = this.FindControl<Calendar>("calendario");
        Label dia = this.FindControl<Label>("pickedDate");
        TextBlock eventos = this.FindControl<TextBlock>("eventos");
        
        calendario.SelectedDatesChanged += (_,_) =>
        {
            dia.Content = "Dia: " + DateOnly.FromDateTime(calendario.SelectedDate.Value);
            
            StringBuilder contenidoDia = new StringBuilder();
            foreach (Pedido pedido in _storage.Pedidos.Lista())
            {
                if (pedido.FechaHora.Equals(DateOnly.FromDateTime(calendario.SelectedDate.Value)))
                {
                    contenidoDia.Append($"- {pedido.ToString()}\n");
                }
            }

            string contenido = contenidoDia.ToString();
            eventos.Text = contenido;
        };
    }
}