using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineShop_WebApp.ReviewApi;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop_WebApp.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя должно содержать от 3 до 50 символов")]
        [NoWhitespace(ErrorMessage = "Имя не может состоять из пробелов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Автор не указан")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Поле должно содержать от 3 до 50 символов")]
        [NoWhitespace(ErrorMessage = "Автор не может состоять из пробелов")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Описание не указано")]
        [NoWhitespace(ErrorMessage = "Описание не может состоять из пробелов")]
        public string AboutTheBook { get; set; }

        [Required(ErrorMessage = "Описание не указано")]
        [NoWhitespace(ErrorMessage = "Описание не может состоять из пробелов")]
        public string AboutAuthor { get; set; }

        [Required(ErrorMessage = "Цитата не указана")]
        [NoWhitespace(ErrorMessage = "Цитата не может состоять из пробелов")]
        public string Quote { get; set; }

        [Required(ErrorMessage = "Стоимость не указана")]
        [NoWhitespace(ErrorMessage = "Стоимость не может состоять из пробелов")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage = "Описание не указано")]
        [NoWhitespace(ErrorMessage = "Описание не может состоять из пробелов")]
        public string Description { get; set; }
        [BindNever]
        public List<string>? ImagePaths { get; set; }
        public string? ImagePath { get; set; }
        public List<IFormFile>? ImageFiles { get; set; }

        public List<Review>? Reviews { get; set; }
    }
}

