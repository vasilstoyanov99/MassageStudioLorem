        namespace MassageStudioLorem.Tests.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using Data.DbModels;
    using MassageStudioLorem.Controllers;
    using MassageStudioLorem.Data.Enums;
    using MassageStudioLorem.Data.Models;
    using Models.Masseurs;
    using Services.Masseurs.Models;

    using static Global.GlobalConstants.Notifications;
    using static Data.DbModels.MasseursControllerTestDbModels;
    using static MassageStudioLorem.Areas.Client.ClientConstants;

    public class MasseursControllerTest
    {
        [Fact]
        public void ControllerShouldHaveAuthorizeFilter()
            => MyMvc
                .Controller<MasseursController>()
                .ShouldHave()
                .Attributes(attrs => attrs
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void BecomeMasseurShouldReturnView()
            => MyController<MasseursController>
                .Instance()
                .Calling(c => c.BecomeMasseur())
                .ShouldReturn()
                .View();

        [Fact]
        public void BecomeMasseurShouldRedirectWithTempDataMessageAndSaveMasseurWithValidData
            ()
        {
            var expectedModel = new BecomeMasseurFormModel()
            {
                Description = DummyDescription,
                FullName = TestMasseur.FullName,
                Gender = TestMasseur.Gender,
                ProfileImageUrl = TestImageUrl,
                CategoryId = TestCategory.Id
            };

            MyController<MasseursController>
                .Instance()
                .WithUser(u => u.InRole(ClientRoleName))
                .WithData
                (TestCategory,
                    MasseursControllerTestDbModels.TestUser,
                    TestClient,
                    MasseurRole)
                .Calling(c => c.BecomeMasseur(expectedModel))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Masseur>(set =>
                    {
                        set.ShouldNotBeNull();
                        set.FirstOrDefault(masseur =>
                                masseur.CategoryId == TestCategory.Id)
                            .ShouldNotBeNull();
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyBecomeMasseurKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction(HomeActionName, HomeControllerName);
        }

        [Fact]
        public void AllShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new AllMasseursQueryServiceModel()
            {
                Masseurs = new List<MasseurListingServiceModel>()
                {
                    new()
                    {
                        FullName = TestMasseur.FullName,
                        CategoryId = TestMasseur.CategoryId,
                        Id = TestMasseur.Id,
                        ProfileImageUrl = TestMasseur.ProfileImageUrl
                    }
                },
                Sorting = Gender.Both,
                CurrentPage = 1,
                MaxPage = 1
            };

            MyController<MasseursController>
                .Instance()
                .WithData(TestCategory, TestMasseur)
                .Calling(c => c.All(expectedModel))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DetailsShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new AvailableMasseurDetailsServiceModel
            {
                CategoryId = TestCategory.Id,
                MassageId = TestMassage.Id,
                Description = DummyDescription,
                FullName = TestMasseur.FullName,
                Id = TestMasseur.Id,
                PhoneNumber = TestMasseurUser.PhoneNumber,
                ProfileImageUrl = TestMasseur.ProfileImageUrl
            };

            MyController<MasseursController>
                .Instance()
                .WithData
                (TestCategory, 
                  TestMassage, 
                  TestMasseur, 
                  TestMasseurUser)
                .Calling(c => c.Details(new MasseurDetailsQueryModel()
                {
                    CategoryId = TestCategory.Id, 
                    MasseurId = TestMasseur.Id, 
                    MassageId = TestMassage.Id
                }))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void AvailableMasseursShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new AvailableMasseursQueryServiceModel()
            {
                CategoryId = TestCategory.Id,
                MassageId = TestMassage.Id,
                Sorting = Gender.Both,
                CurrentPage = 1,
                MaxPage = 1,
                Masseurs = new List<AvailableMasseurListingServiceModel>()
                {
                    new()
                    {
                        Id = TestMasseur.Id,
                        FullName = TestMasseur.FullName,
                        ProfileImageUrl = TestMasseur.ProfileImageUrl
                    }
                }
            };

            MyController<MasseursController>
                .Instance()
                .WithData(TestCategory, TestMasseur, TestMassage)
                .Calling(c => c.AvailableMasseurs(expectedModel))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void AvailableMasseurDetailsShouldReturnViewWithModelWithValidData()
        {
            var expectedModel = new AvailableMasseurDetailsServiceModel()
            {
                CategoryId = TestCategory.Id,
                Description = TestMasseur.Description,
                FullName = TestMasseur.FullName,
                Id = TestMasseur.Id,
                MassageId = null,
                PhoneNumber = TestMasseurUser.PhoneNumber,
                ProfileImageUrl = TestImageUrl
            };

            MyController<MasseursController>
                .Instance()
                .WithData
                (TestMasseur, 
                  TestCategory, 
                  TestMassage, 
                  TestMasseurUser)
                .Calling(c => c.AvailableMasseurDetails(TestMasseur.Id))
                .ShouldReturn()
                .View("/Views/Masseurs/Details.cshtml", expectedModel);
        }
    }
}
