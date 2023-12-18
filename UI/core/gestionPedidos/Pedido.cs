using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UI.core.gestionClientes;
using UI.core.gestionPiezas;

namespace UI.core.gestionPedidos;

public class Pedido
{
    public int Codigo { get; set; }
    public Cliente Cliente { get; set; }
    public DateOnly FechaHora { get; set; }
    
    public Piezas Piezas { get; set; }

    public Pedido(int codigo, Cliente cliente, DateOnly fechaHora)
    {
        this.Codigo = codigo;
        this.Cliente = cliente;
        this.FechaHora = fechaHora;
        this.Piezas = new Piezas();
    }
    
    public XElement ToXElement()
    {
        var toret = new XElement("pedido");
        toret.Add(new XElement("codigo", Codigo));
        toret.Add(new XElement("fecha", FechaHora));
        toret.Add(Cliente.toXML());
        toret.Add(Piezas.toXML());
        return toret;
    }

    public Pedido(XElement xPedido)
    {
        Codigo = int.Parse(xPedido.Element("codigo").Value);
        FechaHora = DateOnly.FromDateTime(XmlConvert.ToDateTime(xPedido.Element("fecha").Value));
        Cliente = new Cliente(xPedido.Element("cliente"));
        foreach (XElement pieza in xPedido.Elements("pieza"))
        {
            Piezas.addPieza(new Pieza(pieza));
        }
    }

    public override string ToString()
    {
        return $"PEDIDO\n" + 
               $"\tCodigo: {Codigo}\n" +
               $"\tCliente: {Cliente.ToString()}\n" +
               $"\tPiezas: {Piezas.ToString()}\n" +
               $"\tFecha: {FechaHora}\n";
    }
}