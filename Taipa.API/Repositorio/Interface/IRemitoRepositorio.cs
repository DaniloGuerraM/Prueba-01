using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Taipa.API.Entidades;

namespace Taipa.API.Repositorio.Interface
{
    public interface IOrdenRepositorio
    {
        Task<bool> GuardarOrdenAsync(Orden orden);
        Task<bool> ActualizarOrdenAsync(Orden orden);
        Task<Orden> ObtenerOrdenPorIdAsync(Guid idOrden);
        Task<Orden> ObtenerOrdenPorVentaIdAsync(Guid idVenta);
    }
}