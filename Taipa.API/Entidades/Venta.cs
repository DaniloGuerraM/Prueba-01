using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Taipa.App.Entidades;

namespace Taipa.API.Entidades
{
    
    
  [Table("ventas")]
  public class Venta
  {
    [Key]
    [Column("id_venta")]
    public Guid IdVenta { get; set; }

    [Column("id_cliente")]
    public Guid IdCliente { get; set; }

    [Column("fecha_venta")]
    public DateTime FechaVenta { get; set; } = DateTime.Now;

    [Column("monto_total", TypeName = "numeric(10,2)")]
    public decimal MontoTotal { get; set; }

    [Column("estado")]
    public bool Estado { get; set; } = true; // Por defecto, activa

    [Column("condicion_venta")]
    public string CondicionVenta { get; set; } = "Contado"; 
    // Posibles: Contado, Cuenta Corriente, Crédito

    // Relación con Cliente
    public Cliente Cliente { get; set; }

    // Relación con Detalles de la venta
    public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();

    // Relación con Pago
    public ICollection<Pago> Pago { get; set; } = new List<Pago>();
    }
}