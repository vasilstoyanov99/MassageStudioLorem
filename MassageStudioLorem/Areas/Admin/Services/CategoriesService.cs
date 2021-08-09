namespace MassageStudioLorem.Areas.Admin.Services
{
    using Data;
    using Data.Models;
    using Ganss.XSS;
    using MassageStudioLorem.Services.Massages;
    using MassageStudioLorem.Services.Massages.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly LoremDbContext _data;
        private readonly IMassagesService _massagesService;

        public CategoriesService(LoremDbContext data, IMassagesService massagesService)
        {
            this._data = data;
            this._massagesService = massagesService;
        }

        public bool CheckIfCategoryIsAddedSuccessfully(string name)
        {
            if (this._data.Categories.Any(c => c.Name == name))
                return false;

            var htmlSanitizer = new HtmlSanitizer();

            var category = new Category() {Name = htmlSanitizer.Sanitize(name)};
            this._data.Categories.Add(category);
            this._data.SaveChanges();

            return true;
        }

        public IEnumerable<CategoryServiceModel> GetAllCategories()
        {
            if (!this._data.Categories.Any())
                return null;

            var allCategoriesModels = this._data.Categories
                .Select(c => new CategoryServiceModel()
                {
                    Id = c.Id, 
                    Name = c.Name, 
                    IsEmpty = !c.Massages.Any()
                })
                .ToList();

            return allCategoriesModels;
        }

        public DeleteCategoryServiceModel GetCategoryDataForDelete
            (string categoryId)
        {
            var category = this.GetCategoryFromDB(categoryId);

            if (this.CheckIfNull(categoryId))
                return null;

            var deleteCategoryModel = new DeleteCategoryServiceModel
            {
                Masseurs = this._data
                    .Masseurs
                    .Where(m => m.CategoryId == categoryId)
                    .Select(m => m.FullName)
                    .ToList(),

                Massages = this._data
                    .Massages
                    .Where(m => m.CategoryId == categoryId)
                    .Select(m => m.Name)
                    .ToList(),

                Id = category.Id,
                Name = category.Name
            };

            return deleteCategoryModel;
        }

        public bool CheckIfCategoryDeletedSuccessfully(string categoryId)
        {
            var category = this.GetCategoryFromDB(categoryId);

            if (this.CheckIfNull(categoryId))
                return false;

            var masseur = this._data.Masseurs
                .FirstOrDefault(m => m.CategoryId == categoryId);

            if (!this.CheckIfNull(masseur)) 
                masseur.CategoryId = null;

            this._data.SaveChanges();

            var massageIds = this._data
                .Massages
                .Where(c => c.CategoryId == categoryId)
                .Select(m => new {m.Id})
                .ToList();

            if (massageIds.Any())
            {
                foreach (var massage in massageIds)
                {
                    this._massagesService
                        .CheckIfMassageDeletedSuccessfully(massage.Id);
                }
            }

            this._data.Categories.Remove(category);
            this._data.SaveChanges();

            return true;
        }

        private bool CheckIfNull(object obj)
            => obj == null;

        private Category GetCategoryFromDB(string categoryId) =>
            this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);
    }
}
