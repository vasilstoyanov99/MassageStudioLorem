namespace MassageStudioLorem.Controllers
{
    using Data.Enums;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Masseurs;
    using Services.Masseurs;
    using Services.Masseurs.Models;
    using System;
    using static Global.GlobalConstants.ErrorMessages;

    public class MasseursController : Controller
    {
        private readonly IMasseursService _masseursService;
        private string _userId;

        public MasseursController(IMasseursService masseursService)
        {
            this._masseursService = masseursService;
        }

        [Authorize]
        public IActionResult BecomeMasseur()
        {
            var userId = User.GetId();

            if (this._masseursService.IsUserMasseur(userId))
                return this.Unauthorized();

            var becomeMasseurModel = new BecomeMasseurFormModel()
                {Categories = this._masseursService.GetCategories()};

            if (becomeMasseurModel.Categories == null)
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
            }

            return this.View(becomeMasseurModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult BecomeMasseur
            (BecomeMasseurFormModel masseurModel)
        {
            var userId = this.User.GetId();

            if (this._masseursService.IsUserMasseur(userId))
                this.ModelState.AddModelError(String.Empty, AlreadyMasseur);

            if (this._masseursService.GetCategoryFromDB
                (masseurModel.CategoryId) == null)
                this.ModelState.AddModelError
                    (String.Empty, CategoryIdError);

            if (!Enum.TryParse(typeof(Gender),
                masseurModel.Gender.ToString(), true, out _))
                this.ModelState.AddModelError
                    (nameof(masseurModel.Gender), GenderIdError);

            if (!this.ModelState.IsValid || this.ModelState.ErrorCount > 0)
            {
                masseurModel.Categories = this._masseursService.GetCategories();

                return this.View(masseurModel);
            }
            
            this._masseursService.RegisterNewMasseur(masseurModel, userId);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All
            ([FromQuery] AllMasseursQueryServiceModel query)
        {
            var allMasseursModel = this._masseursService.GetAllMasseurs(query.CurrentPage);

            if (allMasseursModel.Masseurs == null)
                this.ModelState.AddModelError(String.Empty, NoMasseursFound);

            return this.View(allMasseursModel);
        }

        [Authorize]
        public IActionResult Details
            ([FromQuery] MasseurDetailsQueryModel queryModel)
        {
            var masseurDetails = this._masseursService
                .GetMasseurDetails(queryModel);

            if (masseurDetails == null) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(masseurDetails);
        }

        [Authorize]
        public IActionResult AvailableMasseurs
            ([FromQuery] AvailableMasseursQueryServiceModel queryModel)
        {
            var availableMasseursModel = this._masseursService.
                GetAvailableMasseurs(queryModel);

            if (availableMasseursModel == null)
            {
                this.ModelState.AddModelError
                    (String.Empty, SomethingWentWrong);

                return this.View((AvailableMasseursQueryServiceModel) null);
            }

            if (availableMasseursModel.Masseurs == null)
            {
                this.ModelState
                    .AddModelError(String.Empty, NoMasseursFoundUnderCategory);

                return this.View(availableMasseursModel);
            }

            return this.View(availableMasseursModel);
        }

        [Authorize]
        public IActionResult AvailableMasseurDetails(string masseurId)
        {
            var masseurDetailsModel = this._masseursService
                .GetAvailableMasseurDetails(masseurId);

            if (masseurDetailsModel == null)
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View("Details", masseurDetailsModel);
        }
    }
}