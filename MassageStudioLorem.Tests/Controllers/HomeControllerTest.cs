namespace MassageStudioLorem.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Controllers;

    using static Areas.Client.ClientConstants;
    using static Areas.Masseur.MasseurConstants;
    using static Areas.Admin.AdminConstants;
    using static Data.DbModels.HomeControllerTestDbModels;

    public class HomeControllerTest
    {
        [Fact]
        public void ErrorShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Error())
                .ShouldReturn()
                .View();

        [Fact]
        public void PrivacyShouldReturnView()
            => MyController<HomeController>
                .Instance()
                .Calling(c => c.Privacy())
                .ShouldReturn()
                .View();

        [Theory]
        [InlineData("ClientFirstName")]
        public void ViewBagShouldContainsClientFirstNameWhenLoggedIn
            (string viewBagKey)
            => MyMvc
                .Controller<HomeController>()
                .WithUser(u => u.InRole(ClientRoleName))
                .WithData(TestClient)
                .Calling(c => c.Index())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry(viewBagKey, TestClient.FirstName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void ControllerShouldRedirectToActionWhenUserIsMasseur()
            => MyMvc
                .Controller<HomeController>()
                .WithUser(u => u.InRole(MasseurRoleName))
                .WithData(TestMasseur)
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction(HomeActionName, HomeControllerName,
                    new { area = MasseurAreaName });

        [Fact]
        public void ControllerShouldRedirectToActionWhenUserIsAdmin()
            => MyMvc
                .Controller<HomeController>()
                .WithUser(u => u.InRole(AdminRoleName))
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction(HomeActionName, HomeControllerName,
                    new { area = AdminAreaName });
    }
}
