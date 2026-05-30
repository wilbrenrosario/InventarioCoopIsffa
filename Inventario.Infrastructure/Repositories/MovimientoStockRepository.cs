using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces.Repositories;
using Inventario.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Inventario.Infrastructure.Repositories;

public class MovimientoStockRepository : IMovimientoStockRepository
{
    private readonly ApplicationDbContext _context;

    public MovimientoStockRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovimientoStock>> GetByProductoIdAsync(int productoId, DateTime? startDate = null, DateTime? endDate = null)
    {
        var query = _context.Movimientos.Where(m => m.ProductoId == productoId);
        
        if (startDate.HasValue) query = query.Where(m => m.Fecha >= startDate.Value);
        if (endDate.HasValue) query = query.Where(m => m.Fecha <= endDate.Value);
        
        return await query.OrderByDescending(m => m.Fecha).ToListAsync();
    }

    public async Task AddAsync(MovimientoStock movimiento)
    {
        await _context.Movimientos.AddAsync(movimiento);
    }

    public async Task<IEnumerable<MovimientoStock>> GetAllAsync()
    {
        return await _context.Movimientos
            .Include(m => m.Producto)
            .OrderByDescending(m => m.Fecha)
            .ToListAsync();
    }
}
