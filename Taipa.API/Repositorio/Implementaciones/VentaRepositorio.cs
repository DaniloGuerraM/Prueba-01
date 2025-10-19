using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Taipa.API.Entidades;
using Taipa.API.Repositorio.Interface;
using Taipa.App.Entidades;
using Taipa.App.Repositorio.Contexto;

namespace Taipa.API.Repositorio.Implementaciones
{
    public class VentaRepositorio : IVentaRepositorio
    {
        private readonly AppContexto _context;
        public VentaRepositorio(AppContexto context)
        {
            _context = context;
        }
/*
        // Para el POST: iniciar transacción
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
*/



        public async Task<bool> GuardarVenta(Venta venta)
        {
            try
            {
                _context.Ventas.Add(venta);
                // EF Core detectará automáticamente los detalles y pagos relacionados
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        // Para los GET
        public async Task<List<Venta>> ObtenerVentas()
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Productos)
                .Include(v => v.Pago)
                    .ThenInclude(p => p.MetodoPago)
                .ToListAsync();
        }

        public async Task<List<Venta>> ObtenerVentasPorCliente(Guid idCliente)
        {
            return await _context.Ventas
                .Where(v => v.IdCliente == idCliente)
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Productos)
                .Include(v => v.Pago)
                    .ThenInclude(p => p.MetodoPago)
                .ToListAsync();
        }

        public async Task<Venta> ObtenerVentaPorId(Guid idVenta)
        {
            return await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                    .ThenInclude(d => d.Productos)
                .Include(v => v.Pago)
                    .ThenInclude(p => p.MetodoPago)
                .FirstOrDefaultAsync(v => v.IdVenta == idVenta);
        }

        // Para el delete (dar de baja)
        public async Task<bool> BajaDeVenta(Venta venta)
        {
            try
            {
                _context.Ventas.Update(venta);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
