namespace MassageStudioLorem.Services
{
    using Data;
    using Data.Models;
    using System;
    using System.Linq;
    using static Global.GlobalConstants.Paging;

    public class CommonService
    {
        private readonly LoremDbContext _data;

        public CommonService(LoremDbContext data) => this._data = data;

        public bool CheckIfNull(object massage, string id)
            => String.IsNullOrEmpty(id) || massage == null;

        private double GetMaxPage(int count)
            => Math.Ceiling
                (count * 1.00 / ThreeCardsPerPage * 1.00);

        public Masseur GetMasseurFromDB(string masseurId) =>
            this._data
                .Masseurs
                .FirstOrDefault(m => m.UserId == masseurId);

        public Massage GetMassageFromDB(string massageId) =>
            this._data
                .Massages
                .FirstOrDefault(m => m.Id == massageId);

        public Category GetCategoryFromDB(string categoryId) =>
            this._data
                .Categories
                .FirstOrDefault(c => c.Id == categoryId);
    }
}
