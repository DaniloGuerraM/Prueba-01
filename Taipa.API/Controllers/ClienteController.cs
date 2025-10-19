using Taipa.App.Entidades;
using Taipa.App.Modelos.DTO;
using Taipa.App.Negocio.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Taipa.App.Controladores;

[ApiController]
[Route("api/clientes")]
public class ClienteController : ControllerBase
{
    private readonly IClienteNegocio _clienteNegocio;

    public ClienteController(IClienteNegocio clienteNegocio)
    {
        _clienteNegocio = clienteNegocio;
    }

    // GET: api/clientes
    [HttpGet]
    public async Task<ActionResult<List<Cliente>>> ListarTodos()
    {
        var clientes = await _clienteNegocio.ListarTodos();
        return Ok(clientes);
    }

    // GET: api/clientes/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> ObtenerPorId(Guid id)
    {
        var cliente = await _clienteNegocio.ObtenerPorId(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return Ok(cliente);
    }

    // GET: api/clientes/dni/{dni}
    [HttpGet("dni/{dni}")]
    public async Task<ActionResult<Cliente>> ObtenerPorDni(int dni)
    {
        var cliente = await _clienteNegocio.ObtenerPorDni(dni);
        if (cliente == null)
        {
            return NotFound();
        }
        return Ok(cliente);
    }

    // POST: api/clientes
    [HttpPost]
    public async Task<ActionResult<Cliente>> Crear([FromBody] ClienteDTO clienteDTO)
    {
        var cliente = await _clienteNegocio.Crear(clienteDTO);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = cliente.IdCliente }, cliente);
    }

    // PUT: api/clientes/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ClienteDTO clienteDTO)
    {
        var resultado = await _clienteNegocio.Actualizar(id, clienteDTO);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
    }

    // DELETE: api/clientes/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(Guid id)
    {
        var resultado = await _clienteNegocio.Eliminar(id);
        if (!resultado)
        {
            return NotFound();
        }
        return NoContent();
    }
}