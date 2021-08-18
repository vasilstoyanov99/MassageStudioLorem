namespace MassageStudioLorem.Tests.Controllers
{
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using MassageStudioLorem.Controllers;
    using MassageStudioLorem.Data.Models;
    using Models.Reviews;
    using Services.Reviews.Models;

    using static Data.DbModels.ReviewsControllerTestDbModels;
    using static Global.GlobalConstants.Notifications;
    using static Data.Models.ReviewsControllerTestModels;
    using static MassageStudioLorem.Areas.Client.ClientConstants;

    public class ReviewsControllerTest
    {
        [Fact]
        public void ControllerShouldHaveAuthorizeFilter()
            => MyMvc
                .Controller<ReviewsController>()
                .ShouldHave()
                .Attributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void ReviewMasseurShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new ReviewMasseurFormServiceModel
            {
                MasseurId = TestMasseur.Id,
                MasseurFullName = TestMasseur.FullName,
                ClientId = TestClient.Id,
                AppointmentId = TestPastAppointment.Id
            };

            MyController<ReviewsController>
                .Instance()
                .WithUser(u => u.InRole(ClientRoleName))
                .WithData(TestClient, TestMasseur, TestMassage, TestPastAppointment)
                .Calling(c => c.ReviewMasseur(new ReviewIdsQueryModel()
                {
                    MasseurId = TestMasseur.Id, 
                    AppointmentId = TestPastAppointment.Id
                }))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void ReviewMasseurShouldAddReviewWithValidDataAndRedirectWithTempData()
        {
            MyController<ReviewsController>
                .Instance()
                .WithUser(u => u.InRole(ClientRoleName))
                .WithData(TestClient, TestMasseur, TestPastAppointment)
                .Calling(c => c.ReviewMasseur(TestReviewMasseurFormModel))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Review>(set =>
                    {
                        set.ShouldNotBeNull();
                        set.FirstOrDefault(r => r.MasseurId == TestMasseur.Id)
                            .ShouldNotBeNull();
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyReviewedMasseurKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("Index", "Appointments");
        }

        [Fact]
        public void MasseurReviewsShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = TestAllReviewsModel;

            MyController<ReviewsController>
                .Instance()
                .WithData(TestMasseur, TestCategory, TestReview)
                .Calling(c => c.MasseurReviews(new MasseurDetailsQueryModel()
                {
                    MasseurId = TestMasseur.Id, 
                    CategoryId = TestCategory.Id, 
                    MassageId = TestMassage.Id
                }))
                .ShouldReturn()
                .View(expectedModel);
        }
    }
}
