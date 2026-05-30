using Inventario.Domain.Interfaces;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Categorias.Commands;

public class EliminarCategoriaCommand : IRequest<Unit>
{
    public int Id { get; set; }

    public EliminarCategoriaCommand(int id)
    {
        Id = id;
    }
}

public class EliminarCategoriaCommandHandler : IRequestHandler<EliminarCategoriaCommand, Unit>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EliminarCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(EliminarCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(request.Id);
        if (categoria == null)
            throw new Exception("Categoría no encontrada");

        await _categoriaRepository.DeleteAsync(categoria);
        
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
