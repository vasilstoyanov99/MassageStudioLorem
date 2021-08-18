namespace MassageStudioLorem.Tests.Areas.Admin.Controllers
{
    using System.Collections.Generic;

    using MyTested.AspNetCore.Mvc;
    using Xunit;
    using Shouldly;

    using Services.Reviews.Models;
    using MassageStudioLorem.Areas.Admin.Controllers;
    using MassageStudioLorem.Data.Models;

    using static Data.DbModels.ReviewsControllerTestDbModels;
    using static Data.Models.ReviewsControllerTestModels;
    using static Global.GlobalConstants.Notifications;

    public class ReviewsControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithValidData()
        {
            var expectedModel = new AllReviewsQueryServiceModel()
            {
                CurrentPage = 1,
                MaxPage = 1,
                Reviews = new List<ReviewServiceModel>() { TestReviewModel },
                TotalReviews = 1
            };

            MyController<ReviewsController>
                .Instance()
                .WithData(TestReview)
                .Calling(c => c.All(new AllReviewsQueryServiceModel() { CurrentPage = 1, MaxPage = 1 }))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DeleteReviewShouldReturnViewWithValidData()
        {
            var expectedModel = new DeleteReviewServiceModel()
            {
                Id = TestReview.Id, 
                ClientFirstName = TestReview.ClientFirstName, 
                Content = TestReview.Content
            };

            MyController<ReviewsController>
                .Instance()
                .WithData(TestReview)
                .Calling(c => c.DeleteReview(TestReview.Id))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DeleteReviewShouldDeleteCategoryWithValidDataAndReturnRedirectWithTempData
            ()
        {
            MyController<ReviewsController>
                .Instance()
                .WithData(TestReview)
                .Calling(c => c.Delete(TestReview.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Review>(set => set.ShouldBeEmpty()))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyDeletedReviewKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }
    }
}
