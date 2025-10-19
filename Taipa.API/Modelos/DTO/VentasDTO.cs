using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.DTO
{
    public class VentasDTO
    {
        public Guid IdCliente { get; set; }
        //public decimal MontoTotal { get; set; }
        [DefaultValue("Contado")]
        public string CondicionVenta { get; set; }
        public List<PagoCrearDTO> Pago { get; set; } = new();
        public List<DetalleVentaDTO> Detalles { get; set; } = new();
    }
}