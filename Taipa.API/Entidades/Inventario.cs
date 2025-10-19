using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Taipa.API.Entidades;

[Table("inventario")]
public class Inventario
{
    [Key]
    [Column("id_inventario")]
    public Guid Id_Inventario { get; set; } = Guid.NewGuid();

    [Column("producto_id")]
    [ForeignKey("id_producto")]
    public Guid Proveedor_Id { get; set; }

    [Column("cantidad")]
    public int Cantidad { get; set; }
}
