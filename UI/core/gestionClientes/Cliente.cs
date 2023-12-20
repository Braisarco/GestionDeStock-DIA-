using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Xml.Linq;
using UI.core.gestionPiezas;

namespace UI.core.gestionClientes;

    public class Cliente
    {
        private string cif;
        private string nombre;
        private string direccionFacturacion;
        private List<string> codigoPiezasCompradas;

        public Cliente(){ }
        public Cliente(string cif, string nombre)
        {
            this.cif = cif;
            this.nombre = nombre;
            codigoPiezasCompradas = new List<string>();
        }

        public Cliente(string cif, string nombre, string direccion, IEnumerable<Pieza> piezas)
        {
            this.cif = cif;
            this.nombre = nombre;
            this.direccionFacturacion = direccion;
            this.codigoPiezasCompradas = new List<string>();
            foreach (Pieza p in piezas)
            {
                this.codigoPiezasCompradas.Add(p.Codigo);
            }
        }
        
        public Cliente(string cif, string nombre, string direccion, Piezas piezas)
        {
            this.cif = cif;
            this.nombre = nombre;
            this.direccionFacturacion = direccion;
            foreach (Pieza pieza in piezas.Lista())
            {
                this.codigoPiezasCompradas.Add(pieza.Codigo);
            }
        }
        
        public Cliente(string cif, string nombre, string direccion)
        {
            this.cif = cif;
            this.nombre = nombre;
            this.direccionFacturacion = direccion;
            this.codigoPiezasCompradas = new List<string>();
        }

        public XElement toXML()
        {
            XElement root = new XElement("cliente",
                new XElement("nombre", this.nombre),
                new XElement("direccion", this.direccionFacturacion));
            XAttribute codigo = new XAttribute("cif", this.cif);
            root.Add(codigo);
            foreach (string valor in CodigoPiezasVendidas)
            {
                root.Add(new XElement("codigo", valor));
            }

            return root;
        }

        public Cliente(XElement xCliente)
        {
            this.nombre = xCliente.Element("nombre").Value;
            this.cif = xCliente.Attribute("cif").Value;
            this.direccionFacturacion = xCliente.Element("direccion").Value;
            List<string> codigos = new List<string>();
            foreach (XElement codigo in xCliente.Elements("codigo"))
            {
                codigos.Add(codigo.Value);
            }

            this.codigoPiezasCompradas = codigos;
        }
        
        public string CIF
        {
            get => this.cif;
        }

        public string Nombre
        {
            get => this.nombre;
        }

        public string DireccionFacturacion
        {
            get => this.direccionFacturacion;
            set => this.direccionFacturacion = value;
        }

        public List<string> CodigoPiezasVendidas
        {
            get => this.codigoPiezasCompradas;
        }
        public string CodigoPiezasVendidasString
        {
            get
            {
                if (this.codigoPiezasCompradas.Count == 0)
                {
                    return "La lista está vacia";
                }
                else
                {
                    StringBuilder toret = new StringBuilder();
                    foreach (string codigo in this.codigoPiezasCompradas)
                    {
                        toret.Append($" |{codigo}| ");
                    }
                    return toret.ToString();
                }
            }
         }
        
        public override string ToString()
        {
            return $"CIF: {this.cif}, Nombre: {this.nombre}, Direccion: {this.direccionFacturacion}";
        }
    }

