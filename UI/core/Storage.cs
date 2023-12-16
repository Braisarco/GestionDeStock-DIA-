using System;
using System.IO;
using System.Net;
using UI.core.gestionCompras;
using UI.core.gestionClientes;
using UI.core.gestionPedidos;
using UI.core.gestionPiezas;
using UI.core.gestionProveedores;

namespace UI.core;

class Storage
{
    private const string fileName = "StoreContext.xml";
    
    private GestionCompra _compras;

    private Clientes _clientes;

    private Pedidos _pedidos;

    private Piezas _piezas;

    private Proveedores _proveedores;
    
    
    //Metodo encargado de inicializar a aplicacion, si existen archivos xml, 
    //collera a info de ahi si non inicializará de 0.
    public void init(){
        Console.WriteLine("La aplicación se está inicializando...");

        if (File.Exists(fileName))
        {
            Console.WriteLine("Cargando datos...");
            loadStorage(fileName);
        }else
        {
            Console.WriteLine("Datos no encontrados, creando aplicacion...");
            
            _piezas = new Piezas();
            _compras = new GestionCompra();
            _pedidos = new Pedidos();
            _proveedores = new Proveedores();
            _clientes = new Clientes();
            
            saveStoreContext();
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
        
    }

    private void saveStoreContext()
    {
        
    }
}