using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core;
using UI.core.gestionClientes;
using UI.core.gestionPedidos;
using UI.views.vistaCalendario;

namespace UI;

public partial class MainWindow : Window
{
    private Storage _storage;
    private vistaCalendario _calendario;
    
    public MainWindow()
    {
        _storage = new Storage();
        _storage.Pedidos.Lista().Add(new Pedido(1,new Cliente("88888888T","Juan"),new DateOnly(2023,12,20)));
        _calendario = new vistaCalendario(_storage);
        
        InitializeComponent();
    }

    private void CambiarVistaCalendario(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _calendario;
    }
}