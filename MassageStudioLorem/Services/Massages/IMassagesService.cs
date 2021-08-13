namespace MassageStudioLorem.Services.Massages
{
    using Data.Models;
    using Masseurs.Models;
    using Models;
    using Shared;
    using System.Collections.Generic;

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

        Category GetCategoryFromDB(string categoryId);

        bool CheckIfMassageNameExists(string massageName);

        bool CheckIfMassageEditedSuccessfully
            (EditMassageFormModel editMassageModel);

        EditMassageDetailsServiceModel GetMassageDetailsForEdit
            (string massageId);

        IEnumerable<MassageCategoryServiceModel> GetCategories();

        void AddMassage(AddMassageFormModel addMassageModel);
    }
}
