namespace MassageStudioLorem.Controllers
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure;
    using Models.Reviews;
    using Services.Reviews;
    using Services.Reviews.Models;

    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Notifications;
    using static Areas.Client.ClientConstants;

    [Authorize(Roles = ClientRoleName)]
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
            => this._reviewsService = reviewsService;

        public IActionResult ReviewMasseur
            ([FromQuery] ReviewIdsQueryModel queryModel)
        {
            var userId = this.User.GetId();

            var hasUserLeftAReview = this._reviewsService.
                CheckIfClientHasLeftAReview(queryModel.AppointmentId);

            if (hasUserLeftAReview)
            {
                this.ModelState.AddModelError(String.Empty, UserHasLeftAReview);
                return this.View(null);
            }

            var reviewMasseurModel = this._reviewsService
                .GetDataForReview(userId, queryModel.MasseurId,
                    queryModel.AppointmentId);

            if (CheckIfNull(reviewMasseurModel))
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(reviewMasseurModel);
        }

        [HttpPost]
        public IActionResult ReviewMasseur
            (ReviewMasseurFormServiceModel reviewModel)
        {
            if (!this._reviewsService.CheckIfIdsAreValid(reviewModel) ||
                !this.ModelState.IsValid)
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View(null);
            }

            this._reviewsService.AddNewReview(reviewModel);

            this.TempData[SuccessfullyReviewedMasseurKey] =
                SuccessfullyReviewedMasseur;

            return this.RedirectToAction("Index", "Appointments");
        }

        public IActionResult MasseurReviews
            ([FromQuery] MasseurDetailsQueryModel queryModel)
        {
            var reviews = this._reviewsService
                .GetMasseurReviews(null, queryModel.MasseurId);

            return this.View(new MasseurAllReviewsQueryViewModel()
            {
                MasseurId = queryModel.MasseurId,
                CategoryId = queryModel.CategoryId,
                MassageId = queryModel.MassageId,
                Reviews = reviews
            });
        }

        private static bool CheckIfNull(object obj)
            => obj == null;
    }
}
