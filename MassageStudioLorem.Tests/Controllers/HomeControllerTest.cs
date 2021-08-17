namespace MassageStudioLorem.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Controllers;

    using static Areas.Client.ClientConstants;
    using static Areas.Masseur.MasseurConstants;
    using static Areas.Admin.AdminConstants;
    using static Global.GlobalConstants;
    using static Data.TestDbModels;

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
                .WithUser(u => u.InRole(ClientRoleName)
                    .AndAlso()
                    .WithUsername(TestUserData.Username))
                .WithData(DummyClient)
                .Calling(c => c.Index())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry(viewBagKey, TestClientData.FirstName))
                .AndAlso()
                .ShouldReturn()
                .View();

        [Fact]
        public void ControllerShouldRedirectToActionWhenUserIsMasseur()
            => MyMvc
                .Controller<HomeController>()
                .WithUser(u => u.InRole(MasseurRoleName)
                    .AndAlso()
                    .WithUsername(TestUserData.Username))
                .WithData(DummyMasseur)
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction(HomeActionName, HomeControllerName,
                    new { area = MasseurAreaName });

        [Fact]
        public void ControllerShouldRedirectToActionWhenUserIsAdmin()
            => MyMvc
                .Controller<HomeController>()
                .WithUser(u => u.InRole(AdminRoleName)
                    .AndAlso()
                    .WithUsername(TestUserData.Username))
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction(HomeActionName, HomeControllerName,
                    new { area = AdminAreaName });
    }
}
