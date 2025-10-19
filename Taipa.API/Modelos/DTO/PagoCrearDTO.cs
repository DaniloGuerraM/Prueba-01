using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.DTO
{
    public class PagoCrearDTO
    {
        //public Guid IdVenta { get; set; }
        public Guid IdMetodo { get; set; }
        public decimal Monto { get; set; }
        [DefaultValue("Detalle sin observaciones")]
        public string Detalle { get; set; }
    }
}