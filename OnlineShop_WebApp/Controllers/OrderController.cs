using Microsoft.AspNetCore.Mvc;
using OnlineShop_WebApp.Models;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace OnlineShop_WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICartsRepository cartRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IMapper mapper;
        public OrderController(ICartsRepository cartRepository, IOrdersRepository ordersRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.ordersRepository = ordersRepository;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string userId)
        {
            var cart = await cartRepository.TryGetByUserIdAsync(User.Identity.Name);
            ViewBag.Items = cart.Items;
            ViewBag.TotalCost = mapper.Map<CartViewModel>(cart).Cost;

            return View();
        }
        public IActionResult OrderSuccessfully() => View("OrderSuccessfully");

        [HttpPost]
        public async Task<IActionResult> SaveOrder(OrderViewModel order)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", order);
            }
            var cart = await cartRepository.TryGetByUserIdAsync(User.Identity.Name);
            order.UserId = User.Identity.Name;

            var orderDB = mapper.Map<Order>(order);
            orderDB.ListProducts = cart.Items;

            await ordersRepository.SaveOrdersAsync(orderDB, User.Identity.Name, cart);
            await cartRepository.ClearAsync(User.Identity.Name);

            return View("OrderSuccessfully");
        }
    }
}
