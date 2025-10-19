using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Taipa.API.Modelos.Enum;
using Taipa.API.Negocio.Interface;

namespace Taipa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenNegocio _ordenNegocio;
        public OrdenController(IOrdenNegocio ordenNegocio)
        {
            _ordenNegocio = ordenNegocio;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerOrdenPorId(Guid id)
        {
            var orden = await _ordenNegocio.ObtenerOrdenPorId(id);
            if (orden == null)
            {
                return NotFound();
            }
            return Ok(orden);
        }
        [HttpPost("ActualizarOrden/{idVenta}")]
        public async Task<IActionResult> ActualizarOrden(Guid idVenta ,EstadoOrden nuevoEstado)
        {
            var resultado = await _ordenNegocio.CambiarEstadoOrdenAsync(idVenta, nuevoEstado);
            if (!resultado)
            {
                return BadRequest("No se pudo actualizar el estado del orden.");
            }
            return Ok("Estado del orden actualizado correctamente.");
        }
    }
}