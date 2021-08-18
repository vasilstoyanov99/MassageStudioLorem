namespace MassageStudioLorem.Tests.Areas.Admin.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;

    using static MassageStudioLorem.Areas.Admin.AdminConstants;
    using static Data.DbModels.HomeControllerTestDbModels;

    public class HomeControllerTest
    {
        [Theory]
        [InlineData("AdminUserName")]
        public void ViewBagShouldContainsMasseurFullNameWhenLoggedIn
            (string viewBagKey)
            => MyMvc
                .Controller<HomeController>()
                .WithUser(u => u.InRole(AdminRoleName))
                .WithData(TestAdminUser)
                .Calling(c => c.Index())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry(viewBagKey, TestAdminUser.UserName))
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
