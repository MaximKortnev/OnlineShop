namespace OnlineShop_WebApp.ReviewApi
{
    public class Review
    {
        /// <summary>
        /// Id отзыва
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя, оставившего отзыв
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string? Text { get; set; }

        /// <summary>
        /// Оценка (количество звезд)
        /// </summary>
        public int Grade { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreateDate { get; set; }

    }
}
