namespace MassageStudioLorem.Controllers
{
    using Data;
    using Data.Enums;
    using Data.Models;
    using Ganss.XSS;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Masseurs;
    using Services;
    using Services.Masseurs;
    using Services.Masseurs.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Paging;

    public class MasseursController : Controller
    {
        private readonly IMasseursService _masseursService;
        private readonly ICommonService _commonService;
        private readonly string _userId;

        public MasseursController(IMasseursService masseursService,
            CommonService commonService)
        {
            this._masseursService = masseursService;
            this._commonService = commonService;
            this._userId = this.User.GetId();
        }

        [Authorize]
        public IActionResult BecomeMasseur()
        {
            if (!this._masseursService.IsUserMasseur(this._userId))
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
            if (this._masseursService.IsUserMasseur(this._userId))
                this.ModelState.AddModelError(String.Empty, AlreadyMasseur);

            if (this._commonService.GetCategoryFromDB
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
            
            this._masseursService.RegisterNewMasseur(masseurModel, this._userId);

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All
            ([FromQuery] AllMasseursQueryServiceModel query)
        {
            var allMasseursModel = this._masseursService.GetAllMasseurs(query.CurrentPage);

            if (allMasseursModel.Masseurs == null)
            {
                this.ModelState.AddModelError(String.Empty, NoMasseursFound);

                return this.View(allMasseursModel);
            }

            return this.View(allMasseursModel);
        }

        [Authorize]
        public IActionResult Details
            ([FromQuery] MasseurDetailsQueryModel queryModel)
        {
            var masseurDetails = this._masseursService
                .GetMasseurDetails(queryModel);

            if (masseurDetails == null)
            {
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);
            }

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