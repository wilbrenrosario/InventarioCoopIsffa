using Inventario.Domain.Entities;

namespace Inventario.Domain.Interfaces.Repositories;

public interface IMovimientoStockRepository
{
    Task<IEnumerable<MovimientoStock>> GetByProductoIdAsync(int productoId, DateTime? startDate = null, DateTime? endDate = null);
    Task<IEnumerable<MovimientoStock>> GetAllAsync();
    Task AddAsync(MovimientoStock movimiento);
}
