namespace Inventario.Domain.Exceptions;

public class NegativeStockException : DomainException
{
    public NegativeStockException(int productoId, int stockDeseado, int stockActual) 
        : base($"No se puede registrar la salida. El producto con Id {productoId} tiene un stock de {stockActual} y se intentó descontar {stockDeseado}.")
    {
    }
}
