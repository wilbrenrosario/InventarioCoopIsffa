using Inventario.Application.DTOs;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Reportes.Queries;

public class GetProductosBajoStockQuery : IRequest<List<ProductoBajoStockDto>>
{
    public int Umbral { get; set; } = 10;
}

public class GetProductosBajoStockQueryHandler : IRequestHandler<GetProductosBajoStockQuery, List<ProductoBajoStockDto>>
{
    private readonly IProductoRepository _productoRepository;

    public GetProductosBajoStockQueryHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<List<ProductoBajoStockDto>> Handle(GetProductosBajoStockQuery request, CancellationToken cancellationToken)
    {
        var productos = await _productoRepository.GetAllAsync();
        
        return productos
            .Where(p => p.Activo && p.StockActual <= request.Umbral)
            .Select(p => new ProductoBajoStockDto
            {
                ProductoId = p.Id,
                Sku = p.Sku,
                Nombre = p.Nombre,
                CategoriaNombre = p.Categoria.Nombre,
                StockActual = p.StockActual,
                Umbral = request.Umbral
            })
            .OrderBy(p => p.StockActual)
            .ToList();
    }
}
