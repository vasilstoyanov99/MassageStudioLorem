namespace MassageStudioLorem.Services.Massages
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using static Global.GlobalConstants.Paging;

    public class MassagesService : IMassagesService
    {
        private readonly LoremDbContext _data;

        public MassagesService(LoremDbContext data) => this._data = data;

        public AllCategoriesQueryServiceModel All
            (string id, string name, int currentPage)
        {
            var categoriesQuery = this._data.Categories.AsQueryable();

            var totalCategories = categoriesQuery.Count();

            if (currentPage > totalCategories || currentPage < 1)
            {
                currentPage = CurrentPageStart;
            }

            var allCategoriesModel = this.GetCategoriesWithMassages(categoriesQuery
                .Skip((currentPage - 1) * CategoriesPerPage)
                .Take(CategoriesPerPage)
                .Include(c => c.Massages));

            allCategoriesModel.CurrentPage = currentPage;
            allCategoriesModel.TotalCategories = totalCategories;

            return allCategoriesModel;
        }

        private AllCategoriesQueryServiceModel
            GetCategoriesWithMassages(IQueryable<Category> categoriesQuery) =>
            categoriesQuery
                .Select(c => new AllCategoriesQueryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Massages = c.Massages
                        .Select(m => new MassageListingServiceModel()
                        {
                            Id = m.Id, ImageUrl = m.ImageUrl, ShortDescription = m.ShortDescription, Name = m.Name
                        })
                        .ToList()
                })
                .ToList()
                .FirstOrDefault();
    }
}