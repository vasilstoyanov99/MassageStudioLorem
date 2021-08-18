namespace MassageStudioLorem.Tests.Areas.Admin.Controllers.Data.DbModels
{
    using MassageStudioLorem.Data.Models;

    public class CategoriesControllerTestDbModels
    {
        public static Category TestCategory => new()
        {
            Name = TestCategoryData.Name,
            Id = TestCategoryData.Id
        };

        public static class TestCategoryData
        {
            public const string Id = "TestCategoryId";

            public const string Name = "TestCategory"; 

            public const bool IsEmpty = true;
        }
    }
}
