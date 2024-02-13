namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string DeliveryMethod { get; set; }
        public string PaymentMethod { get; set; }
        public string? PromoCode { get; set; }
        public string TotalCost { get; set; }
        public List<CartItem> ListProducts { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Data { get; set; }

        public Order()
        {
            ListProducts = new List<CartItem>();
            Status = OrderStatus.Created;
        }
    }
}
