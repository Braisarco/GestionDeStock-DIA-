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
    
    public Pieza Pieza { get; set; }
    public int Unidades { get; set; }

    public Pedido(int codigo, Cliente cliente, DateOnly fechaHora, Pieza pieza, int unidades)
    {
        this.Codigo = codigo;
        this.Cliente = cliente;
        this.FechaHora = fechaHora;
        this.Pieza = pieza;
        this.Unidades = unidades;
    }
    
    public XElement ToXElement()
    {
        var toret = new XElement("pedido");
        toret.Add(new XElement("codigo", Codigo));
        toret.Add(new XElement("fecha", FechaHora));
        toret.Add(Cliente.toXML());
        toret.Add(Pieza.ToXElement());
        toret.Add(new XElement("unidades", Unidades));
        return toret;
    }

    public Pedido(XElement xPedido)
    {
        Codigo = int.Parse(xPedido.Element("codigo").Value);
        FechaHora = DateOnly.FromDateTime(DateTime.ParseExact(xPedido.Element("fecha").Value, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None));
        Cliente = new Cliente(xPedido.Element("cliente"));
        Pieza = new Pieza(xPedido.Element("pieza"));
        Unidades = int.Parse(xPedido.Element("unidades").Value);
    }

    public override string ToString()
    {
        return $"PEDIDO\n" + 
               $"\tCodigo: {Codigo}\n" +
               $"\tCliente: {Cliente}\n" +
               $"\tPieza: {Pieza}\n" +
               $"\tUnidades: {Unidades}\n" +
               $"\tFecha: {FechaHora}\n";
    }
}