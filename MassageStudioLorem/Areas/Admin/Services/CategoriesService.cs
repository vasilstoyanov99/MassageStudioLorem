namespace MassageStudioLorem.Areas.Admin.Services
{
    using Data;
    using Data.Models;
    using Ganss.XSS;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly LoremDbContext _data;

        public CategoriesService(LoremDbContext data) => this._data = data;

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
    }
}
