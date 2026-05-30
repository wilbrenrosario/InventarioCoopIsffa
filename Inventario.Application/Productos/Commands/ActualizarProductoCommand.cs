using Inventario.Domain.Interfaces;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Productos.Commands;

public class ActualizarProductoCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public bool Activo { get; set; }
}

public class ActualizarProductoCommandHandler : IRequestHandler<ActualizarProductoCommand, Unit>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActualizarProductoCommandHandler(IProductoRepository productoRepository, IUnitOfWork unitOfWork)
    {
        _productoRepository = productoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ActualizarProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = await _productoRepository.GetByIdAsync(request.Id);
        if (producto == null)
            throw new Exception("Producto no encontrado");

        producto.Nombre = request.Nombre;
        producto.CategoriaId = request.CategoriaId;
        producto.Sku = request.Sku;
        producto.Precio = request.Precio;
        producto.Activo = request.Activo;

        await _productoRepository.UpdateAsync(producto);
        
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }

        return Unit.Value;
    }
}
