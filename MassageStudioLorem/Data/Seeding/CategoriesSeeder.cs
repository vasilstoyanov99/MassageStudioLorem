namespace MassageStudioLorem.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data;
    using Interfaces;
    using Models;

    public class CategoriesSeeder : ISeeder
    {
        public void Seed(LoremDbContext data, IServiceProvider serviceProvider)
        {
            if (!data.Categories.Any())
            {
                var allCategories = new List<Category>();
                var categoriesData = new CategoriesSeedData();
                foreach (var categoryName in CategoriesSeedData.Categories)
                {
                    var category = new Category() { Name = categoryName };
                    allCategories.Add(category);
                }

                data.Categories.AddRange(allCategories);
                data.SaveChanges();
            }
        }
    }
}
