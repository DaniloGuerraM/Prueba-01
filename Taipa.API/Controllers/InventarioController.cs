using Microsoft.AspNetCore.Mvc;
using Taipa.API.Modelos.DTO;
using Taipa.API.Modelos.Response;
using Taipa.API.Negocio.Interface;

namespace Taipa.API.Controladores;

[Route("api/inventario")]
[ApiController]
public class InventarioController : ControllerBase
{
    private readonly IInventarioNegocio _inventarioNegocio;

    public InventarioController(IInventarioNegocio inventarioNegocio)
    {
        _inventarioNegocio = inventarioNegocio;
    }

    [HttpGet]
    public async Task<ActionResult<List<InventarioResponse>>> ListarTodos()
    {
        try
        {
            var inventarios = await _inventarioNegocio.ListarTodos();
            return Ok(inventarios);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InventarioResponse>> ListarPorId(Guid id)
    {
        try
        {
            var inventario = await _inventarioNegocio.ListarPorId(id);
            if (inventario == null)
            {
                return NotFound($"Inventario con ID {id} no encontrado");
            }
            return Ok(inventario);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<ActionResult<InventarioResponse>> Crear([FromBody] InventarioDTO inventarioDTO)
    {
        try
        {
            if (inventarioDTO == null)
            {
                return BadRequest("Los datos del inventario son requeridos");
            }

            if (inventarioDTO.Cantidad < 0)
            {
                return BadRequest("La cantidad no puede ser negativa");
            }

            var inventarioCreado = await _inventarioNegocio.Crear(inventarioDTO);
            return CreatedAtAction(nameof(ListarPorId), new { id = inventarioCreado.Id_Inventario }, inventarioCreado);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Eliminar(Guid id)
    {
        try
        {
            var resultado = await _inventarioNegocio.Eliminar(id);
            if (!resultado)
            {
                return NotFound($"Inventario con ID {id} no encontrado");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }
}