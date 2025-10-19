using Taipa.API.Entidades;
using Taipa.API.Modelos.DTO;
using Taipa.API.Negocio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Taipa.API.Controladores;


[Route("api/productos")]
[ApiController]
public class ProductoController : ControllerBase
{
    private readonly IProductoNegocio _productoNegocio;

    public ProductoController(IProductoNegocio productoNegocio)
    {
        _productoNegocio = productoNegocio;
    }

    [HttpGet]
    public async Task<ActionResult<List<Productos>>> ListarTodos()
    {
        var productos = await _productoNegocio.ListarTodos();
        return Ok(productos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Productos>> ListarPorId(Guid id)
    {
        var producto = await _productoNegocio.ListarPorId(id);
        if (producto == null)
        {
            return NotFound();
        }
        return Ok(producto);
    }

    [HttpPost]
    public async Task<ActionResult<Productos>> Crear([FromBody] ProductoDTO productoDTO)
    {
        if (productoDTO == null)
        {
            return BadRequest();
        }

        var productoCreado = await _productoNegocio.Crear(productoDTO);
        return CreatedAtAction(nameof(ListarPorId), new { id = productoCreado.Id }, productoCreado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Productos>> Actualizar(Guid id, [FromBody] ProductoDTO productoDTO)
    {
        var resultado = await _productoNegocio.Actualizar(id, productoDTO);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Productos>> Eliminar(Guid id)
    {
        var resultado = await _productoNegocio.Eliminar(id);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
    }

}