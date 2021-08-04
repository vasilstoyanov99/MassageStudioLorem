namespace MassageStudioLorem.Services.Reviews
{
    using Microsoft.Net.Http.Headers;
    using Models;

    public interface IReviewsService
    {
        bool CheckIfClientHasLeftAReview(string appointmentId);

        ReviewMasseurFormServiceModel GetDataForReview
            (string userId, string masseurId, string appointmentId);

        bool CheckIfIdsAreValid(ReviewMasseurFormServiceModel reviewModel);

        void AddNewReview(ReviewMasseurFormServiceModel reviewModel);
    }
}
