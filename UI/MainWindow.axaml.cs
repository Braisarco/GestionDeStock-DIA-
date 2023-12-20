using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core;
using UI.core.gestionClientes;
using UI.core.gestionPedidos;
using UI.views.vistaCalendario;
using UI.views.vistaClientes;
using UI.views.vistaPedidos.ComprasProveedores;
using UI.views.vistaPedidos.VentasClientes;
using UI.views.vistaProveedores;
using UI.views.vistaStock;

namespace UI;

public partial class MainWindow : Window
{
    public Storage _storage;
    public vistaCalendario _vistaCalendario;
    public MainWindowClientes _vistaClientes;
    public MainWindowComprarStock _vistaComprarStock;
    public MainWindowPedidos _vistaPedidos;
    public MainWindowProveedores _vistaProveedores;
    public MainWindowStock _vistaStock;
    
    public MainWindow()
    {
        _storage = new Storage();

        _vistaCalendario = new vistaCalendario( this); 
        _vistaPedidos = new MainWindowPedidos(this);
        _vistaClientes = new MainWindowClientes(this);
        _vistaComprarStock = new MainWindowComprarStock(this);
        _vistaProveedores = new MainWindowProveedores(this);
        _vistaStock = new MainWindowStock( this);
        
        InitializeComponent();
        
    }
    
    //***** HEADER USAGE *****

    public void CambiarVistaCalendario(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _vistaCalendario;
    }
    
    public void CambiarVistaProveedores(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _vistaProveedores;
    }
    public void CambiarVistaPedidos(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _vistaPedidos;
    }
    public void CambiarVistaStock(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _vistaStock;
    }
    public void CambiarVistaClientes(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _vistaClientes;
    }
    public void CambiarVistaComprarStock(object? sender, RoutedEventArgs routedEventArgs)
    {
        MainContentControl.Content = _vistaComprarStock;
    }
    
}