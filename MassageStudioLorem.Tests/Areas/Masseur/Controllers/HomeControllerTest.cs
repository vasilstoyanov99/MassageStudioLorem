namespace MassageStudioLorem.Tests.Areas.Masseur.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Masseur.Controllers;

    using static MassageStudioLorem.Areas.Masseur.MasseurConstants;
    using static Data.DbModels.HomeControllerTestDbModels;

    public class HomeControllerTest
    {
        [Theory]
        [InlineData("MasseurFullName")]
        public void ViewBagShouldContainsMasseurFullNameWhenLoggedIn
            (string viewBagKey)
            => MyMvc
                .Controller<HomeController>()
                .WithUser(u => u.InRole(MasseurRoleName))
                .WithData(TestMasseur, TestMasseurUser)
                .Calling(c => c.Index())
                .ShouldHave()
                .ViewBag(viewBag => viewBag
                    .ContainingEntry(viewBagKey, TestMasseur.FullName))
                .AndAlso()
                .ShouldReturn()
                .View();
    }
}
