using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.Response
{
    public class VentasResponse
    {
        public Guid IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal MontoTotal { get; set; }
        public string ClienteNombre { get; set; }
        public string CondicionVenta { get; set; } // Ej: Contado, CC, Crédito
        public bool Estado { get; set; }
        public List<PagoResponse> Pago { get; set; } = new();
        public List<DetalleVentaResponse> Detalles { get; set; } = new();
    }
}