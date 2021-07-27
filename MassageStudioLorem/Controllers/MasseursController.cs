﻿namespace MassageStudioLorem.Controllers
{
    using Data;
    using Data.Enums;
    using Data.Models;
    using Ganss.XSS;
    using Global;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Server.IIS.Core;
    using Models.Categories;
    using Models.Masseurs;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using static Global.GlobalConstants.ErrorMessages;

    using static Global.GlobalConstants.Paging;

    public class MasseursController : Controller
    {
        private readonly LoremDbContext _data;

        public MasseursController(LoremDbContext data) =>
            this._data = data;

        public IActionResult BecomeMasseur()
        {
            if (this._data.Masseurs.Any(x => x.UserId == this.User.GetId()))
            {
                return this.RedirectToAction("Index", "Home");
            }

            return this.View(new BecomeMasseurFormModel()
            {
                Categories = this.GetCategories
            });
        }

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

            if (!this.ModelState.IsValid || this.ModelState.ErrorCount > 0)
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

        public IActionResult All([FromQuery] AllMasseursListViewModel query)
        {
            if (!this._data.Masseurs.Any())
            {
                this.ModelState.AddModelError(String.Empty, NoMasseursFound);
                return this.View(new AllMasseursListViewModel()
                {
                    Masseurs = null
                });
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

            return this.View(new AllMasseursListViewModel()
            {
                Masseurs = allMasseursModel,
                CurrentPage = query.CurrentPage,
                MaxPage =
                    Math.Ceiling(totalMasseurs * 1.00 / MasseursPerPage * 1.00)
            });
        }

        public IActionResult AvailableMasseurs([FromQuery] SortedMasseursListViewModel query)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == query.MassageId);

            if (String.IsNullOrEmpty(query.MassageId) || massage == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == query.CategoryId);

            if (String.IsNullOrEmpty(query.CategoryId) || category == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            if (!this._data.Masseurs.Any(m => m.CategoryId == query.CategoryId))
            {
                this.ModelState
                    .AddModelError(String.Empty, NoMasseursFoundUnderCategory);

                return this.View(new SortedMasseursListViewModel()
                {
                    Masseurs = null
                });
            }

            var totalMasseurs = this._data
                .Masseurs
                .Where(m => m.CategoryId == query.CategoryId).Count();

            if (query.CurrentPage > totalMasseurs
                || query.CurrentPage < 1)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var sortedMasseursModel = this._data
                .Masseurs
                .Skip((query.CurrentPage - 1) * MasseursPerPage)
                .Take(MasseursPerPage)
                .Where(c => c.CategoryId == query.CategoryId)
                .Select(m => new MasseurViewModel()
                {
                    Id = m.UserId,
                    ProfileImageUrl = m.ProfileImageUrl,
                    FirstAndLastName = m.FirstName + " " + m.LastName,
                    RatersCount = m.RatersCount,
                    Rating = 2,
                })
                .ToList();

            return this.View(new SortedMasseursListViewModel()
            {   Masseurs = sortedMasseursModel,
                MassageId = query.MassageId,
                CategoryId = query.CategoryId,
                CurrentPage = query.CurrentPage,
                MaxPage =
                    Math.Ceiling(totalMasseurs * 1.00 / MasseursPerPage * 1.00)
            });
        }

        public IActionResult Details(string massageId,
            string categoryId,
            string masseurId)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

            if (String.IsNullOrEmpty(massageId) || massage == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

            if (String.IsNullOrEmpty(categoryId) || category == null)
            {
                return this.RedirectToAction("All", "Categories");
            }

            var masseur = this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

            if (String.IsNullOrEmpty(masseurId) || masseur == null)
            {
                // TODO: do it better
                return this.RedirectToAction(nameof(this.All));
            }

            var masseurModel = new MasseurDetailsViewModel()
            {
                Id = masseur.UserId,
                Description = masseur.Description,
                FirstAndLastName = masseur.FirstName + " " + masseur.LastName,
                ProfileImageUrl = masseur.ProfileImageUrl,
                RatersCount = masseur.RatersCount,
                Rating = 2
            };

            return this.View(masseurModel);
        }

        public IActionResult MasseurDetails(
            string masseurId)
        {
            var masseur = this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

            if (String.IsNullOrEmpty(masseurId) || masseur == null)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            var masseurModel = new MasseurDetailsViewModel()
            {
                Id = masseur.UserId,
                Description = masseur.Description,
                FirstAndLastName = masseur.FirstName + " " + masseur.LastName,
                ProfileImageUrl = masseur.ProfileImageUrl,
                RatersCount = masseur.RatersCount,
                CategoryId = masseur.CategoryId,
                Rating = 2
            };

            return this.View("Details", masseurModel);
        }

        private IEnumerable<MassageCategoryViewModel> GetCategories
            => this._data
                .Categories
                .Select(c => new MassageCategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToList();
    }
}
