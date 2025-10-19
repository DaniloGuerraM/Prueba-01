using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taipa.API.Entidades;

[Table("detalle_orden")]
public class DetalleOrden
{
    // Propiedades
    [Column("id_detalle_orden")]
    [Key]
    public Guid IdDetalleOrden { get; set; }

    [Column("id_orden")]
    public Guid IdOrden { get; set; }

    [Column("id_producto")]
    public Guid IdProducto { get; set; }

    [Column("cantidad")]
    public decimal Cantidad { get; set; }

    [Column("observaciones")]
    public string? Observaciones { get; set; }

    // Relaciones
    [ForeignKey(nameof(IdOrden))]
    public Orden Orden { get; set; }

    [ForeignKey(nameof(IdProducto))]
    public Productos Productos { get; set; }
}
