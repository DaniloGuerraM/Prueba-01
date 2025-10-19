using Taipa.App.Entidades;
using ITaipa.App.Repositorio.Interface;
using Taipa.App.Repositorio.Contexto;
using Microsoft.EntityFrameworkCore;

namespace Taipa.App.Repositorio;

public class ClienteRepositorio : IClienteRepositorio
{
    private readonly AppContexto _contexto;

    public ClienteRepositorio(AppContexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<List<Cliente>> ListarTodos()
    {
        return await _contexto.Clientes.ToListAsync();
    }

    public async Task<Cliente?> ObtenerPorId(Guid id)
    {
        return await _contexto.Clientes.FindAsync(id);
    }

    public async Task<Cliente?> ObtenerPorDni(int dni)
    {
        return await _contexto.Clientes.FirstOrDefaultAsync(c => c.Dni == dni);
    }

    public async Task<bool> Crear(Cliente cliente)
    {
        await _contexto.Clientes.AddAsync(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Actualizar(Cliente cliente)
    {
        _contexto.Clientes.Update(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(Guid id)
    {
        var cliente = await _contexto.Clientes.FindAsync(id);
        if (cliente == null) return false;
        
        _contexto.Clientes.Remove(cliente);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<Cliente?> ObtenerClienteGenerico()
    {
        var guidCliente = Guid.Parse("123e4567-e89b-12d3-a456-426614174000");
        return await _contexto.Clientes.FirstOrDefaultAsync(c => c.IdCliente == guidCliente);
    }
}