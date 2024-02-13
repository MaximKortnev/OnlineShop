using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop_WebApp.Models;
using OnlineShop.Db;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Db.Models;
using AutoMapper;

namespace OnlineShop_WebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class HomeController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IOrdersRepository orderRepository;
        private readonly UserManager<User> usersManager;
        private readonly RoleManager<IdentityRole> rolesManager;
        private readonly IMapper mapper;
        public HomeController(IProductsRepository productRepository, IOrdersRepository orderRepository, UserManager<User> usersManager, RoleManager<IdentityRole> rolesManager, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.usersManager = usersManager;
            this.rolesManager = rolesManager;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetOrders()
        {
            var orders = orderRepository.GetAll();
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                ordersViewModel.Add(mapper.Map<OrderViewModel>(order)) ;
            }
            return View(ordersViewModel);
        }

        public IActionResult GetUsers()
        {
            var users = mapper.Map<List<UserViewModel>>(usersManager.Users.ToList());
            return View(users);
        }

        public IActionResult GetRoles()
        {
            var roles = rolesManager.Roles.ToList();
            return View(roles.Select(x => new RoleViewModel { Name = x.Name }).ToList());
        }

        public IActionResult GetProducts()
        {
            var products = productRepository.GetAll();
            var productsViewModels = mapper.Map<List<ProductViewModel>>(products);
            return View(productsViewModels);
        }
    }
}
