namespace Inventario.Domain.Entities;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    
    // Navigation property
    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
