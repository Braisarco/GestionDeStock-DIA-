using System.IO;
using System.Xml.Linq;
using Avalonia.Controls;
using UI.core.gestionProveedores;

namespace UI.views.Proveedores;

public partial class MainWindow : Window
{
    private const string Filename = "proveedores.xml";
    public Proveedores _proveedores = new Proveedores();
    public int _posProveedor = 0;
    private int _posPieza = 0;
        
    public MainWindow()
    {
        InitializeComponent();
        
        /*Proveedor p1 = new Proveedor("A12345678", "Pepe", "Calle 123");
        p1.AddPiezas( new int[] {123, 456, 789, 111});
        
        Proveedor p2 = new Proveedor("B87654321", "Paco", "Calle 456");
        p2.AddPiezas( new int[] {444, 555, 8888, 999});
        
        _proveedores.AddProveedor(p1);
        _proveedores.AddProveedor(p2);
        
        GuardarXML();*/
        
        NudProveedor.ValueChanged += (_, _) => ProveedorCambiado();

        BtVentanaAddProveedor.Click += (_, _) => VentanaAddProveedor();
        BtVentanaEditarProveedor.Click += (_, _) => VentanaEditarProveedor();
        BtEliminarProveedor.Click += (_, _) => EliminarProveedor();
        
        CargarXML();
        PrintProveedor();
        ProveedorCambiado();
    }

    public void PrintProveedor()
    {
        var proveedor = _proveedores.Get(_posProveedor);

        if (proveedor == null) {
            TbCIF.Text = "";
            TbNombre.Text = "";
            TbDireccionFacturacion.Text = "";
            LbNumPiezas.Content = "Piezas Provistas: 0";

        } else {
            TbCIF.Text = proveedor.CIF;
            TbNombre.Text = proveedor.Nombre;
            TbDireccionFacturacion.Text = proveedor.DireccionFacturacion;
            
            LbPiezasProvistas.Items.Clear();
            foreach (var pieza in proveedor.PiezasProvistas())
            {
                LbPiezasProvistas.Items.Add(pieza) ;
            }
            LbNumPiezas.Content = "Piezas Provistas: " + proveedor.NumPiezas();
            
        }
        LbNumProveedores.Content = _proveedores.NumProveedores().ToString();
    }
    
    
    
    // === Ventanas ===

    private void VentanaAddProveedor()
    {
        new ProveedorWindow(this, -1).ShowDialog(this);
    }

    public void AddProveedor(Proveedor proveedor)
    {
        _proveedores.AddProveedor(proveedor);
        NudProveedor.Value = _proveedores.NumProveedores();
        
        PrintProveedor();
        GuardarXML();
    }
    
    private void VentanaEditarProveedor()
    {
        new ProveedorWindow(this, _posProveedor).ShowDialog(this);
    }
    
    public void EditarProveedor(int pos, Proveedor proveedor)
    {
        _proveedores.EditarProveedor(pos, proveedor);
        PrintProveedor();
        GuardarXML();
    }
    
    private void EliminarProveedor()
    {
        if (_proveedores.NumProveedores() > 0) {
            _proveedores.EliminarProveedor(_posProveedor);
        }

        if (_posProveedor > _proveedores.NumProveedores() - 1) {
            --NudProveedor.Value;
        }
        
        PrintProveedor();
        GuardarXML();
    } 
    
    
    
    // === NumericUpDown ===
    
    private void ProveedorCambiado()
    {
        if (NudProveedor.Value == null || NudProveedor.Value < 1 ||
                NudProveedor.Value > _proveedores.NumProveedores()) {
            
            NudProveedor.Value = 1;
        }
        else
        {
            _posProveedor = (int)(NudProveedor.Value) - 1;
            PrintProveedor();
        }
    }
    
    
    
    // === XML ===
    
    private void GuardarXML()
    {
        XElement xProveedores = _proveedores.ToXElement();
        xProveedores.Save(Filename);
    }
    
    private void CargarXML()
    {
        if (File.Exists(Filename)) { 
            var xProveedores = XElement.Load(Filename);
            _proveedores = new Proveedores(xProveedores);    
        }
    }
}