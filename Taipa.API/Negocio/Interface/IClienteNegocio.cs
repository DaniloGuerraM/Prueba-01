using Taipa.App.Entidades;
using Taipa.App.Modelos.DTO;

namespace Taipa.App.Negocio.Interface;

public interface IClienteNegocio
{
    Task<List<Cliente>> ListarTodos();
    Task<Cliente?> ObtenerPorId(Guid id);
    Task<Cliente?> ObtenerPorDni(int dni);
    Task<Cliente> Crear(ClienteDTO clienteDTO);
    Task<bool> Actualizar(Guid id, ClienteDTO clienteDTO);
    Task<bool> Eliminar(Guid id);
    Task<Cliente?> ObtenerClienteGenerico();
}