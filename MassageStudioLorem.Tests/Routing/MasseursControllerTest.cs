namespace MassageStudioLorem.Tests.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Controllers;

    public class MasseursControllerTest
    {
        [Fact]
        public void GetBecomeMasseurShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseurs/BecomeMasseur")
                .To<MasseursController>(c => c.BecomeMasseur());

        [Fact]
        public void PostBecomeMasseurShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Masseurs/BecomeMasseur")
                    .WithMethod(HttpMethod.Post))
                .To<MasseursController>(c => c.BecomeMasseur(null));

        [Fact]
        public void GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseurs/All")
                .To<MasseursController>(c => c.All(null));


        [Fact]
        public void GetDetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseurs/Details")
                .To<MasseursController>(c => c.Details(null));

        [Fact]
        public void GetAvailableMasseursShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseurs/AvailableMasseurs")
                .To<MasseursController>(c => c.AvailableMasseurs(null));

        [Fact]
        public void GetAvailableMasseurDetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Masseurs/AvailableMasseurDetails")
                .To<MasseursController>(c => c.AvailableMasseurDetails(null));
    }
}
