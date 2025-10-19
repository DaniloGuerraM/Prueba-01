using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Taipa.API.Entidades;
using Taipa.API.Repositorio.Interface;
using Taipa.App.Repositorio.Contexto;

namespace Taipa.API.Repositorio.Implementaciones
{
    public class OrdenRepositorio : IOrdenRepositorio
    {
        private readonly AppContexto _contexto;
        public OrdenRepositorio(AppContexto contexto)
        {
            _contexto = contexto;
        }



        public async Task<bool> GuardarOrdenAsync(Orden orden)
        {
            try
            {
                _contexto.Ordens.Add(orden);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Orden> ObtenerOrdenPorIdAsync(Guid idOrden)
        {
            try
            {
                var orden = await _contexto.Ordens
                    .Include(r => r.Detalles)
                        .ThenInclude(d => d.Productos) // carga los productos
                    .Include(r => r.Cliente)         // carga el cliente
                    .FirstOrDefaultAsync(r => r.IdOrden == idOrden);
                return orden;

            }
            catch (System.Exception)
            {
                Console.WriteLine(" Error al obtener el orden con ID: {idOrden}");
                return null;
            }
        }

        public async Task<Orden> ObtenerOrdenPorVentaIdAsync(Guid idVenta)
        {
            try
            {
                var orden = await _contexto.Ordens
                    .Include(r => r.Detalles)
                        .ThenInclude(d => d.Productos) // carga los productos
                    .Include(r => r.Cliente)         // carga el cliente
                    .FirstOrDefaultAsync(r => r.IdVenta == idVenta);
                return orden;

            }
            catch (System.Exception)
            {
                Console.WriteLine(" Error al obtener el orden con ID: {idOrden}");
                return null;
            }
        }
        public async Task<bool> ActualizarOrdenAsync(Orden orden)
        {
            try
            {
                _contexto.Ordens.Update(orden);
                await _contexto.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}