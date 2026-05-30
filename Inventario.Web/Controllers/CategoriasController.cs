using Inventario.Application.Categorias.Commands;
using Inventario.Application.Categorias.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventario.Web.Controllers;

public class CategoriasController : Controller
{
    private readonly IMediator _mediator;

    public CategoriasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var categorias = await _mediator.Send(new GetCategoriasQuery());
        return View(categorias);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CrearCategoriaCommand command)
    {
        if (ModelState.IsValid)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        return View(command);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categorias = await _mediator.Send(new GetCategoriasQuery());
        var categoria = categorias.FirstOrDefault(c => c.Id == id);
        
        if (categoria == null) return NotFound();

        var command = new ActualizarCategoriaCommand
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre
        };

        return View(command);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ActualizarCategoriaCommand command)
    {
        if (id != command.Id) return BadRequest();

        if (ModelState.IsValid)
        {
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }
        return View(command);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _mediator.Send(new EliminarCategoriaCommand(id));
        return RedirectToAction(nameof(Index));
    }
}
