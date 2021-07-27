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
    using Models.Masseurs;
    using MassageListingViewModel = Models.Categories.MassageListingViewModel;

    public class CategoriesController : Controller
    {
        private readonly LoremDbContext _data;

        public CategoriesController(LoremDbContext data) =>
            this._data = data;

        public IActionResult All([FromQuery]
            AllCategoriesQueryModel query)
        {
            var totalCategories = this._data.Categories.Count();

            if (query.CurrentPage > totalCategories
                || query.CurrentPage < 1)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var categoryWithMassages = 
                this._data
                    .Categories
                    .Skip((query.CurrentPage - 1) * CategoriesPerPage)
                    .Take(CategoriesPerPage)
                    .Include(c => c.Massages)
                    .Select(c => new AllCategoriesQueryModel()
                    {
                        Id = c.Id,
                        Name = c.Name,
                        CurrentPage = query.CurrentPage,
                        Massages = c.Massages
                            .Select(m => new MassageListingViewModel()
                            {
                                Id = m.Id,
                                ImageUrl = m.ImageUrl,
                                ShortDescription = m.ShortDescription,
                                Price = m.Price,
                                Name = m.Name
                            })
                            .ToList()
                    })
                    .ToList();

            categoryWithMassages[0].TotalCategories = totalCategories;

            return this.View(categoryWithMassages[0]);
        }

        public IActionResult Details(string massageId, string categoryId)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

            if (String.IsNullOrEmpty(massageId) || massage == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

            if (String.IsNullOrEmpty(categoryId) || category == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var massageViewModel = new MassageListingViewModel()
            {
                Id = massage.Id,
                CategoryId = categoryId,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price,
                Name = massage.Name
            };

            return this.View(massageViewModel);
        }

        public IActionResult AvailableMassages([FromQuery] SortedMassagesQueryModel query)
        {
            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == query.CategoryId);

            if (String.IsNullOrEmpty(query.CategoryId) || category == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            if (!this._data.Massages.Any(m => m.CategoryId == query.CategoryId))
            {
                this.ModelState
                    .AddModelError(String.Empty, NoMassagesFoundUnderCategory);

                return this.View(new SortedMassagesQueryModel()
                {
                    Massages = null
                });
            }

            var sortedMassagesModel = this._data
                .Massages
                .Where(m => m.CategoryId == query.CategoryId)
                .Skip((query.CurrentPage - 1) * CategoriesPerPage)
                .Take(CategoriesPerPage)
                .Select(m => new SortedMassageListingViewModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    LongDescription = m.LongDescription,
                    ImageUrl = m.ImageUrl,
                    Price = m.Price,
                    ShortDescription = m.ShortDescription
                })
                .ToList();

            var totalMassages = this._data.Massages.Where(m => m.CategoryId == query.CategoryId).Count();

            return this.View(new SortedMassagesQueryModel()
            {
                Massages = sortedMassagesModel,
                CategoryId = query.CategoryId,
                MasseurId = query.MasseurId,
                CurrentPage = query.CurrentPage,
                MaxPage = Math.Ceiling
                    (totalMassages * 1.00 / MassagesPerPage * 1.00)
            });
        }
    }
}