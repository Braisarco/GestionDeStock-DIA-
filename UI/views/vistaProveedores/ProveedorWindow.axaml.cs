using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Avalonia.Controls;
using UI.core.gestionProveedores;

namespace UI.views.vistaProveedores;

public partial class ProveedorWindow : Window
{
    private MainWindow _main;
    private int _posProveedor;
    private List<int> _listaPiezas = new List<int>();
    private int _posPieza;

    public ProveedorWindow(MainWindow main, int posProveedor)
    {
        InitializeComponent();

        _main = main;
        _posProveedor = posProveedor;
        _posPieza = 1;
        
        NudPiezasProvistas.ValueChanged += (_, _) => PiezaCambiada();
        BtEliminarPieza.Click += (_, _) => EliminarPieza();
        BtAddPieza.Click += (_, _) => AddPieza();

        if (posProveedor == -1) {
            BtProveedor.Click += (_, _) => AddProveedor();
            PiezasPanel.IsVisible = false;
        }
        else
        {
            Proveedor proveedor = main._proveedores.Get(posProveedor);
            
            TbCIF.Text = proveedor.CIF;
            TbNombre.Text = proveedor.Nombre;
            TbDireccionFacturacion.Text = proveedor.DireccionFacturacion;

            _listaPiezas = proveedor.PiezasProvistas();
            
            PiezaCambiada();
            
            BtProveedor.Content = "Editar proveedor";
            BtProveedor.Click += (_, _) => EditarProveedor();
            
        }
    }

    private void PrintPiezas()
    {
        if (_posPieza > _listaPiezas.Count - 1) {
            --NudPiezasProvistas.Value;
        }
        
        if (_listaPiezas.Count > 0) {
            TbPiezaSeleccionada.Text = _listaPiezas[_posPieza].ToString();
        } else {
            TbPiezaSeleccionada.Text = "";
        }
        TbNuevaPieza.Text = "";
        LbNumPiezas.Content = _listaPiezas.Count;
    }
    
    
    
    // === Volver a main === 
    private void AddProveedor()
    {
        Proveedor proveedor = new Proveedor(TbCIF.Text, TbNombre.Text, TbDireccionFacturacion.Text);
        
        _main.AddProveedor(proveedor);
        this.Close();
    }
    
    private void EditarProveedor()
    {
        Proveedor proveedor = new Proveedor(TbCIF.Text, TbNombre.Text, TbDireccionFacturacion.Text);
        proveedor.AddPiezas(_listaPiezas);
        _main.EditarProveedor(_posProveedor, proveedor);
        this.Close();
    }

    
    
    // === NumericUpDown ===
    
    private void PiezaCambiada()
    {
        if (NudPiezasProvistas.Value == null || NudPiezasProvistas.Value < 1 || 
            NudPiezasProvistas.Value > _listaPiezas.Count()) {
            
            NudPiezasProvistas.Value = 1;
        }
        else
        {
            _posPieza = (int)(NudPiezasProvistas.Value) - 1;
            PrintPiezas();
        }
    }
    
    
    
    // === Piezas ===
    
    private void AddPieza()
    {
        try
        {
            _listaPiezas.Add(Convert.ToInt32(TbNuevaPieza.Text));
            NudPiezasProvistas.Value = _listaPiezas.Count();
            PrintPiezas();
        }
        catch (System.FormatException) { }
        finally
        {
            TbNuevaPieza.Text = "";
            
            PrintPiezas();
        }
    }
    
    
    
    private void EliminarPieza()
    {
        if (_listaPiezas.Count() > 0) {
            _listaPiezas.RemoveAt(_posPieza);
        }
        
        PrintPiezas();
    }
}