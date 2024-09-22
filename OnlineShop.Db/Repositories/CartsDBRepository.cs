using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db.Repositories
{
    public class CartsDBRepository : ICartsRepository
    {
        private readonly DatabaseContext databaseContext;

        public CartsDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            if (existingCart == null)
            {
                var newCart = new Cart()
                {
                    UserId = userId,
                };
                newCart.Items = new List<CartItem>() { new CartItem() { Amount = 1, Product = product } };
                await databaseContext.Carts.AddAsync(newCart);
            }
            else
            {
                var existingCardItem = existingCart.Items.FirstOrDefault(prod => prod.Product.Id == product.Id);

                if (existingCardItem != null) existingCardItem.Amount += 1;
                else existingCart.Items.Add(new CartItem() { Amount = 1, Product = product });
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task DecreaseAmountAsync(Product product, string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
            if (existingCart != null)
            {
                var existingCardItem = existingCart.Items.FirstOrDefault(prod => prod.Product.Id == product.Id);

                if (existingCardItem != null)
                {
                    if (existingCardItem.Amount > 1) existingCardItem.Amount -= 1;
                    else { existingCart.Items.Remove(existingCardItem); }
                }
                if (existingCart.Items.Count == 0) ClearAsync(userId);
            }
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var existingCart = await TryGetByUserIdAsync(userId);
           // databaseContext.CartItems.RemoveRange(existingCart.Items);
            databaseContext.Carts.Remove(existingCart);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<Cart> TryGetByUserIdAsync(string userId)
        {
            return await databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
};
