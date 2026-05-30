using Inventario.Application.Movimientos.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Web.Controllers.Api;

[ApiController]
[Route("api/v1/Movimientos")]
public class MovimientosApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public MovimientosApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<int>> Registrar([FromBody] RegistrarMovimientoCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }
}
