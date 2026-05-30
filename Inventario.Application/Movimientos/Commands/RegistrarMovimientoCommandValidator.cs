using FluentValidation;

namespace Inventario.Application.Movimientos.Commands;

public class RegistrarMovimientoCommandValidator : AbstractValidator<RegistrarMovimientoCommand>
{
    public RegistrarMovimientoCommandValidator()
    {
        RuleFor(x => x.ProductoId).GreaterThan(0);
        RuleFor(x => x.Tipo).IsInEnum();
        RuleFor(x => x.Cantidad).GreaterThan(0).WithMessage("La cantidad debe ser mayor a 0.");
    }
}
