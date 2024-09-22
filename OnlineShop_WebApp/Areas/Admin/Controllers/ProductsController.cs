using Microsoft.AspNetCore.Mvc;
using OnlineShop_WebApp.Models;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop_WebApp.Mappings;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace OnlineShop_WebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly IMapper mapper;
        public ProductsController(IProductsRepository productRepository, IWebHostEnvironment appEnvironment, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.appEnvironment = appEnvironment;
            this.mapper = mapper;
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
                    return RedirectToAction("GetProducts", "Home");
                }
            }
            return View("AddProduct", product);
        }
    }
}
