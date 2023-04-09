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

namespace TiendaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : BaseApiController
    {
        private readonly IGenericRepository<Producto> _ProductoRepository;
        private readonly IMapper _mapper;

        public ProductosController(IGenericRepository<Producto> productoRepository, IMapper mapper)
        {
            _ProductoRepository = productoRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Listar todos los productos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            var productos = await _ProductoRepository.GetAll(addProperties: "Tipo,Marca");
            return productos.Select(producto => _mapper.Map<Producto,ProductoDto>(producto)).ToList();
        }

        /// <summary>
        /// Trae un producto por id 
        /// </summary>
        /// <returns></returns>
        [HttpGet("id")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _ProductoRepository.GetFirst(x=> x.Id == id, addProperties: "Tipo,Marca");

            if (producto == null) return NotFound(new ApiResponse(404)); 

            return _mapper.Map<Producto, ProductoDto>(producto); 
        }
    }
}
