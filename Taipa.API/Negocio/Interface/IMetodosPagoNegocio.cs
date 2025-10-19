using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;

namespace Taipa.API.Negocio.Interface
{
    public interface IMetodosPagoNegocio
    {
        Task<IEnumerable<MetodosPago>> ObtenerMetodosPago();
        Task<bool> CrearMetodoPago(MetodosPagoDTO metodoPago);
        Task<bool> EliminarMetodoPago(Guid id);
    }
}