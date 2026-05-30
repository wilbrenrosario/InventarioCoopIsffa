using Inventario.Domain.Entities;
using Inventario.Domain.Exceptions;
using Inventario.Domain.Interfaces;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Movimientos.Commands;

public class RegistrarMovimientoCommandHandler : IRequestHandler<RegistrarMovimientoCommand, int>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IMovimientoStockRepository _movimientoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegistrarMovimientoCommandHandler(
        IProductoRepository productoRepository,
        IMovimientoStockRepository movimientoRepository,
        IUnitOfWork unitOfWork)
    {
        _productoRepository = productoRepository;
        _movimientoRepository = movimientoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(RegistrarMovimientoCommand request, CancellationToken cancellationToken)
    {
        var producto = await _productoRepository.GetByIdAsync(request.ProductoId);
        
        if (producto == null)
            throw new DomainException($"Producto con Id {request.ProductoId} no encontrado.");

        if (!producto.Activo)
            throw new DomainException("No se puede registrar movimientos en un producto inactivo.");

        await _unitOfWork.BeginTransactionAsync();
        
        try
        {
            if (request.Tipo == Inventario.Domain.Enums.TipoMovimiento.Entrada)
            {
                producto.AddStock(request.Cantidad);
            }
            else
            {
                producto.RemoveStock(request.Cantidad); // This will throw NegativeStockException if < 0
            }

            var movimiento = new MovimientoStock
            {
                ProductoId = request.ProductoId,
                Tipo = request.Tipo,
                Cantidad = request.Cantidad,
                Comentario = request.Comentario,
                Fecha = DateTime.UtcNow
            };

            await _movimientoRepository.AddAsync(movimiento);
            await _productoRepository.UpdateAsync(producto);

            await _unitOfWork.CommitTransactionAsync();

            return movimiento.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
