namespace MassageStudioLorem.Tests.Areas.Admin.Routing
{
    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;
    using MassageStudioLorem.Areas.Admin.Models;

    public class CategoriesControllerTest
    {
        [Fact]
        public void GetIndexShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Categories")
                .To<CategoriesController>(c => c.Index());

        [Fact]
        public void GetAddCategoryShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Categories/AddCategory")
                .To<CategoriesController>(c => c.AddCategory());

        [Fact]
        public void PostAddCategoryShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Categories/AddCategory")
                    .WithMethod(HttpMethod.Post))
                .To<CategoriesController>(c => c.AddCategory(null));

        [Fact]
        public void GetAllShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Categories/All")
                .To<CategoriesController>(c => c.All());

        [Fact]
        public void GetDeleteCategoryShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap("/Admin/Categories/DeleteCategory")
                .To<CategoriesController>(c => c.DeleteCategory
                    (new AllCategoriesViewModel()));

        [Fact]
        public void PostDeleteShouldBeMapped()
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithPath("/Admin/Categories/Delete")
                    .WithMethod(HttpMethod.Post))
                .To<CategoriesController>(c => c.Delete(null));
    }
}
