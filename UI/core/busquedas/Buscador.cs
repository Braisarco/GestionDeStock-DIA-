using System.Collections.Generic;

namespace UI.core.busquedas;
using UI.core.gestionClientes;
using UI.core.gestionPedidos;
using UI.core.gestionPiezas;
using UI.core.gestionProveedores;

public class Buscador
{
    private Clientes clientes = new Clientes();
    private Piezas piezas = new Piezas();
    private Pedidos pedidos = new Pedidos();
    private Proveedores proveedores = new Proveedores();

    public Buscador(Clientes clientes, Piezas piezas, Pedidos pedidos, Proveedores proveedores)
    {
        this.clientes = clientes;
        this.piezas = piezas;
        this.pedidos = pedidos;
        this.proveedores = proveedores;
    }
    public List<Cliente> buscarClienteNombre(string nombre)
    {
        List<Cliente> toret = new List<Cliente>();
        foreach (Cliente cliente in clientes.ListaClientes)
        {
            if (cliente.Nombre.StartsWith(nombre))
            {
                toret.Add(cliente);
            }
            
        }

        return toret;
    }
    
    public List<Cliente> buscarClienteCIF (string cif)
    {
        List<Cliente> toret = new List<Cliente>();
        foreach (Cliente cliente in clientes.ListaClientes)
        {
            if (cliente.CIF.StartsWith(cif))
            {
                toret.Add(cliente);
            }
            
        }

        return toret;
    }
    
    public List<Pedido> buscarPedidoCodigo(int codigo)
    {
        List<Pedido> toret = new List<Pedido>();
        foreach (Pedido pedido in pedidos.Lista())
        {
            if (pedido.Codigo == codigo)
            {
                toret.Add(pedido);
            }
            
        }

        return toret;
    }
    
    public List<Pieza> buscarPiezaCodigo(string codigo)
    {
        List<Pieza> toret = new List<Pieza>();
        foreach (Pieza pieza in piezas.Lista())
        {
            if (pieza.Codigo.Equals(codigo))
            {
                toret.Add(pieza);
            }
            
        }

        return toret;
    }
    
    public List<Pieza> buscarPiezaNombre(string nombre)
    {
        List<Pieza> toret = new List<Pieza>();
        foreach (Pieza pieza in piezas.Lista())
        {
            if (pieza.Nombre.StartsWith(nombre))
            {
                toret.Add(pieza);
            }
            
        }

        return toret;
    }
    
    public List<Proveedor> buscarProveedorNombre(string nombre)
    {
        List<Proveedor> toret = new List<Proveedor>();
        foreach (Proveedor proveedor in proveedores.Lista())
        {
            if (proveedor.Nombre.StartsWith(nombre))
            {
                toret.Add(proveedor);
            }
            
        }

        return toret;
    }
    
    public List<Proveedor> buscarProveedorCif(string cif)
    {
        List<Proveedor> toret = new List<Proveedor>();
        foreach (Proveedor proveedor in proveedores.Lista())
        {
            if (proveedor.CIF.StartsWith(cif))
            {
                toret.Add(proveedor);
            }
            
        }

        return toret;
    }




}