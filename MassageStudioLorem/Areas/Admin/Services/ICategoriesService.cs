namespace MassageStudioLorem.Areas.Admin.Services
{
    using Models;
    using System.Collections.Generic;

    public interface ICategoriesService
    {
        bool CheckIfCategoryIsAddedSuccessfully(string name);

        IEnumerable<CategoryServiceModel> GetAllCategories();

        DeleteCategoryServiceModel GetCategoryDataForDelete(string categoryId);

        bool CheckIfCategoryDeletedSuccessfully(string categoryId);
    }
}
