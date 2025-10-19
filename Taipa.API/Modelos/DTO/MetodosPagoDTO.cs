using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Modelos.DTO
{
    public class MetodosPagoDTO
    {
        [DefaultValue("Efectivo")]
        public string NombreMetodo { get; set; }
    }
}