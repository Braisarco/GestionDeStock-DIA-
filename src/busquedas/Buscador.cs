namespace gestor_stock.Buscador;
using gestor_stock.Clientes;
using gestor_stock.Pedidos;
using gestor_stock.Piezas;
using gestor_stock.Proveedores;

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
    public List<Cliente> buscarClienteNombre(String nombre)
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
    
    public List<Cliente> buscarClienteCIF (String cif)
    {
        List<Cliente> toret = new List<Cliente>();
        foreach (Cliente cliente in clientes.ListaClientes)
        {
            if (cliente.Cif.StartsWith(cif))
            {
                toret.Add(cliente);
            }
            
        }

        return toret;
    }
    
    public List<Pedido> buscarPedidoCodigo(String codigo)
    {
        List<Pedido> toret = new List<Pedido>();
        foreach (Pedido pedido in pedidos.ListaPedidos)
        {
            if (pedido.Codigo.StartsWith(codigo))
            {
                toret.Add(pedido);
            }
            
        }

        return toret;
    }
    
    public List<Pieza> buscarPiezaCodigo(String codigo)
    {
        List<Pieza> toret = new List<Pieza>();
        foreach (Pieza pieza in piezas.ListaPiezas)
        {
            if (pieza.Codigo.StartsWith(codigo))
            {
                toret.Add(pieza);
            }
            
        }

        return toret;
    }
    
    public List<Pieza> buscarPiezaNombre(String nombre)
    {
        List<Pieza> toret = new List<Pieza>();
        foreach (Pieza pieza in piezas.ListaPiezas)
        {
            if (pieza.Nombre.StartsWith(nombre))
            {
                toret.Add(pieza);
            }
            
        }

        return toret;
    }
    
    public List<Proveedor> buscarProveedorNombre(String nombre)
    {
        List<Proveedor> toret = new List<Proveedor>();
        foreach (Proveedor proveedor in proveedores.ListaProveedores)
        {
            if (proveedor.Nombre.StartsWith(nombre))
            {
                toret.Add(proveedor);
            }
            
        }

        return toret;
    }
    
    public List<Proveedor> buscarProveedorCif(String cif)
    {
        List<Proveedor> toret = new List<Proveedor>();
        foreach (Proveedor proveedor in proveedores.ListaProveedores)
        {
            if (proveedor.Cif.StartsWith(cif))
            {
                toret.Add(proveedor);
            }
            
        }

        return toret;
    }




}