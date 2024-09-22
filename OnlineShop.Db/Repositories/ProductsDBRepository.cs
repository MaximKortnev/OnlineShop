using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.Db.Repositories
{
    public class ProductsDBRepository : IProductsRepository
    {
        private readonly DatabaseContext databaseContext;

        public ProductsDBRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task<Product> TryGetProductByIdAsync(Guid id) => await databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<List<Product>> GetAllAsync()
        {
            return await databaseContext.Products.ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await databaseContext.Products.AddAsync(product);
            await databaseContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Guid productId)
        {
            var productToDelete = await TryGetProductByIdAsync(productId);

            if (productToDelete != null)
            {
                databaseContext.Products.Remove(productToDelete);
                await databaseContext.SaveChangesAsync();
            }
        }
        public async Task EditAsync(Product product)
        {
            var existingProduct = await TryGetProductByIdAsync(product.Id);

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

                await databaseContext.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> SearchAsync(string productName)
        {
            var products = await databaseContext.Products.Where(p => p.Name.Contains(productName)).ToListAsync();
            return products;
        }
    }
}
