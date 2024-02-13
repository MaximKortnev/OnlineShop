using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using System.Linq;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        public HomeController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public IActionResult Index() => View(productsRepository.GetAll());

        [HttpPost]
        public IActionResult Search(string productName)
        {
            var product = productsRepository.GetAll().Where(p => p.Name.ToLower().Contains(productName.ToLower())).ToList();
            if (product.Count == 0) { return View("BadSearch");}
            return View(product);
        }
    }
}