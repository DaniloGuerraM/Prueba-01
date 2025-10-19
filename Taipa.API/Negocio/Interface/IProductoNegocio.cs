using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;

namespace Taipa.API.Negocio.Interface;

public interface IProductoNegocio
{
    Task<List<Productos>> ListarTodos();
    Task<Productos?> ListarPorId(Guid id);
    Task<Productos> Crear(ProductoDTO producto);
    Task<bool> Actualizar(Guid id, ProductoDTO producto);
    Task<bool> Eliminar(Guid id);
}
