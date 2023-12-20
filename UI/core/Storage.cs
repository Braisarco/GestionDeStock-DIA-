using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using UI.core.gestionCompras;
using UI.core.gestionClientes;
using UI.core.gestionPedidos;
using UI.core.gestionPiezas;
using UI.core.gestionProveedores;

namespace UI.core;

public class Storage
{
    private const string fileName = "StoreContext.xml";
    
    private GestionCompra _compras;

    private Clientes _clientes;

    private Pedidos _pedidos;

    private Piezas _piezas;

    private Proveedores _proveedores;

    public GestionCompra Compras
    {
        get => _compras;
    }
    
    public Clientes Clientes
    {
        get => _clientes;
    }
    
    public Pedidos Pedidos
    {
        get => _pedidos;
    }
    
    public Piezas Piezas
    {
        get => _piezas;
    }
    
    public Proveedores Proveedores
    {
        get => _proveedores;
    }


    //Metodo encargado de inicializar a aplicacion, si existen archivos xml, 
    //collera a info de ahi si non inicializará de 0.
    public Storage(){
        Console.WriteLine("La aplicación se está inicializando...");

        if (File.Exists(fileName))
        {
            _piezas = new Piezas();
            _compras = new GestionCompra();
            _pedidos = new Pedidos();
            _proveedores = new Proveedores();
            _clientes = new Clientes();
            
            Console.WriteLine("Cargando datos...");
            loadStorage(fileName);
            Console.WriteLine("Los datos se han recuperado exitosamente!!");
            
        }else
        {
            Console.WriteLine("Datos no encontrados, creando aplicacion...");
            
            _piezas = new Piezas();
            _compras = new GestionCompra();
            _pedidos = new Pedidos();
            _proveedores = new Proveedores();
            _clientes = new Clientes();
            
           // saveStoreContext();
           Console.WriteLine("Aplicación creada y guardada :D");
        }
    }

    public void ComprarStock()
    {
        
    }

    public void CrearPedido()
    {
        
    }
    

    private void loadStorage(string file)
    {
        XElement root = XElement.Load(file);

        foreach (XElement cliente in root.Element("clientes").Elements("cliente"))
        {
            Clientes.AñadirCliente(new Cliente(cliente));
        }
        foreach (XElement pedido in root.Element("pedidos").Elements("pedido"))
        {
            Pedidos.Lista().Add(new Pedido(pedido));
        }
        foreach (XElement proveedor in root.Element("proveedores").Elements("proveedor"))
        {
            Proveedores.AddProveedor(new Proveedor(proveedor));
        }
        foreach (XElement pieza in root.Element("piezas").Elements("pieza"))
        {
            Piezas.AddPieza(new Pieza(pieza));
        }
        foreach (XElement compra in root.Element("compras").Elements("compra"))
        {
            Compras.Add(new Compra(compra));
        }
    }

    public void saveStoreContext()
    {
        XElement root = new XElement("Storage");
        
        root.Add(_pedidos.toXML());
        root.Add(_compras.toXML());
        root.Add(_clientes.toXML());
        root.Add(_piezas.toXML());
        root.Add(_proveedores.ToXElement());

        root.Save(fileName);
    }
}