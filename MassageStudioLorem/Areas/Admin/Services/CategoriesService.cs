namespace MassageStudioLorem.Areas.Admin.Services
{
    using Data;
    using Data.Models;
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

            var category = new Category() {Name = name};
            this._data.Categories.Add(category);
            this._data.SaveChanges();

            return true;
        }
    }
}
