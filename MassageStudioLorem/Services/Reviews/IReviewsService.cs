namespace MassageStudioLorem.Services.Reviews
{
    using System.Collections.Generic;

    using Models;

    public interface IReviewsService
    {
        bool CheckIfClientHasLeftAReview(string appointmentId);

        ReviewMasseurFormServiceModel GetDataForReview
            (string userId, string masseurId, string appointmentId);

        bool CheckIfIdsAreValid(ReviewMasseurFormServiceModel reviewModel);

        void AddNewReview(ReviewMasseurFormServiceModel reviewModel);

        IEnumerable<ReviewServiceModel> GetMasseurReviews
            (string userId, string masseurId);

        AllReviewsQueryServiceModel GetAllReviews(int currentPage);

        DeleteReviewServiceModel GetReviewDataForDelete(string reviewId);

        bool CheckIfReviewDeletedSuccessfully(string reviewId);
    }
}
