namespace MassageStudioLorem.Tests.Areas.Admin.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;

    public class MasseursControllerTests
    {
        [Fact]
        public void GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Masseurs/All")
                .To<MasseursController>(c => c.All(null));

        [Fact]
        public void GetDetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Masseurs/Details")
                .To<MasseursController>(c => c.Details(null));

        [Fact]
        public void GetEditMasseurShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Masseurs/EditMasseur")
                .To<MasseursController>(c => c.EditMasseur(null));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Masseurs/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<MasseursController>(c => c.Edit(null));

        [Fact]
        public void GetDeleteMasseurShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Masseurs/DeleteMasseur")
                .To<MasseursController>(c => c.DeleteMasseur(null));

        [Fact]
        public void PostDeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Masseurs/Delete")
                    .WithMethod(HttpMethod.Post))
                .To<MasseursController>(c => c.Delete(null));
    }
}
