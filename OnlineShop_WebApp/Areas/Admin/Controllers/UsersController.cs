using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_WebApp.Models;
using OnlineShop.Db.Models;
using OnlineShop.Db;
using Microsoft.AspNetCore.Identity;
using OnlineShop_WebApp.Areas.Admin.Models;
using AutoMapper;


namespace OnlineShop_WebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UsersController : Controller
    {
        private readonly RoleManager<IdentityRole> rolesManager;
        private readonly IMapper mapper;

        private readonly UserManager<User> usersManager;
        public UsersController(RoleManager<IdentityRole> rolesManager, UserManager<User> usersManager, IMapper mapper)
        {
            this.usersManager = usersManager;
            this.rolesManager = rolesManager;
            this.mapper = mapper;
        }

        public IActionResult Info(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            if (user != null)
            {
                var roles = usersManager.GetRolesAsync(user).Result;
                var listRoles = roles.Select(x => new RoleViewModel { Name = x }).ToList();
                string allRoles = "";
                foreach (var role in listRoles)
                {
                    allRoles += $"{role.Name}, ";
                }
                ViewBag.AllRoles = allRoles.TrimEnd(' ', ',');
                var userViewModel = mapper.Map<UserViewModel>(user);
                return View(userViewModel);
            }
            return View("ExistUser");
        }

        public IActionResult Add()
        {
            return View();
        }

        public IActionResult EditRights(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            if (user == null) return View("ExistUser");
            var roles = usersManager.GetRolesAsync(user).Result;
            var allRoles = rolesManager.Roles.ToList();

            var model = new EditRightsViewModel
            {
                UserName = user.UserName,
                Roles = roles.Select(x => new RoleViewModel { Name = x }).ToList(),
                AllRoles = allRoles.Select(x => new RoleViewModel { Name = x.Name }).ToList()
            };
            return View(model);

        }
        [HttpPost]
        public IActionResult SaveEditRights(string name, Dictionary<string, string> userRolesViewModel)
        {
            var userSelectedRoles = userRolesViewModel.Select(x => x.Key);
            var user = usersManager.FindByNameAsync(name).Result;
            if (user == null) return View("ExistUser");
            var roles = usersManager.GetRolesAsync(user).Result;

            usersManager.RemoveFromRolesAsync(user, roles).Wait();
            usersManager.AddToRolesAsync(user, userSelectedRoles).Wait();

            return Redirect($"/Admin/Users/Info?name={name}");
        }

        public IActionResult Delete(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            if (user != null)
            {
                usersManager.DeleteAsync(user).Wait();
                return RedirectToAction("GetUsers", "Home");
            }
            return View("ExistUser");

        }

        public IActionResult EditPassword(string name)
        {
            var changePassword = new ChangePassword()
            {
                UserName = name
            };
            return View(changePassword);

        }

        [HttpPost]
        public IActionResult EditPassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                var user = usersManager.FindByNameAsync(changePassword.UserName).Result;
                if (user == null)
                {
                    return View("ExistUser");
                }
                var newHashPassword = usersManager.PasswordHasher.HashPassword(user, changePassword.Password);
                user.PasswordHash = newHashPassword;
                usersManager.UpdateAsync(user).Wait();
                return RedirectToAction("GetUsers", "Home");
            }
            return View();
        }
        public IActionResult Edit(string name)
        {
            var user = usersManager.FindByNameAsync(name).Result;
            if (user == null) return View("ExistUser");

            var userViewModel = mapper.Map<UserViewModel>(user);

            return View(userViewModel);
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel, string oldName)
        {
            if (ModelState.IsValid)
            {
                var user = usersManager.FindByNameAsync(oldName).Result;

                if (user != null)
                {
                    user.UserName = userViewModel.Login;
                    user.Email = userViewModel.Email;

                    var result = usersManager.UpdateAsync(user).Result;

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Ошибка при обновлении пользователя.");
                    }
                }
            }
            return View(userViewModel);
        }
    }

}
