using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop_WebApp.Models;

namespace OnlineShop_WebApp.Views.Shared.Components.Cart
{
    public class Cart : ViewComponent
    {
        private readonly ICartsRepository cartsRepository;
        private readonly IMapper mapper;

        public Cart(ICartsRepository cartsRepository, IMapper mapper)
        {
            this.cartsRepository = cartsRepository;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var cart = cartsRepository.TryGetByUserIdAsync(User.Identity.Name).Result;
            var cartViewModel = mapper.Map<CartViewModel>(cart);

            var productCounts = cartViewModel?.Amount ?? 0;

            return View("Cart", productCounts);
        }
    }
}
