using Inventario.Domain.Entities;

namespace Inventario.Domain.Interfaces.Repositories;

public interface IProductoRepository
{
    Task<Producto?> GetByIdAsync(int id);
    Task<IEnumerable<Producto>> GetAllAsync();
    Task AddAsync(Producto producto);
    Task UpdateAsync(Producto producto);
}
