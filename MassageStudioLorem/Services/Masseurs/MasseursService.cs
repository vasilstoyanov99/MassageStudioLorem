namespace MassageStudioLorem.Services.Masseurs
{
    using System.Linq;
    using Data;
    using Data.Models;
    using Ganss.XSS;
    using MassageStudioLorem.Models.Masseurs;
    using Microsoft.AspNetCore.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Global.GlobalConstants.Paging;
    using static Areas.Masseur.MasseurConstants;


    public class MasseursService : IMasseursService
    {
        private readonly LoremDbContext _data;
        private readonly UserManager<IdentityUser> _userManager;

        public MasseursService(LoremDbContext data,
            UserManager<IdentityUser> userManager)
        {
            this._data = data;
            this._userManager = userManager;
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

            var user = this._data.Users.FirstOrDefault(u => u.Id == userId);

            AssignUserToMasseurRole(user);

            this._data.Masseurs.Add(masseur);
            var client = this._data.Clients
                .FirstOrDefault(c => c.UserId == userId);
            this._data.Clients.Remove(client);
            this._data.SaveChanges();
        }

        public AllMasseursQueryServiceModel GetAllMasseurs(int currentPage)
        {
            var masseursQuery = this._data.Masseurs.AsQueryable();

            if (!masseursQuery.Any())
                return new AllMasseursQueryServiceModel()
                    { Masseurs = null };

            var totalMasseurs = masseursQuery.Count();

            if (currentPage > totalMasseurs || currentPage < 1)
                currentPage = CurrentPageStart;

            var allMasseursModel = new AllMasseursQueryServiceModel()
            {
                CurrentPage = currentPage,
                MaxPage = this.GetMaxPage(totalMasseurs),
                Masseurs = this.GetAllMasseursModels(masseursQuery
                    .Skip((currentPage - 1) * ThreeCardsPerPage)
                    .Take(ThreeCardsPerPage))
            };

            return allMasseursModel;
        }

        public MasseurDetailsServiceModel GetMasseurDetails(MasseurDetailsQueryModel queryModel)
        {
            var masseur = this.ReturnMasseurIfMasseurDetailsQueryDataIsValid
                (queryModel);

            return this.GetMasseurDetailsModel(masseur, queryModel);
        }

        public AvailableMasseursQueryServiceModel GetAvailableMasseurs
            (AvailableMasseursQueryServiceModel queryModel)
        {
            if (!this.IsAvailableMasseursQueryDataValid
                (queryModel.CategoryId, queryModel.MassageId))
                return null;

            var masseursQuery = this._data.Masseurs.AsQueryable();

            if (!masseursQuery
                .Any(m => m.CategoryId == queryModel.CategoryId))
            {
                return new AvailableMasseursQueryServiceModel() {Masseurs = null};
            }

            var totalMasseurs = masseursQuery
                .Count(m => m.CategoryId == queryModel.CategoryId);

            if (queryModel.CurrentPage > totalMasseurs || 
                queryModel.CurrentPage < 1)
                queryModel.CurrentPage = CurrentPageStart;

            var availableMasseursModel = new AvailableMasseursQueryServiceModel()
            {
                CurrentPage = queryModel.CurrentPage,
                MaxPage = this.GetMaxPage(totalMasseurs),
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
            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur, masseurId))
                return null;

            return this.GetMasseurDetailsModel(masseur);
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

        private bool CheckIfNull(object massage, string id)
            => String.IsNullOrEmpty(id) || massage == null;

        private double GetMaxPage(int count)
            => Math.Ceiling
                (count * 1.00 / ThreeCardsPerPage * 1.00);

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

        private Massage GetMassageFromDB(string massageId) =>
            this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

        public Category GetCategoryFromDB(string categoryId) =>
            this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

        private Masseur ReturnMasseurIfMasseurDetailsQueryDataIsValid
            (MasseurDetailsQueryModel queryModel)
        {
            var massage = this.GetMassageFromDB(queryModel.MassageId);

            if (this.CheckIfNull(massage, queryModel.MassageId))
                return null;

            var category = this.GetCategoryFromDB(queryModel.CategoryId);

            if (this.CheckIfNull(category, queryModel.CategoryId))
                return null;

            var masseur = this.GetMasseurFromDB(queryModel.MasseurId);

            if (this.CheckIfNull(masseur, queryModel.MasseurId))
                return null;

            if (!this._data.Massages.Any
                    (m => m.CategoryId == queryModel.CategoryId) ||
                !this._data.Masseurs.Any
                    (m => m.CategoryId == queryModel.CategoryId))
                return null;

            return masseur;
        }

        private bool IsAvailableMasseursQueryDataValid
            (string categoryId, string massageId)
        {
            var massage = this.GetMassageFromDB(massageId);

            if (this.CheckIfNull(massage, massageId))
                return false;

            var category = this.GetCategoryFromDB(categoryId);

            if (this.CheckIfNull(category, categoryId))
                return false;

            return true;
        }

        private void AssignUserToMasseurRole(IdentityUser user)
        {
            Task
                .Run(async () =>
                {
                    await this._userManager.AddToRoleAsync(user, MasseurRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }
    }
}