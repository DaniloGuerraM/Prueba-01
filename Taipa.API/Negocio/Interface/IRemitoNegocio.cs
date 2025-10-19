using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Taipa.API.Entidades;
using Taipa.API.Modelos.Enum;
using Taipa.API.Modelos.Response;

namespace Taipa.API.Negocio.Interface
{
    public interface IOrdenNegocio
    {
        Task<byte[]> GuardarOrdenAsync(Venta venta,  IDbContextTransaction transaction);
        Task<OrdenResponce> ObtenerOrdenPorId(Guid guidventa);
        Task<bool> CambiarEstadoOrdenAsync(Guid idOrden, EstadoOrden nuevoEstado);
    }
}