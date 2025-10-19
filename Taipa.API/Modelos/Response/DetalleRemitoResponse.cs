using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.Response
{
    public class DetalleOrdenResponse
    {

    public Guid IdDetalleOrden { get; set; }

    //public Guid IdOrden { get; set; }
    //public Guid IdProducto { get; set; }
    public string? Marca { get; set; }
    public string? Descripcion { get; set; }
    public decimal Cantidad { get; set; }
    public string? Observaciones { get; set; }

    }
}