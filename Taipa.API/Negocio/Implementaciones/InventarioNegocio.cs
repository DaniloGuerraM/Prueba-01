using AutoMapper;
using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Modelos.Response;
using Taipa.API.Repositorio.Interface;
using Taipa.API.Negocio.Interface;

namespace Taipa.API.Negocio.Implementaciones;

public class InventarioNegocio : IInventarioNegocio
{
    private readonly IMapper _mapper;
    private readonly IInventarioRepositorio _inventarioRepositorio;

    public InventarioNegocio(IMapper mapper, IInventarioRepositorio inventarioRepositorio)
    {
        _mapper = mapper;
        _inventarioRepositorio = inventarioRepositorio;
    }

    public async Task<List<InventarioResponse>> ListarTodos()
    {
        var inventarios = await _inventarioRepositorio.ListarTodos();
        return _mapper.Map<List<InventarioResponse>>(inventarios);
    }

    public async Task<InventarioResponse?> ListarPorId(Guid id)
    {
        var inventario = await _inventarioRepositorio.ListarPorId(id);
        if (inventario == null) return null;
        
        return _mapper.Map<InventarioResponse>(inventario);
    }

    public async Task<InventarioResponse> Crear(InventarioDTO inventarioDTO)
    {
        var inventario = _mapper.Map<Inventario>(inventarioDTO);
        await _inventarioRepositorio.Crear(inventario);
        return _mapper.Map<InventarioResponse>(inventario);
    }

    public async Task<bool> Eliminar(Guid id)
    {
        return await _inventarioRepositorio.Eliminar(id);
    }

    public Task<int> ExisteInventario(Guid idProducto)
    {
        return _inventarioRepositorio.ExisteInventario(idProducto);
    }

    public async Task<bool> ReduceInventario(Guid idProducto, int cantidad)
    {
        var inventario = await _inventarioRepositorio.ListarPorId(idProducto);
        

        if (inventario == null || inventario.Cantidad < cantidad)
            return false;

        inventario.Cantidad -= cantidad;
        
        return await _inventarioRepositorio.ActualizaInventario(inventario);

    }

    public async Task<bool> AgregaInventario(Guid idProducto, int cantidad)
    {
        var inventario = await _inventarioRepositorio.ListarPorId(idProducto);
        if (inventario == null || inventario.Cantidad < cantidad)
            return false;

        inventario.Cantidad += cantidad;
        return await _inventarioRepositorio.ActualizaInventario(inventario);

    }
}