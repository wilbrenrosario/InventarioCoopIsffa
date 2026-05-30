using Inventario.Domain.Enums;

namespace Inventario.Domain.Entities;

public class MovimientoStock
{
    public int Id { get; set; }
    public int ProductoId { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public string? Comentario { get; set; }

    // Navigation property
    public virtual Producto Producto { get; set; } = null!;
}
