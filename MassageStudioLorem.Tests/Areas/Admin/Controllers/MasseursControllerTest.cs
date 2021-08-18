namespace MassageStudioLorem.Tests.Areas.Admin.Controllers
{
    using System.Collections.Generic;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;
    using MassageStudioLorem.Data.Enums;
    using MassageStudioLorem.Data.Models;
    using Services.Masseurs.Models;
    using Services.SharedModels;
    using System.Linq;
    using static Data.Models.MasseursControllerTestModels;
    using static Data.DbModels.MasseursControllerTestDbModels;
    using static Global.GlobalConstants.Notifications;
    using static MassageStudioLorem.Areas.Masseur.MasseurConstants;

    public class MasseursControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithValidData()
        {
           var expectedModel = new AllMasseursQueryServiceModel()
               {
                   CurrentPage = 1,
                   MaxPage = 1,
                   Sorting = Gender.Both,
                   Masseurs = new List<MasseurListingServiceModel>()
                       { TestMasseurListingModel }
               };

           MyController<MasseursController>
               .Instance()
               .WithData(TestMasseur)
               .Calling(c => c.All(new AllMasseursQueryServiceModel()
               {
                   CurrentPage = expectedModel.CurrentPage,
                   MaxPage = expectedModel.MaxPage,
                   Sorting = expectedModel.Sorting,
               }))
               .ShouldReturn()
               .View(expectedModel);
        }

        [Fact]
        public void DetailsShouldReturnViewWithValidData()
        {
            var expectedModel = new MasseurDetailsForEdit
            {
                Id = TestMasseur.Id,
                Description = TestMasseur.Description,
                FullName = TestMasseur.FullName,
                ProfileImageUrl = TestMasseur.ProfileImageUrl,
                PhoneNumber = TestMasseurUser.PhoneNumber
            };

            MyController<MasseursController>
                .Instance()
                .WithData(TestMasseur, TestMasseurUser)
                .Calling(c => c.Details(TestMasseur.Id))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void EditMasseurShouldReturnViewWithValidData()
        {
            MyController<MasseursController>
                .Instance()
                .WithData(TestMasseur, TestCategory)
                .Calling(c => c.EditMasseur(TestMasseur.Id))
                .ShouldReturn()
                .View(EditMasseurModel);
        }

        [Fact]
        public void EditMasseurShouldEditMasseurWithValidDataAndReturnRedirectWithTempData
            ()
        {
            var editedMasseurModel = EditMasseurModel;
            editedMasseurModel.FullName = "New Test Full Name";
            editedMasseurModel.CategoryId = TestMasseur.CategoryId;

            MyController<MasseursController>
                .Instance()
                .WithData(TestMasseur, TestCategory)
                .Calling(c => c.Edit(editedMasseurModel))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Masseur>(set =>
                    {
                        var editedFullName = set.First().FullName;
                        editedFullName.ShouldNotBeSameAs
                            (TestMasseurData.FullName);
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyEditedMasseurKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }

        [Fact]
        public void DeleteMasseurShouldReturnViewWithValidData()
        {
            var expectedModel = new DeleteEntityServiceModel()
            {
                Id = TestMasseur.Id,
                CategoryName = TestCategory.Name,
                EntityName = "Masseur",
                Name = TestMasseur.FullName
            };

            MyController<MasseursController>
                .Instance()
                .WithData(TestMasseur, TestCategory)
                .Calling(c => c.DeleteMasseur(TestMasseur.Id))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DeleteMasseurShouldDeleteMasseurWithValidDataAndReturnRedirectWithTempData
            ()
        {
            MyController<MasseursController>
                .Instance()
                .WithUser(u => u.InRole(MasseurRoleName))
                .WithData(TestMasseur, TestMasseurUser, TestCategory)
                .Calling(c => c.Delete(TestMasseur.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Masseur>(set => set.ShouldBeEmpty()))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyDeletedMasseurKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }
    }
}
