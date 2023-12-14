using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace src.gestionProveedores;

public class Proveedor
{
    private List<int> _piezasProvistas;
    
    public Proveedor(string cif, string nombre, string direccionFacturacion)
    {
        this.CIF = cif;
        this.Nombre = nombre;
        this.DireccionFacturacion = direccionFacturacion;
        this._piezasProvistas = new List<int>();
    }
    
    public string CIF { get; set; }
    
    public string Nombre { get; set; }
    
    public string DireccionFacturacion { get; set; }

    public List<int> PiezasProvistas()
    {
        return new List<int>(_piezasProvistas);
    }

    public void AddPieza(int codigoPieza)
    {
        _piezasProvistas.Add(codigoPieza);
    }
    
    public void AddPiezas(IEnumerable<int> codigosPiezas)
    {
        _piezasProvistas.AddRange(codigosPiezas);
    }

    public int Get(int pos)
    {
        if (pos >= 0 && pos < _piezasProvistas.Count) {
            return _piezasProvistas[pos];    
        }
        return -1;
    }
    public bool TienePieza(int codigoPieza)
    {
        return _piezasProvistas.Contains(codigoPieza);
    }
    
    public void EliminarPieza(int pos)
    {
        _piezasProvistas.RemoveAt(pos);
    }

    public int NumPiezas()
    {
        return _piezasProvistas.Count;
    }
    
    //--- XML ---
    public XElement ToXElement()
    {
        var toret = new XElement("proveedor");
        toret.Add(new XElement("cif", this.CIF));
        toret.Add(new XElement("nombre", this.Nombre));
        toret.Add(new XElement("direccion_facturacion", this.DireccionFacturacion));

        var piezas = new XElement("piezas_provistas");
        foreach (var pieza in _piezasProvistas)
        {
            piezas.Add(new XElement("pieza",pieza));
        }
        toret.Add(piezas);
        
        return toret;
    }

    public Proveedor(XElement xProveedor)
    {
        this.CIF = xProveedor.Element("cif").Value;
        this.Nombre = xProveedor.Element("nombre").Value;
        this.DireccionFacturacion = xProveedor.Element("direccion_facturacion").Value;
        
        List<int> piezasProvistas = new List<int>();
        XElement xPiezasProvistas = xProveedor.Element("piezas_provistas");
        foreach (var xPieza in xPiezasProvistas.Elements("pieza"))
        {
            piezasProvistas.Add(Convert.ToInt32(xPieza.Value));
        }
        _piezasProvistas = piezasProvistas;
    }

}