using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IComparisonRepository
    {
        Task AddAsync(Product product, string userId);
        Task DeleteAsync(Product product, string userId);
        Task ClearAsync(string userId);
        Task<List<Product>> GetAllAsync(string userId);
    }
}
