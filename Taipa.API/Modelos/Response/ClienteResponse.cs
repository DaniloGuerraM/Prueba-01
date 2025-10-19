namespace Taipa.App.Modelos.Response;

public class ClienteResponse
{
    public Guid IdCliente { get; set; }
    public string NombreApellido { get; set; }
    public string? CorreoElectronico { get; set; }
    public string? Telefono { get; set; }
    public string? Direccion { get; set; }
    public DateTime? FechaNacimiento { get; set; }
    public int Dni { get; set; }
    public string? NombreComercial { get; set; }
    public int? CodigoPostal { get; set; }
    public string? CodigoProvincia { get; set; }
    public string CategoriaArca { get; set; }
    public string? Cuit { get; set; }
    public double Saldo { get; set; }
    public double? LimiteCompra { get; set; }
    public string? Observaciones { get; set; }
    public string? Zona { get; set; }
    public DateTime? FechaUltimaOperacion { get; set; }
}