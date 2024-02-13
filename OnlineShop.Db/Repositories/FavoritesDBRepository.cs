using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace OnlineShop.Db.Repositories
{
    public class FavoritesDBRepository : IFavoritesRepository
    {
        private readonly DatabaseContext databaseContext;

        public FavoritesDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public void Add(Product product, string userId)
        {
            var existingProduct = databaseContext.Favorites.FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            if (existingProduct == null)
            {
                databaseContext.Favorites.Add(new Favorite { Product = product, UserId = userId });
                databaseContext.SaveChanges();
            }
        }

        public void Decrease(Product product, string userId)
        {
            var removingFavorite = databaseContext.Favorites.FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            databaseContext.Favorites.Remove(removingFavorite);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var itemsToRemove = databaseContext.Favorites
                .Where(x => x.UserId == userId)
                .ToList();

            if (itemsToRemove.Any())
            {
                databaseContext.Favorites.RemoveRange(itemsToRemove);
                databaseContext.SaveChanges();
            }
        }
        public List<Product> GetAll(string userId)
        {
            return databaseContext.Favorites.Where(u => u.UserId == userId).Include(x => x.Product).Select(x => x.Product).ToList();
        }
    }
};