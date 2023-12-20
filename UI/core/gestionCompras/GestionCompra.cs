using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace UI.core.gestionCompras;

public class GestionCompra
{
    public GestionCompra()
    {
        this.Compras = new List<Compra>();
    }
    
    public int Count {
        get { return this.Compras.Count; }
    }
    
    public void Elimina(int i)
    {
        if ( i < 0
             || i >= this.Compras.Count )
        {
            throw new ArgumentException( $"valor de {nameof( i )}: " + i );
        }
        
        this.Compras.RemoveAt( i );
    }

    
    public void Add(Compra compra)
    {
        this.Compras.Add(compra);
    }

    public XElement toXML()
    {
        XElement toret = new XElement("compras");
        foreach (Compra compra in Compras)
        {
            toret.Add(compra.toXML());
        }

        return toret;
    }

    public GestionCompra(XElement xCompras)
    {
        Compras = new List<Compra>();
        foreach (XElement xCompra in xCompras.Elements("compra"))
        {
            Compras.Add(new Compra(xCompra));
        }
    }

    public void GuardarReparaciones()
    {
        var serializer = new XmlSerializer(typeof(List<Compra>));
        using (var stream = new FileStream("reparaciones.xml", FileMode.Create))
        {
            serializer.Serialize(stream, Compras);
        }
    }

    public void CargarReparaciones()
    {
        if (File.Exists("reparaciones.xml"))
        {
            var serializer = new XmlSerializer(typeof(List<Compra>));
            using (var stream = new FileStream("reparaciones.xml", FileMode.Open))
            {
                Compras = (List<Compra>)serializer.Deserialize(stream);
            }
        }
    }

    public List<Compra> Compras { get; set; }

    public object Get(int pos)
    {
        throw new NotImplementedException();
    }
}