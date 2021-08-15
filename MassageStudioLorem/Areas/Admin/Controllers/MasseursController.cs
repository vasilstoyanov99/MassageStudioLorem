namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using System;
    using System.Linq;
    using Data.Enums;
    using Microsoft.AspNetCore.Mvc;

    using MassageStudioLorem.Services.Masseurs;
    using MassageStudioLorem.Services.Masseurs.Models;

    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Notifications;

    public class MasseursController : AdminController
    {
        private readonly IMasseursService _masseursService;

        public MasseursController(IMasseursService masseursService)
            => this._masseursService = masseursService;

        public IActionResult All([FromQuery] AllMasseursQueryServiceModel query)
        {
            var allMasseursModel = this._masseursService
                .GetAllMasseurs(query.CurrentPage, query.Sorting);

            if (CheckIfNull(allMasseursModel))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View(null);
            }

            if (CheckIfNull(allMasseursModel.Masseurs))
                this.ModelState.AddModelError(String.Empty, NoMasseursFound);

            return this.View(allMasseursModel);
        }

        public IActionResult Details(string masseurId)
        {
            var masseurDetailsModel = this._masseursService
                .GetMasseurDetailsForEdit(masseurId);

            if (CheckIfNull(masseurDetailsModel))
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(masseurDetailsModel);
        }

        public IActionResult EditMasseur(string masseurId)
        {
            var editMasseurModel = this._masseursService
                .GetMasseurDataForEdit(masseurId);

            if (editMasseurModel == null || 
                editMasseurModel.Categories?.Count() <= 0)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(editMasseurModel);
        }

        [HttpPost]
        public IActionResult Edit(EditMasseurFormModel editMasseurModel)
        {
            if (CheckIfNull(this._masseursService.
                GetCategoryFromDB(editMasseurModel.CategoryId)))
                this.ModelState.AddModelError(String.Empty, CategoryIdError);

            if (!Enum.TryParse(typeof(Gender),
                editMasseurModel.Gender.ToString(), true, out _))
                this.ModelState.AddModelError(String.Empty, GenderIdError);

            if (!this.ModelState.IsValid || this.ModelState.ErrorCount > 0)
            {
                editMasseurModel.Categories = this._masseursService.GetCategories();

                return this.View(nameof(this.EditMasseur), editMasseurModel);
            }

            if (!this._masseursService
                .CheckIfMasseurEditedSuccessfully(editMasseurModel))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

                return this.View(nameof(this.EditMasseur));
            }

            this.TempData[SuccessfullyEditedMasseurKey] =
                SuccessfullyEditedMasseur;

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult DeleteMasseur(string masseurId)
        {
            var masseurData = this._masseursService
                .GetMasseurDataForDelete(masseurId);

            if (CheckIfNull(masseurData)) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(masseurData);
        }

        [HttpPost]
        public IActionResult Delete(string masseurId)
        {
            if (!this._masseursService.CheckIfMasseurDeletedSuccessfully(masseurId))
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View(nameof(this.DeleteMasseur));
            }

            this.TempData[SuccessfullyDeletedMasseurKey] =
                SuccessfullyDeletedMasseur;

            return this.RedirectToAction(nameof(this.All));
        }

        private static bool CheckIfNull(object obj)
            => obj == null;
    }
}
