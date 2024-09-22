using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Interfaces;
using OnlineShop_WebApp.Models;
using OnlineShop_WebApp.ReviewApi;


namespace OnlineShop_WebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;
        private readonly ReviewApiClient reviewApiClient;

        public ProductController(IProductsRepository productsRepository, IMapper mapper, ReviewApiClient reviewApiClient)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.reviewApiClient = reviewApiClient;
        }

        public async Task<IActionResult> Index(Guid productId)
        {
            var reviews = new List<Review>();
            try
            {
                reviews = await reviewApiClient.GetReviewsAsync(productId);
            }
            catch (Exception ex) { }


            var product = await productsRepository.TryGetProductByIdAsync(productId);

            if (product == null) { return View("ErrorProduct"); }
            var productViewModel = mapper.Map<ProductViewModel>(product);

            productViewModel.Reviews = reviews;

            return View(productViewModel);
        }

        public IActionResult AddReview(Guid productId)
        {

            var addReview = new AddReviewModel() { ProductId = productId };

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewModel newReview)
        {

            await reviewApiClient.AddAsync(newReview);

            return RedirectToAction("Index", new { id = newReview.ProductId });
        }
    }
}
