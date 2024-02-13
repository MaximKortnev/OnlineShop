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

        public IActionResult Index()
        {
            var сomparison = сomparisonRepository.GetAll(User.Identity.Name);
            var comparisonViewModel = mapper.Map<List<ProductViewModel>>(сomparison);
            return View(comparisonViewModel);
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsRepository.TryGetProductById(productId);
            if (product == null) { return View("ErrorComparison"); }
            сomparisonRepository.Add(product, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Delete(Guid productId)
        {
            var product = productsRepository.TryGetProductById(productId);
            if (product == null) { return View("ErrorComparison"); }
            сomparisonRepository.Delete(product, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Clear()
        {
            сomparisonRepository.Clear(User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
    }
}
