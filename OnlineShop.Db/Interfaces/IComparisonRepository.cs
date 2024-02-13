using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IComparisonRepository
    {
        void Add(Product product, string userId);
        void Delete(Product product, string userId);
        void Clear(string userId);
        List<Product> GetAll(string userId);
    }
}
