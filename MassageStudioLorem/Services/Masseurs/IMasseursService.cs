namespace MassageStudioLorem.Services.Masseurs
{
    using System.Collections.Generic;

    using Data.Enums;
    using Data.Models;
    using Models;
    using MassageStudioLorem.Models.Masseurs;
    using SharedModels;

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