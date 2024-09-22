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

        public async Task AddAsync(Product product, string userId)
        {
            var existingProduct = await databaseContext.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == product.Id);
            if (existingProduct == null)
            {
                await databaseContext.Favorites.AddAsync(new Favorite { Product = product, UserId = userId });
                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task DecreaseAsync(Product product, string userId)
        {
            var removingFavorite = await databaseContext.Favorites.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == product.Id);
            databaseContext.Favorites.Remove(removingFavorite);
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var itemsToRemove = await databaseContext.Favorites
                                                .Where(x => x.UserId == userId)
                                                .ToListAsync();

            if (itemsToRemove.Any())
            {
                databaseContext.Favorites.RemoveRange(itemsToRemove);
                await databaseContext.SaveChangesAsync();
            }
        }
        public async Task<List<Product>> GetAllAsync(string userId)
        {
            return await databaseContext.Favorites.Where(u => u.UserId == userId).Include(x => x.Product).Select(x => x.Product).ToListAsync();
        }
    }
};