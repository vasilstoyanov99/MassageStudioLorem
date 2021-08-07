namespace MassageStudioLorem.Areas.Masseur.Controllers
{
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Services.Reviews;
    using System;
    using System.Linq;
    using static Global.GlobalConstants.ErrorMessages;

    public class ReviewsController : MasseurController
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService) => 
            this._reviewsService = reviewsService;

        public IActionResult Index()
        {
            var userId = this.User.GetId();
            var reviews = this._reviewsService.GetMasseurReviews(userId, null);

            return this.View(reviews);
        }
    }
}
