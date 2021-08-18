namespace MassageStudioLorem.Tests.Areas.Admin.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;

    public class HomeControllerTest
    {
        [Fact]
        public void GetIndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin")
                .To<HomeController>(c => c.Index());
    }
}
