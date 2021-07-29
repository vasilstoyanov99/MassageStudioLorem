namespace MassageStudioLorem.Services.Masseurs
{
    using System.Linq;
    using Data;

    public class MasseursService : IMasseursService
    {
        private readonly LoremDbContext _data;
        private readonly GlobalService _globalService;
        public MasseursService(LoremDbContext data) => this._data = data;

        public bool IsUserMasseur(string userId) =>
            this._data.Masseurs.Any(m => m.UserId == userId);

        public void ss()
        {
            this._globalService.CheckIfNull("d", "d");
        }
    }
}