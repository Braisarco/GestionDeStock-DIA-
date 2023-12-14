using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using UI.core.gestionClientes;

namespace UI.core.gestionPedidos;

public class Pedido
{
    public Pedido(Cliente cliente, DateTime fechaHora)
    {
        this.Cliente = cliente;
        this.FechaHora = fechaHora;
    }

    public Cliente Cliente { get; set; }
    public DateTime FechaHora { get; set; }
    
    
    public static void GuardaPedidosXML (List<Pedido> pedidos, string filePath)
    {
        XElement root = new XElement("Pedidos",
            from pedido in pedidos
            select new XElement("Pedido",
                new XElement("Cliente",
                    new XElement(pedido.Cliente.toXML())
                ),
                new XElement("FechaHora", pedido.FechaHora.ToString("yyyy-MM-ddTHH:mm:ss"))
            )
        );

        root.Save(filePath);
    }

    public static List<Pedido> RecuperaPedidosXML (string filePath)
    {
        XElement root = XElement.Load(filePath);

        var pedidos = from pedidoXml in root.Elements("Pedido")
            select new Pedido(
                new Cliente(
                    pedidoXml.Element("Cliente").Element("Cif").Value,
                    pedidoXml.Element("Cliente").Element("Nombre").Value,
                    pedidoXml.Element("Cliente").Element("DireccionFacturacion").Value,
                    CodigoPieza = int.Parse(pedidoXml.Element("Cliente").Element("CodigoPieza").Value)
                ),
                DateTime.Parse(pedidoXml.Element("FechaHora").Value)
            );

        return pedidos.ToList();
    }

}