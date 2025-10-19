using Taipa.API.Entidades;

namespace Taipa.API.Repositorio.Interface;

public interface IProductoRepositorio
{
    Task<List<Productos>> ListarTodos();
    Task<Productos?> ListarPorId(Guid id);
    Task<bool> Crear(Productos producto);
    Task<bool> Actualizar(Productos producto);
    Task<bool> Eliminar(Guid id);
}
