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

        public async Task<IActionResult> Index()
        {
            var favorite = await favoriteRepository.GetAllAsync(User.Identity.Name);
            var favoriteViewModel = mapper.Map<List<ProductViewModel>>(favorite);

            return View(favoriteViewModel);
        }

        public async Task<IActionResult> Add(Guid productId) {
            var product = await productsRepository.TryGetProductByIdAsync(productId);

            if (product == null) return View("ErrorFavorite");

            await favoriteRepository.AddAsync(product, User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Decrease(Guid productId)
        {
            var product = await productsRepository.TryGetProductByIdAsync(productId);

            if (product == null) { return View("ErrorFavorite"); }

            await favoriteRepository.DecreaseAsync(product, User.Identity.Name);

            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Clear()
        {
            await favoriteRepository.ClearAsync(User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
