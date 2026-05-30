using Inventario.Application.DTOs;
using Inventario.Application.Reportes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Web.Controllers.Api;

[ApiController]
[Route("api/v1/Reportes")]
public class ReportesApiController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReportesApiController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("valorizacion")]
    [ProducesResponseType(typeof(List<ReporteValorizacionDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ReporteValorizacionDto>>> GetValorizacion()
    {
        var reporte = await _mediator.Send(new GetValorizacionQuery());
        return Ok(reporte);
    }

    [HttpGet("bajo-stock")]
    [ProducesResponseType(typeof(List<ProductoBajoStockDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProductoBajoStockDto>>> GetBajoStock([FromQuery] int umbral = 10)
    {
        var reporte = await _mediator.Send(new GetProductosBajoStockQuery { Umbral = umbral });
        return Ok(reporte);
    }

    [HttpGet("historial-movimientos")]
    [ProducesResponseType(typeof(List<MovimientoHistorialDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<MovimientoHistorialDto>>> GetHistorialMovimientos(
        [FromQuery] int? productoId,
        [FromQuery] DateTime? fechaInicio,
        [FromQuery] DateTime? fechaFin)
    {
        var query = new GetHistorialMovimientosQuery
        {
            ProductoId = productoId,
            FechaInicio = fechaInicio ?? DateTime.Now.AddDays(-30),
            FechaFin = fechaFin ?? DateTime.Now
        };
        var historial = await _mediator.Send(query);
        return Ok(historial);
    }
}

