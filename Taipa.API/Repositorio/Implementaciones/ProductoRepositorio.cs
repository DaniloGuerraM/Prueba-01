using Taipa.API.Entidades;
using Taipa.API.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;
using Taipa.App.Repositorio.Contexto;

namespace Taipa.API.Repositorio.Implementaciones;

public class ProductoRepositorio : IProductoRepositorio
{
    private readonly AppContexto _contexto;

    public ProductoRepositorio(AppContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Productos>> ListarTodos()
    {
        return await _contexto.Productos.ToListAsync();
    }

    public async Task<Productos?> ListarPorId(Guid id)
    {
    return await _contexto.Productos.FindAsync(id);
    }

    public async Task<bool> Crear(Productos producto)
    {
        await _contexto.Productos.AddAsync(producto);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Actualizar(Productos producto)
    {
        _contexto.Productos.Update(producto);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(Guid id)
    {
        var producto = await _contexto.Productos.FindAsync(id);
        if (producto == null) return false;

        _contexto.Productos.Remove(producto);
        return await _contexto.SaveChangesAsync() > 0;

    }
}


