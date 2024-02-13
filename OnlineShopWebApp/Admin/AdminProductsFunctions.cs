using Newtonsoft.Json;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.IO;

namespace OnlineShopWebApp.Admin
{
    public class AdminProductsFunctions : IAdminProductsFunctions
    {
        private readonly IProductsRepository productRepository;
        public AdminProductsFunctions(IProductsRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public void Edit(Product product)
        {
            var products = productRepository.GetAll();
            int index = product.Id - 1;

            product.ImagePath = "image.jpg";
            products[index] = product;
            Save(products);
        }
        public void Delete(int productId)
        {
            var products = productRepository.GetAll();
            int index = products.FindIndex(p=>p.Id == productId);
            products.RemoveAt(index);
            Save(products);
        }
        public void Add(Product product)
        {
            var products = productRepository.GetAll();
            product.ImagePath = "image.jpg";
            products.Add(product);
            Save(products);
        }

        public void Save(List<Product> products)
        {
            var filePath = "wwwroot/data.json";
            string updatedJson = JsonConvert.SerializeObject(products, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }
    }
}
