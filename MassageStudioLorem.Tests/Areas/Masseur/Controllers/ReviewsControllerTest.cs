namespace MassageStudioLorem.Tests.Areas.Masseur.Controllers
{
    using System.Collections.Generic;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Masseur.Controllers;
    using Services.Reviews.Models;

    using static Data.DbModels.ReviewsControllerDbModel;
    using static MassageStudioLorem.Areas.Masseur.MasseurConstants;

    public class ReviewsControllerTest
    {
        [Fact]
        public void MasseurReviewsShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new List<ReviewServiceModel>() { TestReviewModel };

            MyController<ReviewsController>
                .Instance()
                .WithUser(u => u.InRole(MasseurRoleName))
                .WithData(TestMasseur, TestReview)
                .Calling(c => c.All())
                .ShouldReturn()
                .View(expectedModel);
        }
    }
}
