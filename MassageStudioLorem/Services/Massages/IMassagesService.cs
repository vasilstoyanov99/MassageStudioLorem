namespace MassageStudioLorem.Services.Massages
{
    using Models;

    public interface IMassagesService
    {
        AllCategoriesQueryServiceModel All
            (string id, string name, int currentPage);
    }
}
