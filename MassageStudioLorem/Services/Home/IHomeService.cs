namespace MassageStudioLorem.Services.Home
{
    public interface IHomeService
    {
        string GetClientFirstName(string userId);

        string GetMasseurFullName(string userId);
    }
}
