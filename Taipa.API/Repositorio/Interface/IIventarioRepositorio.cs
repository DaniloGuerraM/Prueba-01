using Taipa.API.Entidades;

namespace Taipa.API.Repositorio.Interface;

public interface IInventarioRepositorio
{
    Task<List<Inventario>> ListarTodos();
    Task<Inventario?> ListarPorId(Guid id);
    Task<bool> Crear(Inventario inventario);
    Task<bool> Eliminar(Guid id);
    Task<int> ExisteInventario(Guid idProducto);
    Task<bool> ActualizaInventario(Inventario inventario);
}