using Microsoft.AspNetCore.Mvc;
using OnlineShop_WebApp.Models;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop_WebApp.Mappings;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using OnlineShop_WebApp.Services;
using Newtonsoft.Json;

namespace OnlineShop_WebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly IMapper mapper;
        private readonly RedisCacheService redisCacheService;

        public ProductsController(IProductsRepository productRepository, IWebHostEnvironment appEnvironment, IMapper mapper, RedisCacheService redisCacheService)
        {
            this.productRepository = productRepository;
            this.appEnvironment = appEnvironment;
            this.mapper = mapper;
            this.redisCacheService = redisCacheService;
        }

        public async Task<IActionResult> ViewEdit(Guid productId)
        {
            var product = await productRepository.TryGetProductByIdAsync(productId);
            if (product == null)
            {
                return View("ErrorProduct");
            }
            var productViewModel = mapper.Map<ProductViewModel>(product);
            return View(productViewModel);

        }
        public async Task<IActionResult> Delete(Guid productId)
        {
            var product = await productRepository.TryGetProductByIdAsync(productId);
            if (product != null)
            {
                await productRepository.DeleteAsync(productId);

                await RemoveCacheAsync();
                await UpdateCacheAsync();

                return RedirectToAction("GetProducts", "Home");
            }
            return View("ErrorProduct");
        }
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveEdit(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFiles != null && product.ImageFiles.Any())
                {
                    var imagePaths = FileManager.SaveProductImagesInDB(product, appEnvironment);
                    product.ImagePath = imagePaths.FirstOrDefault();
                    product.ImagePaths = imagePaths;

                    var productDB = mapper.Map<Product>(product);
                    await productRepository.EditAsync(productDB);

                    await RemoveCacheAsync();
                    await UpdateCacheAsync();

                    return RedirectToAction("GetProducts", "Home");
                }
            }
            return View("ViewEdit", product);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFiles != null && product.ImageFiles.Any())
                {
                    var imagePaths = FileManager.SaveProductImagesInDB(product, appEnvironment);
                    product.ImagePath = imagePaths.FirstOrDefault();
                    product.ImagePaths = imagePaths;
                    await productRepository.AddAsync(mapper.Map<Product>(product));

                    await RemoveCacheAsync();
                    await UpdateCacheAsync();

                    return RedirectToAction("GetProducts", "Home");
                }
            }
            return View("AddProduct", product);
        }

        private async Task UpdateCacheAsync() 
        {
            try 
            {
                var products = mapper.Map<List<ProductViewModel>>(await productRepository.GetAllAsync());

                var productJson = JsonConvert.SerializeObject(products);
                await redisCacheService.SetAsync(Constants.RedisCacheKey, productJson);
            }
            catch 
            {
                return;
            }
        }

        private async Task RemoveCacheAsync() 
        {
            try 
            {
                await redisCacheService.RemoveAsync(Constants.RedisCacheKey);
            }
            catch 
            {
                return;
            }
        }
    }
}
