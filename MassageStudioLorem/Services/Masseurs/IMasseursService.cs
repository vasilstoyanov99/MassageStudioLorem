namespace MassageStudioLorem.Services.Masseurs
{
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

        public MasseurDetailsServiceModel GetMasseurDetails
            (MasseurDetailsQueryModel queryModel);

        public AvailableMasseursQueryServiceModel GetAvailableMasseurs
            (AvailableMasseursQueryServiceModel queryModel);

        public MasseurDetailsServiceModel GetAvailableMasseurDetails
            (string masseurId);
    }
}