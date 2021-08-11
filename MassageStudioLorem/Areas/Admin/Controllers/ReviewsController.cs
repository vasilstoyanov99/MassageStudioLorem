namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using MassageStudioLorem.Models.Reviews;
    using MassageStudioLorem.Services.Reviews;
    using MassageStudioLorem.Services.Reviews.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Notifications;

    public class ReviewsController : AdminController
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
            => this._reviewsService = reviewsService;

        public IActionResult All
            ([FromQuery] AllReviewsQueryServiceModel queryModel)
        {
            var allReviewsModel = this._reviewsService
                .GetAllReviews(queryModel.CurrentPage);

            return this.View(allReviewsModel);
        }

        public IActionResult DeleteReview(string reviewId)
        {
            var reviewDataModel = this._reviewsService
                .GetReviewDataForDelete(reviewId);

            if (reviewDataModel == null) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(reviewDataModel);
        }

        [HttpPost]
        public IActionResult Delete(string reviewId)
        {
            if (!this._reviewsService.CheckIfReviewDeletedSuccessfully(reviewId))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View(nameof(this.DeleteReview));
            }

            this.TempData[SuccessfullyDeletedReviewKey] =
                SuccessfullyDeletedReview;

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
