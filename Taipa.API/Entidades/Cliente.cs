using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Taipa.API.Entidades;

namespace Taipa.App.Entidades;

[Table("clientes")]
public class Cliente
{
    [Key]
    [Column("id_cliente")]
    public Guid IdCliente { get; set; } = Guid.NewGuid();

    [Column("nombre_apellido")]
    [Required]
    [MaxLength(100)]
    public string NombreApellido { get; set; }

    [Column("correo_electronico")]
    [MaxLength(100)]
    public string? CorreoElectronico { get; set; }

    [Column("telefono")]
    [MaxLength(100)]
    public string? Telefono { get; set; }

    [Column("direccion")]
    [MaxLength(100)]
    public string? Direccion { get; set; }

    [Column("fecha_nacimiento")]
    public DateTime? FechaNacimiento { get; set; }

    [Column("dni")]
    public int Dni { get; set; }

    [Column("nombre_comercial")]
    [MaxLength(100)]
    public string? NombreComercial { get; set; }

    [Column("codigo_postal")]
    public int? CodigoPostal { get; set; }

    [Column("codigo_provincia")]
    [MaxLength(100)]
    public string? CodigoProvincia { get; set; }

    [Column("categoria_arca")]
    [MaxLength(100)]
    public string CategoriaArca { get; set; }

    [Column("cuit")]
    public string? Cuit { get; set; }

    [Column("saldo")]
    public double Saldo { get; set; } = 0.00;

    [Column("limite_compra")]
    public double? LimiteCompra { get; set; }

    [Column("observaciones")]
    [MaxLength(1000)]
    public string? Observaciones { get; set; }

    [Column("zona")]
    [MaxLength(100)]
    public string? Zona { get; set; }

    [Column("fecha_ultima_operacion")]
    public DateTime? FechaUltimaOperacion { get; set; }

    // Relación con ventas (1:N)
    public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    public ICollection<Orden> Ordens { get; set; } = new List<Orden>();

}