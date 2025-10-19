using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taipa.API.Modelos.Enum;

namespace Taipa.API.Modelos.Response
{
    public class OrdenResponce
    {
    public Guid IdOrden { get; set; }

        public string NumeroOrden { get; set; } = "R-";

    public DateTime Fecha { get; set; }
    public string NombreApellido { get; set; }
    public string Domicilio { get; set; }
    public string Cuit { get; set; }

    //public Guid IdCliente { get; set; }

    //public Guid IdVenta { get; set; }

    public string Observaciones { get; set; }

    public EstadoOrden Estado { get; set; } // Valores posibles: Pendiente, Entregado y Anulado
        public List<DetalleOrdenResponse> Detalles { get; set; }
    }
}