namespace MassageStudioLorem.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Controllers;
    using Models.Reviews;
    using Services.Reviews.Models;

    public class ReviewsControllerTest
    {
        [Fact]
        public void GetReviewMasseurShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Reviews/ReviewMasseur")
                .To<ReviewsController>(c => c.ReviewMasseur
                    (new ReviewIdsQueryModel()));

        [Fact]
        public void PostReviewMasseurShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Reviews/ReviewMasseur")
                    .WithMethod(HttpMethod.Post))
                .To<ReviewsController>(c => c.ReviewMasseur
                    (new ReviewMasseurFormServiceModel()));

        [Fact]
        public void GetMasseurReviewsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Reviews/MasseurReviews")
                .To<ReviewsController>(c => c.MasseurReviews
                    (new MasseurDetailsQueryModel()));
    }
}
