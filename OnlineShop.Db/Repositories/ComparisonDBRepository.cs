using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db.Repositories
{
    public class ComparisonDBRepository : IComparisonRepository
    {
        private readonly DatabaseContext databaseContext;

        public ComparisonDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task AddAsync(Product product, string userId)
        {
            var existingProduct = await databaseContext.Comparisons.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == product.Id);
            if (existingProduct == null)
            {
                await databaseContext.Comparisons.AddAsync(new Comparison { Product = product, UserId = userId });
                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllAsync(string userId)
        {
            return await databaseContext.Comparisons.Where(u => u.UserId == userId).Include(x => x.Product).Select(x => x.Product).ToListAsync();
        }

        public async Task DeleteAsync(Product product, string userId)
        {
            var removingCompare = await databaseContext.Comparisons.FirstOrDefaultAsync(x => x.UserId == userId && x.Product.Id == product.Id);
            databaseContext.Comparisons.Remove(removingCompare);
            await databaseContext.SaveChangesAsync();
        }

        public async Task ClearAsync(string userId)
        {
            var itemsToRemove = await databaseContext.Comparisons
                                                        .Where(x => x.UserId == userId)
                                                        .ToListAsync();

            if (itemsToRemove.Any())
            {
                databaseContext.Comparisons.RemoveRange(itemsToRemove);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
