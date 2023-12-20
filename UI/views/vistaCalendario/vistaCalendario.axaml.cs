using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core;
using UI.core.gestionCompras;
using UI.core.gestionPedidos;

namespace UI.views.vistaCalendario;

public partial class vistaCalendario : UserControl
{
    private MainWindow parentWindow;
    private Storage _storage;
    public vistaCalendario(MainWindow windowParent)
    {
        parentWindow = windowParent;
        _storage = windowParent._storage;
        
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
            foreach (Compra compra in _storage.Compras.Compras)
            {
                if (compra.fechaHoraEntrega.Equals(DateOnly.FromDateTime(calendario.SelectedDate.Value)))
                {
                    contenidoDia.Append($"- {compra.ToString()}\n");
                }
            }

            string contenido = contenidoDia.ToString();
            eventos.Text = contenido;
        };
    }
    //***** HEADER USAGE *****

    private void CambiarVistaCalendario(object? sender, RoutedEventArgs routedEventArgs)
    {
        parentWindow.CambiarVistaCalendario(sender, routedEventArgs);
    }
    
    private void CambiarVistaProveedores(object? sender, RoutedEventArgs routedEventArgs)
    {
        parentWindow.CambiarVistaProveedores(sender, routedEventArgs);
    }
    private void CambiarVistaPedidos(object? sender, RoutedEventArgs routedEventArgs)
    {
        parentWindow.CambiarVistaPedidos(sender, routedEventArgs);
    }
    private void CambiarVistaStock(object? sender, RoutedEventArgs routedEventArgs)
    {
        parentWindow.CambiarVistaStock(sender, routedEventArgs);
    }
    private void CambiarVistaClientes(object? sender, RoutedEventArgs routedEventArgs)
    {
        parentWindow.CambiarVistaClientes(sender, routedEventArgs);
    }
    public void CambiarVistaComprarStock(object? sender, RoutedEventArgs routedEventArgs)
    {
        parentWindow.CambiarVistaComprarStock(sender,routedEventArgs);
    }
}