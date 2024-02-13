using System.ComponentModel.DataAnnotations;

namespace OnlineShop_WebApp.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Не указано имя роли")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Имя роли должно содержать от 3 до 25 символов")]
        [NoWhitespace(ErrorMessage = "Имя не может состоять из пробелов")]
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var role = (RoleViewModel)obj;
            return Name == role.Name;
        }
    }
}
