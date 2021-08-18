namespace MassageStudioLorem.Tests.Areas.Admin.Controllers.Data.Models
{
    using System.Collections.Generic;

    using Services.Massages.Models;

    using static DbModels.MassagesControllerTestDbModels;

    public class MassagesControllerTestModels
    {
        public static MassageListingServiceModel TestMassageListingModel
            => new()
            {
                Id = TestMassage.Id,
                Name = TestMassage.Name,
                ImageUrl = TestMassage.ImageUrl,
                ShortDescription = TestMassage.ShortDescription
            };

        public static AllCategoriesQueryServiceModel AllCategoriesModel
            => new()
            {
                CurrentPage = 1,
                Id = TestCategory.Id,
                Name = TestCategory.Name,
                TotalCategories = 1,
                Massages = new List<MassageListingServiceModel>() { TestMassageListingModel }
            };

        public static EditMassageFormModel EditMassageModel => new()
        {
            Id = TestMassage.Id,
            ImageUrl = TestMassage.ImageUrl,
            ShortDescription = TestMassage.ShortDescription,
            LongDescription = TestMassage.LongDescription,
            Name = TestMassage.Name,
            Price = TestMassage.Price
        };

        public static AddMassageFormModel AddMassageModel => new()
        {
            Id = TestMassage.Id,
            Name = TestMassage.Name,
            CategoryId = TestCategory.Id,
            ImageUrl = TestMassage.ImageUrl,
            ShortDescription = TestMassage.ShortDescription,
            LongDescription = TestMassage.LongDescription,
            Price = TestMassage.Price
        };
    }
}
