using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IOrdersRepository
    {
        void SaveOrders(Order orderData, string userId, Cart existingCart);
        List<Order> GetAll();
        Order TryGetById(Guid Id);
        void EditStatus(Guid Id, OrderStatus status);
        void Delete(Guid Id);
        List<Order> GetAllByUser(string userId);
    }
}
