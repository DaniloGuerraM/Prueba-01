using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Taipa.API.Entidades
{
    [Table("pagos")]
    public class Pago
    {
        [Key]
        [Column("id_pago")]
        public Guid IdPago { get; set; }

        [Required]
        [Column("id_venta")]
        public Guid IdVenta { get; set; }

        [Required]
        [Column("id_metodo")]
        public Guid IdMetodo { get; set; }

        [Required]
        [Column("monto", TypeName = "numeric(10,2)")]
        public decimal Monto { get; set; }

        [Column("detalle")]
        public string Detalle { get; set; }

        [Column("fecha_pago")]
        public DateTime FechaPago { get; set; } = DateTime.UtcNow;

        // Relaciones
        [ForeignKey(nameof(IdMetodo))]
        public MetodosPago MetodoPago { get; set; }
        [ForeignKey(nameof(IdVenta))]
        public Venta Venta { get; set; }
    }
}
