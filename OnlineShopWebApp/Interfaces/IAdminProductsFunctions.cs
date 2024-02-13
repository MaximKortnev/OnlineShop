using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Interfaces
{
    public interface IAdminProductsFunctions
    {
        void Edit(Product product);
        void Delete(int productId);
        void Add(Product product);
    }
}
