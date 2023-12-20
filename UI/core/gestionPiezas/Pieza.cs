using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UI.core.gestionProveedores;

namespace UI.core.gestionPiezas;
public class Pieza
{
    private List<string> _proveedoresPieza;
    
    public Pieza(string nombre, int unidades, string codigo)
    {
        this.Codigo = codigo;
        this.Nombre = nombre;
        this.Unidades = unidades;
        this._proveedoresPieza = new List<string>();
    }

    public Pieza()
    {
        this.Codigo = "";
        this.Nombre = "";
        this.Unidades = 0;
        this._proveedoresPieza = new List<string>();
    }

    public string Nombre{ get; set; }
    
    public string Codigo { get; set; }
    
    public int Unidades { get; set; }

    public List<string> ProveedoresPieza()
    {
        return new List<string>(_proveedoresPieza);
    }

    public void AddProveedor(string proveedor)
    {
        _proveedoresPieza.Add(proveedor);
    }
    
    public void AddProveedores(IEnumerable<string> proveedor)
    {
        _proveedoresPieza.AddRange(proveedor);
    }

    public string Get(int pos)
    {
        if (pos >= 0 && pos < _proveedoresPieza.Count) {
            return _proveedoresPieza[pos];    
        }
        return "";
    }
    public bool TieneProveedor(string proveedor)
    {
        return _proveedoresPieza.Contains(proveedor);
    }
    
    public void EliminarProveedor(int pos)
    {
        _proveedoresPieza.RemoveAt(pos);
    }

    public int NumProveedores()
    {
        return _proveedoresPieza.Count;
    }

    public XElement ToXElement()
    {
        var toret = new XElement("pieza");
        toret.Add(new XElement("codigo", this.Codigo));
        toret.Add(new XElement("nombre", this.Nombre));
        toret.Add(new XElement("unidades", this.Unidades));

        var proveedores = new XElement("proveedores-piezas");
        foreach (var proveedor in _proveedoresPieza)
        {
            proveedores.Add(new XElement("proveedor",proveedor));
        }
        toret.Add(proveedores);
        
        return toret;
    }

    public Pieza(XElement xPieza)
    {
        this.Codigo = xPieza.Element("codigo").Value;
        this.Nombre = xPieza.Element("nombre").Value;
        this.Unidades = Convert.ToInt32(xPieza.Element("unidades").Value);
        
        //List<string> proveedoresPiezas = new List<string>();
        XElement xProveedorPieza = xPieza.Element("proveedores-piezas");
        _proveedoresPieza = xProveedorPieza.Elements("proveedor").Select(x => x.Value).ToList();
        //_proveedoresPieza = proveedoresPiezas;
    }

    public override string ToString()
    {
        return $"Pieza: {Nombre}, Codigo: {Codigo}";
    }
}