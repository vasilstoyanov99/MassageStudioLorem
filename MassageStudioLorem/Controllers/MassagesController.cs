namespace MassageStudioLorem.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Microsoft.AspNetCore.Authorization;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Paging;
    using Models.Categories;
    using Models.Massages;
    using Services.Massages;
    using Services.Massages.Models;

    public class MassagesController : Controller
    {
        private readonly LoremDbContext _data;
        private readonly IMassagesService _categoriesService;

        public MassagesController
            (LoremDbContext data, IMassagesService categoriesService)
        {
            this._data = data;
            this._categoriesService = categoriesService;
        }
        
        [Authorize]
        public IActionResult All
            ([FromQuery] AllCategoriesQueryServiceModel queryModel) =>
            this.View(_categoriesService
                .All(queryModel.Id, queryModel.Name, queryModel.CurrentPage));

        [Authorize]
        public IActionResult Details
            ([FromQuery] MassageDetailsQueryModel queryModel)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == queryModel.MassageId);

            if (String.IsNullOrEmpty(queryModel.MassageId) || massage == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == queryModel.CategoryId);

            if (String.IsNullOrEmpty(queryModel.CategoryId) || category == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var massageDetailsModel = new MassageDetailsViewModel()
            {
                Id = massage.Id,
                CategoryId = queryModel.CategoryId,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price,
                Name = massage.Name
            };

            return this.View(massageDetailsModel);
        }

        [Authorize]
        public IActionResult AvailableMassages
            ([FromQuery] AvailableMassagesQueryViewModel queryModel)
        {
            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == queryModel.CategoryId);

            if (String.IsNullOrEmpty(queryModel.CategoryId) || category == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (!this._data.Massages.Any(m => m.CategoryId == queryModel.CategoryId))
            {
                this.ModelState
                    .AddModelError(String.Empty, NoMassagesFoundUnderCategory);

                return this.View(new AvailableMassagesQueryViewModel() {Massages = null});
            }

            var availableMassagesModel = this._data
                .Massages
                .Where(m => m.CategoryId == queryModel.CategoryId)
                .Skip((queryModel.CurrentPage - 1) * CategoriesPerPage)
                .Take(CategoriesPerPage)
                .Select(m => new MassageListingViewModel()
                {
                    Id = m.Id, Name = m.Name, ImageUrl = m.ImageUrl, ShortDescription = m.ShortDescription
                })
                .ToList();

            var totalMassages = this._data.Massages.Where(m => m.CategoryId == queryModel.CategoryId).Count();

            return this.View(new AvailableMassagesQueryViewModel()
            {
                Massages = availableMassagesModel,
                CategoryId = queryModel.CategoryId,
                MasseurId = queryModel.MasseurId,
                CurrentPage = queryModel.CurrentPage,
                MaxPage = Math.Ceiling
                    (totalMassages * 1.00 / MassagesPerPage * 1.00)
            });
        }

        [Authorize]
        public IActionResult AvailableMassageDetails
            ([FromQuery] AvailableMassageDetailsQueryModel queryModel)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == queryModel.MassageId);

            if (String.IsNullOrEmpty(queryModel.MassageId) || massage == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var masseur = this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == queryModel.MasseurId);

            if (String.IsNullOrEmpty(queryModel.MasseurId) || masseur == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var massageDetailsModel = new MassageDetailsViewModel()
            {
                Id = massage.Id,
                MasseurId = queryModel.MasseurId,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price,
                Name = massage.Name
            };

            return this.View("Details", massageDetailsModel);
        }
    }
}