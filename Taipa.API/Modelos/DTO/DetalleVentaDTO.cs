using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.DTO
{
    public class DetalleVentaDTO
    {
        public Guid Id { get; set; }
        public int Cantidad { get; set; }
        [DefaultValue("Detalle sin observaciones")]
        public string Observaciones { get; set; }
    }
}