using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Taipa.API.Entidades;
using Taipa.API.Modelos.Enum;
using Taipa.API.Modelos.Response;
using Taipa.API.Negocio.Interface;
using Taipa.API.Repositorio.Implementaciones;
using Taipa.API.Repositorio.Interface;
using Taipa.API.Utilidades.PDF;

namespace Taipa.API.Negocio.Implementaciones
{
    public class OrdenNegocio : IOrdenNegocio
    {
        private readonly IOrdenRepositorio _ordenRepositorio;
        private readonly IMapper _mapper;
        private readonly ITransactionRepositorio _transactionRepositorio;
        public OrdenNegocio(
            IOrdenRepositorio ordenRepositorio,
            IMapper mapper,
            ITransactionRepositorio transactionRepositorio
            )
        {
            _ordenRepositorio = ordenRepositorio;
            _mapper = mapper;
            _transactionRepositorio = transactionRepositorio;
        }
        public async Task<byte[]> GuardarOrdenAsync(Venta venta,  IDbContextTransaction transaction)
        {

            // Iniciar una transacción
            //using var transaction = await _transactionRepositorio.BeginTransactionAsync();
            try
            {
                //  Crear la instancia del orden
                var orden = new Orden
                {
                    IdOrden = Guid.NewGuid(),
                    Fecha = DateTime.UtcNow,
                    IdCliente = venta.IdCliente,
                    IdVenta = venta.IdVenta,
                    Observaciones = "Generado automáticamente desde la venta.",
                    Estado = EstadoOrden.Pendiente,
                    

                    // Mapear los detalles
                    Detalles = venta.Detalles.Select(d => new DetalleOrden
                    {
                        IdDetalleOrden = Guid.NewGuid(),
                        IdProducto = d.Id,
                        Cantidad = d.Cantidad,
                        Observaciones = d.Observaciones
                    }).ToList()
                };

                // Guardar usando el repositorio
                var estado = await _ordenRepositorio.GuardarOrdenAsync(orden);
                if (!estado)
                {
                    Console.WriteLine(" No se pudo guardar el orden");
                    //await transaction.RollbackAsync();
                    return null;
                }

                // Obtener orden completo si necesitás productos cargados
                var ordenGuardado = await _ordenRepositorio.ObtenerOrdenPorIdAsync(orden.IdOrden);
                if (ordenGuardado == null)
                {
                    Console.WriteLine(" No se pudo obtener el orden guardado");
                    //await transaction.RollbackAsync();
                    return null;
                }
                // Generar PDF
                byte[] pdfBytes = OrdenPdfHelper.GenerarOrdenPdf(ordenGuardado);
                if (pdfBytes == null || pdfBytes.Length == 0)
                {
                    Console.WriteLine(" Error al generar el PDF del orden");
                    //await transaction.RollbackAsync();
                    return null;
                }
                // Confirmar la transacción
                //await transaction.CommitAsync();
                return pdfBytes;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine($" Error al crear el orden: {ex.Message}");
                return null;
            }
        }

        public async Task<OrdenResponce> ObtenerOrdenPorId(Guid guidventa)
        {
            var orden = await _ordenRepositorio.ObtenerOrdenPorVentaIdAsync(guidventa);
            if (orden == null)
            {
                Console.WriteLine("No se encontró el orden para la venta con ID: " + guidventa);
                return null;
            }
            var responce = _mapper.Map<OrdenResponce>(orden);
            return responce;
        }
        public async Task<bool> CambiarEstadoOrdenAsync(Guid idOrden, EstadoOrden nuevoEstado)
        {
            var orden = await _ordenRepositorio.ObtenerOrdenPorIdAsync(idOrden);
            if (orden == null)
            {
                Console.WriteLine(" No se encontró el orden con ID: " + idOrden);
                return false;
            }
            orden.Estado = nuevoEstado;
            var resultado = await _ordenRepositorio.ActualizarOrdenAsync(orden);
            if (!resultado)
            {
                Console.WriteLine(" No se pudo actualizar el estado del orden.");
                return false;
            }                
            Console.WriteLine("Estado del orden actualizado correctamente.");
            return true;
        }
    }
}