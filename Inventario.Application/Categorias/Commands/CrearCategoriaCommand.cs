using Inventario.Domain.Entities;
using Inventario.Domain.Interfaces;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;

namespace Inventario.Application.Categorias.Commands;

public class CrearCategoriaCommand : IRequest<int>
{
    public string Nombre { get; set; } = string.Empty;
}

public class CrearCategoriaCommandHandler : IRequestHandler<CrearCategoriaCommand, int>
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CrearCategoriaCommandHandler(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CrearCategoriaCommand request, CancellationToken cancellationToken)
    {
        var categoria = new Categoria
        {
            Nombre = request.Nombre
        };

        await _categoriaRepository.AddAsync(categoria);
        
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

        return categoria.Id;
    }
}
