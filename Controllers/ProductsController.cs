using Microsoft.AspNetCore.Mvc;
using PoyFC_Aranda.Modelo;
using PoyFC_Aranda.Services;

namespace PoyFC_Aranda.Controllers
{
    [ApiController]
    [Route("api/Productos")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productosService;

        public ProductsController(IProductService productosService)
        {
            _productosService = productosService;
        }
        
        [HttpGet (Name ="GetProductos")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductos()
        {
            try
            {
                var productos = await _productosService.ObtenerTodosLosProductos();
                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los productos.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducto(int id)
        {
            try
            {
                var producto = await _productosService.ObtenerProductoPorId(id);
                if (producto == null)
                {
                    return NotFound();
                }

                return producto;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener el producto.");
            }
        }

        [HttpPost (Name ="CrearProducto")]
        public async Task<ActionResult<Product>> CrearProducto(Product producto)
        {
            try
            {
                var nuevoProducto = await _productosService.CrearProducto(producto);
                return CreatedAtAction(nameof(GetProducto), new { id = nuevoProducto.Id }, nuevoProducto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el producto.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarProducto(int id, Product producto)
        {
            try
            {
                if (id != producto.Id)
                {
                    return BadRequest();
                }

                await _productosService.ActualizarProducto(producto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar el producto.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarProducto(int id)
        {
            try
            {
                await _productosService.EliminarProducto(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al eliminar el producto.");
            }
        }
        [HttpGet ("GetProductosFiltro")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductos(string filtro)
        {
            try
            {
                if (string.IsNullOrEmpty(filtro))
                {
                    var productos = await _productosService.ObtenerTodosLosProductos();
                    return Ok(productos);
                }
                else
                {
                    var productos = await _productosService.ObtenerProductosPorFiltro(filtro);
                    return Ok(productos);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al obtener los productos.");
            }
        }

    }
}
