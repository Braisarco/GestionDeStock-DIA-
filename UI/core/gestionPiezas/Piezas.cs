using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UI.core.gestionProveedores;

namespace UI.core.gestionPiezas;

public class Piezas
{
    private List<Pieza> piezas;
    
    public Piezas()
    {
        this.piezas = new List<Pieza>();
    }
    
    public Piezas(List<Pieza> piezaArg)
    {
        this.piezas = piezaArg;
    }
    
    public List<Pieza> Lista()
    {
        return piezas;
    }
    

    public void addPieza(Pieza p)
    {
        this.piezas.Add(p);
    }

    public void removePieza(Pieza p)
    {
        this.piezas.Remove(p);
    }
    
    
    //-------XML-------
    public XElement toXML()
    {
        XElement root = new XElement("piezas");
        foreach (var pieza in piezas)
        {
            root.Add(pieza.ToXElement());
        }
        return root;
    }
    
    public void Guarda(string filePath)
    {
        toXML().Save(filePath);
    }
    
    public Piezas Recupera(string filePath)
    {

        XElement root = XElement.Load(filePath);

        Piezas toret = new Piezas();

        foreach (XElement pieza in root.Elements("piezas"))
        {
            Pieza toAdd = new Pieza{
                codigo = int.Parse(pieza.Element("codigo").Value),
                nombre = pieza.Element("nombre").Value,
                unidades = int.Parse(pieza.Element("unidadesDisponibles").Value),
                proveedores = new Proveedores(pieza.Element("proveedores"))
                
            };
            toret.addPieza(toAdd);
        }
        
        return toret;
    }
}