using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.Response
{
    public class MetodosPagoResponse
    {
        public Guid IdMetodo { get; set; }
        public string NombreMetodo { get; set; }
    }
}