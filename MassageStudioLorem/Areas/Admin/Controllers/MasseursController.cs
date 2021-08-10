namespace MassageStudioLorem.Areas.Admin.Controllers
{
    using Data.Enums;
    using MassageStudioLorem.Services.Masseurs;
    using MassageStudioLorem.Services.Masseurs.Models;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
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

            if (allMasseursModel == null)
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
                return this.View(null);
            }

            if (allMasseursModel.Masseurs == null)
                this.ModelState.AddModelError(String.Empty, NoMasseursFound);

            return this.View(allMasseursModel);
        }

        public IActionResult EditMasseur(string masseurId)
        {
            var editMasseurModel = this._masseursService
                .GetMasseurDataForEdit(masseurId);

            if (editMasseurModel.Categories?.Count() <= 0)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(editMasseurModel);
        }

        [HttpPost]
        public IActionResult Edit(EditMasseurFormModel editMasseurModel)
        {
            if (this._masseursService.
                GetCategoryFromDB(editMasseurModel.CategoryId) == null)
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
    }
}
