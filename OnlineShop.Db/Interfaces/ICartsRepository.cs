using Microsoft.VisualBasic;
using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface ICartsRepository
    {
        void Add(Product product, string userId);
        void DecreaseAmount(Product product, string userId);
        void Clear(string userId);
        Cart TryGetByUserId(string userId);
    }
}
