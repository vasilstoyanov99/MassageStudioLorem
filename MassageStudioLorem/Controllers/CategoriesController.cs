namespace MassageStudioLorem.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Paging;
    using Models.Categories;
    using Models.Massages;

    public class CategoriesController : Controller
    {
        private readonly LoremDbContext _data;

        public CategoriesController(LoremDbContext data) =>
            this._data = data;

        public IActionResult All([FromQuery]
            AllCategoriesQueryViewModel queryModel)
        {
            var totalCategories = this._data.Categories.Count();

            if (queryModel.CurrentPage > totalCategories
                || queryModel.CurrentPage < 1)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var allCategoriesModel = 
                this._data
                    .Categories
                    .Skip((queryModel.CurrentPage - 1) * CategoriesPerPage)
                    .Take(CategoriesPerPage)
                    .Include(c => c.Massages)
                    .Select(c => new AllCategoriesQueryViewModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CurrentPage = queryModel.CurrentPage,
                        Massages = c.Massages
                            .Select(m => new MassageListingViewModel()
                            {
                                Id = m.Id,
                                ImageUrl = m.ImageUrl,
                                ShortDescription = m.ShortDescription,
                                Name = m.Name
                            })
                            .ToList()
                    })
                    .ToList();

            allCategoriesModel[0].TotalCategories = totalCategories;

            return this.View(allCategoriesModel[0]);
        }

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

                return this.View(new AvailableMassagesQueryViewModel()
                {
                    Massages = null
                });
            }

            var availableMassagesModel = this._data
                .Massages
                .Where(m => m.CategoryId == queryModel.CategoryId)
                .Skip((queryModel.CurrentPage - 1) * CategoriesPerPage)
                .Take(CategoriesPerPage)
                .Select(m => new MassageListingViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImageUrl = m.ImageUrl,
                    ShortDescription = m.ShortDescription
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