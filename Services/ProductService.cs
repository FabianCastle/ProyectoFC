using Microsoft.EntityFrameworkCore;
using PoyFC_Aranda.Datos;
using PoyFC_Aranda.Modelo;

namespace PoyFC_Aranda.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> ObtenerTodosLosProductos()
        {
            try
            {
                return await _context.Product.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener los productos.", ex);
            }
        }

        public async Task<Product> ObtenerProductoPorId(int id)
        {
            try
            {
                return await _context.Product.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al obtener el producto.", ex);
            }
        }

        public async Task<Product> CrearProducto(Product producto)
        {
            try
            {
                _context.Product.Add(producto);
                await _context.SaveChangesAsync();
                return producto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al crear el producto.", ex);
            }
        }

        public async Task ActualizarProducto(Product producto)
        {
            try
            {
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al actualizar el producto.", ex);
            }
        }

        public async Task EliminarProducto(int id)
        {
            try
            {
                var producto = await _context.Product.FindAsync(id);
                if (producto == null)
                {
                    throw new ApplicationException("Producto no encontrado.");
                }

                _context.Product.Remove(producto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error al eliminar el producto.", ex);
            }
        }
        public async Task<List<Product>> ObtenerProductosPorFiltro(string filtro)
        {
            try
            {
                return await _context.Product
                .Where(p => p.Nombre.Contains(filtro) || p.Descripcion.Contains(filtro) || p.Categoria.Contains(filtro))
                .ToListAsync();
            }
            catch(Exception ex)
            {
                throw new ApplicationException("Error al obtener los productos.", ex);
            }
        }
    }
}
