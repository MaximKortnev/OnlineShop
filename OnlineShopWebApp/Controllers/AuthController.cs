using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Index", user);
        }

        [HttpPost]
        public IActionResult Registration(User user)
        {

            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Index", user);
        }
    }
}
