namespace OnlineShop_WebApp.ReviewApi
{
    public class ReviewApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ReviewApiClient(IHttpClientFactory httpClientFactory)
        {

            _httpClientFactory = httpClientFactory;
        }
        public async Task<List<Review>> GetReviewsAsync(Guid productId)
        {

            var httpClient = _httpClientFactory.CreateClient("ReviewApi");
            var reviews = await httpClient.GetFromJsonAsync<List<Review>>($"/Review/GetByProductId?productId={productId}");

            return reviews;
        }

        public async Task AddAsync(AddReviewModel newReview)
        {

            var httpClient = _httpClientFactory.CreateClient("ReviewApi");

            await httpClient.PostAsJsonAsync("/Review/Add", newReview);

        }

    }
}
