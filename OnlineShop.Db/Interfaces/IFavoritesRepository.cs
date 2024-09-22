using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoritesRepository
    {
        Task AddAsync(Product product, string userId);
        Task DecreaseAsync(Product product, string userId);
        Task ClearAsync(string userId);
        Task<List<Product>> GetAllAsync(string userId);

    }
}
