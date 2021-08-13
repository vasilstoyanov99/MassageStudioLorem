namespace MassageStudioLorem.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Models.Massages;
    using Services.Massages;
    using Services.Massages.Models;

    using static Global.GlobalConstants.ErrorMessages;
    using static Areas.Client.ClientConstants;

    [Authorize(Roles = ClientRoleName)]
    public class MassagesController : Controller
    {
        private readonly IMassagesService _massagesService;

        public MassagesController
            (IMassagesService massagesService) =>
            this._massagesService = massagesService;

        public IActionResult All
            ([FromQuery] AllCategoriesQueryServiceModel queryModel)
        {
            var allCategoriesWithMassagesModel = this._massagesService
                .GetAllCategoriesWithMassages
                    (queryModel.Id, queryModel.Name, queryModel.CurrentPage);

            if (CheckIfNull(allCategoriesWithMassagesModel))
                this.ModelState.AddModelError
                    (String.Empty, NoCategoriesAndMassagesFound);

            return this.View(allCategoriesWithMassagesModel);
        }

        public IActionResult Details
            ([FromQuery] MassageDetailsQueryModel queryModel)
        {
            var massageDetailsModel = this._massagesService
                .GetMassageDetails(queryModel.MassageId, queryModel.CategoryId);

            if (CheckIfNull(massageDetailsModel)) 
                this.ModelState.AddModelError(String.Empty, SomethingWentWrong);

            return this.View(massageDetailsModel);
        }

        public IActionResult AvailableMassages
            ([FromQuery] AvailableMassagesQueryServiceModel queryModel)
        {
            var availableMassagesModel = this._massagesService.GetAvailableMassages
            (queryModel.MasseurId, queryModel.CategoryId, queryModel.CurrentPage);

            if (CheckIfNull(availableMassagesModel) || 
                !availableMassagesModel.Massages.Any())
                this.ModelState
                    .AddModelError(String.Empty, SomethingWentWrong);

            return this.View(availableMassagesModel);
        }

        public IActionResult AvailableMassageDetails
            ([FromQuery] AvailableMassageDetailsQueryModel queryModel)
        {
            var massageDetailsModel =
                this._massagesService.GetAvailableMassageDetails
                    (queryModel.MassageId, queryModel.MasseurId);

            if (CheckIfNull(massageDetailsModel))
                this.ModelState
                    .AddModelError(String.Empty, SomethingWentWrong);

            return this.View("/Views/Massages/Details.cshtml", massageDetailsModel);
        }

        private static bool CheckIfNull(object obj)
            => obj == null;
    }
}