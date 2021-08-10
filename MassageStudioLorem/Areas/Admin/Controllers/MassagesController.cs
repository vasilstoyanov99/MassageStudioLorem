namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using MassageStudioLorem.Services.Massages;
    using MassageStudioLorem.Services.Massages.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
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

        public IActionResult Delete(string massageId)
        {
            if (!this._massagesService
                .CheckIfMassageDeletedSuccessfully(massageId))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View("DeleteMassage");
            }

            this.TempData[SuccessfullyDeletedMassageKey] =
                SuccessfullyDeletedMassage;

            return this.RedirectToAction(nameof(this.All));
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
                return this.View("EditMassage", editMassageModel);

            if (!this._massagesService
                .CheckIfMassageEditedSuccessfully(editMassageModel))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View("EditMassage");
            }

            this.TempData[SuccessfullyDeletedMasseurKey] =
                SuccessfullyDeletedMasseur;

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
