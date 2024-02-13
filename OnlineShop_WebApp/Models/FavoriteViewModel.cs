using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineShop_WebApp.Models
{
    public class FavoriteViewModel
    {
        public Guid Id { get; set; }
        [BindNever]
        public string UserId { get; set; }
        public List<ProductViewModel> Items { get; set; }
        public decimal Cost { get => Items.Sum(prod => prod.Cost); }
    }
}
