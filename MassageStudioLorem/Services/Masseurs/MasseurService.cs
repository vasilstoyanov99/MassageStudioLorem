namespace MassageStudioLorem.Services.Masseurs
{
    using System.Linq;
    using Data;

    public class MasseurService : IMasseurService
    {
        private readonly LoremDbContext _data;

        public MasseurService(LoremDbContext data) => this._data = data;

        public bool IsUserMasseur(string userId) =>
            this._data.Masseurs.Any(m => m.UserId == userId);
    }
}