using System.ComponentModel.DataAnnotations;

namespace OnlineShop_WebApp.Models
{
        public enum OrderStatusViewModel
        {
            [Display(Name = "Создан")]
            Created = 0,

            [Display(Name = "Обработан")]
            Processed = 1,

            [Display(Name = "В пути")]
            InTransit = 2,

            [Display(Name = "Отменен")]
            Canceled = 3,

            [Display(Name = "Доставлен")]
            Delivered = 4
        }
}
