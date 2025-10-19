using Microsoft.EntityFrameworkCore.Storage;
using Taipa.API.Entidades;
using Taipa.App.Entidades;

namespace Taipa.API.Repositorio.Interface;

public interface IVentaRepositorio
{
    //para el POST
    Task<bool> GuardarVenta(Venta ventas);
    // para los GET
    Task<List<Venta>> ObtenerVentas();
    Task<List<Venta>> ObtenerVentasPorCliente(Guid idCliente);
    Task<Venta> ObtenerVentaPorId(Guid idVenta);
    // para el delete(dar de baja)
    Task<bool> BajaDeVenta(Venta venta);
   // Task<IDbContextTransaction> BeginTransactionAsync();
}
