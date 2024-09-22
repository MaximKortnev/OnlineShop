using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        Task<Product> TryGetProductByIdAsync(Guid productId);
        Task<List<Product>> GetAllAsync();
        Task AddAsync(Product product);
        Task DeleteAsync(Guid productId);
        Task EditAsync(Product product);
        Task<List<Product>> SearchAsync(string productName);
    }
}
