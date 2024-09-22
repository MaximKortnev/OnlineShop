using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        Task SaveOrdersAsync(Order orderData, string userId, Cart existingCart);
        Task<List<Order>> GetAllAsync();
        Task<Order> TryGetByIdAsync(Guid Id);
        Task EditStatusAsync(Guid Id, OrderStatus status);
        Task DeleteAsync(Guid Id);
        Task<List<Order>> GetAllByUserAsync(string userId);
    }
}
