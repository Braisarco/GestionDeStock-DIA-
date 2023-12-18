using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Linq;

namespace UI.core.gestionClientes;

    public class Clientes
    {
        private List<Cliente> listaClientes;

        public Clientes()
        {
            if (this.listaClientes == null)
            {
                this.listaClientes = new List<Cliente>();
            }
        }

        public Clientes(IEnumerable<Cliente> clientes)
        {
            this.listaClientes = clientes.ToList();
        }

        public Clientes(Cliente cliente)
        {
            this.listaClientes = new List<Cliente>();
            this.listaClientes.Add(cliente);
        }

        public void AñadirCliente(Cliente cliente)
        {
            if (cliente != null)
            {
                this.listaClientes.Add(cliente);
            }
            else
            {
                Console.WriteLine("El cliente no puede ser nulo");
            }
        }

        public void EliminarCliente(string cif)
        {
            Cliente clienteEliminar = new Cliente();
            foreach (Cliente cliente in this.listaClientes)
            {
                if (cliente.CIF.Equals(cif))
                {
                    clienteEliminar = cliente;
                }
            }
            this.listaClientes.Remove(clienteEliminar);
        }
        
        public IEnumerable<Cliente> ListaClientes
        {
            get => this.listaClientes;
        }
        
        public XElement toXML()
        {
            XElement root = new XElement("clientes");
            foreach (Cliente cliente in this.listaClientes)
            {
                root.Add(cliente.toXML());
            }
            return root;
        }
        
        public void saveXML()
        {
             toXML().Save("Clientes.xml");
        }

        public void fromXML(string fn)
        {
            XElement root = XElement.Load(fn);

            foreach (XElement cliente in root.Elements("cliente"))
            {
                this.listaClientes.Add(new Cliente(cliente));
            }
        }

        public List<Cliente> buscarClienteNombre(String nombre)
        {
            List<Cliente> toret = new List<Cliente>();
            foreach (Cliente cliente in listaClientes)
            {
             if (cliente.Nombre.StartsWith(nombre)) {
                 toret.Add(cliente);
             }   
            }
            return toret;
        }

        public List<Cliente> buscarClienteCIF(String cif)
        {
            List<Cliente> toret = new List<Cliente>();
            foreach (Cliente cliente in listaClientes)
            {
                if (cliente.CIF.StartsWith(cif)) {
                    toret.Add(cliente);
                }   
            }
            return toret;
        }
        
        public override string ToString()
        {
            StringBuilder toret = new StringBuilder();
            foreach (Cliente cliente in this.listaClientes)
            {
                toret.Append(cliente.ToString());
                toret.Append("\n---------------\n");
            }
            return toret.ToString();
        }
    }
