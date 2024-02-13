using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using AutoMapper;
using OnlineShop_WebApp.Models;

namespace OnlineShop_WebApp.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {

        private readonly IProductsRepository productsRepository;
        private readonly IFavoritesRepository favoriteRepository;
        private readonly IMapper mapper;
        public FavoriteController(IProductsRepository productsRepository, IMapper mapper, IFavoritesRepository favoriteRepository)
        {
            this.favoriteRepository = favoriteRepository;
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var favorite = favoriteRepository.GetAll(User.Identity.Name);
            var favoriteViewModel = mapper.Map<List<ProductViewModel>>(favorite);
            return View(favoriteViewModel);
        }

        public IActionResult Add(Guid productId) {
            var product = productsRepository.TryGetProductById(productId);
            if (product == null) return View("ErrorFavorite");
            favoriteRepository.Add(product, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Decrease(Guid productId)
        {
            var product = productsRepository.TryGetProductById(productId);
            if (product == null) { return View("ErrorFavorite"); }
            favoriteRepository.Decrease(product, User.Identity.Name);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Clear()
        {
            favoriteRepository.Clear(User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
