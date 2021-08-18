namespace MassageStudioLorem.Tests.Areas.Admin.Controllers.Data.Models
{
    using MassageStudioLorem.Areas.Admin.Services.Models;
    using System.Collections.Generic;

    using static DbModels.CategoriesControllerTestDbModels;

    public class CategoriesControllerTestModels
    {
        public static CategoryServiceModel TestCategoryModel => new()
        {
            Id = TestCategoryData.Id, 
            Name = TestCategoryData.Name, 
            IsEmpty = TestCategoryData.IsEmpty
        };

        public static DeleteCategoryServiceModel TestDeleteCategoryModel
            => new()
        {
            Id = TestCategoryData.Id,
            Name = TestCategoryData.Name,
            Massages = new List<string>(),
            Masseurs = new List<string>()
        };
    }
}
