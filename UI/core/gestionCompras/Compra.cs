using System;
using System.Collections.Generic;
using UI.core.gestionProveedores;
using UI.core.gestionPiezas;

namespace UI.core.gestionCompras;

public class Compra
{
    public Proveedor proveedor { get; set; }
    public string fechaHoraEntrega { get; set; }
    public List<Pieza> piezas { get; set; }

    public Compra(Proveedor proveedor, string fechaHoraEntrega, List<Pieza> piezas)
    {
        this.proveedor = proveedor;
        this.fechaHoraEntrega = fechaHoraEntrega;
        this.piezas = piezas;
    }

    public Compra()
    {
        throw new NotImplementedException();
    }
}