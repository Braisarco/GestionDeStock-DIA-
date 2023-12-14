using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UI.core.gestionProveedores;

namespace UI.core.gestionPiezas;
public class Pieza
{
    public Pieza(int codigo, string nombre, int unidades, List<Proveedor> proveedores)
    {
        this.codigo = codigo;
        this.nombre = nombre;
        this.unidades = unidades;
        this.proveedores = proveedores;
    }

    public Pieza()
    {
        this.piezas = new List<Pieza>();
    }
    public required int codigo { get; set; }
    public required string nombre { get; set; }
    public required int unidades { get; set; }
    public List<Proveedor> proveedores { get; set; }
    

    public override string ToString()
    {
        return $"{this.codigo},{this.nombre}:{this.unidades}";
    }
    
    private List<Pieza> piezas = new List<Pieza>();

    public bool tienePieza(int c)
    {
        //return proveedores.Codigo.equals(c);
        return true;
    }

    public void addPieza(Pieza p)
    {
        this.piezas.Add(p);
    }

    public void removePieza(Pieza p)
    {
        this.piezas.Remove(p);
    }
    //-------XML-------
    
    public static void Guarda(List<Pieza> piezas, string filePath)
    {
        XElement root = new XElement("Piezas",
            from pieza in piezas
            select new XElement("Pieza",
                new XElement("Codigo", pieza.codigo),
                new XElement("Nombre", pieza.nombre),
                new XElement("Unidades", pieza.unidades),
                new XElement("Proveedores",
                    from proveedor in pieza.proveedores
                    select new XElement("Proveedor",
                        new XElement("CIF", proveedor.CIF),
                        new XElement("Nombre", proveedor.Nombre),
                        new XElement("Direccion", proveedor.DireccionFacturacion),
                        new XElement("CodigosPiezasProvistas", proveedor.PiezasProvistas()
                        )
                    )
                )
            )
        );

        root.Save(filePath);
    }
    
    
    public static List<Pieza> Recupera(string filePath)
    {

        XElement root = XElement.Load(filePath);

        var piezas = from piezaXml in root.Elements("Pieza")
            select new Pieza
            {
                codigo = int.Parse(piezaXml.Element("Codigo").Value),
                nombre = piezaXml.Element("Nombre").Value,
                unidades = int.Parse(piezaXml.Element("UnidadesDisponibles").Value),
                proveedores = (
                    from proveedorXml in piezaXml.Element("Proveedores").Elements("Proveedor")
                    select new Proveedor
                    {
                        CIF = proveedorXml.Element("CIF").Value,
                        Nombre = proveedorXml.Element("Nombre").Value,
                        DireccionFacturacion = proveedorXml.Element("DireccionFacturacion").Value,
                        codigosPiezasProvistas = int.Parse(proveedorXml.Element("CodigoPiezaProvista").Value)
                    }
                ).ToList()
            };

        return piezas.ToList();
    }
    
}