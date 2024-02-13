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

        public void Add(Product product, string userId)
        {
            var existingProduct = databaseContext.Comparisons.FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            if (existingProduct == null)
            {
                databaseContext.Comparisons.Add(new Comparison { Product = product, UserId = userId });
                databaseContext.SaveChanges();
            }
        }

        public List<Product> GetAll(string userId)
        {
            return databaseContext.Comparisons.Where(u => u.UserId == userId).Include(x => x.Product).Select(x => x.Product).ToList();
        }

        public void Delete(Product product, string userId)
        {
            var removingCompare = databaseContext.Comparisons.FirstOrDefault(x => x.UserId == userId && x.Product.Id == product.Id);
            databaseContext.Comparisons.Remove(removingCompare);
            databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var itemsToRemove = databaseContext.Comparisons
        .Where(x => x.UserId == userId)
        .ToList();

            if (itemsToRemove.Any())
            {
                databaseContext.Comparisons.RemoveRange(itemsToRemove);
                databaseContext.SaveChanges();
            }
        }
    }
}
