namespace MassageStudioLorem.Services.Massages
{
    using Data;
    using Data.Models;
    using Ganss.XSS;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Shared;
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

            if (totalCategories == 0)
                return null;

            if (currentPage > totalCategories || currentPage < 1)
                currentPage = CurrentPageStart;

            var allCategoriesModel = GetAllCategoriesWithMassagesModel(categoriesQuery
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

            if (CheckIfNull(massage))
                return null;

            var category = this.GetCategoryFromDB(categoryId);

            if (CheckIfNull(category))
                return null;

            return GetMassageDetailsModel(massage);
        }

        public AvailableMassagesQueryServiceModel GetAvailableMassages
            (string masseurId, string categoryId, int currentPage)
        {
            if (!this.IsAvailableMassagesQueryDataValid(masseurId, categoryId))
                return null;

            var massagesQuery = this._data.Massages.AsQueryable();

            var totalMassages = massagesQuery
                .Count(m => m.CategoryId == categoryId);

            if (currentPage > totalMassages || currentPage < 1)
                currentPage = CurrentPageStart;

            return GetAvailableMassagesModel
                (categoryId, masseurId, currentPage, totalMassages, massagesQuery);
        }

        public MassageDetailsServiceModel GetAvailableMassageDetails
            (string massageId, string masseurId)
        {
            var massage = this.ReturnMassageIfAvailableMassageQueryDataIsValid
                (massageId, masseurId);

            if (CheckIfNull(massage))
                return null;

            return GetAvailableMassagesDetailsModel(massage, masseurId);
        }

        public DeleteEntityServiceModel GetMassageDataForDelete(string massageId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (CheckIfNull(massageId))
                return null;

            var category = this.GetCategoryFromDB(massage.CategoryId);

            if (CheckIfNull(category))
                return null;

            var massageData = new DeleteEntityServiceModel()
            {
                Id = massage.Id, 
                Name = massage.Name,
                CategoryName = category.Name,
                EntityName = "Massage"
            };

            return massageData;
        }

        public bool CheckIfMassageDeletedSuccessfully(string massageId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (CheckIfNull(massage))
                return false;

            var appointments = this._data.Appointments
                .Where(a => a.MassageId == massageId)
                ?.ToList();

            foreach (var appointment in appointments) 
                this._data.Appointments.Remove(appointment);

            this._data.SaveChanges();

            this._data.Massages.Remove(massage);
            this._data.SaveChanges();

            var category = this.GetCategoryFromDB(massage.CategoryId);
            category.Massages.Remove(massage);
            this._data.SaveChanges();

            return true;
        }

        public EditMassageFormModel GetMassageDataForEdit(string massageId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (CheckIfNull(massage))
                return null;

            var massageEditModel = new EditMassageFormModel()
            {
                Id = massage.Id,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                ShortDescription = massage.ShortDescription,
                Name = massage.Name,
                Price = massage.Price
            };

            return massageEditModel;
        }

        public EditMassageDetailsServiceModel GetMassageDetailsForEdit
            (string massageId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (CheckIfNull(massage))
                return null;

            var editMassageDetailsModel = new EditMassageDetailsServiceModel()
            {
                Id = massage.Id,
                Name = massage.Name,
                ImageUrl = massage.ImageUrl,
                LongDescription = massage.LongDescription,
                Price = massage.Price
            };

            return editMassageDetailsModel;
        }

        public bool CheckIfMassageEditedSuccessfully
            (EditMassageFormModel editMassageModel)
        {
            var massage = this.GetMassageFromDB(editMassageModel.Id);

            if (CheckIfNull(massage))
                return false;

            var htmlSanitizer = new HtmlSanitizer();

            massage.LongDescription = htmlSanitizer
                .Sanitize(editMassageModel.LongDescription);
            massage.ShortDescription = htmlSanitizer
                .Sanitize(editMassageModel.ShortDescription);
            massage.Price = editMassageModel.Price;
            massage.Name = htmlSanitizer.Sanitize(editMassageModel.Name);
            massage.ImageUrl = htmlSanitizer.Sanitize(editMassageModel.ImageUrl);
            this._data.SaveChanges();

            return true;
        }

        private static bool CheckIfNull(object obj)
            => obj == null;

        private bool IsAvailableMassagesQueryDataValid
            (string masseurId, string categoryId)
        {
            var masseur = this.GetMasseurFromDB(masseurId);

            if (CheckIfNull(masseur))
                return false;

            var category = this.GetCategoryFromDB(categoryId);

            if (CheckIfNull(category))
                return false;

            if (masseur.CategoryId != categoryId ||
                !this._data.Massages.Any(m => m.CategoryId == categoryId))
                return false;

            return true;
        }

        private static MassageDetailsServiceModel GetMassageDetailsModel
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

        private static AllCategoriesQueryServiceModel
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
                        .OrderBy(m => m.Name)
                        .ToList()
                })
                .OrderBy(m => m.Name)
                .ToList()
                .FirstOrDefault();

        private static AvailableMassagesQueryServiceModel GetAvailableMassagesModel
        (string categoryId, 
         string masseurId, 
         int currentPage,
         int totalMassages,
         IQueryable<Massage> massagesQuery)
        => new()
        {
            CategoryId = categoryId,
            MasseurId = masseurId,
            CurrentPage = currentPage,
            MaxPage = Math
                .Ceiling(totalMassages * 1.00 / ThreeCardsPerPage * 1.00),
            Massages = GetAvailableMassagesModels
            (massagesQuery
                .Where(m => m.CategoryId == categoryId)
                .Skip((currentPage - 1) * CategoriesPerPage)
                .Take(CategoriesPerPage))
        };

        private static IEnumerable<MassageListingServiceModel>
            GetAvailableMassagesModels
            (IQueryable<Massage> massagesQuery) =>
            massagesQuery
                .Select(m => new MassageListingServiceModel()
                {
                    Id = m.Id,
                    Name = m.Name,
                    ImageUrl = m.ImageUrl,
                    ShortDescription = m.ShortDescription
                })
                .OrderBy(m => m.Name)
                .ToList();

        private static MassageDetailsServiceModel GetAvailableMassagesDetailsModel
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

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.Id == masseurId);

        private Massage GetMassageFromDB(string massageId) =>
            this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

        private Category GetCategoryFromDB(string categoryId) =>
            this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

        private Massage ReturnMassageIfAvailableMassageQueryDataIsValid
            (string massageId, string masseurId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (CheckIfNull(massage))
                return null;

            var masseur = this.GetMasseurFromDB(masseurId);

            if (CheckIfNull(masseur))
                return null;

            if (masseur.CategoryId != massage.CategoryId)
                return null;

            return massage;
        }
    }
}