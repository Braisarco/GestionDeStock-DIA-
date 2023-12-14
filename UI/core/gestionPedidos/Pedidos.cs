using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace UI.core.gestionPedidos;

public class Pedidos
{
    private List<Pedido> pedidos;

    public Pedidos() {}

    public Pedidos(List<Pedido> pedidosArg)
    {
        this.pedidos = pedidosArg;
    }
    
    public List<Pedido> Lista()
    {
        return pedidos;
    }
    
    
    
    public XElement toXML()
    {
        var toret = new XElement("pedidos");

        foreach (Pedido pedido in pedidos)
        {
            toret.Add(pedido.ToXElement());
        }
        return toret;
    }

    public void saveXML(string fileName)
    {
        toXML().Save(fileName);
    }
    
    public void RecuperaPedidosXML (string filePath)
    {
        XElement root = XElement.Load(filePath);

        foreach (XElement pedido in root.Elements("pedido"))
        {
            this.pedidos.Add(new Pedido(pedido));
        }
    }
}