namespace MassageStudioLorem.Controllers
{
    using Data;
    using Data.Enums;
    using Data.Models;
    using Ganss.XSS;
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


    public class MasseursController : Controller
    {
        private readonly LoremDbContext _data;

        public MasseursController(LoremDbContext data) =>
            this._data = data;

        public IActionResult BecomeMasseur()
        {
            if (this._data.Masseurs.Any(x => x.UserId == this.User.GetId()))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new BecomeMasseurFormModel()
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

            if (!ModelState.IsValid || this.ModelState.ErrorCount > 0)
            {
                masseurModel.Categories = this.GetCategories;

                return View(masseurModel);
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

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            if (!this._data.Masseurs.Any())
            {
                this.ModelState.AddModelError(String.Empty, NoMasseursFound);
                return View(new MasseursListViewModel()
                {
                    Masseurs = null
                });
            }

            var allMasseursModel = this._data
                .Masseurs
                .Select(m => new MasseurViewModel()
                {
                    Id = m.UserId,
                    ProfileImageUrl = m.ProfileImageUrl,
                    FirstAndLastName = m.FirstName + " " + m.LastName,
                    RatersCount = m.RatersCount,
                    Rating = 2
                })
                .ToList();

            return this.View(new MasseursListViewModel() 
                {Masseurs = allMasseursModel});
        }

        public IActionResult Sorted(string massageId,
            string categoryId)
        {
            var massage = this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

            if (String.IsNullOrEmpty(massageId) || massage == null)
            {
                return RedirectToAction("All", "Categories");
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

            if (String.IsNullOrEmpty(categoryId) || category == null)
            {
                return RedirectToAction("All", "Categories");
            }

            if (!this._data.Masseurs.Any(m => m.CategoryId == categoryId))
            {
                this.ModelState.AddModelError(String.Empty, 
                    NoMasseursFoundUnderCategory);
                return View(new MasseursListViewModel()
                {
                    Masseurs = null
                });
            }

            var sortedMasseursModel = this._data
                .Masseurs
                .Where(c => c.CategoryId == categoryId)
                .Select(m => new MasseurViewModel()
                {
                    Id = m.UserId,
                    ProfileImageUrl = m.ProfileImageUrl,
                    FirstAndLastName = m.FirstName + " " + m.LastName,
                    RatersCount = m.RatersCount,
                    Rating = 2
                })
                .ToList();

            return this.View(new MasseursListViewModel() { Masseurs = sortedMasseursModel });
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
                return RedirectToAction("All", "Categories");
            }

            var category = this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);

            if (String.IsNullOrEmpty(categoryId) || category == null)
            {
                return RedirectToAction("All", "Categories");
            }

            var masseur = this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

            if (String.IsNullOrEmpty(categoryId) || masseur == null)
            {
                // TODO: do it better
                return RedirectToAction("All", "Categories");
            }

            var masseurModel = new MasseurDetailsViewModel()
            {
                Id = masseur.UserId,
                Description = masseur.Description,
                FirstAndLastName = masseur.FirstName + " " + masseur.LastName,
                ProfileImageUrl = masseur.ProfileImageUrl,
                RatersCount = masseur.RatersCount,
                Rating = masseur.Rating
            };

            return View(masseurModel);
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
