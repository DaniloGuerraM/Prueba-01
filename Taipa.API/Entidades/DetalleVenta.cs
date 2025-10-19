
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taipa.API.Entidades
{
    [Table("detalles_venta")]
    public class DetalleVenta
    {
        [Key]
        [Column("id_detalle_venta")]
        public Guid IdDetalleVenta { get; set; }

        [Column("venta_id")]
        public Guid VentaId { get; set; }

        [Column("producto_id")]
        public Guid Id { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }
        [Column("precio_unitario")]
        public decimal PrecioUnitario { get; set; }

        [Column("observaciones")]
        public string Observaciones { get; set; }
        [Column("subtotal")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Subtotal { get; set; }

        // Relaciones
        public Venta Venta { get; set; }
        public Productos Productos { get; set; }
    }
}

