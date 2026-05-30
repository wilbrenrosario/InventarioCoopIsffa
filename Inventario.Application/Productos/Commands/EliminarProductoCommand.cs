using Inventario.Domain.Interfaces;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Productos.Commands;

public class EliminarProductoCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public EliminarProductoCommand(int id)
    {
        Id = id;
    }
}

public class EliminarProductoCommandHandler : IRequestHandler<EliminarProductoCommand, Unit>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EliminarProductoCommandHandler(IProductoRepository productoRepository, IUnitOfWork unitOfWork)
    {
        _productoRepository = productoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(EliminarProductoCommand request, CancellationToken cancellationToken)
    {
        var producto = await _productoRepository.GetByIdAsync(request.Id);
        if (producto == null)
            throw new Exception("Producto no encontrado");

        // Soft delete
        producto.Activo = false;

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
