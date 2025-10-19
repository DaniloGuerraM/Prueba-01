using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.Response
{
    public class DetalleVentaResponse
    {
        public string Productos { get; set; } 
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}