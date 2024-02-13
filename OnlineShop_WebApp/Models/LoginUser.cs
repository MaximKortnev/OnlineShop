using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;


namespace OnlineShop_WebApp.Models
{
    public class LoginUser
    {
        [Required(ErrorMessage = "Логин не указан")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Имя должно содержать от 3 до 25 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Пароль не указан")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Пароль должен содержать от 5 до 15 символов")]
        public string Password { get; set; }

        public bool rememberMe { get; set; }
        [BindNever]
        public string? ReturnUrl { get; set; }
    }
}
