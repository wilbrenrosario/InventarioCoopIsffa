using Inventario.Domain.Interfaces;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Categorias.Commands;

public class ActualizarCategoriaCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}

public class ActualizarCategoriaCommandHandler : IRequestHandler<ActualizarCategoriaCommand, Unit>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActualizarCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ActualizarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(request.Id);
        if (categoria == null)
            throw new Exception("Categoría no encontrada");

        categoria.Nombre = request.Nombre;

        await _categoriaRepository.UpdateAsync(categoria);
        
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }

        return Unit.Value;
    }
}
