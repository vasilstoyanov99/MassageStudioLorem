namespace MassageStudioLorem.Services.Masseurs
{
    using Data.Enums;
    using Data.Models;
    using MassageStudioLorem.Models.Masseurs;
    using Models;
    using Shared;
    using System.Collections.Generic;

    public interface IMasseursService
    {
        public IEnumerable<MassageCategoryServiceModel> GetCategories();

        public void RegisterNewMasseur(BecomeMasseurFormModel masseurModel,
            string userId);

        public AllMasseursQueryServiceModel GetAllMasseurs
            (int currentPage, Gender gender);

        public AvailableMasseurDetailsServiceModel GetMasseurDetails
            (MasseurDetailsQueryModel queryModel);

        public AvailableMasseursQueryServiceModel GetAvailableMasseurs
            (AvailableMasseursQueryServiceModel queryModel);

        public AvailableMasseurDetailsServiceModel GetAvailableMasseurDetails
            (string masseurId);

        Category GetCategoryFromDB(string categoryId);

        EditMasseurFormModel GetMasseurDataForEdit(string masseurId);

        MasseurDetailsForEdit GetMasseurDetailsForEdit(string masseurId);

        bool CheckIfMasseurEditedSuccessfully
            (EditMasseurFormModel editMasseurModel);

        DeleteEntityServiceModel GetMasseurDataForDelete(string masseurId);

        bool CheckIfMasseurDeletedSuccessfully(string masseurId);
    }
}