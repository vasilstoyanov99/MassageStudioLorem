namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using MassageStudioLorem.Services.Massages;
    using MassageStudioLorem.Services.Massages.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Notifications;

    public class MassagesController : AdminController
    {
        private readonly IMassagesService _massagesService;

        public MassagesController(IMassagesService massagesService)
            => this._massagesService = massagesService;

        public IActionResult All
            ([FromQuery] AllCategoriesQueryServiceModel queryModel)
        {
            var allCategoriesWithMassagesModel = this._massagesService
                .GetAllCategoriesWithMassages
                    (queryModel.Id, queryModel.Name, queryModel.CurrentPage);

            if (allCategoriesWithMassagesModel == null)
                this.ModelState.AddModelError
                    (String.Empty, NoMassagesFound);

            return this.View(allCategoriesWithMassagesModel);
        }

        public IActionResult Details(string massageId)
        {
            var editMassageDetailsModel =
                this._massagesService.GetMassageDetailsForEdit(massageId);

            if (editMassageDetailsModel == null)
                this.ModelState.AddModelError
                    (String.Empty, SomethingWentWrong);

            return this.View(editMassageDetailsModel);
        }

        public IActionResult DeleteMassage(string massageId)
        {
            var massageDetails = this._massagesService
                .GetMassageDataForDelete(massageId);

            if (massageDetails == null)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(massageDetails);
        }

        [HttpPost]
        public IActionResult Delete(string massageId)
        {
            if (!this._massagesService
                .CheckIfMassageDeletedSuccessfully(massageId))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View(nameof(this.DeleteMassage));
            }

            this.TempData[SuccessfullyDeletedMassageKey] =
                SuccessfullyDeletedMassage;

            return this.View(nameof(this.All));
        }

        public IActionResult EditMassage(string massageId)
        {
            var editMassageModel = this._massagesService
                .GetMassageDataForEdit(massageId);

            if (editMassageModel == null)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(editMassageModel);
        }

        [HttpPost]
        public IActionResult Edit(EditMassageFormModel editMassageModel)
        {
            if (!this.ModelState.IsValid)
                return this.View(nameof(this.EditMassage), editMassageModel);

            if (!this._massagesService
                .CheckIfMassageEditedSuccessfully(editMassageModel))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View(nameof(this.EditMassage));
            }

            this.TempData[SuccessfullyEditedMassageKey] =
                SuccessfullyEditedMassage;

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult AddMassage()
        {
            var addMassageModel = new AddMassageFormModel() 
                { Categories = this._massagesService.GetCategories() };

            if (addMassageModel.Categories?.Count() <= 0)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(addMassageModel);
        }

        [HttpPost]
        public IActionResult Add(AddMassageFormModel addMassageModel)
        {
            if (this._massagesService
                .GetCategoryFromDB(addMassageModel.CategoryId) == null)
                this.ModelState.AddModelError(String.Empty, CategoryIdError);

            if (this._massagesService.CheckIfMassageNameExists(addMassageModel.Name))
                this.ModelState.AddModelError(String.Empty,
                    String.Format(MassageNameError, addMassageModel.Name));

            if (!this.ModelState.IsValid || this.ModelState.ErrorCount > 0)
            {
                addMassageModel.Categories = this._massagesService.GetCategories();

                return this.View(nameof(this.AddMassage), addMassageModel);
            }

            this._massagesService.AddMassage(addMassageModel);

            this.TempData[SuccessfullyAddedMassageKey] =
                SuccessfullyAddedMassage;

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
