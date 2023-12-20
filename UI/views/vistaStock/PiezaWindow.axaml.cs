using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using UI.core;
using UI.core.gestionPiezas;

namespace UI.views.vistaStock;

public partial class PiezaWindow : Window
{
    private MainWindowStock _main;
    private int _posPieza;
    private List<string> _listaProveedores = new List<string>();
    private int _posProveedor;

    public PiezaWindow(MainWindowStock main, int posPieza)
    {
        InitializeComponent();

        _main = main;
        _posPieza = posPieza;
        _posProveedor = 1;
        
        NudProveedorPieza.ValueChanged += (_, _) => ProveedorCambiada();
        BtEliminarProveedor.Click += (_, _) => EliminarProveedor();
        BtAddProveedor.Click += (_, _) => AddProveedor();

        if (posPieza == -1) {
            BtPieza.Click += (_, _) => AddPieza();
            ProveedoresPanel.IsVisible = false;
        }
        else
        {
            Pieza pieza = main._piezas.Get(posPieza);
            
            TbCodigo.Text = pieza.Codigo;
            TbNombre.Text = pieza.Nombre;
            TbUnidades.Text = pieza.Unidades.ToString();

            _listaProveedores = pieza.ProveedoresPieza();
            
            ProveedorCambiada();
            
            BtPieza.Content = "Editar pieza";
            BtPieza.Click += (_, _) => EditarPieza();
            
        }
    }

    private void PrintProveedores()
    {
        if (_posProveedor > _listaProveedores.Count - 1) {
            --NudProveedorPieza.Value;
        }
        
        if (_listaProveedores.Count > 0) {
            TbProveedorSeleccionada.Text = _listaProveedores[_posProveedor];
        } else {
            TbProveedorSeleccionada.Text = "";
        }
        TbNuevoProveedor.Text = "";
        LbNumProveedores.Content = _listaProveedores.Count;
    }
    
    
    
    // === Volver a main === 
    private void AddPieza()
    {
        Pieza pieza = new Pieza(TbNombre.Text, Convert.ToInt32(TbUnidades.Text), TbCodigo.Text);
        
        _main.AddPieza(pieza);
        this.Close();
    }
    
    private void EditarPieza()
    {
        Pieza pieza = new Pieza(TbNombre.Text, Convert.ToInt32(TbUnidades.Text), TbCodigo.Text);
        pieza.AddProveedores(_listaProveedores);
        _main.EditarPieza(_posPieza, pieza);
        this.Close();
    }

    
    
    // === NumericUpDown ===
    
    private void ProveedorCambiada()
    {
        if (NudProveedorPieza.Value == null || NudProveedorPieza.Value < 1 || 
            NudProveedorPieza.Value > _listaProveedores.Count()) {
            
            NudProveedorPieza.Value = 1;
        }
        else
        {
            _posProveedor = (int)(NudProveedorPieza.Value) - 1;
            PrintProveedores();
        }
    }
    
    
    
    // === Proveedores ===
    
    private void AddProveedor()
    {
        try
        {
            _listaProveedores.Add(TbNuevoProveedor.Text);
            NudProveedorPieza.Value = _listaProveedores.Count();
            PrintProveedores();
        }
        catch (System.FormatException) { }
        finally
        {
            TbNuevoProveedor.Text = "";
            
            PrintProveedores();
        }
    }
    
    
    
    private void EliminarProveedor()
    {
        if (_listaProveedores.Count() > 0) {
            _listaProveedores.RemoveAt(_posPieza);
        }
        
        PrintProveedores();
    }
}