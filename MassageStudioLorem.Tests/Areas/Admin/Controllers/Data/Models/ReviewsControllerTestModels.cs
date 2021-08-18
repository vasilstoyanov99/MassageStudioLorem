namespace MassageStudioLorem.Tests.Areas.Admin.Controllers.Data.Models
{
    using Services.Reviews.Models;

    using static DbModels.ReviewsControllerTestDbModels;

    public class ReviewsControllerTestModels
    {
        public static ReviewServiceModel TestReviewModel => new()
        {
            ClientFirstName = TestClientData.FirstName,
            Content = TestReview.Content,
            CreatedOn = TestReview.CreatedOn,
            Id = TestReview.Id
        };
    }
}
