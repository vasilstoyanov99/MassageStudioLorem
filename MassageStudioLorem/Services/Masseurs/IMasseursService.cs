namespace MassageStudioLorem.Services.Masseurs
{
    using Data.Models;
    using MassageStudioLorem.Models.Masseurs;
    using Models;
    using System.Collections.Generic;

    public interface IMasseursService
    {
        bool IsUserMasseur(string userId);

        public IEnumerable<MassageCategoryServiceModel> GetCategories();

        public void RegisterNewMasseur(BecomeMasseurFormModel masseurModel,
            string userId);

        public AllMasseursQueryServiceModel GetAllMasseurs
            (int currentPage);

        public AvailableMasseurDetailsServiceModel GetMasseurDetails
            (MasseurDetailsQueryModel queryModel);

        public AvailableMasseursQueryServiceModel GetAvailableMasseurs
            (AvailableMasseursQueryServiceModel queryModel);

        public AvailableMasseurDetailsServiceModel GetAvailableMasseurDetails
            (string masseurId);

        Category GetCategoryFromDB(string categoryId);
    }
}