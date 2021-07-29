namespace MassageStudioLorem.Services
{
    using Data.Models;

    public interface ICommonService
    {
        public bool CheckIfNull(object massage, string id);

        public double GetMaxPage(int count);

        public Masseur GetMasseurFromDB(string masseurId);

        public Massage GetMassageFromDB(string massageId);

        public Category GetCategoryFromDB(string categoryId);
    }
}
