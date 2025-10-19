using System.Security.Cryptography.X509Certificates;
using AutoMapper;
using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Negocio.Interface;
using Taipa.API.Repositorio.Interface;
using Taipa.App.Negocio.Interface;
using Taipa.App.Perfiles;

namespace Taipa.API.Negocio.Implementaciones;

public class VentaNegocio : IVentaNegocio
{
    private readonly IVentaRepositorio _ventaRepositorio;
    private readonly IClienteNegocio _clienteNegocio;
    private readonly IInventarioNegocio _inventarioNegocio;
    private readonly IProductoNegocio _productoNegocio;
    private readonly IOrdenNegocio _ordenNegocio;
    private readonly IMapper _mapperPerfil;
    private readonly ITransactionRepositorio _transactionRepositorio;
    public VentaNegocio(
        IVentaRepositorio ventaRepositorio,
        IClienteNegocio clienteNegocio,
        IInventarioNegocio inventarioNegocio,
        IProductoNegocio productoNegocio,
        IOrdenNegocio ordenNegocio,
        IMapper mapperPerfil,
        ITransactionRepositorio transactionRepositorio
        )
    {
        _mapperPerfil = mapperPerfil;
        _ventaRepositorio = ventaRepositorio;
        _clienteNegocio = clienteNegocio;
        _inventarioNegocio = inventarioNegocio;
        _productoNegocio = productoNegocio;
        _ordenNegocio = ordenNegocio;
        _transactionRepositorio = transactionRepositorio;
    }
    // Para el POST
    public async Task<byte[]> CrearVenta(VentasDTO ventasDto)
    {
        var venta = _mapperPerfil.Map<Venta>(ventasDto);
        using var transaction = await _transactionRepositorio.BeginTransactionAsync();

        try
        {
            decimal montoTotal = 0;
            decimal total = 0;

            //  Validar cliente
            var cliente = await _clienteNegocio.ObtenerPorId(venta.IdCliente);
            if (cliente == null)
            {
                var clienteGenerico = await _clienteNegocio.ObtenerClienteGenerico();
                if (clienteGenerico == null)
                {
                    await transaction.RollbackAsync();
                        return null;//"1. Cliente no encontrado y no hay cliente genérico disponible";
                }
                venta.IdCliente = clienteGenerico.IdCliente;
                venta.Cliente = clienteGenerico;
                venta.CondicionVenta = "Contado";
            }

            //  Validar stock y calcular total
            foreach (var detalle in venta.Detalles)
            {
                var stock = await _inventarioNegocio.ExisteInventario(detalle.Id);
                if (stock < detalle.Cantidad && detalle.Cantidad > 0)
                {
                    await transaction.RollbackAsync();
                        return null;//$"2. No hay suficiente stock para el producto {detalle.Id}";
                }

                var producto = await _productoNegocio.ListarPorId(detalle.Id);
                detalle.PrecioUnitario = producto.Precio;
                total += producto.Precio * detalle.Cantidad;
            }

            //  Validar pagos
            foreach (var pago in ventasDto.Pago)
                montoTotal += pago.Monto;

            if (montoTotal < total && venta.CondicionVenta != "Cuenta Corriente")
            {
                await transaction.RollbackAsync();
                    return null;// "2. El monto total de los pagos es menor al monto total de la venta";
            }

            if (montoTotal > total)
            {
                await transaction.RollbackAsync();
                    return null; //"2. El monto total de los pagos es mayor al monto total de la venta";
            }

            venta.MontoTotal = total;

            //  Guardar la venta
            var guardado = await _ventaRepositorio.GuardarVenta(venta);
            if (!guardado)
            {
                await transaction.RollbackAsync();
                    return null;// "3. Error al guardar la venta";
            }

            //  Reducir stock
            foreach (var detalle in ventasDto.Detalles)
            {
                var resultado = await _inventarioNegocio.ReduceInventario(detalle.Id, detalle.Cantidad);
                if (!resultado)
                {
                    await transaction.RollbackAsync();
                        return null; // $"4. Error al reducir el stock del producto {detalle.Id}";
                }
            }

            //  Crear automáticamente el Orden
            var ordenCreado = await _ordenNegocio.GuardarOrdenAsync(venta, transaction);
            if (ordenCreado == null)
            {
                await transaction.RollbackAsync();
                    return null; //"5. Error al generar el orden de la venta";
            }
                // Aquí guarda el PDF en algún lugar o enviarlo por correo si es necesario
            string ruta = @"C:\MisOrdens\Orden_" + venta.IdVenta + ".pdf";
            File.WriteAllBytes(ruta, ordenCreado);
            
            Console.WriteLine(" PDF guardado en: " + ruta);
                
            // 7️ Confirmar todo
            await transaction.CommitAsync();
            return ordenCreado;//"Venta y orden generados correctamente.";
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
                return null; //$"Error inesperado: {ex.Message}";
        }
    }

    // Para los GET
    public async Task<List<Venta>> ObtenerVentas()
    {
        var ventas = await _ventaRepositorio.ObtenerVentas();
        /*
        foreach (var venta in ventas)
        {
            venta.MontoTotal = venta.Detalles.Sum(d => d.Subtotal );
        }
        */
        return ventas;
    }
    public async Task<List<Venta>> ObtenerVentasPorCliente(Guid idCliente)
    {
        var ventas = await _ventaRepositorio.ObtenerVentasPorCliente(idCliente);
        return ventas;
    }
    public async Task<Venta> ObtenerVentaPorId(Guid idVenta)
    {
        var venta = await _ventaRepositorio.ObtenerVentaPorId(idVenta);
        return venta;
    }
    // para el delete(dar de baja)
    public async Task<bool> BajaDeVenta(Guid idVenta)
    {
        using var transaction = await _transactionRepositorio.BeginTransactionAsync();
        try
        {
            var venta = await _ventaRepositorio.ObtenerVentaPorId(idVenta);
            if (venta == null)
                return false; // Venta no encontrada
            
            if (!venta.Estado)
                return false; // Venta ya está inactiva
            
            venta.Estado = false; // Cambiar el estado a inactivo

            bool estado = await _ventaRepositorio.BajaDeVenta(venta);
            if (estado)
            {
                foreach (var detalle in venta.Detalles)
                {
                                  //await _ventaRepositorio.AgregaStock(detalle.Id, detalle.Cantidad);
                    var resultado = await _inventarioNegocio.AgregaInventario(detalle.Id, detalle.Cantidad);
                    if (!resultado)
                    {
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
            // 5. Confirmar la transacción si todo salió bien
            await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return false;
        }

    }
}