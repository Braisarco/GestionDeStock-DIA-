using System;
using System.IO;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core.gestionClientes;
using UI.core.gestionPedidos;
using UI.core.gestionPiezas;

namespace UI.views.vistaPedidos.VentasClientes;

public partial class MainWindowPedidos : UserControl
{
    private MainWindow parentWindow;
    public Piezas _piezas;
    public Clientes _clientes;
    public Pedidos _pedidos;
    public int _posPieza = 0;
    private int _posProveedor = 0;
    private Cliente selectedClient = new Cliente();
    private Pieza selectedPieza = new Pieza();
    
    public MainWindowPedidos(MainWindow windowParent)
    {
        parentWindow = windowParent;
        
        _pedidos = parentWindow._storage.Pedidos;
        _piezas = parentWindow._storage.Piezas;
        _clientes = parentWindow._storage.Clientes;
        
        InitializeComponent();
        PrintClientesList();
        PrintPiezas();
        this.BtnHacerPedido.Click += (_,_) => this.onHacerPedido();
    }
    
    //LbProeevoresAsociados a LbClientesDisponibles
    private void LbClientesDisponibles_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LbClientesDisponibles.SelectedItem != null)
        {
            selectedClient = _clientes.Get(LbClientesDisponibles.SelectedIndex);
        }
    }
    
    private void onHacerPedido()
    {
        var tbCantidadPedido = this.FindControl<TextBox>("TbCantidadPedido");
        if (tbCantidadPedido != null && int.TryParse(tbCantidadPedido.Text, out int cantidadPedida))
        {
            Pieza piezaSeleccionada = _piezas.Get(LbPiezasDisponibles.SelectedIndex);

            if (cantidadPedida <= piezaSeleccionada.Unidades)
            {
                _pedidos.Lista().Add(new Pedido((_pedidos.Lista().Count + 1),selectedClient,DateOnly.FromDateTime(DateTime.Now)
                    , selectedPieza ,cantidadPedida));
                selectedClient.CodigoPiezasVendidas.Add(selectedPieza.Codigo);
                piezaSeleccionada.Unidades -= cantidadPedida;
                parentWindow._storage.saveStoreContext();
            }
            else
            {
                MostrarVentanaNotificacion();
            }
        }
    }
    
    async void MostrarVentanaNotificacion()
    {
        var dlgVentanaFecha = new DlgVentanaFecha();
        await dlgVentanaFecha.ShowDialog(parentWindow);
    }
    
    private void LbPiezasDisponibles_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _posPieza = LbPiezasDisponibles.SelectedIndex;
        selectedPieza = _piezas.Get(_posPieza);

        // Mostrar la cantidad de piezas disponibles
        MostrarCantidadPiezas(selectedPieza.Unidades);
    }

    private void MostrarCantidadPiezas(int piezaSeleccionadaUnidades)
    {
        var tbCantidadPiezas = this.FindControl<TextBox>("TbCantidadPiezas"); // Asegúrate de tener un TextBox con este nombre en tu interfaz
        if (tbCantidadPiezas != null)
        {
            tbCantidadPiezas.Text = piezaSeleccionadaUnidades.ToString();
        }
    }
    
    public void PrintPiezas()
    {
        LbPiezasDisponibles.Items.Clear(); // Limpiar antes de agregar elementos

        foreach (var pieza in _piezas.Lista())
        {
            // Crear un nuevo elemento ListBoxItem y asignar el contenido
            var listBoxItem = new ListBoxItem();
            listBoxItem.Content = $"{pieza.Codigo} - {pieza.Nombre}";

            // Agregar el elemento ListBoxItem al ListBox
            LbPiezasDisponibles.Items.Add(listBoxItem);
        }

        LbPieza.Content = $"Piezas: {_piezas.NumPiezas()}";
    }
    
   
    
    private void PrintClientesList()
    {
        LbClientesDisponibles.Items.Clear(); // Limpiar antes de agregar elementos

        foreach (var cliente in _clientes.ListaClientes)
        {
            // Crear un nuevo elemento ListBoxItem y asignar el contenido
            var listBoxItem = new ListBoxItem();
            listBoxItem.Content = $"{cliente.CIF} - {cliente.Nombre}- {cliente.DireccionFacturacion}";

            // Agregar el elemento ListBoxItem al ListBox
            LbClientesDisponibles.Items.Add(listBoxItem);
        }

        LbClientes.Content = $"Clientes: {_clientes.Count()}";
    }
    // === XML ===
    
    // private void GuardarXML()
    // {
    //     XElement xPiezas = _piezas.toXML();
    //     xPiezas.Save(Filename);
    // }
    //
    // private void CargarXML()
    // {
    //     if (File.Exists(Filename)) { 
    //         var xPiezas = XElement.Load(Filename);
    //         _piezas = new Piezas(xPiezas);    
    //     }
    // }
    
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
