using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tienda.Controllers;
using Tienda.Errors;
using TiendaApi.Dtos;
using Core.Specifications;
using Tienda.Dtos;

namespace TiendaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : BaseApiController
    {
        private readonly IGenericRepository<Producto> _ProductoRepository;
        private readonly IGenericRepository<Marca> _MarcaRepository;
        private readonly IGenericRepository<Tipo> _TipoRepository;

        private readonly IMapper _mapper;

        public ProductosController(IGenericRepository<Producto> productoRepository, IGenericRepository<Marca> marcaRepository, IGenericRepository<Tipo> tipoRepository, IMapper mapper)
        {
            _ProductoRepository = productoRepository;
            _MarcaRepository = marcaRepository;
            _TipoRepository = tipoRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Listar todos los productos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
         public async Task<ActionResult<Pagination<ProductoDto>>> GetProducts(
            [FromQuery] ProductosSpecParams productoParams)
        {
            var spec = new ProductosConMarcayTipo(productoParams);
            var countSpec = new ProductosConFiltrosParaEspecificaciónDeConteo(productoParams);

            var totalPruductos = await _ProductoRepository.CountAsync(countSpec);
            var productos = await _ProductoRepository.ListAsync(spec);


            var data = _mapper.Map<IReadOnlyList<ProductoDto>>(productos);

            return Ok(new Pagination<ProductoDto>(productoParams.PageIndex,productoParams.PageSize,totalPruductos,data));
        }

        /// <summary>
        /// Trae un producto por id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var spec = new ProductosConMarcayTipo(id);
            var producto = await _ProductoRepository.GetEntityWithSpec(spec);

            if (producto == null) return NotFound(new ApiResponse(404)); 

            return _mapper.Map<Producto, ProductoDto>(producto); 
        }

        [HttpGet("marcas")]
        public async Task<ActionResult<IReadOnlyList<Marca>>> getMarcas()
        {
            return Ok(await _MarcaRepository.ListAllAsync()); 
        }

        [HttpGet("tipos")]
        public async Task<ActionResult<IReadOnlyList<Tipo>>> getTipos()
        {
            return Ok(await _TipoRepository.ListAllAsync());
        }
    }
}
