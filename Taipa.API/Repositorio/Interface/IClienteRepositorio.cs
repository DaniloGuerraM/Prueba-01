using Taipa.App.Entidades;


namespace ITaipa.App.Repositorio.Interface;

public interface IClienteRepositorio
{
    Task<List<Cliente>> ListarTodos();
    Task<Cliente?> ObtenerPorId(Guid id);
    Task<Cliente?> ObtenerPorDni(int dni);
    Task<bool> Crear(Cliente cliente);
    Task<bool> Actualizar(Cliente cliente);
    Task<bool> Eliminar(Guid id);
    Task<Cliente?> ObtenerClienteGenerico();
}