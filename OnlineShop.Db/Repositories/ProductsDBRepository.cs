using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class ProductsDBRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public Product TryGetProductById(Guid id) => databaseContext.Products.FirstOrDefault(p => p.Id == id);

        public List<Product> GetAll()
        {
            return databaseContext.Products.ToList();
        }

        public void Add(Product product)
        {
            databaseContext.Products.Add(product);
            databaseContext.SaveChanges();
        }
        public void Delete(Guid productId)
        {
            var productToDelete = databaseContext.Products.FirstOrDefault(x => x.Id == productId);

            if (productToDelete != null)
            {
                databaseContext.Products.Remove(productToDelete);
                databaseContext.SaveChanges();
            }
        }
        public void Edit(Product product)
        {
            var existingProduct = databaseContext.Products.FirstOrDefault(x => x.Id == product.Id);

            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Author = product.Author;
                existingProduct.Cost = product.Cost;
                existingProduct.Description = product.Description;
                existingProduct.Quote = product.Quote;
                existingProduct.AboutAuthor = product.AboutAuthor;
                existingProduct.AboutTheBook = product.AboutTheBook;
                existingProduct.ImagePath = product.ImagePath;

                databaseContext.SaveChanges();
            }
        }

        public List<Product> Search(string productName)
        {
            var products = databaseContext.Products.Where(p => p.Name.Contains(productName)).ToList();
            return products;
        }
    }
}
