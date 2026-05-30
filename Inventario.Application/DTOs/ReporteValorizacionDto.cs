namespace Inventario.Application.DTOs;

public class ReporteValorizacionDto
{
    public string CategoriaNombre { get; set; } = string.Empty;
    public decimal ValorTotal { get; set; }
    public int CantidadProductos { get; set; }
}
