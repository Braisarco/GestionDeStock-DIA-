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
    public Pieza pieza { get; set; }

    public int Cantidad { get; set; }

    public Compra(Proveedor proveedor, DateOnly fechaHoraEntrega, Pieza pieza, int cantidad)
    {
        this.proveedor = proveedor;
        this.fechaHoraEntrega = fechaHoraEntrega;
        this.pieza = pieza;
        this.Cantidad = cantidad;
    }

    public Compra(XElement xCompra)
    {
        proveedor = new Proveedor(xCompra.Element("proveedor"));
        pieza = new Pieza(xCompra.Element("pieza"));
        Cantidad = int.Parse(xCompra.Element("cantidad").Value);
        fechaHoraEntrega = DateOnly.FromDateTime(DateTime.ParseExact(xCompra.Element("fecha").Value, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None));
    }

    public XElement toXML()
    {
        XElement toret = new XElement("compra");
        toret.Add(proveedor.ToXElement());
        toret.Add(pieza.ToXElement());
        toret.Add(new XElement("cantidad",Cantidad));
        toret.Add(new XElement("fecha", fechaHoraEntrega));

        return toret;
    }

    public Compra()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"COMPRA\n" + 
               $"\tProveedor: {proveedor}\n" +
               $"\tPieza: {pieza}\n" +
               $"\tCantidad: {Cantidad}\n" +
               $"\tFecha: {fechaHoraEntrega}\n";
    }
}