using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db.Repositories
{
    public class OrdersDBRepository : IOrdersRepository
    {
        private readonly DatabaseContext databaseContext;

        public OrdersDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
      
        public void SaveOrders(Order order, string userId, Cart cart)
        {
            order.ListProducts = cart.Items;
            order.Data = DateTime.Now;
            databaseContext.Orders.Add(order);
            databaseContext.SaveChanges();
        }

        public Order TryGetById(Guid Id) => databaseContext.Orders.FirstOrDefault(x => x.Id == Id);

        public List<Order> GetAll()
        {
            return databaseContext.Orders
            .Include(x => x.ListProducts)
            .ThenInclude(p => p.Product)
            .ToList();
        }

        public void EditStatus(Guid Id, OrderStatus status)
        {
            var order = TryGetById(Id);
            if (order != null)
            {
                order.Status = status;
            }
            databaseContext.SaveChanges();
        }
        public void Delete(Guid Id)
        {
            var order = TryGetById(Id);
            if (order != null)
            {
                databaseContext.Orders.Remove(order);
            }
            databaseContext.SaveChanges();
        }

        public List<Order> GetAllByUser(string userId)
        {
            return databaseContext.Orders.Where(u => u.UserId == userId).Include(x => x.ListProducts).ToList();
        }
    }
}
