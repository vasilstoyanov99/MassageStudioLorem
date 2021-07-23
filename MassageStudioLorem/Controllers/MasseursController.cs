namespace MassageStudioLorem.Controllers
{
    using Data;
    using Data.Enums;
    using Data.Models;
    using Ganss.XSS;
    using Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Server.IIS.Core;
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

        public IActionResult BecomeMasseur() => View(new BecomeMasseurFormModel()
        {
            Categories = this.GetCategories
        });

        [HttpPost]
        public IActionResult BecomeMasseur
            (BecomeMasseurFormModel masseurModel)
        {
            if (!this._data.Categories.Any
                (c => c.Id == masseurModel.CategoryId))
            {
                this.ModelState.AddModelError
                    (nameof(masseurModel.CategoryId), CategoryIdError);
            }

            if (this._data.Masseurs.Any(x => x.UserId == this.User.GetId()))
            {
                this.ModelState.AddModelError(String.Empty, AlreadyMasseur);
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
