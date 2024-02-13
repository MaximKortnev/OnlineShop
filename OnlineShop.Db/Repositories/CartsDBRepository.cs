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

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart == null)
            {
                var newCart = new Cart()
                {
                    UserId = userId,
                };
                newCart.Items = new List<CartItem>() { new CartItem() { Amount = 1, Product = product } };
                databaseContext.Carts.Add(newCart);
            }
            else
            {
                var existingCardItem = existingCart.Items.FirstOrDefault(prod => prod.Product.Id == product.Id);

                if (existingCardItem != null) existingCardItem.Amount += 1;
                else existingCart.Items.Add(new CartItem() { Amount = 1, Product = product });
            }
            databaseContext.SaveChanges();
        }

        public void DecreaseAmount(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart != null)
            {
                var existingCardItem = existingCart.Items.FirstOrDefault(prod => prod.Product.Id == product.Id);

                if (existingCardItem != null)
                {
                    if (existingCardItem.Amount > 1) existingCardItem.Amount -= 1;
                    else { existingCart.Items.Remove(existingCardItem); }
                }
                if (existingCart.Items.Count == 0) Clear(userId);
            }
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var existingCart = TryGetByUserId(userId);
           // databaseContext.CartItems.RemoveRange(existingCart.Items);
            databaseContext.Carts.Remove(existingCart);
            databaseContext.SaveChanges();
        }

        public Cart TryGetByUserId(string userId)
        {
            return databaseContext.Carts.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefault(x => x.UserId == userId);
        }
    }
};
