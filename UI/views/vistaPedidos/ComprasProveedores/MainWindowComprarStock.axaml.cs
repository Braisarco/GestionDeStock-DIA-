using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core.gestionPiezas;

namespace UI.views.vistaPedidos.ComprasProveedores;

public partial class MainWindowComprarStock : UserControl
{
    
    private const string Filename = "piezas.xml";
    private MainWindow parentWindow;
    private Piezas Piezas;
    public int PosPieza;
    
    public MainWindowComprarStock(MainWindow windowParent)
    {
        parentWindow = windowParent;
        Piezas = parentWindow._storage.Piezas;
        
        InitializeComponent();
        
        PrintPiezasList();
    }
    private void LbPiezasDisponibles_SelectionChanged(object? sender, SelectionChangedEventArgs selectionChangedEventArgs)
    {
        PosPieza = LbPiezasDisponibles.SelectedIndex;
        PrintPieza();
        var piezaSeleccionada = (Pieza)LbPiezasDisponibles.SelectedItem!;

        // Mostrar la cantidad de piezas disponibles
        MostrarCantidadPiezas(piezaSeleccionada.Unidades);
    }

    private void MostrarCantidadPiezas(int piezaSeleccionadaUnidades)
    {
        var tbCantidadPiezas = this.FindControl<TextBox>("TbCantidadPiezas"); 
        if (tbCantidadPiezas != null)
        {
            tbCantidadPiezas.Text = piezaSeleccionadaUnidades.ToString();
        }
    }

    private void LbProeevoresAsociados_SelectionChanged(object? sender, SelectionChangedEventArgs selectionChangedEventArgs)
    {
        if (LbProveedoresAsociados.SelectedItem != null)
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
        var btnHacerPedido = this.FindControl<Button>("btnHacerPedido");
        this.btnHacerPedido!.Click += (_, _) => this.onHacerPedido();
        
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


    public void PrintPieza()
    {
        var pieza = Piezas.Get(PosPieza);

        
            
            LbProveedoresAsociados.Items.Clear();
            foreach (var proveedor in pieza.ProveedoresPieza())
            {
                LbProveedoresAsociados.Items.Add(proveedor) ;
            }
            LbProveedores.Content = "Proveedores Piezas: " + pieza.NumProveedores();
            LbPiezasDisponibles.SelectedIndex = PosPieza;
       
        LbPieza.Content = "Piezas: " + Piezas.NumPiezas();
    }
    
   
    
    private void PrintPiezasList()
    {
        LbPiezasDisponibles.Items.Clear(); // Limpiar antes de agregar elementos

        foreach (var pieza in Piezas.Lista())
        {
            // Crear un nuevo elemento ListBoxItem y asignar el contenido
            var listBoxItem = new ListBoxItem();
            listBoxItem.Content = $"{pieza.Codigo} - {pieza.Nombre}";

            // Agregar el elemento ListBoxItem al ListBox
            LbPiezasDisponibles.Items.Add(listBoxItem);
        }

        LbPieza.Content = $"Piezas: {Piezas.NumPiezas()}";
    }
    // === XML ===
    
    private void GuardarXML()
    {
        XElement xPiezas = Piezas.toXML();
        xPiezas.Save(Filename);
    }
    
    private void CargarXml()
    {
        if (File.Exists(Filename)) { 
            var xPiezas = XElement.Load(Filename);
            Piezas = new Piezas(xPiezas);    
        }
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
