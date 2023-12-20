using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core.gestionCompras;
using UI.core.gestionPiezas;
using UI.core.gestionProveedores;

namespace UI.views.vistaPedidos.ComprasProveedores;

public partial class MainWindowComprarStock : UserControl
{
    
    private MainWindow parentWindow;
    public Piezas _piezas;
    public Proveedores _proveedores;
    public GestionCompra _compras;
    public int _posPieza = 0;
    private int _posProveedor = 0;
    private Proveedor selectedProveedor = new Proveedor();
    private Pieza selectedPieza = new Pieza();
    
    public MainWindowComprarStock(MainWindow windowParent)
    {
        parentWindow = windowParent;
        
        _compras = parentWindow._storage.Compras;
        _piezas = parentWindow._storage.Piezas;
        _proveedores = parentWindow._storage.Proveedores;
        
        InitializeComponent();
        PrintProveedoresList();
        PrintPiezas();
        this.BtnHacerPedido.Click += (_,_) => this.onHacerPedido();
    }
    
    //LbProeevoresAsociados a LbClientesDisponibles
    private void LbProeevoresAsociados_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (LbProveedoresAsociados.SelectedItem != null)
        {
            selectedProveedor = _proveedores.Get(LbProveedoresAsociados.SelectedIndex);
        }
    }
    
    private void onHacerPedido()
    {
        var tbCantidadPedido = this.FindControl<TextBox>("TbCantidadPedido");
        if (tbCantidadPedido != null && int.TryParse(tbCantidadPedido.Text, out int cantidadPedida))
        {
            Pieza piezaSeleccionada = _piezas.Get(LbPiezasDisponibles.SelectedIndex);
            
            _compras.Add(new Compra(selectedProveedor ,DateOnly.FromDateTime(DateTime.Now), selectedPieza ,cantidadPedida));
            piezaSeleccionada.Unidades += cantidadPedida;
            parentWindow._storage.saveStoreContext();
        }
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
    
   
    
    private void PrintProveedoresList()
    {
        LbProveedoresAsociados.Items.Clear(); // Limpiar antes de agregar elementos

        foreach (var proveedor in _proveedores.Lista())
        {
            // Crear un nuevo elemento ListBoxItem y asignar el contenido
            var listBoxItem = new ListBoxItem();
            listBoxItem.Content = $"{proveedor.CIF} - {proveedor.Nombre}- {proveedor.DireccionFacturacion}";

            // Agregar el elemento ListBoxItem al ListBox
            LbProveedoresAsociados.Items.Add(listBoxItem);
        }

        LbProveedores.Content = $"Proveedores: {_proveedores.NumProveedores()}";
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
