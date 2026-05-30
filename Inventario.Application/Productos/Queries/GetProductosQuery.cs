using Inventario.Application.DTOs;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Productos.Queries;

public class GetProductosQuery : IRequest<List<ProductoDto>>
{
}

public class GetProductosQueryHandler : IRequestHandler<GetProductosQuery, List<ProductoDto>>
{
    private readonly IProductoRepository _productoRepository;

    public GetProductosQueryHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<List<ProductoDto>> Handle(GetProductosQuery request, CancellationToken cancellationToken)
    {
        var productos = await _productoRepository.GetAllAsync();
        
        return productos.Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            CategoriaId = p.CategoriaId,
            CategoriaNombre = p.Categoria.Nombre,
            Sku = p.Sku,
            Precio = p.Precio,
            StockActual = p.StockActual,
            Activo = p.Activo
        }).ToList();
    }
}
