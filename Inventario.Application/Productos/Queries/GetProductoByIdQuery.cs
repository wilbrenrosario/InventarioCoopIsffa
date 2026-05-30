using Inventario.Application.DTOs;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Productos.Queries;

public class GetProductoByIdQuery : IRequest<ProductoDto?>
{
    public int Id { get; set; }

    public GetProductoByIdQuery(int id)
    {
        Id = id;
    }
}

public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, ProductoDto?>
{
    private readonly IProductoRepository _productoRepository;

    public GetProductoByIdQueryHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<ProductoDto?> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
    {
        var p = await _productoRepository.GetByIdAsync(request.Id);
        
        if (p == null) return null;

        return new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            CategoriaId = p.CategoriaId,
            CategoriaNombre = p.Categoria?.Nombre ?? "",
            Sku = p.Sku,
            Precio = p.Precio,
            StockActual = p.StockActual,
            Activo = p.Activo
        };
    }
}
