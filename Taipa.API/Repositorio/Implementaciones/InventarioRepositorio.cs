using Taipa.API.Entidades;
using Taipa.API.Repositorio.Interface;
using Microsoft.EntityFrameworkCore;
using Taipa.App.Repositorio.Contexto;

namespace Taipa.API.Repositorio.Implementaciones;

public class InventarioRepositorio : IInventarioRepositorio
{
    private readonly AppContexto _contexto;

    public InventarioRepositorio(AppContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Inventario>> ListarTodos()
    {
        return await _contexto.Inventario.ToListAsync();
    }

    public async Task<Inventario?> ListarPorId(Guid id)
    {
        return await _contexto.Inventario.FirstOrDefaultAsync(i => i.Proveedor_Id == id);
        //return await _contexto.Inventario.FindAsync(id);
    }

    public async Task<bool> Crear(Inventario inventario)
    {
        await _contexto.Inventario.AddAsync(inventario);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(Guid id)
    {
        var inventario = await _contexto.Inventario.FindAsync(id);
        if (inventario == null) return false;

        _contexto.Inventario.Remove(inventario);
        return await _contexto.SaveChangesAsync() > 0;
    }
    // Este método verifica si existe inventario para un producto dado
    // y devuelve la cantidad disponible
    public async Task<int> ExisteInventario(Guid idProducto)
    {
        return await _contexto.Inventario
            .Where(i => i.Proveedor_Id == idProducto)
            .Select(i => i.Cantidad)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ActualizaInventario(Inventario inventario)
    {
        _contexto.Inventario.Update(inventario);
        await _contexto.SaveChangesAsync();
        return true;
    }

}