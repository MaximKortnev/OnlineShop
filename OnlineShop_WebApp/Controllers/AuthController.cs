using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop_WebApp.Models;
using OnlineShop.Db.Models;

namespace OnlineShop_WebApp.Controllers
{
    public class AuthController : Controller
    {

        //private readonly IUserManager userManager;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;

        public AuthController(SignInManager<User> singInManager, UserManager<User> _userManager)
        {

            _singInManager = singInManager;
            this._userManager = _userManager;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginUser() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Login(LoginUser loginUser)
        {
            if (ModelState.IsValid)
            {
                var result = _singInManager.PasswordSignInAsync(loginUser.Login, loginUser.Password, loginUser.rememberMe, false).Result;
                if (result.Succeeded)
                {
                    return Redirect(loginUser.ReturnUrl ?? "/Home");
                }
                ModelState.AddModelError("", "Неправильный пароль");
            }
            return View("Login", loginUser);
        }
        public IActionResult Registration(string returnUrl)
        {
            return View(new UserViewModel() { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Registration(UserViewModel userView)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = userView.Login, 
                    Email = userView.Email, 
                    NickName = userView.NickName, 
                    AvatarImagePath = "/images/users/DefaultAvatar.png"
                };
                var result = await _userManager.CreateAsync(user, userView.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                    var res = _singInManager.PasswordSignInAsync(userView.Login, userView.Password, true, false).Result;
                    if (res.Succeeded)
                    {
                        return Redirect(userView.ReturnUrl ?? "/Home");
                    }
                    return Redirect(userView.ReturnUrl ?? "/Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            return View("Registration", userView);
        }
        public IActionResult Logout()
        {
            _singInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }
    }
}
