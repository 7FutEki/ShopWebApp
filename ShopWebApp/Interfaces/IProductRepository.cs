using ShopWebApp.Models;

namespace ShopWebApp.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> GetProductByIdAsyncNoTracking(int id);

        bool Add(Product product);
        bool Delete(Product product);
        bool Update(Product product);
        bool Save();
    }
}
