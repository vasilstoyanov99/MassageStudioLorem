namespace MassageStudioLorem.Tests.Areas.Admin.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;

    public class ReviewsControllerTest
    {
        [Fact]
        public void GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Reviews/All")
                .To<ReviewsController>(c => c.All(null));

        [Fact]
        public void GetDeleteReviewShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Reviews/DeleteReview")
                .To<ReviewsController>(c => c.DeleteReview(null));

        [Fact]
        public void PostDeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Reviews/Delete")
                    .WithMethod(HttpMethod.Post))
                .To<ReviewsController>(c => c.Delete(null));

    }
}
