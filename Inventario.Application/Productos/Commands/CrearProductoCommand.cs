using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Productos.Commands;

public class CrearProductoCommand : IRequest<int>
{
    public string Nombre { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public decimal Precio { get; set; }
}

public class CrearProductoCommandHandler : IRequestHandler<CrearProductoCommand, int>
{
    private readonly IProductoRepository _productoRepository;

    public CrearProductoCommandHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<int> Handle(CrearProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = new Producto
        {
            Nombre = request.Nombre,
            CategoriaId = request.CategoriaId,
            Sku = request.Sku,
            Precio = request.Precio,
            Activo = true,
            FechaCreacion = DateTime.UtcNow
        };

        await _productoRepository.AddAsync(producto);

        return producto.Id;
    }
}
