namespace MassageStudioLorem.Tests.Areas.Admin.Controllers.Data.Models
{
    using Services.Masseurs.Models;
    using System.Collections.Generic;
    using static Data.DbModels.MasseursControllerTestDbModels;

    public class MasseursControllerTestModels
    {
        public static MasseurListingServiceModel TestMasseurListingModel
            => new()
            {
                Id = TestMasseur.Id,
                CategoryId = TestMasseur.CategoryId,
                FullName = TestMasseur.FullName,
                ProfileImageUrl = TestMasseur.ProfileImageUrl
            };

        public static EditMasseurFormModel EditMasseurModel => new()
        {
            Id = TestMasseur.Id,
            Description = TestMasseur.Description,
            FullName = TestMasseur.FullName,
            ProfileImageUrl = TestMasseur.ProfileImageUrl,
            Gender = TestMasseur.Gender,
            CurrentCategoryName = TestCategory.Name,
            Categories = new List<MassageCategoryServiceModel>()
            {
                new()
                {
                    Id = TestCategory.Id,
                    Name = TestCategory.Name
                }
            }
        };
    }
}
