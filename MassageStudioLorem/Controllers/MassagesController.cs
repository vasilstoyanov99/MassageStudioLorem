namespace MassageStudioLorem.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using static Global.GlobalConstants.ErrorMessages;
    using Models.Massages;
    using Services.Massages;
    using Services.Massages.Models;

    public class MassagesController : Controller
    {
        private readonly LoremDbContext _data;
        private readonly IMassagesService _massagesService;

        public MassagesController
            (LoremDbContext data, IMassagesService massagesService)
        {
            this._data = data;
            this._massagesService = massagesService;
        }

        [Authorize]
        public IActionResult All
            ([FromQuery] AllCategoriesQueryServiceModel queryModel)
        {
            var allCategoriesWithMassagesModel = this._massagesService
                .GetAllCategoriesWithMassages
                    (queryModel.Id, queryModel.Name, queryModel.CurrentPage);

            if (allCategoriesWithMassagesModel == null)
            {
                this.ModelState.AddModelError
                    (String.Empty, NoMassagesAndCategoriesFound);
            }

            return this.View(allCategoriesWithMassagesModel);
        }

        [Authorize]
        public IActionResult Details
            ([FromQuery] MassageDetailsQueryModel queryModel)
        {
            var massageDetailsModel = this._massagesService
                .GetMassageDetails(queryModel.MassageId, queryModel.CategoryId);

            if (massageDetailsModel == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            return this.View(massageDetailsModel);
        }

        [Authorize]
        public IActionResult AvailableMassages
            ([FromQuery] AvailableMassagesQueryServiceModel queryModel)
        {
            var availableMassagesModel = this._massagesService.GetAvailableMassages
            (queryModel.MasseurId, queryModel.CategoryId, queryModel.CurrentPage);

            if (!availableMassagesModel.Massages.Any())
            {
                this.ModelState
                    .AddModelError(String.Empty, NoMassagesFoundUnderCategory);

                return this.View
                    (new AvailableMassagesQueryServiceModel() {Massages = null});
            }

            return this.View(availableMassagesModel);
        }

        [Authorize]
        public IActionResult AvailableMassageDetails
            ([FromQuery] AvailableMassageDetailsQueryModel queryModel)
        {
            var massageDetailsModel =
                this._massagesService.GetAvailableMassageDetails
                    (queryModel.MassageId, queryModel.MasseurId);

            if (massageDetailsModel == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            return this.View("Details", massageDetailsModel);
        }
    }
}