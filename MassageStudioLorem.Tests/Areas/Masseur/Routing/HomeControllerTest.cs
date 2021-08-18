namespace MassageStudioLorem.Tests.Areas.Masseur.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Masseur.Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseur")
                .To<HomeController>(c => c.Index());
    }
}
