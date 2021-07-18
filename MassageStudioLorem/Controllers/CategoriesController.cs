namespace MassageStudioLorem.Controllers
{
    using Data;
    using Global;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.Categories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesController : Controller
    {
        private readonly LoremDbContext _data;

        public CategoriesController(LoremDbContext data) => this._data = data;

        public IActionResult All([FromQuery]
            AllCategoriesQueryModel query)
        {
            var totalCategories = this._data.Categories.Count();

            if (query.CurrentPage > totalCategories
                || query.CurrentPage < 1)
            {
                return this.RedirectToAction("All");
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
                return RedirectToAction("Index");
            }

            var massageViewModel = new MassageListingViewModel()
            {
                Id = massage.Id,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price
            };

            return View(massageViewModel);
        }
    }
}
