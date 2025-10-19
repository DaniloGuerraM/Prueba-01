namespace Taipa.API.Modelos.Response;

public class ProductoResponse
{
    public Guid Id { get; set; }
    public string? Marca { get; set; }
    public double Precio { get; set; }
    public string? Descripcion { get; set; }
    public Guid Proveedor_Id { get; set; }
    public string? Tipo { get; set; }
}