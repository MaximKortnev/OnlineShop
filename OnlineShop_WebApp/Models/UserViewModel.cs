using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop_WebApp.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Логин не указан")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Имя должно содержать от 3 до 25 символов")]
        [NoWhitespace(ErrorMessage = "Имя не может состоять из пробелов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан e-mail")]
        [EmailAddress(ErrorMessage = "Указан не валидный e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Пароль не указан")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "Пароль должен содержать от 5 до 15 символов")]
        [NoWhitespace(ErrorMessage = "Имя не может состоять из пробелов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароль не указан")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [NoWhitespace(ErrorMessage = "Имя не может состоять из пробелов")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "Имя должно содержать от 3 до 15 символов")]
        [NoWhitespace(ErrorMessage = "Имя не может состоять из пробелов")]
        public string? NickName { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; }
        [BindNever]
        public string? AvatarImagePath { get; set; }

        public IFormFile? UploadNewAvatar { get; set; }
    }
}
