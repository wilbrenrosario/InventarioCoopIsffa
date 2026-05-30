using Inventario.Application.DTOs;
using Inventario.Domain.Interfaces.Repositories;
using MediatR;
using AutoMapper;

namespace Inventario.Application.Categorias.Queries;

public class GetCategoriasQuery : IRequest<List<CategoriaDto>>
{
}

public class GetCategoriasQueryHandler : IRequestHandler<GetCategoriasQuery, List<CategoriaDto>>
{
    private readonly ICategoriaRepository _repository;
    private readonly IMapper _mapper;

    public GetCategoriasQueryHandler(ICategoriaRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<CategoriaDto>> Handle(GetCategoriasQuery request, CancellationToken cancellationToken)
    {
        var categorias = await _repository.GetAllAsync();
        return _mapper.Map<List<CategoriaDto>>(categorias);
    }
}
