using Inventario.Application.Movimientos.Commands;
using Inventario.Application.Productos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventario.Web.Controllers;

public class MovimientosController : Controller
{
    private readonly IMediator _mediator;

    public MovimientosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var movimientos = await _mediator.Send(new Inventario.Application.Movimientos.Queries.GetMovimientosQuery());
        return View(movimientos);
    }

    public async Task<IActionResult> Registrar()
    {
        var productos = await _mediator.Send(new GetProductosQuery());
        ViewBag.ProductoId = new SelectList(productos.Where(p => p.Activo), "Id", "Nombre");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Registrar(RegistrarMovimientoCommand command)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _mediator.Send(command);
                TempData["Success"] = "Movimiento registrado con éxito.";
                return RedirectToAction("Index", "Productos");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }
        
        var productos = await _mediator.Send(new GetProductosQuery());
        ViewBag.ProductoId = new SelectList(productos.Where(p => p.Activo), "Id", "Nombre");
        return View(command);
    }
}
