using Inventario.Application.DTOs;
using Inventario.Application.Productos.Commands;
using Inventario.Application.Productos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Web.Controllers.Api;

[ApiController]
[Route("api/v1/Productos")]
public class ProductosApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductosApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductoDto>>> Get()
    {
        var productos = await _mediator.Send(new GetProductosQuery());
        return Ok(productos);
    }

    [HttpPost]
    public async Task<ActionResult<int>> Create([FromBody] CrearProductoCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }
}
