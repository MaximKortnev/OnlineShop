using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AdminProductsController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IAdminProductsFunctions adminProductFunction;
        public AdminProductsController(IProductsRepository productRepository, IAdminProductsFunctions adminProductFunction)
        {
            this.productRepository = productRepository;
            this.adminProductFunction = adminProductFunction;
        }

        public IActionResult ViewEdit(int productId)
        {
            var product = productRepository.TryGetProductById(productId);
            return View(product);
        }
        public IActionResult Delete(int productId)
        {
            adminProductFunction.Delete(productId);
            return RedirectToAction("GetProducts", "Administrator");
        }
        public IActionResult AddProduct()
        {
            var id = productRepository.GetAll();
            ViewBag.ProductId = id.Count + 1;
            return View();
        }

        [HttpPost]
        public IActionResult SaveEdit(Product product)
        {
            if (ModelState.IsValid)
            {
                adminProductFunction.Edit(product);
                return RedirectToAction("GetProducts", "Administrator");
            }
            else { return View("ViewEdit", product); }
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                adminProductFunction.Add(product);
                return RedirectToAction("GetProducts", "Administrator");
            }
            else { return View("AddProduct", product); }
        }
    }
}
