using CsvHelper;
using Inventario.Application.DTOs;
using Inventario.Application.Productos.Queries;
using Inventario.Application.Reportes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Inventario.Web.Controllers;

public class ReportesController : Controller
{
    private readonly IMediator _mediator;

    public ReportesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var reporte = await _mediator.Send(new GetValorizacionQuery());
        return View(reporte);
    }

    public async Task<IActionResult> ExportarCsv()
    {
        var reporte = await _mediator.Send(new GetValorizacionQuery());
        
        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        
        csv.WriteHeader(typeof(ReporteValorizacionDto));
        csv.NextRecord();
        foreach (var item in reporte)
        {
            csv.WriteRecord(item);
            csv.NextRecord();
        }
        
        writer.Flush();
        var content = memoryStream.ToArray();
        return File(content, "text/csv", $"valorizacion_{DateTime.Now:yyyyMMdd}.csv");
    }

    public async Task<IActionResult> BajoStock(int umbral = 10)
    {
        var reporte = await _mediator.Send(new GetProductosBajoStockQuery { Umbral = umbral });
        ViewBag.Umbral = umbral;
        return View(reporte);
    }

    public async Task<IActionResult> ExportarBajoStockCsv(int umbral = 10)
    {
        var reporte = await _mediator.Send(new GetProductosBajoStockQuery { Umbral = umbral });

        using var memoryStream = new MemoryStream();
        using var writer = new StreamWriter(memoryStream);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

        csv.WriteHeader(typeof(ProductoBajoStockDto));
        csv.NextRecord();
        foreach (var item in reporte)
        {
            csv.WriteRecord(item);
            csv.NextRecord();
        }

        writer.Flush();
        var content = memoryStream.ToArray();
        return File(content, "text/csv", $"bajo_stock_{DateTime.Now:yyyyMMdd}.csv");
    }

    public async Task<IActionResult> HistorialMovimientos(int? productoId, DateTime? fechaInicio, DateTime? fechaFin)
    {
        var productos = await _mediator.Send(new GetProductosQuery());
        ViewBag.Productos = productos;

        var query = new GetHistorialMovimientosQuery
        {
            ProductoId = productoId,
            FechaInicio = fechaInicio ?? DateTime.Now.AddDays(-30),
            FechaFin = fechaFin ?? DateTime.Now
        };

        var historial = await _mediator.Send(query);

        ViewBag.ProductoId = productoId;
        ViewBag.FechaInicio = query.FechaInicio.ToString("yyyy-MM-dd");
        ViewBag.FechaFin = query.FechaFin.ToString("yyyy-MM-dd");

        return View(historial);
    }
}
