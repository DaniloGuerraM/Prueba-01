using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taipa.API.Modelos.Enum;
using Taipa.App.Entidades;

namespace Taipa.API.Entidades;

[Table("orden")]
public class Orden
{
    // Propiedades
    [Column("id_orden")]
    [Key]
    public Guid IdOrden { get; set; }

    [Column("numero_orden")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NumeroOrden { get; set; }

    [Column("fecha")]
    public DateTime Fecha { get; set; }

    [Column("id_cliente")]
    public Guid IdCliente { get; set; }

    [Column("id_venta")]
    public Guid IdVenta { get; set; }

    [Column("observaciones")]
    public string Observaciones { get; set; }

    [Column("estado")]
    public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente; // Valores posibles: Pendiente, Entregado y Anulado
    // otros valores pueden ser Agregado, En Proceso, Completado, Cancelado
 //  Campos solo para el PDF (NO se guardan en la BD)
    [NotMapped]
    public string DestinatarioNombre { get; set; }

    [NotMapped]
    public string DestinatarioLocalidad { get; set; }

    [NotMapped]
    public string DestinatarioTelefono { get; set; }

    [NotMapped]
    public string DestinatarioAgencia { get; set; }

    [NotMapped]
    public decimal Subtotal { get; set; }

    [NotMapped]
    public decimal Total { get; set; }

    [NotMapped]
    public decimal ValorAsegurado { get; set; }

    // Relaciones
    [ForeignKey(nameof(IdCliente))]
    public Cliente Cliente { get; set; }

    [ForeignKey(nameof(IdVenta))]
    public Venta Venta { get; set; }

    public ICollection<DetalleOrden> Detalles { get; set; } = new List<DetalleOrden>();
}
