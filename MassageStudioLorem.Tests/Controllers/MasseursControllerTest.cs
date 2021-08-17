namespace MassageStudioLorem.Tests.Controllers
{
    using System.Linq;
    using System.Collections.Generic;

    using Data;
    using MassageStudioLorem.Controllers;
    using MassageStudioLorem.Data.Enums;
    using MassageStudioLorem.Data.Models;
    using Models.Masseurs;
    using Services.Masseurs.Models;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using static Global.GlobalConstants;
    using static MassageStudioLorem.Global.GlobalConstants.Notifications;
    using static Data.TestDbModels;
    using static Areas.Client.ClientConstants;

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
            var model = new BecomeMasseurFormModel()
            {
                Description = DummyDescription,
                FullName = TestMasseurData.FullName,
                Gender = TestMasseurData.Gender,
                ProfileImageUrl = TestImageUrl,
                CategoryId = TestCategory.Id
            };

            MyController<MasseursController>
                .Instance()
                .WithUser(u => u.InRole(ClientRoleName))
                .WithData
                (TestCategory,
                    TestDbModels.TestUser,
                    TestClient,
                    MasseurRole)
                .Calling(c => c.BecomeMasseur(model))
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
            var model = new AllMasseursQueryServiceModel()
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
                .Calling(c => c.All(model))
                .ShouldReturn()
                .View(model);
        }

        [Fact]
        public void DetailsShouldReturnViewWithModelWithValidData()
        {
            var model = new AvailableMasseurDetailsServiceModel
            {
                CategoryId = TestCategory.Id,
                MassageId = TestMassage.Id,
                Description = DummyDescription,
                FullName = TestMasseur.FullName,
                Id = TestMasseur.Id,
                PhoneNumber = DummyPhoneNumber,
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
                .View(model);
        }

        [Fact]
        public void AvailableMasseursShouldReturnViewWithModelWithValidData()
        {
            var model = new AvailableMasseursQueryServiceModel()
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
                .Calling(c => c.AvailableMasseurs(model))
                .ShouldReturn()
                .View(model);
        }

        [Fact]
        public void AvailableMasseurDetailsShouldReturnViewWithModelWithValidData()
        {
            var model = new AvailableMasseurDetailsServiceModel()
            {
                CategoryId = TestCategory.Id,
                Description = TestMasseur.Description,
                FullName = TestMasseur.FullName,
                Id = TestMasseur.Id,
                MassageId = null,
                PhoneNumber = DummyPhoneNumber,
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
                .View("/Views/Masseurs/Details.cshtml", model);
        }
    }
}
