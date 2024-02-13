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

        public IActionResult Index(Guid userId)
        {
            var cart = cartRepository.TryGetByUserId(User.Identity.Name);
            if (cart == null) { return View();}
            var cartViewModel = mapper.Map<CartViewModel>(cart);
            return View(cartViewModel);
        }
        public IActionResult Add(Guid productId) { 
            var product = productRepository.TryGetProductById(productId);
            if (product == null) { return View("ErrorAddCart");}
            cartRepository.Add(product, User.Identity.Name);
            return RedirectToAction("Index");
        }
        public IActionResult DecreaseAmount(Guid productId)
        {
            var product = productRepository.TryGetProductById(productId);
            if (product == null) { return View("ErrorAddCart"); }
            cartRepository.DecreaseAmount(product, User.Identity.Name);
            return RedirectToAction("Index");
        }
        public IActionResult Clear() {
            cartRepository.Clear(User.Identity.Name);
            return RedirectToAction("Index");
        }
    }
}
