using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        Task AddAsync(Product product, string userId);
        Task DecreaseAmountAsync(Product product, string userId);
        Task ClearAsync(string userId);
        Task<Cart> TryGetByUserIdAsync(string userId);
    }
}
