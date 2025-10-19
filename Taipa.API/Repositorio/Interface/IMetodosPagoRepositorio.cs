using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taipa.API.Entidades;

namespace Taipa.API.Repositorio.Interface
{
    public interface IMetodosPagoRepositorio
    {
        Task<IEnumerable<MetodosPago>> ObtenerMetodosPago();
        Task<bool> CrearMetodoPago(MetodosPago metodoPago);
        Task<bool> EliminarMetodoPago(Guid id);
    }
}