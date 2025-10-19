using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Taipa.API.Modelos.DTO;
using Taipa.API.Modelos.Response;
using Taipa.API.Negocio.Interface;

namespace Taipa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VentaController : ControllerBase
{
    private readonly IVentaNegocio _ventaNegocio;
    private readonly IMapper _mapperPerfil;
    public VentaController(IVentaNegocio ventaNegocio, IMapper mapperPerfil)
    {
        _mapperPerfil = mapperPerfil;

        _ventaNegocio = ventaNegocio;
    }

    [HttpPost("crear")]
    public async Task<IActionResult> CrearVenta([FromBody] VentasDTO ventasDto)
    {
        var iTextSharp = await _ventaNegocio.CrearVenta(ventasDto);
        if (iTextSharp == null)
        {
            return BadRequest("No se pudo crear la venta.");
        }
        return File(iTextSharp, "application/pdf", $"Orden-.pdf");
        //return Ok(new { Message = $"{_ventaNegocio.CrearVenta(ventasDto).Result}", Data = ventasDto });
    }
    [HttpGet("obtener")]
    public async Task<ActionResult<List<VentasResponse>>> ObtenerVentas()
    {
        var ventas = await _ventaNegocio.ObtenerVentas();
        if (ventas == null || !ventas.Any())
        {
            return NotFound("No se encontraron ventas.");
        }
        return Ok(_mapperPerfil.Map<List<VentasResponse>>(ventas));
    }
    [HttpGet("obtenerPorCliente/{idCliente}")]
    public async Task<ActionResult<List<VentasResponse>>> ObtenerVentasPorCliente(Guid idCliente)
    {
        var ventas = await _ventaNegocio.ObtenerVentasPorCliente(idCliente);
        if (ventas == null || !ventas.Any())
        {
            return NotFound("No se encontraron ventas para el cliente especificado.");
        }
        return Ok(_mapperPerfil.Map<List<VentasResponse>>(ventas));
    }
    [HttpGet("obtenerPorId/{idVenta}")]
    public async Task<ActionResult<VentasResponse>> ObtenerVentaPorId(Guid idVenta)
    {
        var venta = await _ventaNegocio.ObtenerVentaPorId(idVenta);
        if (venta == null)
        {
            return NotFound("No se encontró la venta especificada.");
        }
        return Ok(_mapperPerfil.Map<VentasResponse>(venta));
    }
    [HttpDelete("baja/{idVenta}")]
    public async Task<IActionResult> BajaDeVenta(Guid idVenta)
    { 
        var resultado = await _ventaNegocio.BajaDeVenta(idVenta);
        if (!resultado)
        {
            return NotFound("No se pudo dar de baja la venta especificada.");
        }
        return Ok("Venta dada de baja correctamente.");
    }
}