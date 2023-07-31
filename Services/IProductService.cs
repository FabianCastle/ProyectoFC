using PoyFC_Aranda.Modelo;

namespace PoyFC_Aranda.Services
{
    public interface IProductService
    {
        Task<List<Product>> ObtenerTodosLosProductos();
        Task<List<Product>> ObtenerProductosPorFiltro(string filtro);
        Task<Product> ObtenerProductoPorId(int id);
        Task<Product> CrearProducto(Product producto);
        Task ActualizarProducto(Product producto);
        Task EliminarProducto(int id);
    }
}
