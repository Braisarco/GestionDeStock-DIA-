using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using UI.core.gestionProveedores;
using UI.core.gestionPiezas;

namespace UI.core.gestionCompras;

public class Compra
{
    public Proveedor proveedor { get; set; }
    public DateOnly fechaHoraEntrega { get; set; }
    public Piezas piezas { get; set; }

    public Compra(Proveedor proveedor, DateOnly fechaHoraEntrega, Piezas piezas)
    {
        this.proveedor = proveedor;
        this.fechaHoraEntrega = fechaHoraEntrega;
        this.piezas = piezas;
    }

    public Compra(XElement xCompra)
    {
        proveedor = new Proveedor(xCompra.Element("proveedor"));
        fechaHoraEntrega = DateOnly.FromDateTime(XmlConvert.ToDateTime(xCompra.Element("fecha").Value));
        piezas = new Piezas();
        foreach (XElement pieza in xCompra.Elements("piezas"))
        {
            piezas.addPieza(new Pieza(pieza));
        }
    }

    public XElement toXML()
    {
        XElement toret = new XElement("compra");
        toret.Add(proveedor.ToXElement());
        toret.Add(piezas.toXML());
        toret.Add(new XElement("fecha", fechaHoraEntrega));

        return toret;
    }

    public Compra()
    {
        throw new NotImplementedException();
    }
}