namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Services;
    using Services.Models;
    using System;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Notifications;

    public class CategoriesController : AdminController
    {
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(ICategoriesService categoriesService) 
            => this._categoriesService = categoriesService;

        public IActionResult AddCategory() => this.View();

        [HttpPost]
        public IActionResult AddCategory(AddCategoryServiceModel model)
        {
            if (!this._categoriesService
                .CheckIfCategoryIsAddedSuccessfully(model.Name))
            {
                this.ModelState.AddModelError(String.Empty, CategoryNameExists);
                return this.View();
            }

            this.TempData[SuccessfullyAddedCategoryKey] =
                SuccessfullyAddedCategory;

            return this.RedirectToAction("AddCategory", "Categories");
        }
    }
}
