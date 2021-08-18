namespace MassageStudioLorem.Tests.Controllers
{
    using System.Collections.Generic;

    using MassageStudioLorem.Controllers;
    using Services.Massages.Models;
    using Models.Massages;

    using MyTested.AspNetCore.Mvc;
    using Xunit;

    using static Data.DbModels.MassagesControllerTestDbModels;
    public class MassagesControllerTest
    {
        [Fact]
        public void ControllerShouldHaveAuthorizeFilter()
            => MyMvc
                .Controller<MassagesController>()
                .ShouldHave()
                .Attributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());


        [Fact]
        public void AllShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new AllCategoriesQueryServiceModel()
            {
                CurrentPage = 1,
                TotalCategories = 1,
                Id = TestCategory.Id,
                Name = TestCategory.Name,
                Massages = new List<MassageListingServiceModel>()
                {
                    new()
                    {
                        Id = TestMassage.Id,
                        Name = TestMassage.Name,
                        ImageUrl = TestMassage.ImageUrl,
                        ShortDescription = DummyDescription
                    }
                }
            };

            MyController<MassagesController>
                .Instance()
                .WithData(TestCategory, TestMassage)
                .Calling(c => c.All(expectedModel))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DetailsShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new MassageDetailsServiceModel
            {
                Id = TestMassage.Id,
                CategoryId = TestCategory.Id,
                ImageUrl = TestMassage.ImageUrl,
                LongDescription = DummyDescription,
                Name = TestMassage.Name,
                Price = TestMassage.Price
            };

            MyController<MassagesController>
                .Instance()
                .WithData(TestCategory, TestMassage)
                .Calling(c => c.Details(new MassageDetailsQueryModel()
                {
                    CategoryId = TestCategory.Id, MassageId = TestMassage.Id
                }))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void AvailableMassagesShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new AvailableMassagesQueryServiceModel()
            {
                CategoryId = TestCategory.Id,
                CurrentPage = 1,
                MaxPage = 1,
                MasseurId = TestMasseur.Id,
                Massages = new List<MassageListingServiceModel>()
                {
                    new()
                    {
                        Id = TestMassage.Id,
                        Name = TestMassage.Name,
                        ImageUrl = TestMassage.ImageUrl,
                        ShortDescription = DummyDescription
                    }
                }
            };

            MyController<MassagesController>
                .Instance()
                .WithData(TestCategory, TestMassage, TestMasseur)
                .Calling(c => c.AvailableMassages(expectedModel))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void AvailableMassageDetailsShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new MassageDetailsServiceModel()
            {
                Id = TestMassage.Id,
                MasseurId = TestMasseur.Id,
                CategoryId = null,
                Name = TestMassage.Name,
                ImageUrl = TestMassage.ImageUrl,
                LongDescription = DummyDescription,
                Price = TestMassage.Price
            };

            MyController<MassagesController>
                .Instance()
                .WithData(TestCategory, TestMassage, TestMasseur)
                .Calling(c => c.AvailableMassageDetails(new AvailableMassageDetailsQueryModel()
                {
                    MasseurId = TestMasseur.Id, MassageId = TestMassage.Id
                }))
                .ShouldReturn()
                .View("/Views/Massages/Details.cshtml", expectedModel);
        }
    }
}
