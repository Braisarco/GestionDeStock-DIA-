using System.IO;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core.gestionClientes;
using UI.core.gestionPiezas;

namespace UI.views.vistaPedidos.VentasClientes;

public partial class MainWindowPedidos : UserControl
{
    private MainWindow parentWindow;
    public Piezas _piezas;
    public Clientes _clientes;
    public int _posPieza = 0;
    private int _posProveedor = 0;
    
    public MainWindowPedidos(MainWindow windowParent)
    {
        parentWindow = windowParent;
        _piezas = parentWindow._storage.Piezas;
        _clientes = parentWindow._storage.Clientes;
        InitializeComponent();
        PrintClientesList();
        PrintPieza();
    }
    
    //LbProeevoresAsociados a LbClientesDisponibles
    private void LbClientesDisponibles_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LbClientesDisponibles.SelectedItem != null)
        {
            // Mostrar TextBox para introducir la cantidad de piezas a pedir
            MostrarTextBoxCantidad();
        }
    }

    private void MostrarTextBoxCantidad()
    {
        var tbCantidadPedido = new TextBox();
        tbCantidadPedido.Name = "TbCantidadPedido"; // Asigna un nombre único



        // Configurar el botón para hacer el pedido
        var BtnHacerPedido = this.FindControl<Button>("BtnHacerPedido");
        this.BtnHacerPedido!.Click += (_,_) => this.onHacerPedido();

        var stackPanelContenedor = this.FindControl<StackPanel>("StackPanelContenedor");

        if (stackPanelContenedor != null)
        {
            stackPanelContenedor.Children.Add(tbCantidadPedido);
        }
    }
    
    private void onHacerPedido()
    {
        var tbCantidadPedido = this.FindControl<TextBox>("TbCantidadPedido");
        if (tbCantidadPedido != null && int.TryParse(tbCantidadPedido.Text, out int cantidadPedida))
        {
            var piezaSeleccionada = (Pieza)LbPiezasDisponibles.SelectedItem!;

            if (cantidadPedida <= piezaSeleccionada.Unidades)
            {
                piezaSeleccionada.Unidades += cantidadPedida;
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
        PrintPieza();
        var piezaSeleccionada = (Pieza)LbPiezasDisponibles.SelectedItem;

        // Mostrar la cantidad de piezas disponibles
        MostrarCantidadPiezas(piezaSeleccionada.Unidades);
    }

    private void MostrarCantidadPiezas(int piezaSeleccionadaUnidades)
    {
        var tbCantidadPiezas = this.FindControl<TextBox>("TbCantidadPiezas"); // Asegúrate de tener un TextBox con este nombre en tu interfaz
        if (tbCantidadPiezas != null)
        {
            tbCantidadPiezas.Text = piezaSeleccionadaUnidades.ToString();
        }
    }
    
    private void BtnHacerPedido_Click(object sender, RoutedEventArgs e)
    {
        var tbCantidadPedido = this.FindControl<TextBox>("TbCantidadPedido");
        if (tbCantidadPedido != null && int.TryParse(tbCantidadPedido.Text, out int cantidadPedida))
        {
            var piezaSeleccionada = (Pieza)LbPiezasDisponibles.SelectedItem!;

            if (cantidadPedida <= piezaSeleccionada.Unidades)
            {
                 piezaSeleccionada.Unidades += cantidadPedida;
            }
        }
    }
    

    public void PrintPieza()
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
