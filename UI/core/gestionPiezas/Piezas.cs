using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UI.core.gestionProveedores;

namespace UI.core.gestionPiezas;

public class Piezas
{
    private List<Pieza> _listaPiezas;
    
    public Piezas()
    {
        _listaPiezas = new List<Pieza>();
    }
    
    public Piezas(List<Pieza> listaPiezas)
    {
        _listaPiezas = listaPiezas;
    }

    //GET
    public Pieza Get(int pos)
    {
        if (pos >= 0 && pos < _listaPiezas.Count) {
            return _listaPiezas[pos];    
        }
        return null;
    }
    
    public Pieza Get(string codigo)
    {
        return _listaPiezas.Find(pieza => pieza.Codigo == codigo);
    }
    
    //AÑADIR
    public void AddPieza(Pieza pieza)
    {
        _listaPiezas.Add(pieza);
    }
    
    //EDITAR
    public void EditarPieza(int pos, Pieza pieza)
    {
        _listaPiezas[pos] = pieza;
    }
    
    //ELIMINAR
    public void EliminarPieza(Pieza pieza)
    {
        _listaPiezas.Remove(pieza);
    }
    
    public void EliminarPieza(int pos)
    {
        _listaPiezas.RemoveAt(pos);
    }

    //LISTAR
    public List<Pieza> Lista()
    {
        return new List<Pieza>(_listaPiezas);
    }
    
    public List<Pieza> ProveedoresPieza(string cif)
    {
        return new List<Pieza>(_listaPiezas.Where(
            pieza => pieza.TieneProveedor(cif)
        ).ToList());
    }
    
    //CONTAR
    public int NumPiezas()
    {
        return _listaPiezas.Count;
    }
    
    //-------XML-------
    public XElement toXML()
    {
        XElement root = new XElement("piezas");
        foreach (var pieza in _listaPiezas)
        {
            root.Add(pieza.ToXElement());
        }
        return root;
    }
    
    public void Guarda(string filePath)
    {
        toXML().Save(filePath);
    }
    
    public Piezas(XElement xPiezas)
    {
        _listaPiezas = new List<Pieza>();
        
        foreach (var xPieza in xPiezas.Elements("pieza"))
        {
            _listaPiezas.Add(new Pieza(xPieza));
        }
    }
}