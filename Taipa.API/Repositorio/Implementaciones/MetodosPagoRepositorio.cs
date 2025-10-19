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
    public class MetodosPagoRepositorio : IMetodosPagoRepositorio
    {
        private readonly AppContexto _appContexto;
        public MetodosPagoRepositorio(AppContexto appContexto)
        {
            _appContexto = appContexto;
        }
        public async Task<IEnumerable<MetodosPago>> ObtenerMetodosPago()
        {
            try
            {
                var metodosPago = await _appContexto.MetodosPago.ToListAsync();
                return metodosPago;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error al obtener los métodos de pago: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearMetodoPago(MetodosPago metodoPago)
        {
            try
            {
                _appContexto.MetodosPago.Add(metodoPago);
                await _appContexto.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error al crear el método de pago: {ex.Message}");
                return false;

            }
        }

        public async Task<bool> EliminarMetodoPago(Guid id)
        {
            try
            {
                var metodosPago = _appContexto.MetodosPago.Find(id);
                if (metodosPago == null)
                {
                    Console.WriteLine("Método de pago no encontrado.");
                    return false;
                }
                _appContexto.MetodosPago.Remove(metodosPago);
                await _appContexto.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error al crear el método de pago: {ex.Message}");
                return false;

            }
        }
    }
}