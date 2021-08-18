namespace MassageStudioLorem.Tests.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using Shouldly;
    using Xunit;

    using MassageStudioLorem.Areas.Admin.Controllers;
    using MassageStudioLorem.Areas.Admin.Models;
    using MassageStudioLorem.Areas.Admin.Services.Models;
    using MassageStudioLorem.Data.Models;

    using static Data.DbModels.CategoriesControllerTestDbModels;
    using static Data.Models.CategoriesControllerTestModels;
    using static Global.GlobalConstants.Notifications;

    public class CategoriesControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
        {
            MyController<CategoriesController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void AddCategoryShouldReturnView()
        {
            MyController<CategoriesController>
                .Instance()
                .Calling(c => c.AddCategory())
                .ShouldReturn()
                .View();
        }

        [Fact]
        public void AddCategoryShouldAddCategoryWithValidDataAndReturnRedirectWithTempData
            ()
        {
            MyController<CategoriesController>
                .Instance()
                .Calling(c => c.AddCategory(new AddCategoryFormModel()
                {
                    Name = TestCategoryData.Name
                }))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Category>(set =>
                    {
                        set.ShouldNotBeNull();
                        set.FirstOrDefault(c => c.Name == TestCategoryData.Name)
                            .ShouldNotBeNull();
                    }))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyAddedCategoryKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("AddCategory");
        }

        [Fact]
        public void AllShouldReturnViewWithValidData()
        {
            var expectedModel = new AllCategoriesViewModel()
                { Categories = new List<CategoryServiceModel>() 
                    { TestCategoryModel } };

            MyController<CategoriesController>
                .Instance()
                .WithData(TestCategory)
                .Calling(c => c.All())
                .ShouldReturn()
                .View(expectedModel);
        }

        [Fact]
        public void DeleteCategoryShouldReturnView()
        {
            MyController<CategoriesController>
                .Instance()
                .WithData(TestCategory)
                .Calling(c => c.DeleteCategory(new AllCategoriesViewModel()
                {
                    CategoryId = TestCategory.Id,
                    Categories = new List<CategoryServiceModel>() { TestCategoryModel }
                }))
                .ShouldReturn()
                .View("DeleteCategory", TestDeleteCategoryModel);
        }

        [Fact]
        public void DeleteCategoryShouldDeleteCategoryWithValidDataAndReturnRedirectWithTempData
            ()
        {
            MyController<CategoriesController>
                .Instance()
                .WithData(TestCategory)
                .Calling(c => c.Delete(TestCategory.Id))
                .ShouldHave()
                .Data(data => data
                    .WithSet<Category>(set => set.ShouldBeEmpty()))
                .AndAlso()
                .ShouldHave()
                .TempData(tempData => tempData
                    .ContainingEntryWithKey(SuccessfullyDeletedCategoryKey))
                .AndAlso()
                .ShouldReturn()
                .RedirectToAction("All");
        }
    }
}
