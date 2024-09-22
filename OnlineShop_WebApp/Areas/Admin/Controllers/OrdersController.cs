using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop_WebApp.Models;
using OnlineShop.Db.Models;
using OnlineShop.Db;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace OnlineShop_WebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository ordersRepository;
        private readonly IMapper mapper;
        public OrdersController( IOrdersRepository ordersRepository, IMapper mapper)
        {
            this.ordersRepository = ordersRepository;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Info(Guid Id)
        {
            var order = await ordersRepository.TryGetByIdAsync(Id);
            if (order != null) { return View(mapper.Map<OrderViewModel>(order)); }
            return View("BadOrder");
        }
        [HttpPost]
        public async Task<IActionResult> Save(Guid orderId, OrderStatusViewModel status)
        {
            var order = await ordersRepository.TryGetByIdAsync(orderId);
            if (order != null)
            {
                await ordersRepository.EditStatusAsync(orderId, (OrderStatus)(OrderStatusViewModel)(int)status);
                return RedirectToAction("GetOrders", "Home");
            }
            return View("BadOrder");

        }
        public async Task<IActionResult> Delete(Guid orderId)
        {
            var order = await ordersRepository.TryGetByIdAsync(orderId);
            if (order != null)
            {
                await ordersRepository.DeleteAsync(orderId);
                return RedirectToAction("GetOrders", "Home");
            }
            return View("BadOrder");

        }

    }
}
