namespace MassageStudioLorem.Tests.Areas.Admin.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;

    public class MassagesControllerTest
    {
        [Fact]
        public void GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Massages/All")
                .To<MassagesController>(c => c.All(null));

        [Fact]
        public void GetDetailsShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Massages/Details")
                .To<MassagesController>(c => c.Details(null));

        [Fact]
        public void GetDeleteMassageShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Massages/DeleteMassage")
                .To<MassagesController>(c => c.DeleteMassage(null));

        [Fact]
        public void PostDeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Massages/Delete")
                    .WithMethod(HttpMethod.Post))
                .To<MassagesController>(c => c.Delete(null));

        [Fact]
        public void GetEditMassageShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Massages/EditMassage")
                .To<MassagesController>(c => c.EditMassage(null));

        [Fact]
        public void PostEditShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Massages/Edit")
                    .WithMethod(HttpMethod.Post))
                .To<MassagesController>(c => c.Edit(null));

        [Fact]
        public void GetAddMassageShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Massages/AddMassage")
                .To<MassagesController>(c => c.AddMassage());

        [Fact]
        public void PostAddShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Massages/Add")
                    .WithMethod(HttpMethod.Post))
                .To<MassagesController>(c => c.Add(null));
    }
}
