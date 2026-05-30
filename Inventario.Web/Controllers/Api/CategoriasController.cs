using Inventario.Application.Categorias.Commands;
using Inventario.Application.Categorias.Queries;
using Inventario.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Web.Controllers.Api;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoriasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<CategoriaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategorias()
    {
        var categorias = await _mediator.Send(new GetCategoriasQuery());
        return Ok(categorias);
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategoria(CrearCategoriaCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetCategorias), new { id }, id);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCategoria(int id, ActualizarCategoriaCommand command)
    {
        if (id != command.Id)
            return BadRequest(new ProblemDetails { Title = "ID Mismatch", Detail = "El ID de la ruta no coincide con el cuerpo." });

        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        await _mediator.Send(new EliminarCategoriaCommand(id));
        return NoContent();
    }
}
