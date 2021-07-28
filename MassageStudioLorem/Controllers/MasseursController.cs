namespace MassageStudioLorem.Controllers
{
    using Data;
    using Data.Enums;
    using Data.Models;
    using Ganss.XSS;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models.Masseurs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Global.GlobalConstants.ErrorMessages;
    using static Global.GlobalConstants.Paging;

    public class MasseursController : Controller
    {
        private readonly LoremDbContext _data;

        public MasseursController(LoremDbContext data) =>
            this._data = data;

        [Authorize]
        public IActionResult BecomeMasseur()
        {
            if (this._data.Masseurs.Any(x => x.UserId == this.User.GetId()))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new BecomeMasseurFormModel() {Categories = this.GetCategories});
        }

        [Authorize]
        [HttpPost]
        public IActionResult BecomeMasseur
            (BecomeMasseurFormModel masseurModel)
        {
            //TODO: Check if I should redirect to home!

            if (this._data.Masseurs.Any(x => x.UserId == this.User.GetId()))
            {
                this.ModelState.AddModelError(String.Empty, AlreadyMasseur);
            }

            if (!this._data.Categories.Any
                (c => c.Id == masseurModel.CategoryId))
            {
                this.ModelState.AddModelError
                    (nameof(masseurModel.CategoryId), CategoryIdError);
            }

            //TODO: I'm not sure if this is necessary!

            if (!Enum.TryParse(typeof(Gender),
                masseurModel.Gender.ToString(), true, out _))
            {
                this.ModelState.AddModelError
                    (nameof(masseurModel.Gender), GenderIdError);
            }

            if (!this.ModelState.IsValid ||
                this.ModelState.ErrorCount > 0)
            {
                masseurModel.Categories = this.GetCategories;

                return this.View(masseurModel);
            }

            var htmlSanitizer = new HtmlSanitizer();

            var masseur = new Masseur()
            {
                FirstName = htmlSanitizer.Sanitize(masseurModel.FirstName),
                LastName = htmlSanitizer.Sanitize(masseurModel.LastName),
                ProfileImageUrl = htmlSanitizer
                    .Sanitize(masseurModel.ProfileImageUrl),
                Description = htmlSanitizer.Sanitize(masseurModel.Description),
                CategoryId = masseurModel.CategoryId,
                Gender = masseurModel.Gender,
                UserId = this.User.GetId()
            };

            this._data.Masseurs.Add(masseur);
            this._data.SaveChanges();

            return this.RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult All([FromQuery] AllMasseursQueryViewModel query)
        {
            if (!this._data.Masseurs.Any())
            {
                this.ModelState.AddModelError(String.Empty, NoMasseursFound);

                return this.View(new AllMasseursQueryViewModel() {Masseurs = null});
            }

            var totalMasseurs = this._data.Masseurs.Count();

            if (query.CurrentPage > totalMasseurs
                || query.CurrentPage < 1)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var allMasseursModel = this._data
                .Masseurs
                .Skip((query.CurrentPage - 1) * MasseursPerPage)
                .Take(MasseursPerPage)
                .Select(m => new MasseurDetailsViewModel()
                {
                    Id = m.UserId,
                    ProfileImageUrl = m.ProfileImageUrl,
                    FirstAndLastName = m.FirstName + " " + m.LastName,
                    RatersCount = m.RatersCount,
                    CategoryId = m.CategoryId,
                    Rating = 2 // TODO: Get Rating from the DB!
                })
                .ToList();

            return this.View(new AllMasseursQueryViewModel()
            {
                Masseurs = allMasseursModel,
                CurrentPage = query.CurrentPage,
                MaxPage =
                    Math.Ceiling(totalMasseurs * 1.00 / MasseursPerPage * 1.00)
            });
        }

        [Authorize]
        public IActionResult Details
            ([FromQuery] MasseurDetailsQueryModel queryModel)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == queryModel.MassageId);

            if (String.IsNullOrEmpty(queryModel.MassageId) || massage == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == queryModel.CategoryId);

            if (String.IsNullOrEmpty(queryModel.CategoryId) || category == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            var masseur = this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == queryModel.MasseurId);

            if (String.IsNullOrEmpty(queryModel.MasseurId) || masseur == null)
            {
                // TODO: do it better
                return this.RedirectToAction(nameof(this.All));
            }

            var masseurDetails = new MasseurDetailsViewModel()
            {
                Id = masseur.UserId,
                CategoryId = queryModel.CategoryId,
                MassageId = queryModel.MassageId,
                Description = masseur.Description,
                PhoneNumber = this.GetMasseurPhoneNumber(queryModel.MasseurId),
                FirstAndLastName = masseur.FirstName + " " + masseur.LastName,
                ProfileImageUrl = masseur.ProfileImageUrl,
                RatersCount = masseur.RatersCount,
                Rating = 2
            };

            return this.View(masseurDetails);
        }

        [Authorize]
        public IActionResult AvailableMasseurs
            ([FromQuery] AvailableMasseursQueryViewModel queryModel)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == queryModel.MassageId);

            if (String.IsNullOrEmpty(queryModel.MassageId) || massage == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == queryModel.CategoryId);

            if (String.IsNullOrEmpty(queryModel.CategoryId) || category == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            if (!this._data.Masseurs
                .Any(m => m.CategoryId == queryModel.CategoryId))
            {
                this.ModelState
                    .AddModelError(String.Empty, NoMasseursFoundUnderCategory);

                return this.View(new AvailableMasseursQueryViewModel() {Masseurs = null});
            }

            var totalMasseurs = this._data
                .Masseurs
                .Where(m => m.CategoryId == queryModel.CategoryId).Count();

            if (queryModel.CurrentPage > totalMasseurs
                || queryModel.CurrentPage < 1)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var availableMasseursModel = this._data
                .Masseurs
                .Skip((queryModel.CurrentPage - 1) * MasseursPerPage)
                .Take(MasseursPerPage)
                .Where(c => c.CategoryId == queryModel.CategoryId)
                .Select(m => new AvailableMasseurListingViewModel()
                {
                    Id = m.UserId,
                    ProfileImageUrl = m.ProfileImageUrl,
                    FirstAndLastName = m.FirstName + " " + m.LastName,
                    RatersCount = m.RatersCount,
                    Rating = 2,
                })
                .ToList();

            return this.View(new AvailableMasseursQueryViewModel()
            {
                Masseurs = availableMasseursModel,
                MassageId = queryModel.MassageId,
                CategoryId = queryModel.CategoryId,
                CurrentPage = queryModel.CurrentPage,
                MaxPage =
                    Math.Ceiling(totalMasseurs * 1.00 / MasseursPerPage * 1.00)
            });
        }

        [Authorize]
        public IActionResult AvailableMasseurDetails(string masseurId)
        {
            var masseur = this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

            if (String.IsNullOrEmpty(masseurId) || masseur == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var masseurDetailsModel = new MasseurDetailsViewModel()
            {
                Id = masseur.UserId,
                Description = masseur.Description,
                PhoneNumber = this.GetMasseurPhoneNumber(masseurId),
                FirstAndLastName = masseur.FirstName + " " + masseur.LastName,
                ProfileImageUrl = masseur.ProfileImageUrl,
                RatersCount = masseur.RatersCount,
                CategoryId = masseur.CategoryId,
                Rating = 2
            };

            return this.View("Details", masseurDetailsModel);
        }

        private IEnumerable<MassageCategoryViewModel> GetCategories
            => this._data
                .Categories
                .Select(c => new MassageCategoryViewModel() {Id = c.Id, Name = c.Name})
                .ToList();

        private string GetMasseurPhoneNumber(string masseurId) =>
            this._data.Users.FirstOrDefault(u => u.Id == masseurId)?.PhoneNumber;
    }
}