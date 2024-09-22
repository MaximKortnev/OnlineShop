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
      
        public async Task SaveOrdersAsync(Order order, string userId, Cart cart)
        {
            order.ListProducts = cart.Items;
            order.Data = DateTime.Now;
            await databaseContext.Orders.AddAsync(order);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<Order> TryGetByIdAsync(Guid Id) => await databaseContext.Orders.FirstOrDefaultAsync(x => x.Id == Id);

        public async Task<List<Order>> GetAllAsync()
        {
            return await databaseContext.Orders
                        .Include(x => x.ListProducts)
                        .ThenInclude(p => p.Product)
                        .ToListAsync();
        }

        public async Task EditStatusAsync(Guid Id, OrderStatus status)
        {
            var order = await TryGetByIdAsync(Id);
            if (order != null)
            {
                order.Status = status;
            }
            await databaseContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid Id)
        {
            var order = await TryGetByIdAsync(Id);
            if (order != null)
            {
                databaseContext.Orders.Remove(order);
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllByUserAsync(string userId)
        {
            return await databaseContext.Orders.Where(u => u.UserId == userId).Include(x => x.ListProducts).ToListAsync();
        }
    }
}
