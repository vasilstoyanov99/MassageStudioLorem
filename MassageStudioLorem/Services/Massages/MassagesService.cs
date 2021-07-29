namespace MassageStudioLorem.Services.Massages
{
    using Data;
    using Data.Models;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Global.GlobalConstants.Paging;

    public class MassagesService : IMassagesService
    {
        private readonly LoremDbContext _data;
        public MassagesService(LoremDbContext data) => this._data = data;

        public AllCategoriesQueryServiceModel GetAllCategoriesWithMassages
            (string id, string name, int currentPage)
        {
            var categoriesQuery = this._data.Categories.AsQueryable();

            var totalCategories = categoriesQuery.Count();

            if (totalCategories == 0 || !this._data.Massages.Any())
                return null;

            if (currentPage > totalCategories || currentPage < 1)
                currentPage = CurrentPageStart;

            var allCategoriesModel = this.GetAllCategoriesWithMassagesModel(categoriesQuery
                .Skip((currentPage - 1) * CategoriesPerPage)
                .Take(CategoriesPerPage)
                .Include(c => c.Massages));

            allCategoriesModel.CurrentPage = currentPage;
            allCategoriesModel.TotalCategories = totalCategories;

            return allCategoriesModel;
        }

        public MassageDetailsServiceModel GetMassageDetails
            (string massageId, string categoryId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (this.CheckIfNull(massage, massageId))
                return null;

            var category = this.GetCategoryFromDB(categoryId);

            if (this.CheckIfNull(category, categoryId))
                return null;

            return this.GetMassageDetailsModel(massage);
        }

        public AvailableMassagesQueryServiceModel GetAvailableMassages
            (string masseurId, string categoryId, int currentPage)
        {
            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur, masseurId))
                return null;

            var category = this.GetCategoryFromDB(categoryId);

            if (this.CheckIfNull(category, categoryId))
                return null;

            if (!this._data.Masseurs.Any(m => m.CategoryId == categoryId))
                return null;

            var massagesQuery = this._data.Massages.AsQueryable();

            var totalMassages = massagesQuery?
                .Count(m => m.CategoryId == categoryId) ?? 0;

            return new AvailableMassagesQueryServiceModel()
            {
                CategoryId = categoryId,
                MasseurId = masseurId,
                CurrentPage = currentPage,
                MaxPage = Math
                    .Ceiling(totalMassages * 1.00 / ThreeCardsPerPage * 1.00),
                Massages = this.GetAvailableMassagesModels
                (massagesQuery
                    .Where(m => m.CategoryId == categoryId)
                    .Skip((currentPage - 1) * CategoriesPerPage)
                    .Take(CategoriesPerPage))
            };
        }

        public MassageDetailsServiceModel GetAvailableMassageDetails
            (string massageId, string masseurId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (this.CheckIfNull(massage, massageId))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur, masseurId))
                return null;

            return this.GetAvailableMassagesDetailsModel(massage, masseurId);
        }

        private MassageDetailsServiceModel GetMassageDetailsModel
            (Massage massage) =>
            new()
            {
                Id = massage.Id,
                CategoryId = massage.CategoryId,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price,
                Name = massage.Name
            };

        private AllCategoriesQueryServiceModel
            GetAllCategoriesWithMassagesModel
            (IQueryable<Category> categoriesQuery) =>
            categoriesQuery
                .Select(c => new AllCategoriesQueryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Massages = c.Massages
                        .Select(m => new MassageListingServiceModel()
                        {
                            Id = m.Id,
                            ImageUrl = m.ImageUrl,
                            ShortDescription = m.ShortDescription,
                            Name = m.Name
                        })
                        .ToList()
                })
                .ToList()
                .FirstOrDefault();

        private IEnumerable<MassageListingServiceModel> GetAvailableMassagesModels
            (IQueryable<Massage> massagesQuery) =>
            massagesQuery
                .Select(m => new MassageListingServiceModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImageUrl = m.ImageUrl,
                    ShortDescription = m.ShortDescription
                })
                .ToList();

        private MassageDetailsServiceModel GetAvailableMassagesDetailsModel
            (Massage massage, string masseurId) =>
            new()
            {
                Id = massage.Id,
                MasseurId = masseurId,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price,
                Name = massage.Name
            };

        private bool CheckIfNull(object massage, string id)
            => String.IsNullOrEmpty(id) || massage == null;

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

        private Massage GetMassageFromDB(string massageId) =>
            this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

        private Category GetCategoryFromDB(string categoryId) =>
            this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);
    }
}