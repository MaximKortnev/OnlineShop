using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineShop_WebApp.Models
{
    public class CartViewModel
    {
        public Guid Id { get; set; }

        [BindNever]
        public string UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public decimal Cost { get => Items?.Sum(prod => prod.Cost) ?? 0; }
        public decimal Amount { get => Items?.Sum(prod => prod.Amount) ?? 0; }
    }
}
