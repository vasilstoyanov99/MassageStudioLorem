namespace MassageStudioLorem.Services.Masseurs
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Ganss.XSS;
    using MassageStudioLorem.Models.Masseurs;
    using Models;
    using System;
    using System.Collections.Generic;

    using static Global.GlobalConstants.Paging;


    public class MasseursService : IMasseursService
    {
        private readonly LoremDbContext _data;
        private readonly CommonService _commonService;

        public MasseursService(LoremDbContext data, CommonService commonService)
        {
            this._data = data;
            this._commonService = commonService;
        }

        public bool IsUserMasseur(string userId) =>
            this._data.Masseurs.Any(m => m.UserId == userId);

        public IEnumerable<MassageCategoryServiceModel> GetCategories()
            => this._data
                ?.Categories
                .Select(c => new MassageCategoryServiceModel()
                    { Id = c.Id, Name = c.Name })
                .ToList();

        public void RegisterNewMasseur(BecomeMasseurFormModel masseurModel,
            string userId)
        {
            var htmlSanitizer = new HtmlSanitizer();

            var masseur = new Masseur()
            {
                FullName = htmlSanitizer.Sanitize(masseurModel.FullName),
                ProfileImageUrl = htmlSanitizer
                    .Sanitize(masseurModel.ProfileImageUrl),
                Description = htmlSanitizer.Sanitize(masseurModel.Description),
                CategoryId = masseurModel.CategoryId,
                Gender = masseurModel.Gender,
                UserId = userId
            };

            this._data.Masseurs.Add(masseur);
            this._data.SaveChanges();
        }

        public AllMasseursQueryServiceModel GetAllMasseurs(int currentPage)
        {
            var masseursQuery = this._data.Masseurs.AsQueryable();

            if (!masseursQuery.Any())
                return new AllMasseursQueryServiceModel()
                    { Masseurs = null };

            var totalMasseurs = masseursQuery.Count();

            var allMasseursModel = new AllMasseursQueryServiceModel()
            {
                CurrentPage = currentPage,
                MaxPage = this._commonService.GetMaxPage(totalMasseurs),
                Masseurs = GetAllMasseursModels(masseursQuery
                    .Skip((currentPage - 1) * ThreeCardsPerPage)
                    .Take(ThreeCardsPerPage))
            };

            this.CheckAndFixCurrentPage(allMasseursModel, totalMasseurs);

            return allMasseursModel;
        }

        public MasseurDetailsServiceModel GetMasseurDetails(MasseurDetailsQueryModel queryModel)
        {
            var massage = this._commonService
                .GetMassageFromDB(queryModel.MassageId);

            if (this._commonService.CheckIfNull(massage, queryModel.MassageId))
                return null;

            var category = this._commonService
                .GetCategoryFromDB(queryModel.CategoryId);

            if (this._commonService.CheckIfNull(category, queryModel.CategoryId))
                return null;

            var masseur = this._commonService
                .GetMasseurFromDB(queryModel.MasseurId);

            if (this._commonService.CheckIfNull(masseur, queryModel.MasseurId))
                return null;

            return GetMasseurDetailsModel(masseur, queryModel);
        }

        public AvailableMasseursQueryServiceModel GetAvailableMasseurs(AvailableMasseursQueryServiceModel queryModel)
        {
            var massage = this._commonService
                .GetMassageFromDB(queryModel.MassageId);

            if (this._commonService.CheckIfNull(massage, queryModel.MassageId))
                return null;

            var category = this._commonService
                .GetCategoryFromDB(queryModel.CategoryId);

            if (this._commonService.CheckIfNull(category, queryModel.CategoryId))
                return null;

            var masseursQuery = this._data.Masseurs.AsQueryable();

            if (!masseursQuery
                .Any(m => m.CategoryId == queryModel.CategoryId))
            {
                return new AvailableMasseursQueryServiceModel() {Masseurs = null};
            }

            var totalMasseurs = masseursQuery
                .Count(m => m.CategoryId == queryModel.CategoryId);

            this.CheckAndFixCurrentPage(queryModel, totalMasseurs);

            var availableMasseursModel = new AvailableMasseursQueryServiceModel()
            {
                CurrentPage = queryModel.CurrentPage,
                MaxPage = this._commonService.GetMaxPage(totalMasseurs),
                MassageId = queryModel.MassageId,
                CategoryId = queryModel.CategoryId,
                Masseurs = this.GetAvailableMasseursModels(masseursQuery
                    .Skip((queryModel.CurrentPage - 1) * ThreeCardsPerPage)
                    .Take(ThreeCardsPerPage)
                    .Where(c => c.CategoryId == queryModel.CategoryId))
            };

            return availableMasseursModel;
        }

        public MasseurDetailsServiceModel GetAvailableMasseurDetails
            (string masseurId)
        {
            var masseur = this._commonService
                .GetMasseurFromDB(masseurId);

            if (this._commonService.CheckIfNull(masseur, masseurId))
                return null;

            return GetMasseurDetailsModel(masseur);
        }

        private IEnumerable<MasseurDetailsServiceModel>
            GetAllMasseursModels(IQueryable<Masseur> masseursQuery)
            => masseursQuery
                .Select(m => new MasseurDetailsServiceModel()
                {
                    Id = m.UserId,
                    ProfileImageUrl = m.ProfileImageUrl,
                    FullName = m.FullName,
                    RatersCount = m.RatersCount,
                    CategoryId = m.CategoryId,
                    Rating = m.Rating
                })
                .ToList();

        private MasseurDetailsServiceModel
            GetMasseurDetailsModel(Masseur masseur,
                MasseurDetailsQueryModel queryModel)
        => new()
        {
            Id = masseur.UserId,
            CategoryId = queryModel.CategoryId,
            MassageId = queryModel.MassageId,
            Description = masseur.Description,
            PhoneNumber = this.GetMasseurPhoneNumber(queryModel.MasseurId),
            FullName = masseur.FullName,
            ProfileImageUrl = masseur.ProfileImageUrl,
            RatersCount = masseur.RatersCount,
            Rating = masseur.Rating
        };

        private IEnumerable<AvailableMasseurListingServiceModel>
            GetAvailableMasseursModels (IQueryable<Masseur> masseursQuery)
        => masseursQuery
            .Select(m => new AvailableMasseurListingServiceModel()
            {
                Id = m.UserId,
                ProfileImageUrl = m.ProfileImageUrl,
                FullName = m.FullName,
                RatersCount = m.RatersCount,
                Rating = m.Rating,
            })
            .ToList();

        private MasseurDetailsServiceModel GetMasseurDetailsModel(Masseur masseur)
        => new()
        {
            Id = masseur.UserId,
            Description = masseur.Description,
            PhoneNumber = this.GetMasseurPhoneNumber(masseur.UserId),
            FullName = masseur.FullName,
            ProfileImageUrl = masseur.ProfileImageUrl,
            RatersCount = masseur.RatersCount,
            CategoryId = masseur.CategoryId,
            Rating = masseur.Rating
        };

        private string GetMasseurPhoneNumber(string masseurId) =>
            this._data.Users.FirstOrDefault(u => u.Id == masseurId)?.PhoneNumber;

        private void CheckAndFixCurrentPage
            (object queryModel, int count)
        {
            if (queryModel.CurrentPage > count || queryModel.CurrentPage < 1)
                queryModel.CurrentPage = CurrentPageStart;
        }
    }
}