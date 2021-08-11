namespace MassageStudioLorem.Services.Massages
{
    using Models;
    using Shared;

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

        DeleteEntityServiceModel GetMassageDataForDelete(string massageId);

        bool CheckIfMassageDeletedSuccessfully(string massageId);

        EditMassageFormModel GetMassageDataForEdit(string massageId);

        bool CheckIfMassageEditedSuccessfully
            (EditMassageFormModel editMassageModel);

        EditMassageDetailsServiceModel GetMassageDetailsForEdit
            (string massageId);
    }
}
