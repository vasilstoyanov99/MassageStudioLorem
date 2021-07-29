namespace MassageStudioLorem.Services.Massages
{
    using Models;

    public interface IMassagesService
    {
        AllCategoriesQueryServiceModel GetAllCategoriesWithMassages
            (string id, string name, int currentPage);

        MassageDetailsServiceModel GetMassageDetails
            (string massageId, string categoryId);

        AvailableMassagesQueryServiceModel GetAvailableMassages
            (string masseurId, string categoryId, int currentPage);

        MassageDetailsServiceModel GetAvailableMassageDetails
            (string massageId, string masseurId);
    }
}
