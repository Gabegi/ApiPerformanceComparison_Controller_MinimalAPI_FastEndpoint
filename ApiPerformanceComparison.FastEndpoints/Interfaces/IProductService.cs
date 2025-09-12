using ApiPerformanceComparison.Shared;

namespace ApiPerformanceComparison.FastEndpoints.Interfaces
{
    // ====================
    // Product Service (Performance Optimized)
    // ====================
    public interface IProductService
    {
        List<Product> GetProducts();
        List<Product> GetProducts(int count);
        Product? GetProduct(int id);
        Product CreateProduct(string name, decimal price);
        Product? UpdateProduct(int id, string name, decimal price);
        bool DeleteProduct(int id);
    }
}