using Inventario.Application.Categorias.Queries;
using Inventario.Application.DTOs;
using Inventario.Application.Productos.Commands;
using Inventario.Application.Productos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Inventario.Web.Controllers;

public class ProductosController : Controller
{
    private readonly IMediator _mediator;

    public ProductosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var productos = await _mediator.Send(new GetProductosQuery());
        return View(productos);
    }

    public async Task<IActionResult> Create()
    {
        await PopulateCategorias();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CrearProductoCommand command)
    {
        if (ModelState.IsValid)
        {
            await _mediator.Send(command);
            TempData["Success"] = "Producto creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategorias();
        return View(command);
    }

    private async Task PopulateCategorias()
    {
        var categorias = await _mediator.Send(new GetCategoriasQuery());
        ViewBag.CategoriaId = new SelectList(categorias, "Id", "Nombre");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var producto = await _mediator.Send(new GetProductoByIdQuery(id));
        if (producto == null) return NotFound();

        var command = new ActualizarProductoCommand
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            CategoriaId = producto.CategoriaId,
            Sku = producto.Sku,
            Precio = producto.Precio,
            Activo = producto.Activo
        };

        await PopulateCategorias();
        return View(command);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ActualizarProductoCommand command)
    {
        if (ModelState.IsValid)
        {
            await _mediator.Send(command);
            TempData["Success"] = "Producto actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
        await PopulateCategorias();
        return View(command);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var producto = await _mediator.Send(new GetProductoByIdQuery(id));
        if (producto == null) return NotFound();

        return View(producto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _mediator.Send(new EliminarProductoCommand(id));
        TempData["Success"] = "Producto eliminado exitosamente.";
        return RedirectToAction(nameof(Index));
    }
}
