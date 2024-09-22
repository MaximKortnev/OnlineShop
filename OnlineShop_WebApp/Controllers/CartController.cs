using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using AutoMapper;
using OnlineShop_WebApp.Models;

namespace OnlineShop_WebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartsRepository cartRepository;
        private readonly IProductsRepository productRepository;
        private readonly IMapper mapper;
        public CartController(ICartsRepository cartRepository, IProductsRepository productRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(Guid userId)
        {
            var cart = await cartRepository.TryGetByUserIdAsync(User.Identity.Name);

            if (cart == null) { return View(); }

            var cartViewModel = mapper.Map<CartViewModel>(cart);

            return View(cartViewModel);
        }
        public async Task<IActionResult> Add(Guid productId)
        {
            var product = await productRepository.TryGetProductByIdAsync(productId);

            if (product == null) { return View("ErrorAddCart"); }

            await cartRepository.AddAsync(product, User.Identity.Name);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DecreaseAmount(Guid productId)
        {
            var product = await productRepository.TryGetProductByIdAsync(productId);

            if (product == null) { return View("ErrorAddCart"); }

            await cartRepository.DecreaseAmountAsync(product, User.Identity.Name);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Clear()
        {
            await cartRepository.ClearAsync(User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
