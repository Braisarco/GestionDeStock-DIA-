using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace UI.core.gestionProveedores;

public class Proveedores
{
    private List<Proveedor> _listaProveedores;
    
    public Proveedores()
    {
        _listaProveedores = new List<Proveedor>();
    }
    
    public Proveedores(List<Proveedor> listaProveedores)
    {
        _listaProveedores = listaProveedores;
    }

    //GET
    public Proveedor Get(int pos)
    {
        if (pos >= 0 && pos < _listaProveedores.Count) {
            return _listaProveedores[pos];    
        }
        return null;
    }
    
    public Proveedor Get(string cif)
    {
        return _listaProveedores.Find(proveedor => proveedor.CIF == cif);
    }
    
    //AÑADIR
    public void AddProveedor(Proveedor proveedor)
    {
        _listaProveedores.Add(proveedor);
    }
    
    //EDITAR
    public void EditarProveedor(int pos, Proveedor proveedor)
    {
        _listaProveedores[pos] = proveedor;
    }
    
    //ELIMINAR
    public void EliminarProveedor(Proveedor proveedor)
    {
        _listaProveedores.Remove(proveedor);
    }
    
    public void EliminarProveedor(int pos)
    {
        _listaProveedores.RemoveAt(pos);
    }

    //LISTAR
    public List<Proveedor> Lista()
    {
        return new List<Proveedor>(_listaProveedores);
    }
    
    public List<Proveedor> SuministranPieza(int codigoPieza)
    {
        return new List<Proveedor>(_listaProveedores.Where(
            proveedor => proveedor.TienePieza(codigoPieza)
        ).ToList());
    }
    
    //CONTAR
    public int NumProveedores()
    {
        return _listaProveedores.Count;
    }
    
    //XML
    public Proveedores(XElement xProveedores)
    {
        _listaProveedores = new List<Proveedor>();
        
        foreach (var xProveedor in xProveedores.Elements("proveedor"))
        {
            _listaProveedores.Add(new Proveedor(xProveedor));
        }
    }
    
    public XElement ToXElement()
    {
        var toret = new XElement("proveedores");

        foreach (var proveedor in _listaProveedores)
        {
            toret.Add(proveedor.ToXElement());
        }
        return toret;
    }
}