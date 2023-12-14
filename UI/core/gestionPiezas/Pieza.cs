using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UI.core.gestionProveedores;

namespace UI.core.gestionPiezas;
public class Pieza
{
    public Pieza(int codigo, string nombre, int unidades, Proveedores proveedores)
    {
        this.codigo = codigo;
        this.nombre = nombre;
        this.unidades = unidades;
        this.proveedores = proveedores;
    }

    public Pieza() {}
    public int codigo { get; set; }
    public string nombre { get; set; }
    public int unidades { get; set; }
    public Proveedores proveedores { get; set; }
    

    public override string ToString()
    {
        return $"{this.codigo},{this.nombre}:{this.unidades}";
    }

    public bool tienePieza(int c)
    {
        //return proveedores.Codigo.equals(c);
        return true;
    }

    public XElement ToXElement()
    {
        var toret = new XElement("pieza");
        toret.Add(new XElement("codigo", this.codigo));
        toret.Add(new XElement("nombre", this.nombre));
        toret.Add(new XElement("unidades", this.unidades));

        var piezas = new XElement("piezas");
        foreach (var proveedor in proveedores.Lista())
        {
            piezas.Add(proveedor.ToXElement());
        }
        toret.Add(piezas);
        
        return toret;
    }

    public Pieza(XElement xPieza)
    {
        this.codigo = int.Parse(xPieza.Element("codigo").Value);
        this.nombre = xPieza.Element("cif").Value;
        this.unidades = int.Parse(xPieza.Element("unidades").Value);
        foreach (XElement proveedor in xPieza.Elements("proveedor"))
        {
            proveedores.AddProveedor(new Proveedor(proveedor));
        }
    }
}