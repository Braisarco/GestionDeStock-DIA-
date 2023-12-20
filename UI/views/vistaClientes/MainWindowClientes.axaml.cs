using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core.gestionClientes;


namespace UI.views.vistaClientes;

public partial class MainWindowClientes : UserControl
{
    private Clientes clientes;
    private MainWindow parentWindow;
    public List<Cliente> ListaClientes { get; set; }

    public MainWindowClientes(MainWindow windowParent)
    {
        parentWindow = windowParent;

        clientes = parentWindow._storage.Clientes;
        
        InitializeComponent();

        UpdateView();

        var botonAnhadir = this.FindControl<Button>("nuevoCliente");
        var textoBuscar = this.FindControl<TextBox>("buscador");
        
        botonAnhadir.Click += (_, _) => this.AnhadirCliente();
        textoBuscar.TextChanging += (_, _) => this.BuscarCliente(textoBuscar.Text);
    }

    void AnhadirCliente()
    {
        var nombre = this.FindControl<TextBox>("newName");
        var cif = this.FindControl<TextBox>("newCIF");
        var direccion = this.FindControl<TextBox>("newDireccion");
        
        Cliente c = new Cliente(cif.Text, nombre.Text, direccion.Text);
        
        this.clientes.AñadirCliente(c);
        parentWindow._storage.saveStoreContext();

        UpdateView();
    }

    void BuscarCliente(String nombre)
    {
        if (!nombre.Equals(""))
        {
            List<Cliente> auxiliar = this.clientes.buscarClienteNombre(nombre);
            this.ListaClientes = auxiliar;
            ItemsControl.ItemsSource = this.ListaClientes;
        }else{
         UpdateView();   
        }
        
    }

    void EliminarCliente(object? sender, RoutedEventArgs routedEventArgs)
    {
        Clientes auxiliar = new Clientes(this.clientes.ListaClientes);
        Button boton = (Button)sender;
        this.clientes.EliminarCliente(boton.Tag.ToString());
        parentWindow._storage.saveStoreContext();
        
        UpdateView();
    }

    void UpdateView()
    {
        this.ListaClientes = clientes.ListaClientes.ToList();
        ItemsControl.ItemsSource = this.ListaClientes;
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