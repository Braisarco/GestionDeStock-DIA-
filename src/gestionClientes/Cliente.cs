using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Xml.Linq;

namespace gestor_stock_clientes.Properties
{
    public class Cliente
    {
        private string cif;
        private string nombre;
        private string direccionFacturacion;
        private List<int> codigoPiezasVendidas;

        public Cliente(){ }
        public Cliente(string cif, string nombre)
        {
            this.cif = cif;
            this.nombre = nombre;
        }

        public Cliente(string cif, string nombre, string direccion, IEnumerable<Pieza> piezas)
        {
            this.cif = cif;
            this.nombre = nombre;
            this.direccionFacturacion = direccion;
            this.codigoPiezasVendidas = new List<int>();
            foreach (Pieza p in piezas)
            {
                this.codigoPiezasVendidas.Add(p.CIF);
            }
        }
        
        public Cliente(string cif, string nombre, string direccion, List<int> piezas)
        {
            this.cif = cif;
            this.nombre = nombre;
            this.direccionFacturacion = direccion;
            this.codigoPiezasVendidas = new List<int>();
            this.codigoPiezasVendidas = piezas;
        }
        
        public Cliente(string cif, string nombre, string direccion)
        {
            this.cif = cif;
            this.nombre = nombre;
            this.direccionFacturacion = direccion;
            this.codigoPiezasVendidas = new List<int>();
        }

        public XElement toXML()
        {
            XElement root = new XElement("cliente",
                new XElement("nombre", this.nombre),
                new XElement("direccion", this.direccionFacturacion));
            XAttribute codigo = new XAttribute("cif", this.cif);
            root.Add(codigo);
            foreach (int valor in CodigoPiezasVendidas)
            {
                root.Add(new XElement("codigo", valor));
            }

            return root;
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

        public List<int> CodigoPiezasVendidas
        {
            get => this.codigoPiezasVendidas;
        }
        public string CodigoPiezasVendidasString
        {
            get
            {
                StringBuilder toret = new StringBuilder();
                foreach (int codigo in this.codigoPiezasVendidas)
                {
                    toret.Append($" |{codigo}| ");
                }
                return toret.ToString();
            }
         }
        
        public override string ToString()
        {
            return $"CIF: {this.cif}\nNombre: {this.nombre}\nDireccion: {this.direccionFacturacion}\nCodigos: {CodigoPiezasVendidasString}";
        }
    }
}
