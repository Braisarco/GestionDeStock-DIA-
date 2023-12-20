using System;
using System.IO;
using System.Xml.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using UI.core.gestionProveedores;

namespace UI.views.vistaProveedores;

public partial class MainWindowProveedores : UserControl
{
    private const string Filename = "proveedores.xml";
    public Proveedores _proveedores;
    private MainWindow parentWindow;
        
    public MainWindowProveedores(MainWindow windowParent)
    {
        _proveedores = windowParent._storage.Proveedores;
        parentWindow = windowParent;
        InitializeComponent();

        //CargarDatosEjemplo(12, 3);
        
        LbListProveedores.SelectionChanged += (_, _) => PrintProveedor();
        BtModoAddProveedor.Click += (_, _) => ModoAddProveedor();
        
        BtModoEditarProveedor.Click += (_, _) => ModoEditarProveedor();
        BtEliminarProveedor.Click += (_, _) => EliminarProveedor();
        BtAddProveedor.Click += (_, _) => AddProveedor();
        BtEditarProveedor.Click += (_, _) => EditarProveedor();
        
        BtAddPieza.Click += (_, _) => AddPieza();
        BtEliminarPieza.Click += (_, _) => EliminarPieza();
        
        PrintListProveedores();
        //PrintProveedor();
        LbListProveedores.SelectedIndex = 0;
    }


    
    // === MODOS DE VISTA DE PROVEEDOR ===
    
    private void ModoNormalProveedor()
    {
        TbCIF.IsReadOnly = true;
        TbNombre.IsReadOnly = true;
        TbDireccionFacturacion.IsReadOnly = true;
        PanelPiezas.IsVisible = true;
        
        PanelCambiarCampos.IsVisible = false;
        BtAddProveedor.IsVisible = false;
        BtEditarProveedor.IsVisible = false;
    }
    
    private void ModoAddProveedor()
    {
        TbCIF.Text = "";
        TbNombre.Text = "";
        TbDireccionFacturacion.Text = "";
        
        TbCIF.IsReadOnly = false;
        TbNombre.IsReadOnly = false;
        TbDireccionFacturacion.IsReadOnly = false;
        PanelPiezas.IsVisible = false;
        
        PanelCambiarCampos.IsVisible = true;
        BtAddProveedor.IsVisible = true;
        BtEditarProveedor.IsVisible = false;
    }
    
    private void ModoEditarProveedor()
    {
        TbCIF.IsReadOnly = false;
        TbNombre.IsReadOnly = false;
        TbDireccionFacturacion.IsReadOnly = false;
        PanelPiezas.IsVisible = false;
        
        PanelCambiarCampos.IsVisible = true;
        BtAddProveedor.IsVisible = false;
        BtEditarProveedor.IsVisible = true;
    }
    
    
    
    // === CAMBIAR PROVEEDORES ===

    private void AddProveedor()
    {
        try
        {
            var proveedor = new Proveedor(TbCIF.Text, TbNombre.Text, TbDireccionFacturacion.Text);
            _proveedores.AddProveedor(proveedor);
            LbListProveedores.SelectedIndex = _proveedores.NumProveedores() - 1;
            PrintProveedor();
            PrintListProveedores();
            parentWindow._storage.saveStoreContext();
        }
        catch (Exception) {}
    }
    
    private void EditarProveedor()
    {
        int index = LbListProveedores.SelectedIndex;
        if (index < 0 || index > _proveedores.NumProveedores() - 1) { return; }
        
        var proveedor = _proveedores.Get(index);

        if (!TbCIF.Text.Equals("") && !TbNombre.Text.Equals("") && !TbDireccionFacturacion.Text.Equals(""))
        {
            proveedor.CIF = TbCIF.Text;
            proveedor.Nombre = TbNombre.Text;
            proveedor.DireccionFacturacion = TbDireccionFacturacion.Text;
            PrintListProveedores();
            LbListProveedores.SelectedIndex = index;
        }
        PrintListProveedores();
    }
    
    private void EliminarProveedor()
    {
        if (_proveedores.NumProveedores() > 0) {
            _proveedores.EliminarProveedor(LbListProveedores.SelectedIndex);
            --LbListProveedores.SelectedIndex;
        }
        
        PrintListProveedores();
        PrintProveedor();
        parentWindow._storage.saveStoreContext();
    }
    
    
    
    // === MOSTRAR PROVEEDORES ===
    
    public void PrintProveedor()
    {
        ModoNormalProveedor();
        var proveedor = _proveedores.Get(LbListProveedores.SelectedIndex);

        if (proveedor is null) {
            ModoAddProveedor();
        } else {
            TbCIF.Text = proveedor.CIF;
            TbNombre.Text = proveedor.Nombre;
            TbDireccionFacturacion.Text = proveedor.DireccionFacturacion;
            
            LbPiezasProvistas.Items.Clear();
            foreach (var pieza in proveedor.PiezasProvistas())
            {
                LbPiezasProvistas.Items.Add(pieza) ;
            }
            LbNumPiezasProvistas.Content = "Piezas Provistas: " + proveedor.NumPiezas();
            
            LbPiezasNoProvistas.Items.Clear();

            for (int i = 0; i < _proveedores.NumProveedores(); i++) {
                foreach (var pieza in _proveedores.Get(i).PiezasProvistas())  {
                    if (!proveedor.TienePieza(pieza)) {
                        LbPiezasNoProvistas.Items.Add(pieza);
                    }
                }
            }
            LbNumPiezasNoProvsitas.Content = "Piezas No Provistas: " + LbPiezasNoProvistas.ItemCount;

        }
        LbNumProveedores.Content = "Número de proveedores: " + _proveedores.NumProveedores();
    }
    
    
    
    private void PrintListProveedores()
    {
        LbListProveedores.Items.Clear();
        foreach (var proveedor in _proveedores.Lista())
        {
            LbListProveedores.Items.Add(proveedor.Nombre) ;
        }
        LbNumProveedores.Content = "Número de proveedores: " + _proveedores.NumProveedores();
        LbListProveedores.SelectedIndex = 0;
    }
    
    
    
    // === PIEZAS ===

    private void AddPieza()
    {
        var proveedor = _proveedores.Get(LbListProveedores.SelectedIndex);
        int index = Convert.ToInt32(LbPiezasNoProvistas.SelectedIndex);

        if (index >= 0 && index < LbPiezasNoProvistas.ItemCount)
        {
            proveedor.AddPieza(Convert.ToInt32(LbPiezasNoProvistas.SelectedItem));
            parentWindow._storage.saveStoreContext();
            PrintProveedor();    
        }
    }
    
    
    private void EliminarPieza()
    {
        var proveedor = _proveedores.Get(LbListProveedores.SelectedIndex);
        int index = Convert.ToInt32(LbPiezasProvistas.SelectedIndex);
        
        if (index >= 0 && index < LbPiezasProvistas.ItemCount)
        {
            proveedor.EliminarPieza(index);
            parentWindow._storage.saveStoreContext();
            PrintProveedor();    
        }
    }
    
    
    
    // === XML ===
    
    private void GuardarXML()
    {
        XElement xProveedores = _proveedores.ToXElement();
        //xProveedores.Save(Filename);
    }
    
    private void CargarXML()
    {
        if (File.Exists(Filename)) { 
            var xProveedores = XElement.Load(Filename);
            _proveedores = new Proveedores(xProveedores);    
        }
    }

    
    
    // === EJEMPLO ===
    
    private void CargarDatosEjemplo(int numProveedores, int numPiezas)
    {
        for (int i = 0; i < numProveedores; i++)
        {
            String cif = "A" + i.ToString().PadLeft(8, '0'); //A00000001
            String nombre = "Proveedor " + i;
            String calle = "Calle " + i;
            Proveedor proveedor = new Proveedor(cif, nombre, calle);

            for (int j = 0; j < numPiezas; j++)
            {
                proveedor.AddPieza(1000 + 100 * i + j); //1001, 1002, 1003, 1004
            }
            _proveedores.AddProveedor(proveedor);
        }
        parentWindow._storage.saveStoreContext();
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