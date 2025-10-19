using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.Response
{
    public class PagoResponse
    {
        public string NombreMetodo { get; set; }
        public decimal Monto { get; set; }
        public string Detalle { get; set; }
    }
}