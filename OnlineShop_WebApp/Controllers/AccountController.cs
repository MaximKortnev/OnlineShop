using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop.Db.Models;
using OnlineShop_WebApp.Mappings;
using OnlineShop_WebApp.Models;

namespace OnlineShop_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOrdersRepository orderRepository;
        private readonly UserManager<User> usersManager;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly SignInManager<User> _singInManager;
        private readonly IMapper mapper;
        public AccountController(IOrdersRepository orderRepository, IMapper mapper, UserManager<User> usersManager, IWebHostEnvironment appEnvironment, SignInManager<User> singInManager)
        {
            this.orderRepository = orderRepository;
            this.usersManager = usersManager;
            this.appEnvironment = appEnvironment;
            _singInManager = singInManager;
            this.mapper = mapper;
        }

        public IActionResult Main()
        {
            return View();
        }

        public IActionResult GetOptions()
        {
            var user = usersManager.FindByNameAsync(User.Identity.Name).Result;
            if (user == null) { return View("ExistUser"); }
            var userViewModel = mapper.Map<UserViewModel>(user);
            return View(userViewModel);
        }

        public async Task<IActionResult> GetOrders()
        {
            var orders = await orderRepository.GetAllByUserAsync(User.Identity.Name);
            if (orders == null) { return View(); }
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                ordersViewModel.Add(mapper.Map<OrderViewModel>(order));
            }
            return View(ordersViewModel);
        }

        public IActionResult EditAvatar(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            if (user == null) { return View("ExistUser"); }
            return View(new UserEditAvatarViewModel
            {
                Login = user.UserName,
                AvatarImagePath = user.AvatarImagePath
            });
        }

        [HttpPost]
        public IActionResult SaveEditAvatar(UserEditAvatarViewModel userView)
        {
            var user = usersManager.FindByNameAsync(userView.Login).Result;
            if (user == null) { return View("ExistUser"); }
            if (userView.UploadNewAvatar != null)
            {
                var filePath = FileManager.SavehImageAvatarInDB(userView, appEnvironment);
                user.AvatarImagePath = filePath;
                usersManager.UpdateAsync(user).Wait();
            }
            return RedirectToAction("Main");
        }

        public IActionResult Edit(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            if (user == null) return View("ExistUser");

            var userViewModel = mapper.Map<UserViewModel>(user);

            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult SaveEdit(UserViewModel userViewModel, string oldName)
        {
            if (ModelState.IsValid)
            {
                if (userViewModel.Password != userViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Пароли не совпадают");
                    return View("Edit", userViewModel);
                }
                else
                {
                    var user = usersManager.FindByNameAsync(oldName).Result;

                    if (user != null)
                    {
                        user.UserName = userViewModel.Login;
                        user.Email = userViewModel.Email;
                        user.NickName = userViewModel.NickName;
                        var newHashPassword = usersManager.PasswordHasher.HashPassword(user, userViewModel.Password);
                        user.PasswordHash = newHashPassword;

                        var result = usersManager.UpdateAsync(user).Result;

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Main");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Ошибка при обновлении пользователя.");
                        }
                    }
                }
            }
            return View("Edit", userViewModel);
        }
        public IActionResult Delete(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            if (user != null)
            {
                usersManager.DeleteAsync(user).Wait();
                _singInManager.SignOutAsync().Wait();
                return RedirectToAction("Login", "Auth");
            }
            return View("ExistUser");

        }
    }
}
