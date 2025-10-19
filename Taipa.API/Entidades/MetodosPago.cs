using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Entidades
{
    [Table("Metodos?Pago")]
    public class MetodosPago
    {
        [Key]
        [Column("id_metodo")]
        public Guid IdMetodo { get; set; }
        [Column("nombre")]
        [MaxLength(50)]
        public string NombreMetodo { get; set; }
    }
}