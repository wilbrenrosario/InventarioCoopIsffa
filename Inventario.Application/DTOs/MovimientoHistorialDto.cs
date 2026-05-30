namespace Inventario.Application.DTOs;

public class MovimientoHistorialDto
{
    public int MovimientoId { get; set; }
    public string ProductoNombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; }
    public string Comentario { get; set; } = string.Empty;
}
