using Inventario.Application.DTOs;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Reportes.Queries;

public class GetValorizacionQuery : IRequest<List<ReporteValorizacionDto>>
{
}

public class GetValorizacionQueryHandler : IRequestHandler<GetValorizacionQuery, List<ReporteValorizacionDto>>
{
    private readonly IProductoRepository _productoRepository;

    public GetValorizacionQueryHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<List<ReporteValorizacionDto>> Handle(GetValorizacionQuery request, CancellationToken cancellationToken)
    {
        var productos = await _productoRepository.GetAllAsync();
        
        var reportes = productos
            .Where(p => p.Activo)
            .GroupBy(p => p.Categoria.Nombre)
            .Select(g => new ReporteValorizacionDto
            {
                CategoriaNombre = g.Key,
                CantidadProductos = g.Count(),
                ValorTotal = g.Sum(p => p.Precio * p.StockActual)
            })
            .OrderByDescending(r => r.ValorTotal)
            .ToList();

        return reportes;
    }
}
