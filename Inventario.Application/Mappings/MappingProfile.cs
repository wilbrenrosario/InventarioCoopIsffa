using AutoMapper;
using Inventario.Application.DTOs;
using Inventario.Domain.Entities;

namespace Inventario.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Producto, ProductoDto>()
            .ForMember(dest => dest.CategoriaNombre, opt => opt.MapFrom(src => src.Categoria.Nombre));
            
        CreateMap<Categoria, CategoriaDto>();
    }
}
