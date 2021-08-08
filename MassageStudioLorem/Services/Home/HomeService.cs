namespace MassageStudioLorem.Services.Home
{
    using Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class HomeService : IHomeService
    {
        private readonly LoremDbContext _data;

        public HomeService(LoremDbContext data) => this._data = data;

        public string GetClientFirstName(string userId) =>
            this._data.Clients
                .FirstOrDefault(c => c.UserId == userId)?.FirstName;

        public string GetMasseurFullName(string userId) =>
            this._data.Masseurs.FirstOrDefault(m => m.UserId == userId)?.FullName;

        public string GetAdminUsername(string userId)
            => this._data.Users.FirstOrDefault(u => u.Id == userId)?.UserName;
    }
}
