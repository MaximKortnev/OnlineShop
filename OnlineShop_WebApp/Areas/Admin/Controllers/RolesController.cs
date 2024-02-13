using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_WebApp.Models;
using OnlineShop.Db;
using Microsoft.AspNetCore.Identity;

namespace OnlineShop_WebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RolesController : Controller
    {

        private readonly RoleManager<IdentityRole> rolesManager;
        public RolesController(RoleManager<IdentityRole> rolesManager)
        {
            this.rolesManager = rolesManager;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(RoleViewModel role)
        {
            var result = rolesManager.CreateAsync(new IdentityRole(role.Name)).Result;
            if (result.Succeeded)
            {
                return RedirectToAction("GetRoles", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(role);
        }

        public IActionResult Delete(string name)
        {
            var role = rolesManager.FindByNameAsync(name).Result;
            if (role != null)
            {
                rolesManager.DeleteAsync(role).Wait();
            }
            return RedirectToAction("GetRoles", "Home");
        }
    }
}
