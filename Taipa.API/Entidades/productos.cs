using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Taipa.API.Entidades;

[Table("productos")]
public class Productos
{
    [Key]
    [Column("id_producto")]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Column("marca")]
    [Required]
    [MaxLength(100)]
    public string? Marca { get; set; }

    [Column("precio")]
    public decimal Precio { get; set; } = 0.00M;

    [Column("descripcion")]
    [MaxLength(1000)]
    public string? Descripcion { get; set; }

    [Column("proveedor_id")]
    [ForeignKey("proveedores")]
    public Guid Proveedor_Id { get; set; }

    [Column("tipo")]
    [MaxLength(100)]
    public string? Tipo { get; set; }

}
