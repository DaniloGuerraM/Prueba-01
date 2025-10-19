using AutoMapper;
using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Repositorio.Interface;
using Taipa.API.Negocio.Interface;

namespace Taipa.API.Negocio.Implementaciones;

public class ProductoNegocio : IProductoNegocio
{
    private readonly IMapper _mapper;
    private readonly IProductoRepositorio _productoRepositorio;

    public ProductoNegocio(IMapper mapper, IProductoRepositorio productoRepositorio)
    {
        _mapper = mapper;
        _productoRepositorio = productoRepositorio;
    }

    public async Task<List<Productos>> ListarTodos()
    {
        return await _productoRepositorio.ListarTodos();
    }

    public async Task<Productos?> ListarPorId(Guid id)
    {
        return await _productoRepositorio.ListarPorId(id);
    }

    public async Task<Productos> Crear(ProductoDTO productoDTO)
    {
        var producto = _mapper.Map<Productos>(productoDTO);
        await _productoRepositorio.Crear(producto);
        return producto;
    }

    public async Task<bool> Actualizar(Guid id, ProductoDTO productoDTO)
    {
        var producto = _mapper.Map<Productos>(productoDTO);
        producto.Id = id; 
        return await _productoRepositorio.Actualizar(producto);
    }

    public async Task<bool> Eliminar(Guid id)
    {
        return await _productoRepositorio.Eliminar(id);
    }
}
