using Inventario.Application.DTOs;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Reportes.Queries;

public class GetHistorialMovimientosQuery : IRequest<List<MovimientoHistorialDto>>
{
    public DateTime FechaInicio { get; set; } = DateTime.Now.AddDays(-30);
    public DateTime FechaFin { get; set; } = DateTime.Now;
    public int? ProductoId { get; set; }
}

public class GetHistorialMovimientosQueryHandler : IRequestHandler<GetHistorialMovimientosQuery, List<MovimientoHistorialDto>>
{
    private readonly IMovimientoStockRepository _movimientoRepository;

    public GetHistorialMovimientosQueryHandler(IMovimientoStockRepository movimientoRepository)
    {
        _movimientoRepository = movimientoRepository;
    }

    public async Task<List<MovimientoHistorialDto>> Handle(GetHistorialMovimientosQuery request, CancellationToken cancellationToken)
    {
        var movimientos = await _movimientoRepository.GetAllAsync();
        
        var query = movimientos.Where(m => m.Fecha >= request.FechaInicio && m.Fecha <= request.FechaFin);
        
        if (request.ProductoId.HasValue)
        {
            query = query.Where(m => m.ProductoId == request.ProductoId.Value);
        }

        return query
            .OrderByDescending(m => m.Fecha)
            .Select(m => new MovimientoHistorialDto
            {
                MovimientoId = m.Id,
                ProductoNombre = m.Producto.Nombre,
                Tipo = m.Tipo.ToString(),
                Cantidad = m.Cantidad,
                Fecha = m.Fecha,
                Comentario = m.Comentario ?? string.Empty
            })
            .ToList();
    }
}
