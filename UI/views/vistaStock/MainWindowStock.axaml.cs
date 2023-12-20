using System.IO;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core;
using UI.core.gestionPiezas;

namespace UI.views.vistaStock;

public partial class MainWindowStock : UserControl
{
    private const string Filename = "piezas.xml";
    private MainWindow parentWindow;
    public Piezas _piezas;
    public int _posPieza = 0;
    private int _posProveedor = 0;
    public MainWindowStock(MainWindow windowParent)
    {
        parentWindow = windowParent;
        _piezas = windowParent._storage.Piezas;
        
        InitializeComponent();
        NudPieza.ValueChanged += (_, _) => PiezaCambiado();

        BtVentanaAddPieza.Click += (_, _) => VentanaAddPieza();
        BtVentanaEditarPieza.Click += (_, _) => VentanaEditarPieza();
        BtEliminarPieza.Click += (_, _) => EliminarPieza();
        
        PrintPieza();
        PrintPiezasList();
        PiezaCambiado();
    }
    
    public void PrintPieza()
    {
        var pieza = _piezas.Get(_posPieza);

        if (pieza == null) {
            TbCodigo.Text = "";
            TbNombre.Text = "";
            TbUnidades.Text = "";
            LbNumProveedores.Content = "Proveedores Piezas: 0";

        } else {
            TbCodigo.Text = pieza.Codigo;
            TbNombre.Text = pieza.Nombre;
            TbUnidades.Text = pieza.Unidades.ToString();
            
            LbProveedoresPiezas.Items.Clear();
            foreach (var proveedor in pieza.ProveedoresPieza())
            {
                LbProveedoresPiezas.Items.Add(proveedor) ;
            }
            LbNumProveedores.Content = "Proveedores Piezas: " + pieza.NumProveedores();
            ListaDePiezas.SelectedIndex = _posPieza;
            
        }
        LbNumPiezas.Content = _piezas.NumPiezas().ToString();
        LbNumPiezasLista.Content = "Piezas: " + _piezas.NumPiezas();
    }
    private void PrintPiezasList()
    {
        ListaDePiezas.Items.Clear(); // Limpiar antes de agregar elementos

        foreach (var pieza in _piezas.Lista())
        {
            // Crear un nuevo elemento ListBoxItem y asignar el contenido
            var listBoxItem = new ListBoxItem();
            listBoxItem.Content = $"{pieza.Codigo} - {pieza.Nombre}";

            // Agregar el elemento ListBoxItem al ListBox
            ListaDePiezas.Items.Add(listBoxItem);
        }

        LbNumPiezasLista.Content = $"Piezas: {_piezas.NumPiezas()}";
    }
    private void ListaDePiezas_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        _posPieza = ListaDePiezas.SelectedIndex;
        var pieza = _piezas.Get(_posPieza);
        PrintPieza();
       // TbUnidades.Text = pieza.Unidades.ToString();
    }
    
    
    
    // === Ventanas ===

    private void VentanaAddPieza()
    {
        new PiezaWindow(this, -1).ShowDialog(parentWindow);
    }

    public void AddPieza(Pieza pieza)
    {
        _piezas.AddPieza(pieza);
        NudPieza.Value = _piezas.NumPiezas();
        
        PrintPieza();
        PrintPiezasList();
        parentWindow._storage.saveStoreContext();
    }
    
    private void VentanaEditarPieza()
    {
        new PiezaWindow(this, _posPieza).ShowDialog(parentWindow);
    }
    
    public void EditarPieza(int pos, Pieza pieza)
    {
        _piezas.EditarPieza(pos, pieza);
        PrintPieza();
        PrintPiezasList();
        parentWindow._storage.saveStoreContext();
    }
    
    private void EliminarPieza()
    {
        if (_piezas.NumPiezas() > 0) {
            _piezas.EliminarPieza(_posPieza);
        }

        if (_posPieza > _piezas.NumPiezas() - 1) {
            --NudPieza.Value;
        }
        
        PrintPieza();
        PrintPiezasList();
        parentWindow._storage.saveStoreContext();
    } 
    
    
    
    // === NumericUpDown ===
    
    private void PiezaCambiado()
    {
        if (NudPieza.Value == null || NudPieza.Value < 1 ||
                NudPieza.Value > _piezas.NumPiezas()) {
            
            NudPieza.Value = 1;
        }
        else
        {
            _posPieza = (int)(NudPieza.Value) - 1;
            PrintPieza();
        }
    }
    
    
    
    // === XML ===
    
    private void GuardarXML()
    {
        XElement xPiezas = _piezas.toXML();
        xPiezas.Save(Filename);
    }
    
    private void CargarXML()
    {
        if (File.Exists(Filename)) { 
            var xPiezas = XElement.Load(Filename);
            _piezas = new Piezas(xPiezas);    
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