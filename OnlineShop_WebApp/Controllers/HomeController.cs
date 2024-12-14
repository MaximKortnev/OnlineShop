using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineShop.Db;
using OnlineShop.Db.Interfaces;
using OnlineShop_WebApp.Models;
using OnlineShop_WebApp.Services;

namespace OnlineShop_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;
        private readonly RedisCacheService redisCacheService;

        public HomeController(IProductsRepository productsRepository, IMapper mapper, RedisCacheService redisCacheService)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.redisCacheService = redisCacheService;
        }

        public async Task<IActionResult> Index()
        {
            var cacheProducts = await redisCacheService.TryGetAsync(Constants.RedisCacheKey);
            List<ProductViewModel> productsViewModels;

            if (!string.IsNullOrEmpty(cacheProducts))
            {
                try
                {
                    productsViewModels = JsonConvert.DeserializeObject<List<ProductViewModel>>(cacheProducts);
                }
                catch (JsonException ex)
                {
                    Console.Error.WriteLine($"Ошибка десериализации: {ex.Message}");
                    productsViewModels = new List<ProductViewModel>();
                }
            }
            else
            {
                var products = await productsRepository.GetAllAsync();
                productsViewModels = mapper.Map<List<ProductViewModel>>(products);

                try
                {
                    var productJson = JsonConvert.SerializeObject(productsViewModels);
                    await redisCacheService.SetAsync(Constants.RedisCacheKey, productJson);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Ошибка записи в кэш: {ex.Message}");
                }
            }

            return View(productsViewModels ?? new List<ProductViewModel>());
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