namespace MassageStudioLorem.Controllers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    using Data;
    using Global;
    using Models.Categories;

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
                    .Skip((query.CurrentPage - 1) * 
                          GlobalConstants.Paging.CategoriesPerPage)
                    .Take(GlobalConstants.Paging.CategoriesPerPage)
                    .Include(c => c.Massages)
                    .Select(c => new AllCategoriesQueryModel()
                    {
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

            return View(categoryWithMassages[0]);
        }

        public IActionResult Details(string id)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == id);

            if (String.IsNullOrEmpty(id) || massage is null)
            {
                return RedirectToAction(nameof(this.All));
            }

            var massageViewModel = new MassageListingViewModel()
            {
                Id = massage.Id,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price,
                Name = massage.Name
            };

            return View(massageViewModel);
        }
    }
}