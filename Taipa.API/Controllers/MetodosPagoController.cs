using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Modelos.Response;
using Taipa.API.Negocio.Interface;

namespace Taipa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodosPagoController : ControllerBase
    {
        private readonly IMetodosPagoNegocio _metodosPagoNegocio;
        private readonly IMapper _mapper;
        public MetodosPagoController(IMetodosPagoNegocio metodosPagoNegocio, IMapper mapper)
        {
            _metodosPagoNegocio = metodosPagoNegocio;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MetodosPagoResponse>>> ObtenerMetodosPago()
        {
            var metodosPago = await _metodosPagoNegocio.ObtenerMetodosPago();
            if (metodosPago == null || !metodosPago.Any())
            {
                return NotFound("No se encontraron métodos de pago.");
            }
            return Ok(_mapper.Map<List<MetodosPagoResponse>>(metodosPago));
        }
        [HttpPost("crear")]
        public async Task<IActionResult> CrearMetodoPago([FromBody] MetodosPagoDTO metodoPagoDto)
        {
            if (metodoPagoDto == null)
            {
                return BadRequest("El método de pago no puede ser nulo.");
            }

            var resultado = await _metodosPagoNegocio.CrearMetodoPago(metodoPagoDto);
            if (resultado)
            {
                return Ok(new { Message = "Método de pago creado exitosamente." });
            }
            return BadRequest("Error al crear el método de pago.");
        }
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarMetodoPago(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El ID del método de pago no puede ser nulo.");
            }

            var resultado = await _metodosPagoNegocio.EliminarMetodoPago(id);
            if (resultado)
            {
                return Ok(new { Message = "Método de pago eliminado exitosamente." });
            }
            return NotFound("Método de pago no encontrado.");
        }
    }
}