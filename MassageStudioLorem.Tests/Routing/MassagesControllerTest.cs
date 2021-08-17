namespace MassageStudioLorem.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Controllers;

    public class MassagesControllerTest
    {
        [Fact]
        public void GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Massages/All")
                .To<MassagesController>(c => c.All(null));

        [Fact]
        public void GetDetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Massages/Details")
                .To<MassagesController>(c => c.Details(null));

        [Fact]
        public void GetAvailableMassagesShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Massages/AvailableMassages")
                .To<MassagesController>(c => c.AvailableMassages(null));

        [Fact]
        public void GetAvailableMassageDetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Massages/AvailableMassageDetails")
                .To<MassagesController>(c => c.AvailableMassageDetails(null));
    }
}
