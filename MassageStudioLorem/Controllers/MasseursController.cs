namespace MassageStudioLorem.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using Data.Enums;
    using Infrastructure;
    using Models.Masseurs;
    using Services.Masseurs;
    using Services.Masseurs.Models;

    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Notifications;
    using static Areas.Client.ClientConstants;

    [Authorize(Roles = ClientRoleName)]
    public class MasseursController : Controller
    {
        private readonly IMasseursService _masseursService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public MasseursController(IMasseursService masseursService,
            SignInManager<IdentityUser> signInManager)
        {
            this._masseursService = masseursService;
            this._signInManager = signInManager;
        }

        public IActionResult BecomeMasseur()
        {
            var becomeMasseurModel = new BecomeMasseurFormModel()
                {Categories = this._masseursService.GetCategories()};

            if (becomeMasseurModel.Categories?.Count() <= 0) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(becomeMasseurModel);
        }

        [HttpPost]
        public IActionResult BecomeMasseur
            (BecomeMasseurFormModel masseurModel)
        {
            if (CheckIfNull(this._masseursService.
                GetCategoryFromDB(masseurModel.CategoryId)))
                this.ModelState.AddModelError(String.Empty, CategoryIdError);

            if (!Enum.TryParse(typeof(Gender),
                masseurModel.Gender.ToString(), true, out _))
                this.ModelState.AddModelError(String.Empty, GenderIdError);

            if (!this.ModelState.IsValid || this.ModelState.ErrorCount > 0)
            {
                masseurModel.Categories = this._masseursService.GetCategories();

                return this.View(masseurModel);
            }

            var userId = this.User.GetId();
            this._masseursService.RegisterNewMasseur(masseurModel, userId);

            Task
                .Run(async () =>
                {
                    await this._signInManager.SignOutAsync();
                })
                .GetAwaiter()
                .GetResult();


            this.TempData[SuccessfullyBecomeMasseurKey] = 
                SuccessfullyBecomeMasseur;

            return this.RedirectToAction("Index", "Home");
        }

        public IActionResult All
            ([FromQuery] AllMasseursQueryServiceModel query)
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

        public IActionResult Details
            ([FromQuery] MasseurDetailsQueryModel queryModel)
        {
            var masseurDetails = this._masseursService
                .GetMasseurDetails(queryModel);

            if (CheckIfNull(masseurDetails)) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(masseurDetails);
        }

        public IActionResult AvailableMasseurs
            ([FromQuery] AvailableMasseursQueryServiceModel queryModel)
        {
            var availableMasseursModel = this._masseursService.
                GetAvailableMasseurs(queryModel);
            
            if (CheckIfNull(availableMasseursModel))
            {
                this.ModelState.AddModelError
                    (String.Empty, SomethingWentWrong);

                return this.View((AvailableMasseursQueryServiceModel) null);
            }

            if (CheckIfNull(availableMasseursModel.Masseurs))
            {
                this.ModelState
                    .AddModelError(String.Empty, NoMasseursFoundUnderCategory);

                return this.View(availableMasseursModel);
            }

            return this.View(availableMasseursModel);
        }

        public IActionResult AvailableMasseurDetails(string masseurId)
        {
            var masseurDetailsModel = this._masseursService
                .GetAvailableMasseurDetails(masseurId);

            if (CheckIfNull(masseurDetailsModel))
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View("/Views/Masseurs/Details.cshtml", masseurDetailsModel);
        }

        private static bool CheckIfNull(object obj)
            => obj == null;
    }
}