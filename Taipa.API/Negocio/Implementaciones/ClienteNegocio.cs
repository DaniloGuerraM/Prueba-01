using AutoMapper;
using Taipa.App.Entidades;
using Taipa.App.Modelos.DTO;
using Taipa.App.Negocio.Interface;
using ITaipa.App.Repositorio.Interface;

namespace Taipa.App.Negocio.Implementaciones;

public class ClienteNegocio : IClienteNegocio
{
    private readonly IClienteRepositorio _repositorio;
    private readonly IMapper _mapper;

    public ClienteNegocio(IClienteRepositorio repositorio, IMapper mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<List<Cliente>> ListarTodos()
    {
        return await _repositorio.ListarTodos();
    }

    public async Task<Cliente?> ObtenerPorId(Guid id)
    {
        return await _repositorio.ObtenerPorId(id);
    }

    public async Task<Cliente?> ObtenerPorDni(int dni)
    {
        return await _repositorio.ObtenerPorDni(dni);
    }

    public async Task<Cliente> Crear(ClienteDTO clienteDTO)
    {
        var cliente = _mapper.Map<Cliente>(clienteDTO);
        await _repositorio.Crear(cliente);
        return cliente;
    }

    public async Task<bool> Actualizar(Guid id, ClienteDTO clienteDTO)
    {
        var cliente = _mapper.Map<Cliente>(clienteDTO);
        cliente.IdCliente = id;
        return await _repositorio.Actualizar(cliente);
    }

    public async Task<bool> Eliminar(Guid id)
    {
        return await _repositorio.Eliminar(id);
    }
    // Este método obtiene el cliente genérico desde el repositorio
    public async Task<Cliente?> ObtenerClienteGenerico()
    {
        return await _repositorio.ObtenerClienteGenerico();
    }
}