using Microsoft.EntityFrameworkCore;
using ShopWebApp.Data;
using ShopWebApp.Interfaces;
using ShopWebApp.Models;

namespace ShopWebApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationContext _db;

        public ProductRepository(ApplicationContext db)
        {
            _db = db;
        }
        public bool Add(Product product)
        {
            _db.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _db.Remove(product);
            return Save();
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<Product> GetProductByIdAsyncNoTracking(int id)
        {
            return await _db.Products.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _db.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product product)
        {
            _db.Update(product);
            return Save();
        }
    }
}
