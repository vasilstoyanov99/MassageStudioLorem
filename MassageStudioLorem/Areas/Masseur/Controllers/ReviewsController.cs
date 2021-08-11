namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services.Reviews;

    public class ReviewsController : MasseurController
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService) => 
            this._reviewsService = reviewsService;

        public IActionResult All()
        {
            var userId = this.User.GetId();
            var reviews = this._reviewsService.GetMasseurReviews(userId, null);

            return this.View(reviews);
        }
    }
}
