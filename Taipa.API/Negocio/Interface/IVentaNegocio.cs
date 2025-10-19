using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;

namespace Taipa.API.Negocio.Interface;

public interface IVentaNegocio
{
    // Para el POST
    Task<byte[]> CrearVenta(VentasDTO ventasDto);
    // Para los GET
    Task<List<Venta>> ObtenerVentas();
    Task<List<Venta>> ObtenerVentasPorCliente(Guid idCliente);
    Task<Venta> ObtenerVentaPorId(Guid idVenta);
    // para el delete(dar de baja)
    Task<bool> BajaDeVenta(Guid idVenta);
}
