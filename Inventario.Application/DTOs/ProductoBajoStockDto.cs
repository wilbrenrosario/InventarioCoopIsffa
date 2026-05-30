namespace Inventario.Application.DTOs;

public class ProductoBajoStockDto
{
    public int ProductoId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Nombre { get; set; } = string.Empty;
    public string CategoriaNombre { get; set; } = string.Empty;
    public int StockActual { get; set; }
    public int Umbral { get; set; }
}
