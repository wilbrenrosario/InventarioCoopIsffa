using Inventario.Domain.Enums;
using MediatR;

namespace Inventario.Application.Movimientos.Commands;

public class RegistrarMovimientoCommand : IRequest<int>
{
    public int ProductoId { get; set; }
    public TipoMovimiento Tipo { get; set; }
    public int Cantidad { get; set; }
    public string? Comentario { get; set; }
}
