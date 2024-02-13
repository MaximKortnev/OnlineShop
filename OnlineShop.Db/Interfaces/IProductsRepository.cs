using OnlineShop.Db.Models;

namespace OnlineShop.Db.Interfaces
{
    public interface IProductsRepository
    {
        Product TryGetProductById(Guid productId);
        List<Product> GetAll();
        void Add(Product product);
        void Delete(Guid productId);
        void Edit(Product product);
        List<Product> Search(string productName);
    }
}
