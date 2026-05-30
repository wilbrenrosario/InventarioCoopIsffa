using Inventario.Application.DTOs;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Movimientos.Queries;

public class GetMovimientosQuery : IRequest<List<MovimientoDto>>
{
}

public class GetMovimientosQueryHandler : IRequestHandler<GetMovimientosQuery, List<MovimientoDto>>
{
    private readonly IMovimientoStockRepository _movimientoRepository;

    public GetMovimientosQueryHandler(IMovimientoStockRepository movimientoRepository)
    {
        _movimientoRepository = movimientoRepository;
    }

    public async Task<List<MovimientoDto>> Handle(GetMovimientosQuery request, CancellationToken cancellationToken)
    {
        var movimientos = await _movimientoRepository.GetAllAsync();

        return movimientos.Select(m => new MovimientoDto
        {
            Id = m.Id,
            ProductoId = m.ProductoId,
            ProductoNombre = m.Producto?.Nombre ?? "Desconocido",
            Tipo = m.Tipo.ToString(),
            Cantidad = m.Cantidad,
            Fecha = m.Fecha,
            Comentario = m.Comentario
        }).ToList();
    }
}
