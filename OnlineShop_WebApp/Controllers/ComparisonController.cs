using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using OnlineShop_WebApp.Models;

namespace OnlineShop_WebApp.Controllers
{
    [Authorize]
    public class ComparisonController : Controller
    {

        private readonly IProductsRepository productsRepository;
        private readonly IComparisonRepository сomparisonRepository;
        private readonly IMapper mapper;
        public ComparisonController(IComparisonRepository сomparisonRepository, IMapper mapper, IProductsRepository productsRepository)
        {
            this.сomparisonRepository = сomparisonRepository;
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var сomparison = await сomparisonRepository.GetAllAsync(User.Identity.Name);
            var comparisonViewModel = mapper.Map<List<ProductViewModel>>(сomparison);

            return View(comparisonViewModel);
        }

        public async Task<IActionResult> Add(Guid productId)
        {
            var product = await productsRepository.TryGetProductByIdAsync(productId);

            if (product == null) { return View("ErrorComparison"); }

            await сomparisonRepository.AddAsync(product, User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(Guid productId)
        {
            var product = await productsRepository.TryGetProductByIdAsync(productId);

            if (product == null) { return View("ErrorComparison"); }

            await сomparisonRepository.DeleteAsync(product, User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Clear()
        {
            await сomparisonRepository.ClearAsync(User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
    }
}
