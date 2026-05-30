namespace Inventario.Application.DTOs;

public class ProductoDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public string CategoriaNombre { get; set; } = string.Empty;
    public string Sku { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public int StockActual { get; set; }
    public bool Activo { get; set; }
}
