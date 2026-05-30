using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Categoria?> GetByIdAsync(int id)
    {
        return await _context.Categorias.FindAsync(id);
    }

    public async Task<IEnumerable<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.OrderBy(c => c.Nombre).ToListAsync();
    }

    public async Task AddAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
    }
}
