namespace MassageStudioLorem.Controllers
{
    using Data;
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

        public IActionResult Index()
        {
            var categoriesWithMassages = 
                this._data
                    .Categories
                    .Include(x => x.Massages)
                    .Select(c => new AllCategoriesViewModel()
                    {
                        Name = c.Name,
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

            return View(categoriesWithMassages);
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
