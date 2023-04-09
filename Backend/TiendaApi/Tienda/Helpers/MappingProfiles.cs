using AutoMapper;
using Core.Entities;
using TiendaApi.Dtos;

namespace Tienda.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Producto, ProductoDto>()
            .ForMember(p => p.Marca, o => o.MapFrom(o => o.Marca.Nombre))
            .ForMember(p => p.Tipo, o => o.MapFrom(o => o.Tipo.Nombre))
            .ForMember(p => p.FotoUrl, o => o.MapFrom<ProductUrlResolver>());
        }
    }
}
