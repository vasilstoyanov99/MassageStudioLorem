namespace MassageStudioLorem.Controllers
{
    using Data;
    using Data.Enums;
    using Data.Models;
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

            //TODO: I'm not sure if this is necessary!

            if (!Enum.TryParse(typeof(Gender),
                masseurModel.Gender.ToString(), true, out _))
            {
                this.ModelState.AddModelError
                    (nameof(masseurModel.Gender), GenderIdError);
            }

            if (!ModelState.IsValid)
            {
                masseurModel.Categories = this.GetCategories;

                return View(masseurModel);
            }

            var masseur = new Masseur()
            {
                ProfileImageUrl = masseurModel.ProfileImageUrl, 
                Description = masseurModel.Description,
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
