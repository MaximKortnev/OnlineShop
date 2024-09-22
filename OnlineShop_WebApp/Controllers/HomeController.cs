using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop_WebApp.Models;

namespace OnlineShop_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;
        public HomeController(IProductsRepository productsRepository, IMapper mapper)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            var productsViewModels = mapper.Map<List<ProductViewModel>>(products);

            return View(productsViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> Search(string productName)
        {
            var products = await productsRepository.SearchAsync(productName);

            if (products == null) { View("ErrorPdoduct", "Product"); }

            var productsViewModels = mapper.Map<List<ProductViewModel>>(products);

            return View(productsViewModels);
        }
    }
}