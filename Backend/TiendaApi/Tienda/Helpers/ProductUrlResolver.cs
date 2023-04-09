using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using TiendaApi.Dtos;

namespace Tienda.Helpers
{
    public class ProductUrlResolver : IValueResolver<Producto, ProductoDto, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Producto source, ProductoDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.FotoUrl))
            {
                return _config["ApiUrl"] + source.FotoUrl;
            }

            return null;
        }
    }
}
