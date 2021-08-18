namespace MassageStudioLorem.Tests.Areas.Admin.Controllers
{
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;
    using MassageStudioLorem.Data.Models;
    using Services.Massages.Models;
    using Services.SharedModels;

    using static Data.DbModels.MassagesControllerTestDbModels;
    using static Data.Models.MassagesControllerTestModels;
    using static Global.GlobalConstants.Notifications;

    public class MassagesControllerTest
    {
        [Fact]
        public void AllShouldReturnViewWithValidData()
        {
            var expectedModel = AllCategoriesModel;

            MyController<MassagesController>
                .Instance()
                .WithData(TestMassage, TestCategory)
                .Calling(c => c.All(new AllCategoriesQueryServiceModel()
                {
                    CurrentPage = expectedModel.CurrentPage, 
                    Id = expectedModel.Id, 
                    Name = expectedModel.Name, 
                    TotalCategories = expectedModel.TotalCategories
                }))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DetailsShouldReturnViewWithValidData()
        {
            var expectedModel = new EditMassageDetailsServiceModel
            {
                Id = TestMassage.Id,
                Name = TestMassage.Name,
                ImageUrl = TestMassage.ImageUrl,
                LongDescription = TestMassage.LongDescription,
                Price = TestMassage.Price
            };

            MyController<MassagesController>
                .Instance()
                .WithData(TestMassage)
                .Calling(c => c.Details(TestMassage.Id))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DeleteMassageShouldReturnViewWithValidData()
        {
            var expectedModel = new DeleteEntityServiceModel
            {
                Id = TestMassage.Id,
                Name = TestMassage.Name,
                CategoryName = TestCategory.Name,
                EntityName = "Massage"
            };

            MyController<MassagesController>
                .Instance()
                .WithData(TestMassage, TestCategory)
                .Calling(c => c.DeleteMassage(TestMassage.Id))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DeleteMassageShouldDeleteMassageWithValidDataAndReturnViewWithTempData
            ()
        {
            MyController<MassagesController>
                .Instance()
                .WithData(TestMassage, TestCategory, TestUpcomingAppointment)
                .Calling(c => c.Delete(TestMassage.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Massage>(set => set.ShouldBeEmpty()))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyDeletedMassageKey))
                .AndAlso()
                .ShouldReturn()
                .View("All");
        }

        [Fact]
        public void EditMassageShouldReturnViewWithValidData()
        {
            var expectedModel = EditMassageModel;

            MyController<MassagesController>
                .Instance()
                .WithData(TestMassage)
                .Calling(c => c.EditMassage(TestMassage.Id))
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void EditMassageShouldEditMassageWithValidDataAndReturnRedirectWithTempData
            ()
        {
            var editedMassageModel = EditMassageModel;
            editedMassageModel.Name = "New Test Massage Name";

            MyController<MassagesController>
                .Instance()
                .WithData(TestMassage)
                .Calling(c => c.Edit(editedMassageModel))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Massage>(set =>
                    {
                        var editedName = set.First().Name;
                        editedName.ShouldNotBeSameAs(TestMassageData.Name);
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyEditedMassageKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }

        [Fact]
        public void AddMassageShouldReturnViewWithValidData()
        {
            MyController<MassagesController>
                .Instance()
                .WithData(TestCategory)
                .Calling(c => c.AddMassage())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void AddMassageShouldAddMassageWithValidDataAndReturnRedirectWithTempData
            ()
        {
            MyController<MassagesController>
                .Instance()
                .WithData(TestCategory)
                .Calling(c => c.Add(AddMassageModel))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Massage>(set =>
                    {
                        var newMassageName = set.First().Name;
                        newMassageName
                            .ShouldBe(TestMassage.Name);
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyAddedMassageKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }
    }
}
