namespace MassageStudioLorem.Tests.Areas.Masseur.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Masseur.Controllers;

    public class ReviewsControllerTest
    {
        [Fact]
        public void GetIndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseur/Reviews/All")
                .To<ReviewsController>(c => c.All());
    }
}
