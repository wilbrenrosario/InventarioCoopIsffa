using Inventario.Domain.Entities;

namespace Inventario.Domain.Interfaces.Repositories;

public interface ICategoriaRepository
{
    Task<Categoria?> GetByIdAsync(int id);
    Task<IEnumerable<Categoria>> GetAllAsync();
    Task AddAsync(Categoria categoria);
    Task UpdateAsync(Categoria categoria);
    Task DeleteAsync(Categoria categoria);
}
