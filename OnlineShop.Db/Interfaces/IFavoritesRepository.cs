using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IFavoritesRepository
    {
        void Add(Product product, string userId);
        void Decrease(Product product, string userId);
        void Clear(string userId);
        List<Product> GetAll(string userId);

    }
}
