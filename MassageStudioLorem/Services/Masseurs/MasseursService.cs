namespace MassageStudioLorem.Services.Masseurs
{
    using System.Linq;
    using Data;
    using Data.Enums;
    using Data.Models;
    using Ganss.XSS;
    using Massages.Models;
    using MassageStudioLorem.Models.Masseurs;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Query.Internal;
    using Models;
    using Shared;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Global.GlobalConstants.Paging;
    using static Areas.Masseur.MasseurConstants;
    using static Areas.Client.ClientConstants;


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

        public IEnumerable<MassageCategoryServiceModel> GetCategories()
            => this._data
                ?.Categories
                .Select(c => new MassageCategoryServiceModel()
                    { Id = c.Id, Name = c.Name })
                .ToList();

        public void RegisterNewMasseur
        (BecomeMasseurFormModel masseurModel, string userId)
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

            var user = this.GetUserFromDB(userId);

            this.AssignUserToMasseurRole(user);

            this._data.Masseurs.Add(masseur);
            var client = this._data.Clients
                .FirstOrDefault(c => c.UserId == userId);
            this._data.Clients.Remove(client);
            this._data.SaveChanges();
        }

        public AllMasseursQueryServiceModel GetAllMasseurs
            (int currentPage, Gender sorting)
        {
            var masseursQuery = this._data.Masseurs.AsQueryable();

            if (!masseursQuery.Any())
                return new AllMasseursQueryServiceModel()
                    { Masseurs = null };

            masseursQuery = sorting switch
            {
                Gender.Female or Gender.Male => masseursQuery
                    .Where(m => m.Gender == sorting),
                _ => masseursQuery
            };

            var totalMasseurs = masseursQuery?.Count() ?? 0;

            if (totalMasseurs <= 0)
                return null;

            if (currentPage > totalMasseurs || currentPage < 1)
                currentPage = CurrentPageStart;

            var allMasseursModel = new AllMasseursQueryServiceModel()
            {
                CurrentPage = currentPage,
                MaxPage = this.GetMaxPage(totalMasseurs),
                Sorting = sorting,
                Masseurs = this.GetAllMasseursModels(masseursQuery
                    .Skip((currentPage - 1) * ThreeCardsPerPage)
                    .Take(ThreeCardsPerPage))
            };

            return allMasseursModel;
        }

        public AvailableMasseurDetailsServiceModel GetMasseurDetails
            (MasseurDetailsQueryModel queryModel)
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
                return new AvailableMasseursQueryServiceModel() {Masseurs = null};

            masseursQuery = queryModel.Sorting switch
            {
                Gender.Female or Gender.Male => masseursQuery
                    .Where(m => m.Gender == queryModel.Sorting),
                _ => masseursQuery
            };

            var totalMasseurs = masseursQuery
                ?.Count(m => m.CategoryId == queryModel.CategoryId) ?? 0;

            if (totalMasseurs <= 0)
                return null;

            if (queryModel.CurrentPage > totalMasseurs || 
                queryModel.CurrentPage < 1)
                queryModel.CurrentPage = CurrentPageStart;

            var availableMasseursModel = new AvailableMasseursQueryServiceModel()
            {
                CurrentPage = queryModel.CurrentPage,
                MaxPage = this.GetMaxPage(totalMasseurs),
                MassageId = queryModel.MassageId,
                CategoryId = queryModel.CategoryId,
                Sorting = queryModel.Sorting,
                Masseurs = this.GetAvailableMasseursModels(masseursQuery
                    .Skip((queryModel.CurrentPage - 1) * ThreeCardsPerPage)
                    .Take(ThreeCardsPerPage)
                    .Where(c => c.CategoryId == queryModel.CategoryId))
            };

            return availableMasseursModel;
        }

        public AvailableMasseurDetailsServiceModel GetAvailableMasseurDetails
            (string masseurId)
        {
            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur))
                return null;

            return this.GetAvailableMasseurDetailsModel(masseur);
        }

        public Category GetCategoryFromDB(string categoryId) =>
            this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

        public IdentityUser GetUserFromDB(string userId) =>
            this._data.Users.FirstOrDefault(u => u.Id == userId);

        public EditMasseurFormModel GetMasseurDataForEdit(string masseurId)
        {
            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur))
                return null;

            var currentCategoryName = this.GetCategoryName(masseur.CategoryId);

            var masseurEditModel = new EditMasseurFormModel()
            {
                Id = masseur.Id,
                CurrentCategoryName = currentCategoryName,
                Description = masseur.Description,
                FullName = masseur.FullName,
                Gender = masseur.Gender,
                ProfileImageUrl = masseur.ProfileImageUrl,
                Categories = this.GetCategories()
            };

            return masseurEditModel;
        }

        public bool CheckIfMasseurEditedSuccessfully
            (EditMasseurFormModel editMasseurModel)
        {
            var masseur = this.GetMasseurFromDB(editMasseurModel.Id);

            if (this.CheckIfNull(masseur))
                return false;

            var htmlSanitizer = new HtmlSanitizer();

            masseur.CategoryId = editMasseurModel.CategoryId;
            masseur.Description = htmlSanitizer
                .Sanitize(editMasseurModel.Description);
            masseur.FullName = htmlSanitizer.Sanitize(editMasseurModel.FullName);
            masseur.Gender = editMasseurModel.Gender;
            masseur.ProfileImageUrl = htmlSanitizer
                .Sanitize(editMasseurModel.ProfileImageUrl);

            this._data.SaveChanges();

            return true;
        }

        public DeleteEntityServiceModel GetMasseurDataForDelete(string masseurId)
        {
            var masseur = this.GetMasseurFromDB(masseurId);

            if (this.CheckIfNull(masseur))
                return null;

            var masseurData = new DeleteEntityServiceModel()
            {
                Id = masseur.Id,
                Name = masseur.FullName,
                CategoryName = this.GetCategoryName(masseur.CategoryId),
                EntityName = "Masseur",
            };

            return masseurData;
        }

        public bool CheckIfMasseurDeletedSuccessfully(string masseurId)
        {
            var masseur = this._data
                .Masseurs
                .Include(m => m.Reviews)
                .Include(m => m.WorkSchedule)
                .FirstOrDefault(m => m.Id == masseurId);

            if (this.CheckIfNull(masseur))
                return false;

            var reviews = masseur?.Reviews.ToList();

            foreach (var review in reviews)
            {
                this._data.Reviews.Remove(review);
            }

            this._data.SaveChanges();

            var workSchedule = masseur?.WorkSchedule.ToList();

            foreach (var appointment in workSchedule)
            {
                this._data.Appointments.Remove(appointment);
            }

            var user = this.GetUserFromDB(masseur.UserId);

            this.RemoveUserFromMasseurRole(user);
            this._data.Masseurs.Remove(masseur);
            this._data.SaveChanges();
            this._data.Users.Remove(user);
            this._data.SaveChanges();

            return true;
        }

        private IEnumerable<MasseurListingServiceModel>
            GetAllMasseursModels(IQueryable<Masseur> masseursQuery)
            => masseursQuery
                .Select(m => new MasseurListingServiceModel()
                {
                    Id = m.Id,
                    ProfileImageUrl = m.ProfileImageUrl,
                    FullName = m.FullName,
                    CategoryId = m.CategoryId
                })
                .OrderBy(m => m.FullName)
                .ToList();

        private AvailableMasseurDetailsServiceModel
            GetMasseurDetailsModel(Masseur masseur,
                MasseurDetailsQueryModel queryModel)
        => new()
        {
            Id = masseur.Id,
            CategoryId = queryModel.CategoryId,
            MassageId = queryModel.MassageId,
            Description = masseur.Description,
            PhoneNumber = this.GetMasseurPhoneNumber(masseur.UserId),
            FullName = masseur.FullName,
            ProfileImageUrl = masseur.ProfileImageUrl
        };

        private IEnumerable<AvailableMasseurListingServiceModel>
            GetAvailableMasseursModels (IQueryable<Masseur> masseursQuery)
        => masseursQuery
            .Select(m => new AvailableMasseurListingServiceModel()
            {
                Id = m.Id,
                ProfileImageUrl = m.ProfileImageUrl,
                FullName = m.FullName,
            })
            .OrderBy(m => m.FullName)
            .ToList();

        private AvailableMasseurDetailsServiceModel
            GetAvailableMasseurDetailsModel(Masseur masseur)
        => new()
        {
            Id = masseur.Id,
            Description = masseur.Description,
            PhoneNumber = this.GetMasseurPhoneNumber(masseur.UserId),
            FullName = masseur.FullName,
            ProfileImageUrl = masseur.ProfileImageUrl,
            CategoryId = masseur.CategoryId,
        };

        private string GetMasseurPhoneNumber(string userId) =>
            this._data.Users.FirstOrDefault(u => u.Id == userId)?.PhoneNumber;

        private bool CheckIfNull(object obj)
            => obj == null;

        private double GetMaxPage(int count)
            => Math.Ceiling
                (count * 1.00 / ThreeCardsPerPage * 1.00);

        private Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.Id == masseurId);

        private Massage GetMassageFromDB(string massageId) =>
            this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

        private Masseur ReturnMasseurIfMasseurDetailsQueryDataIsValid
            (MasseurDetailsQueryModel queryModel)
        {
            var massage = this.GetMassageFromDB(queryModel.MassageId);

            if (this.CheckIfNull(massage))
                return null;

            var category = this.GetCategoryFromDB(queryModel.CategoryId);

            if (this.CheckIfNull(category))
                return null;

            var masseur = this.GetMasseurFromDB(queryModel.MasseurId);

            if (this.CheckIfNull(masseur))
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

            if (this.CheckIfNull(massage))
                return false;

            var category = this.GetCategoryFromDB(categoryId);

            if (this.CheckIfNull(category))
                return false;

            return true;
        }

        private void AssignUserToMasseurRole(IdentityUser user)
        {
            Task
                .Run(async () =>
                {
                    await this._userManager.RemoveFromRoleAsync(user, ClientRoleName);
                    await this._userManager.AddToRoleAsync(user, MasseurRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }

        private void RemoveUserFromMasseurRole(IdentityUser user)
        {
            Task
                .Run(async () =>
                {
                    await this._userManager.RemoveFromRoleAsync(user, MasseurRoleName);
                })
                .GetAwaiter()
                .GetResult();
        }

        private string GetCategoryName(string categoryId) =>
            categoryId != null
                ? this._data.Categories
                    .FirstOrDefault(c => c.Id == categoryId).Name
                : "Empty";
    }
}