using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Modelos.Response;

namespace Taipa.API.Negocio.Interface;

public interface IInventarioNegocio
{
    Task<List<InventarioResponse>> ListarTodos();
    Task<InventarioResponse?> ListarPorId(Guid id);
    Task<InventarioResponse> Crear(InventarioDTO inventarioDTO);
    Task<bool> Eliminar(Guid id);
    Task<int> ExisteInventario(Guid idProducto);
    Task<bool> ReduceInventario(Guid idProducto, int cantidad);
    Task<bool> AgregaInventario(Guid idProducto, int cantidad);
}