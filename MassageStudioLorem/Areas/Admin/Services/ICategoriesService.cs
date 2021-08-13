namespace MassageStudioLorem.Areas.Admin.Services
{
    using System.Collections.Generic;

    using Models;

    public interface ICategoriesService
    {
        bool CheckIfCategoryIsAddedSuccessfully(string name);

        IEnumerable<CategoryServiceModel> GetAllCategories();

        DeleteCategoryServiceModel GetCategoryDataForDelete(string categoryId);

        bool CheckIfCategoryDeletedSuccessfully(string categoryId);
    }
}
