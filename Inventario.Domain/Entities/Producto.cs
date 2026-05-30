using Inventario.Domain.Exceptions;

namespace Inventario.Domain.Entities;

public class Producto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int StockActual { get; private set; }
    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    // Concurrency token
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    // Navigation properties
    public virtual Categoria Categoria { get; set; } = null!;
    public virtual ICollection<MovimientoStock> Movimientos { get; set; } = new List<MovimientoStock>();

    public void AddStock(int cantidad)
    {
        if (cantidad <= 0) throw new DomainException("La cantidad debe ser mayor a 0");
        StockActual += cantidad;
    }

    public void RemoveStock(int cantidad)
    {
        if (cantidad <= 0) throw new DomainException("La cantidad debe ser mayor a 0");
        if (StockActual - cantidad < 0)
        {
            throw new NegativeStockException(Id, cantidad, StockActual);
        }
        StockActual -= cantidad;
    }
}
